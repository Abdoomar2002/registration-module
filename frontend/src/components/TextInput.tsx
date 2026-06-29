import type { UseFormRegisterReturn } from 'react-hook-form';
import { ValidationMessage } from './ValidationMessage';

interface TextInputProps {
  id: string;
  label: string;
  registration: UseFormRegisterReturn;
  error?: string;
  required?: boolean;
  type?: string;
  placeholder?: string;
  autoComplete?: string;
  inputMode?: 'text' | 'email' | 'tel';
  max?: string;
}

/** Labeled text field wired to react-hook-form, with linked inline error and a11y attributes. */
export function TextInput({
  id,
  label,
  registration,
  error,
  required = false,
  type = 'text',
  placeholder,
  autoComplete,
  inputMode,
  max,
}: TextInputProps) {
  const errorId = `${id}-error`;

  return (
    <div className="field">
      <label htmlFor={id}>
        {label}
        {required && <span className="required" aria-hidden="true">*</span>}
      </label>
      <input
        id={id}
        type={type}
        placeholder={placeholder}
        autoComplete={autoComplete}
        inputMode={inputMode}
        max={max}
        aria-required={required}
        aria-invalid={error ? true : undefined}
        aria-describedby={error ? errorId : undefined}
        {...registration}
      />
      <ValidationMessage id={errorId} message={error} />
    </div>
  );
}
