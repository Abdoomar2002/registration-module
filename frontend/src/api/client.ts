import type { ProblemDetails } from './types';

const API_BASE = '/api';

/** Error carrying the parsed ProblemDetails so the UI can map server errors to fields. */
export class ApiError extends Error {
  constructor(
    public readonly status: number,
    public readonly problem: ProblemDetails | null,
    message: string,
  ) {
    super(message);
    this.name = 'ApiError';
  }

  /** Per-field validation errors (lower-cased keys), from a 400 response. */
  get fieldErrors(): Record<string, string[]> {
    return this.problem?.errors ?? {};
  }

  /** The conflicting field for a 409 (e.g. "email" or "mobile"). */
  get conflictField(): string | undefined {
    return this.problem?.field;
  }
}

async function parseProblem(response: Response): Promise<ProblemDetails | null> {
  try {
    const text = await response.text();
    return text ? (JSON.parse(text) as ProblemDetails) : null;
  } catch {
    return null;
  }
}

async function request<T>(path: string, init?: RequestInit): Promise<T> {
  let response: Response;
  try {
    response = await fetch(`${API_BASE}${path}`, {
      headers: { 'Content-Type': 'application/json', ...(init?.headers ?? {}) },
      ...init,
    });
  } catch {
    throw new ApiError(0, null, 'Could not reach the server. Please try again.');
  }

  if (!response.ok) {
    const problem = await parseProblem(response);
    const message = problem?.detail ?? problem?.title ?? `Request failed (${response.status}).`;
    throw new ApiError(response.status, problem, message);
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return (await response.json()) as T;
}

export const apiGet = <T>(path: string) => request<T>(path, { method: 'GET' });

export const apiPost = <T>(path: string, body: unknown) =>
  request<T>(path, { method: 'POST', body: JSON.stringify(body) });
