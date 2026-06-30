import { apiGet, apiPost } from './client';
import type {
  CreateRegistrationRequest,
  CreateRegistrationResult,
  RegistrationDetails,
} from './types';

export const createRegistration = (request: CreateRegistrationRequest) =>
  apiPost<CreateRegistrationResult>('/registrations', request);

export const getRegistrationById = (id: string) =>
  apiGet<RegistrationDetails>(`/registrations/${encodeURIComponent(id)}`);
