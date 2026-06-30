import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { beforeEach, describe, expect, it, vi } from 'vitest';
import { ApiError } from '../api/client';
import { getRegistrationById } from '../api/registrations';
import type { RegistrationDetails } from '../api/types';
import { ViewRegistration } from './ViewRegistration';

vi.mock('../api/registrations', () => ({ getRegistrationById: vi.fn() }));

const mockGet = vi.mocked(getRegistrationById);

const sample: RegistrationDetails = {
  id: '3f2504e0-4f89-41d3-9a0c-0305e82c3301',
  firstName: 'Abdelrahman',
  middleName: 'Mohamed',
  lastName: 'Omar',
  fullName: 'Abdelrahman Mohamed Omar',
  birthDate: '1998-01-01',
  email: 'abdo@example.com',
  mobile: '+201006158123',
  createdAtUtc: '2026-06-29T00:00:00Z',
  updatedAtUtc: null,
  addresses: [
    {
      id: 'a1',
      governorateId: 1,
      governorateName: 'Cairo',
      cityId: 1,
      cityName: 'Nasr City',
      street: 'Tahrir Street',
      buildingNumber: '12A',
      flatNumber: '3',
      isPrimary: true,
    },
  ],
};

describe('ViewRegistration', () => {
  beforeEach(() => mockGet.mockReset());

  it('disables Find until an id is entered', () => {
    render(<ViewRegistration />);
    expect(screen.getByRole('button', { name: /find/i })).toBeDisabled();
  });

  it('renders details on a successful lookup', async () => {
    mockGet.mockResolvedValueOnce(sample);
    const user = userEvent.setup();

    render(<ViewRegistration />);
    await user.type(screen.getByLabelText(/registration id/i), sample.id);
    await user.click(screen.getByRole('button', { name: /find/i }));

    expect(await screen.findByText('Abdelrahman Mohamed Omar')).toBeInTheDocument();
    expect(screen.getByText('Nasr City')).toBeInTheDocument();
    expect(screen.getByText('Primary')).toBeInTheDocument();
  });

  it('shows a not-found state for a 404', async () => {
    mockGet.mockRejectedValueOnce(new ApiError(404, null, 'not found'));

    render(<ViewRegistration initialId="missing-id" />);

    await waitFor(() =>
      expect(screen.getByText(/no registration found/i)).toBeInTheDocument(),
    );
  });
});
