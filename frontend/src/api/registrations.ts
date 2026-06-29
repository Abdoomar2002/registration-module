import { apiGet, apiPost } from './client';
import type { CreateRegistrationRequest, CreateRegistrationResult } from './types';

export const createRegistration = (request: CreateRegistrationRequest) =>
  apiPost<CreateRegistrationResult>('/registrations', request);

export const getRegistrationById = (id: string) => apiGet(`/registrations/${id}`);
