import { RegistrationForm } from './components/RegistrationForm';

export function App() {
  return (
    <main className="page">
      <header>
        <h1>Create your profile</h1>
        <p className="subtitle">
          Enter your personal details and at least one address. Fields marked
          <span className="required" aria-hidden="true"> *</span> are required.
        </p>
      </header>
      <RegistrationForm />
    </main>
  );
}
