import { render, screen } from '@testing-library/react';
import { describe, expect, it, vi } from 'vitest';
import type { UseFormRegisterReturn } from 'react-hook-form';
import { TextInput } from './TextInput';

const registration = (name: string): UseFormRegisterReturn => ({
  name,
  onChange: vi.fn(),
  onBlur: vi.fn(),
  ref: vi.fn(),
});

describe('TextInput', () => {
  it('associates the label with the input', () => {
    render(<TextInput id="firstName" label="First name" registration={registration('firstName')} />);
    expect(screen.getByLabelText(/first name/i)).toBeInTheDocument();
  });

  it('shows the error and links it to the input for assistive tech', () => {
    render(
      <TextInput
        id="email"
        label="Email"
        required
        error="Enter a valid email address."
        registration={registration('email')}
      />,
    );

    const input = screen.getByLabelText(/email/i);
    expect(input).toHaveAttribute('aria-invalid', 'true');
    expect(input).toHaveAttribute('aria-describedby', 'email-error');

    const message = screen.getByRole('alert');
    expect(message).toHaveAttribute('id', 'email-error');
    expect(message).toHaveTextContent('Enter a valid email address.');
  });

  it('does not render an error message when there is no error', () => {
    render(<TextInput id="lastName" label="Last name" registration={registration('lastName')} />);
    expect(screen.queryByRole('alert')).not.toBeInTheDocument();
  });
});
