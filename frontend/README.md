# Registration Frontend

React + TypeScript (Vite) UI for the registration module.

## Stack
- **React 18 + TypeScript**, Vite
- **React Hook Form + Zod** (`zodResolver`) for form state and validation
- Reusable components: `TextInput`, `DateInput`, `LookupSelect`, `AddressForm`, `ValidationMessage`
- **Vitest** + Testing Library (unit/component), **Playwright** (E2E)

## Features
- Personal details + 1–5 addresses, exactly one primary
- Inline validation messages linked to inputs (a11y), submit disabled while invalid/submitting
- Governorate/City lookups from the API; **City depends on the selected Governorate**
- Graceful API error handling, including duplicate email/mobile (409) mapped to the field

## Getting started
```bash
npm install
npm run dev          # http://localhost:5173 (proxies /api -> VITE_API_PROXY_TARGET)
```
Set `VITE_API_PROXY_TARGET` (see `.env.example`) to your running API (default `http://localhost:8080`).

## Tests
```bash
npm test             # Vitest unit/component tests
npm run e2e:install  # one-time: download Playwright Chromium
npm run e2e          # Playwright E2E (needs the API reachable for the @happy test)
```

The app calls a relative `/api`; in dev the Vite proxy forwards it, and in production
nginx proxies `/api` to the API container (see `docker/`).
