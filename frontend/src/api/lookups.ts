import { apiGet } from './client';
import type { City, Governorate } from './types';

export const getGovernorates = () => apiGet<Governorate[]>('/lookups/governorates');

export const getCities = (governorateId: number) =>
  apiGet<City[]>(`/lookups/cities?governorateId=${governorateId}`);
