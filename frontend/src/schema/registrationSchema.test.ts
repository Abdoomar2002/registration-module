import { describe, expect, it } from 'vitest';
import {
  ageOn,
  defaultRegistrationValues,
  isValidName,
  registrationSchema,
  toCreateRequest,
} from './registrationSchema';

const validValues = () => ({
  ...defaultRegistrationValues(),
  firstName: 'Abdelrahman',
  middleName: '',
  lastName: 'Omar',
  birthDate: '1998-01-01',
  email: 'abdo@example.com',
  mobile: '+201006158123',
  addresses: [
    {
      governorateId: 1,
      cityId: 1,
      street: 'Tahrir Street',
      buildingNumber: '12A',
      flatNumber: '3',
      isPrimary: true,
    },
  ],
});

describe('isValidName', () => {
  it.each(['Ahmed', 'Al-Sayed', "O'Brien", 'محمد', 'عبد الرحمن'])('accepts "%s"', (name) => {
    expect(isValidName(name)).toBe(true);
  });

  it.each(['Ahmed123', 'Ahmed@', 'a_b', '', '   '])('rejects "%s"', (name) => {
    expect(isValidName(name)).toBe(false);
  });

  it('rejects names longer than 50 characters', () => {
    expect(isValidName('a'.repeat(51))).toBe(false);
  });
});

describe('ageOn', () => {
  it('does not count the birthday before it occurs in the year', () => {
    const asOf = new Date('2026-06-29');
    expect(ageOn('2006-06-29', asOf)).toBe(20); // birthday today
    expect(ageOn('2006-06-30', asOf)).toBe(19); // birthday tomorrow
  });
});

describe('registrationSchema', () => {
  it('accepts a valid registration', () => {
    expect(registrationSchema.safeParse(validValues()).success).toBe(true);
  });

  it('rejects an underage registrant', () => {
    const result = registrationSchema.safeParse({ ...validValues(), birthDate: '2015-01-01' });
    expect(result.success).toBe(false);
  });

  it('rejects an invalid E.164 mobile', () => {
    const result = registrationSchema.safeParse({ ...validValues(), mobile: '01006158123' });
    expect(result.success).toBe(false);
  });

  it('requires at least one address', () => {
    const result = registrationSchema.safeParse({ ...validValues(), addresses: [] });
    expect(result.success).toBe(false);
  });

  it('rejects more than one primary address', () => {
    const values = validValues();
    const result = registrationSchema.safeParse({
      ...values,
      addresses: [
        { ...values.addresses[0], isPrimary: true },
        { ...values.addresses[0], cityId: 2, isPrimary: true },
      ],
    });
    expect(result.success).toBe(false);
  });
});

describe('toCreateRequest', () => {
  it('normalizes names and maps an empty middle name to null', () => {
    const request = toCreateRequest({
      ...validValues(),
      firstName: '  Abdel   rahman ',
      middleName: '   ',
    });
    expect(request.firstName).toBe('Abdel rahman');
    expect(request.middleName).toBeNull();
  });
});
