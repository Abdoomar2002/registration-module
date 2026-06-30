import { useEffect, useState } from 'react';
import { useForm, useFieldArray, type FieldPath } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { ApiError } from '../api/client';
import { getCities, getGovernorates } from '../api/lookups';
import { createRegistration } from '../api/registrations';
import type { City, Governorate } from '../api/types';
import {
  defaultRegistrationValues,
  emptyAddress,
  registrationSchema,
  toCreateRequest,
  type RegistrationFormValues,
} from '../schema/registrationSchema';
import { AddressForm } from './AddressForm';
import { DateInput } from './DateInput';
import { TextInput } from './TextInput';

const MAX_ADDRESSES = 5;

// Maps a server ProblemDetails key (e.g. "Addresses[0].CityId") to an RHF path.
function toFieldPath(serverKey: string): string {
  return serverKey
    .replace(/\[(\d+)\]/g, '.$1')
    .split('.')
    .map((segment) => segment.charAt(0).toLowerCase() + segment.slice(1))
    .join('.');
}

interface RegistrationFormProps {
  onViewRegistration?: (id: string) => void;
}

export function RegistrationForm({ onViewRegistration }: RegistrationFormProps) {
  const {
    register,
    control,
    handleSubmit,
    setValue,
    getValues,
    setError,
    reset,
    watch,
    formState: { errors, isValid, isSubmitting },
  } = useForm<RegistrationFormValues>({
    resolver: zodResolver(registrationSchema),
    defaultValues: defaultRegistrationValues(),
    mode: 'onChange',
  });

  const { fields, append, remove } = useFieldArray({ control, name: 'addresses' });

  const [governorates, setGovernorates] = useState<Governorate[]>([]);
  const [citiesByKey, setCitiesByKey] = useState<Record<string, City[]>>({});
  const [lookupError, setLookupError] = useState<string | null>(null);
  const [serverError, setServerError] = useState<string | null>(null);
  const [createdId, setCreatedId] = useState<string | null>(null);

  useEffect(() => {
    getGovernorates()
      .then(setGovernorates)
      .catch(() => setLookupError('Could not load governorates. Is the API running?'));
  }, []);

  const watchedAddresses = watch('addresses');
  const showPrimaryChoice = fields.length > 1;

  const handleGovernorateChange = async (index: number, governorateId: number) => {
    const key = fields[index]?.id ?? String(index);
    setValue(`addresses.${index}.cityId`, 0, { shouldValidate: true });

    if (!governorateId) {
      setCitiesByKey((prev) => ({ ...prev, [key]: [] }));
      return;
    }

    try {
      const cities = await getCities(governorateId);
      setCitiesByKey((prev) => ({ ...prev, [key]: cities }));
    } catch {
      setCitiesByKey((prev) => ({ ...prev, [key]: [] }));
    }
  };

  const handleSetPrimary = (index: number) => {
    getValues('addresses').forEach((_, i) =>
      setValue(`addresses.${i}.isPrimary`, i === index, { shouldValidate: true }),
    );
  };

  const handleAddAddress = () => {
    if (fields.length < MAX_ADDRESSES) {
      append(emptyAddress());
    }
  };

  const handleRemoveAddress = (index: number) => {
    const wasPrimary = getValues(`addresses.${index}.isPrimary`);
    remove(index);
    if (wasPrimary) {
      setValue('addresses.0.isPrimary', true, { shouldValidate: true });
    }
  };

  const onSubmit = async (values: RegistrationFormValues) => {
    setServerError(null);
    try {
      const result = await createRegistration(toCreateRequest(values));
      setCreatedId(result.id);
    } catch (error) {
      if (!(error instanceof ApiError)) {
        setServerError('Something went wrong. Please try again.');
        return;
      }

      if (error.status === 409 && error.conflictField) {
        const field = error.conflictField === 'mobile' ? 'mobile' : 'email';
        setError(field, { type: 'server', message: error.problem?.detail });
        setServerError(error.problem?.detail ?? 'This value is already registered.');
        return;
      }

      if (error.status === 400 && Object.keys(error.fieldErrors).length > 0) {
        for (const [key, messages] of Object.entries(error.fieldErrors)) {
          setError(toFieldPath(key) as FieldPath<RegistrationFormValues>, {
            type: 'server',
            message: messages[0],
          });
        }
        setServerError('Please fix the highlighted fields.');
        return;
      }

      setServerError(error.message);
    }
  };

  const handleRegisterAnother = () => {
    reset(defaultRegistrationValues());
    setCitiesByKey({});
    setServerError(null);
    setCreatedId(null);
  };

  if (createdId) {
    return (
      <div className="card success-card pop-in" role="status">
        <div className="success-check" aria-hidden="true">
          <svg viewBox="0 0 52 52" className="success-check-svg">
            <circle className="success-check-circle" cx="26" cy="26" r="24" fill="none" />
            <path className="success-check-mark" fill="none" d="M14 27l8 8 16-16" />
          </svg>
        </div>
        <h2>You're registered!</h2>
        <p className="subtitle">Your registration was created successfully.</p>
        <p className="reference">
          Reference id: <code>{createdId}</code>
        </p>
        <div className="form-actions center">
          {onViewRegistration && (
            <button type="button" className="btn-primary" onClick={() => onViewRegistration(createdId)}>
              View registration
            </button>
          )}
          <button type="button" className="btn-secondary" onClick={handleRegisterAnother}>
            Register another
          </button>
        </div>
      </div>
    );
  }

  // Array-level errors (min/max addresses, single-primary rule).
  const addressesError =
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    (errors.addresses as any)?.root?.message ??
    (typeof errors.addresses?.message === 'string' ? errors.addresses.message : undefined);

  return (
    <form onSubmit={handleSubmit(onSubmit)} noValidate>
      {lookupError && <div className="alert alert-error" role="alert">{lookupError}</div>}
      {serverError && <div className="alert alert-error" role="alert">{serverError}</div>}

      <section className="card">
        <h2>Personal details</h2>
        <div className="grid-2">
          <TextInput
            id="firstName"
            label="First name"
            required
            autoComplete="given-name"
            registration={register('firstName')}
            error={errors.firstName?.message}
          />
          <TextInput
            id="middleName"
            label="Middle name"
            autoComplete="additional-name"
            registration={register('middleName')}
            error={errors.middleName?.message}
          />
        </div>
        <TextInput
          id="lastName"
          label="Last name"
          required
          autoComplete="family-name"
          registration={register('lastName')}
          error={errors.lastName?.message}
        />
        <div className="grid-2">
          <DateInput
            id="birthDate"
            label="Birth date"
            required
            max={new Date().toISOString().slice(0, 10)}
            registration={register('birthDate')}
            error={errors.birthDate?.message}
          />
          <TextInput
            id="mobile"
            label="Mobile number"
            required
            placeholder="+201006158123"
            inputMode="tel"
            autoComplete="tel"
            registration={register('mobile')}
            error={errors.mobile?.message}
          />
        </div>
        <TextInput
          id="email"
          label="Email"
          required
          inputMode="email"
          autoComplete="email"
          registration={register('email')}
          error={errors.email?.message}
        />
      </section>

      <section className="card">
        <h2>Addresses</h2>
        {fields.map((field, index) => (
          <AddressForm
            key={field.id}
            index={index}
            register={register}
            governorates={governorates}
            cities={citiesByKey[field.id] ?? []}
            isPrimary={Boolean(watchedAddresses?.[index]?.isPrimary)}
            canRemove={fields.length > 1}
            showPrimaryChoice={showPrimaryChoice}
            errors={{
              governorateId: errors.addresses?.[index]?.governorateId?.message,
              cityId: errors.addresses?.[index]?.cityId?.message,
              street: errors.addresses?.[index]?.street?.message,
              buildingNumber: errors.addresses?.[index]?.buildingNumber?.message,
              flatNumber: errors.addresses?.[index]?.flatNumber?.message,
            }}
            onGovernorateChange={handleGovernorateChange}
            onSetPrimary={handleSetPrimary}
            onRemove={handleRemoveAddress}
          />
        ))}

        {addressesError && <p className="validation-message" role="alert">{addressesError}</p>}

        <button
          type="button"
          className="btn-secondary"
          onClick={handleAddAddress}
          disabled={fields.length >= MAX_ADDRESSES}
        >
          + Add address
        </button>
      </section>

      <div className="form-actions">
        <button type="submit" className="btn-primary" disabled={!isValid || isSubmitting}>
          {isSubmitting ? 'Submitting...' : 'Submit registration'}
        </button>
      </div>
    </form>
  );
}
