import type { UseFormRegister } from 'react-hook-form';
import type { City, Governorate } from '../api/types';
import type { RegistrationFormValues } from '../schema/registrationSchema';
import { LookupSelect } from './LookupSelect';
import { TextInput } from './TextInput';

interface AddressErrors {
  governorateId?: string;
  cityId?: string;
  street?: string;
  buildingNumber?: string;
  flatNumber?: string;
}

interface AddressFormProps {
  index: number;
  register: UseFormRegister<RegistrationFormValues>;
  governorates: Governorate[];
  cities: City[];
  isPrimary: boolean;
  canRemove: boolean;
  showPrimaryChoice: boolean;
  errors: AddressErrors;
  onGovernorateChange: (index: number, governorateId: number) => void;
  onSetPrimary: (index: number) => void;
  onRemove: (index: number) => void;
}

const bilingual = (en: string, ar: string) => `${en} - ${ar}`;

export function AddressForm({
  index,
  register,
  governorates,
  cities,
  isPrimary,
  canRemove,
  showPrimaryChoice,
  errors,
  onGovernorateChange,
  onSetPrimary,
  onRemove,
}: AddressFormProps) {
  const governorateReg = register(`addresses.${index}.governorateId`, { valueAsNumber: true });
  const cityReg = register(`addresses.${index}.cityId`, { valueAsNumber: true });

  const governorateRegistration = {
    ...governorateReg,
    onChange: async (event: Parameters<typeof governorateReg.onChange>[0]) => {
      await governorateReg.onChange(event);
      onGovernorateChange(index, Number((event.target as HTMLSelectElement).value));
    },
  };

  return (
    <fieldset className="address" data-testid={`address-${index}`}>
      <div className="address-header">
        <legend>
          <strong>Address {index + 1}</strong>
        </legend>
        {canRemove && (
          <button
            type="button"
            className="btn-danger"
            onClick={() => onRemove(index)}
            aria-label={`Remove address ${index + 1}`}
          >
            Remove
          </button>
        )}
      </div>

      <div className="grid-2">
        <LookupSelect
          id={`addresses-${index}-governorate`}
          label="Governorate"
          required
          registration={governorateRegistration}
          options={governorates.map((g) => ({ value: g.id, label: bilingual(g.nameEn, g.nameAr) }))}
          error={errors.governorateId}
        />
        <LookupSelect
          id={`addresses-${index}-city`}
          label="City"
          required
          disabled={cities.length === 0}
          placeholder={cities.length === 0 ? 'Select a governorate first' : 'Select...'}
          registration={cityReg}
          options={cities.map((c) => ({ value: c.id, label: bilingual(c.nameEn, c.nameAr) }))}
          error={errors.cityId}
        />
      </div>

      <TextInput
        id={`addresses-${index}-street`}
        label="Street"
        required
        registration={register(`addresses.${index}.street`)}
        error={errors.street}
      />

      <div className="grid-2">
        <TextInput
          id={`addresses-${index}-building`}
          label="Building number"
          required
          placeholder="e.g. 12A"
          registration={register(`addresses.${index}.buildingNumber`)}
          error={errors.buildingNumber}
        />
        <TextInput
          id={`addresses-${index}-flat`}
          label="Flat number"
          required
          placeholder="e.g. 10/2"
          registration={register(`addresses.${index}.flatNumber`)}
          error={errors.flatNumber}
        />
      </div>

      {showPrimaryChoice && (
        <div className="field inline-checkbox">
          <input
            id={`addresses-${index}-primary`}
            type="radio"
            name="primaryAddress"
            checked={isPrimary}
            onChange={() => onSetPrimary(index)}
          />
          <label htmlFor={`addresses-${index}-primary`}>Primary address</label>
        </div>
      )}
    </fieldset>
  );
}
