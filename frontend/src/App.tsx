import { useState } from 'react';
import { RegistrationForm } from './components/RegistrationForm';
import { ViewRegistration } from './components/ViewRegistration';

type View = 'register' | 'find';

export function App() {
  const [view, setView] = useState<View>('register');
  const [lookupId, setLookupId] = useState<string | null>(null);

  const openFind = (id?: string) => {
    setLookupId(id ?? null);
    setView('find');
  };

  return (
    <div className="app-shell">
      <header className="hero">
        <div className="hero-inner">
          <div className="brand">
            <span className="brand-mark" aria-hidden="true">3S</span>
            <span className="brand-text">Registration</span>
          </div>
          <nav className="tabs" aria-label="Pages">
            <button
              type="button"
              className={`tab ${view === 'register' ? 'active' : ''}`}
              aria-current={view === 'register'}
              onClick={() => setView('register')}
            >
              Register
            </button>
            <button
              type="button"
              className={`tab ${view === 'find' ? 'active' : ''}`}
              aria-current={view === 'find'}
              onClick={() => openFind()}
            >
              Find registration
            </button>
          </nav>
        </div>
      </header>

      <main className="page">
        {view === 'register' ? (
          <div key="register" className="view-enter">
            <header className="page-head">
              <h1>Create your profile</h1>
              <p className="subtitle">
                Enter your personal details and at least one address. Fields marked
                <span className="required" aria-hidden="true"> *</span> are required.
              </p>
            </header>
            <RegistrationForm onViewRegistration={openFind} />
          </div>
        ) : (
          <div key="find" className="view-enter">
            <ViewRegistration initialId={lookupId} />
          </div>
        )}
      </main>

      <footer className="app-footer muted">3S Group · Registration module</footer>
    </div>
  );
}
