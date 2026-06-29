export interface Governorate {
  id: number;
  nameEn: string;
  nameAr: string;
}

export interface City {
  id: number;
  governorateId: number;
  nameEn: string;
  nameAr: string;
}

export interface CreateAddressRequest {
  governorateId: number;
  cityId: number;
  street: string;
  buildingNumber: string;
  flatNumber: string;
  isPrimary: boolean;
}

export interface CreateRegistrationRequest {
  firstName: string;
  middleName?: string | null;
  lastName: string;
  birthDate: string; // yyyy-MM-dd
  mobile: string;
  email: string;
  addresses: CreateAddressRequest[];
}

export interface CreateRegistrationResult {
  id: string;
}

/** RFC 7807 ProblemDetails (and ValidationProblemDetails) returned by the API. */
export interface ProblemDetails {
  type?: string;
  title?: string;
  status?: number;
  detail?: string;
  errors?: Record<string, string[]>;
  field?: string;
  correlationId?: string;
}
