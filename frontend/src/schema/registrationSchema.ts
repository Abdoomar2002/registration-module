import { z } from 'zod';
import type { CreateRegistrationRequest } from '../api/types';

// These rules intentionally mirror the backend (defense in depth); the server
// validates the same constraints again.
const MAX_NAME = 50;
const MAX_EMAIL = 254;
const MIN_AGE = 20;
const MAX_ADDRESSES = 5;

const NAME_PATTERN = /^[A-Za-z؀-ۿ]+(?:[ '\-][A-Za-z؀-ۿ]+)*$/;
const MOBILE_PATTERN = /^\+[1-9]\d{7,14}$/;
const BUILDING_FLAT_PATTERN = /^[A-Za-z0-9/\- ]+$/;

const NAME_MESSAGE =
  'Only Arabic or English letters, spaces, hyphens and apostrophes are allowed.';
const BUILDING_FLAT_MESSAGE =
  'Only letters, numbers, spaces, slashes and dashes are allowed.';

export function normalizeName(value: string): string {
  return value.trim().replace(/\s+/g, ' ');
}

export function isValidName(value: string): boolean {
  const normalized = normalizeName(value);
  return normalized.length > 0 && normalized.length <= MAX_NAME && NAME_PATTERN.test(normalized);
}

export function ageOn(birthDate: string, asOf: Date = new Date()): number {
  const dob = new Date(birthDate);
  let age = asOf.getFullYear() - dob.getFullYear();
  const monthDelta = asOf.getMonth() - dob.getMonth();
  if (monthDelta < 0 || (monthDelta === 0 && asOf.getDate() < dob.getDate())) {
    age -= 1;
  }
  return age;
}

const addressSchema = z.object({
  governorateId: z.number({ invalid_type_error: 'Governorate is required.' })
    .int()
    .positive('Governorate is required.'),
  cityId: z.number({ invalid_type_error: 'City is required.' })
    .int()
    .positive('City is required.'),
  street: z.string().trim().min(1, 'Street is required.').max(200, 'Street is too long.'),
  buildingNumber: z.string().trim().min(1, 'Building number is required.').max(20, 'Too long.')
    .regex(BUILDING_FLAT_PATTERN, BUILDING_FLAT_MESSAGE),
  flatNumber: z.string().trim().min(1, 'Flat number is required.').max(20, 'Too long.')
    .regex(BUILDING_FLAT_PATTERN, BUILDING_FLAT_MESSAGE),
  isPrimary: z.boolean(),
});

export const registrationSchema = z
  .object({
    firstName: z.string().min(1, 'First name is required.').refine(isValidName, NAME_MESSAGE),
    middleName: z.string().refine((v) => v.trim() === '' || isValidName(v), NAME_MESSAGE),
    lastName: z.string().min(1, 'Last name is required.').refine(isValidName, NAME_MESSAGE),
    birthDate: z
      .string()
      .min(1, 'Birth date is required.')
      .refine((v) => !Number.isNaN(new Date(v).getTime()), 'Enter a valid date.')
      .refine((v) => new Date(v) <= new Date(), 'Birth date cannot be in the future.')
      .refine((v) => ageOn(v) >= MIN_AGE, `You must be at least ${MIN_AGE} years old.`),
    email: z
      .string()
      .min(1, 'Email is required.')
      .max(MAX_EMAIL, 'Email is too long.')
      .email('Enter a valid email address.'),
    mobile: z
      .string()
      .min(1, 'Mobile number is required.')
      .regex(MOBILE_PATTERN, 'Enter a valid E.164 number, e.g. +201006158123.'),
    addresses: z
      .array(addressSchema)
      .min(1, 'Add at least one address.')
      .max(MAX_ADDRESSES, `You can add up to ${MAX_ADDRESSES} addresses.`),
  })
  .superRefine((data, ctx) => {
    if (data.addresses.length > 1) {
      const primaries = data.addresses.filter((a) => a.isPrimary).length;
      if (primaries !== 1) {
        ctx.addIssue({
          code: z.ZodIssueCode.custom,
          message: 'Exactly one address must be marked as primary.',
          path: ['addresses'],
        });
      }
    }
  });

export type RegistrationFormValues = z.infer<typeof registrationSchema>;

export const emptyAddress = (): RegistrationFormValues['addresses'][number] => ({
  governorateId: 0,
  cityId: 0,
  street: '',
  buildingNumber: '',
  flatNumber: '',
  isPrimary: false,
});

export const defaultRegistrationValues = (): RegistrationFormValues => ({
  firstName: '',
  middleName: '',
  lastName: '',
  birthDate: '',
  email: '',
  mobile: '',
  addresses: [{ ...emptyAddress(), isPrimary: true }],
});

/** Maps validated form values to the API request shape (normalizing as the server does). */
export function toCreateRequest(values: RegistrationFormValues): CreateRegistrationRequest {
  const middle = normalizeName(values.middleName);
  return {
    firstName: normalizeName(values.firstName),
    middleName: middle === '' ? null : middle,
    lastName: normalizeName(values.lastName),
    birthDate: values.birthDate,
    email: values.email.trim(),
    mobile: values.mobile.trim(),
    addresses: values.addresses.map((a) => ({
      governorateId: a.governorateId,
      cityId: a.cityId,
      street: a.street.trim(),
      buildingNumber: a.buildingNumber.trim(),
      flatNumber: a.flatNumber.trim(),
      isPrimary: a.isPrimary,
    })),
  };
}
