import type { RegistrationDetails } from '../api/types';

interface RegistrationDetailsCardProps {
  data: RegistrationDetails;
}

function formatDate(value?: string | null): string {
  if (!value) return '—';
  const date = new Date(value);
  return Number.isNaN(date.getTime()) ? value : date.toLocaleDateString();
}

function initials(fullName: string): string {
  return fullName
    .split(' ')
    .filter(Boolean)
    .slice(0, 2)
    .map((part) => part[0]?.toUpperCase())
    .join('');
}

/** Read-only presentation of a registration, with personal info and addresses. */
export function RegistrationDetailsCard({ data }: RegistrationDetailsCardProps) {
  return (
    <div className="details pop-in">
      <header className="details-header">
        <div className="avatar" aria-hidden="true">{initials(data.fullName)}</div>
        <div>
          <h2 className="details-name">{data.fullName}</h2>
          <p className="details-id">
            <span className="muted">ID</span> <code>{data.id}</code>
          </p>
        </div>
      </header>

      <section className="detail-grid" aria-label="Personal details">
        <Detail label="First name" value={data.firstName} />
        <Detail label="Middle name" value={data.middleName || '—'} />
        <Detail label="Last name" value={data.lastName} />
        <Detail label="Birth date" value={formatDate(data.birthDate)} />
        <Detail label="Email" value={data.email} />
        <Detail label="Mobile" value={data.mobile} />
      </section>

      <h3 className="details-subhead">
        Addresses <span className="count-pill">{data.addresses.length}</span>
      </h3>

      <ul className="address-list">
        {data.addresses.map((address, index) => (
          <li
            key={address.id}
            className="address-chip stagger"
            style={{ animationDelay: `${index * 70}ms` }}
          >
            <div className="address-chip-head">
              <strong>{address.cityName ?? `City #${address.cityId}`}</strong>
              <span className="muted">{address.governorateName ?? `Governorate #${address.governorateId}`}</span>
              {address.isPrimary && <span className="badge badge-primary">Primary</span>}
            </div>
            <div className="address-chip-body">
              {address.street}, Bldg {address.buildingNumber}, Flat {address.flatNumber}
            </div>
          </li>
        ))}
      </ul>

      <footer className="details-footer muted">
        Created {formatDate(data.createdAtUtc)}
        {data.updatedAtUtc ? ` · Updated ${formatDate(data.updatedAtUtc)}` : ''}
      </footer>
    </div>
  );
}

function Detail({ label, value }: { label: string; value: string }) {
  return (
    <div className="detail-item">
      <span className="detail-label">{label}</span>
      <span className="detail-value">{value}</span>
    </div>
  );
}
