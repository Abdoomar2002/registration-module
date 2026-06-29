import type { UseFormRegisterReturn } from 'react-hook-form';
import { ValidationMessage } from './ValidationMessage';

export interface LookupOption {
  value: number;
  label: string;
}

interface LookupSelectProps {
  id: string;
  label: string;
  registration: UseFormRegisterReturn;
  options: LookupOption[];
  error?: string;
  required?: boolean;
  disabled?: boolean;
  placeholder?: string;
}

/** Labeled <select> for lookup values, wired to react-hook-form with linked inline error. */
export function LookupSelect({
  id,
  label,
  registration,
  options,
  error,
  required = false,
  disabled = false,
  placeholder = 'Select...',
}: LookupSelectProps) {
  const errorId = `${id}-error`;

  return (
    <div className="field">
      <label htmlFor={id}>
        {label}
        {required && <span className="required" aria-hidden="true">*</span>}
      </label>
      <select
        id={id}
        disabled={disabled}
        aria-required={required}
        aria-invalid={error ? true : undefined}
        aria-describedby={error ? errorId : undefined}
        {...registration}
      >
        <option value={0}>{placeholder}</option>
        {options.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </select>
      <ValidationMessage id={errorId} message={error} />
    </div>
  );
}
