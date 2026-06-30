import { useCallback, useEffect, useState } from 'react';
import { ApiError } from '../api/client';
import { getRegistrationById } from '../api/registrations';
import type { RegistrationDetails } from '../api/types';
import { RegistrationDetailsCard } from './RegistrationDetailsCard';

type Status = 'idle' | 'loading' | 'found' | 'notfound' | 'error';

interface ViewRegistrationProps {
  initialId?: string | null;
}

/** Look up a registration by its id and render its details. */
export function ViewRegistration({ initialId }: ViewRegistrationProps) {
  const [id, setId] = useState(initialId ?? '');
  const [status, setStatus] = useState<Status>('idle');
  const [data, setData] = useState<RegistrationDetails | null>(null);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const fetchById = useCallback(async (value: string) => {
    const trimmed = value.trim();
    if (!trimmed) {
      return;
    }

    setStatus('loading');
    setErrorMessage(null);
    setData(null);

    try {
      const result = await getRegistrationById(trimmed);
      setData(result);
      setStatus('found');
    } catch (error) {
      if (error instanceof ApiError && error.status === 404) {
        setStatus('notfound');
        return;
      }
      setErrorMessage(error instanceof ApiError ? error.message : 'Something went wrong.');
      setStatus('error');
    }
  }, []);

  useEffect(() => {
    if (initialId) {
      setId(initialId);
      void fetchById(initialId);
    }
  }, [initialId, fetchById]);

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    void fetchById(id);
  };

  return (
    <div className="view-registration">
      <section className="card lookup-card">
        <h2>Find a registration</h2>
        <p className="subtitle">Enter a registration id to view its details.</p>

        <form onSubmit={handleSubmit} className="lookup-form" noValidate>
          <div className="field lookup-field">
            <label htmlFor="lookup-id">Registration id</label>
            <input
              id="lookup-id"
              value={id}
              onChange={(event) => setId(event.target.value)}
              placeholder="e.g. 3f2504e0-4f89-41d3-9a0c-0305e82c3301"
              autoComplete="off"
              spellCheck={false}
            />
          </div>
          <button type="submit" className="btn-primary" disabled={!id.trim() || status === 'loading'}>
            {status === 'loading' ? (
              <span className="btn-loading">
                <span className="spinner" aria-hidden="true" /> Searching…
              </span>
            ) : (
              'Find'
            )}
          </button>
        </form>
      </section>

      <div className="result" aria-live="polite">
        {status === 'loading' && (
          <div className="card skeleton-card" aria-hidden="true">
            <div className="skeleton skeleton-avatar" />
            <div className="skeleton skeleton-line w-60" />
            <div className="skeleton skeleton-line w-40" />
            <div className="skeleton skeleton-line w-80" />
          </div>
        )}

        {status === 'found' && data && <RegistrationDetailsCard data={data} />}

        {status === 'notfound' && (
          <div className="empty-state pop-in">
            <div className="empty-emoji" aria-hidden="true">🔍</div>
            <h3>No registration found</h3>
            <p className="muted">There is no registration with that id. Check it and try again.</p>
          </div>
        )}

        {status === 'error' && (
          <div className="alert alert-error" role="alert">{errorMessage}</div>
        )}
      </div>
    </div>
  );
}
