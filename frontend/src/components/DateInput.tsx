import type { UseFormRegisterReturn } from 'react-hook-form';
import { TextInput } from './TextInput';

interface DateInputProps {
  id: string;
  label: string;
  registration: UseFormRegisterReturn;
  error?: string;
  required?: boolean;
  max?: string;
}

/** A date-only field; reuses TextInput with type="date" for consistent styling/a11y. */
export function DateInput(props: DateInputProps) {
  return <TextInput {...props} type="date" />;
}
