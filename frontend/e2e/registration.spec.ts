import { expect, test } from '@playwright/test';

const uniqueEmail = () => `e2e-${Date.now()}-${Math.floor(Math.random() * 1e6)}@example.com`;
const uniqueMobile = () => `+2010${String(Math.floor(10_000_000 + Math.random() * 89_999_999))}`;

test.describe('Registration form', () => {
  test('shows validation messages and keeps submit disabled for invalid input', async ({ page }) => {
    await page.goto('/');

    const submit = page.getByRole('button', { name: /submit registration/i });
    await expect(submit).toBeDisabled();

    await page.getByLabel(/first name/i).fill('Ahmed123');
    await page.getByLabel(/first name/i).blur();

    await expect(
      page.getByText(/only arabic or english letters/i).first(),
    ).toBeVisible();
    await expect(submit).toBeDisabled();
  });

  // Requires the API (and DB) reachable through the Vite proxy.
  test('submits a valid registration and shows the created id @happy', async ({ page }) => {
    await page.goto('/');

    await page.getByLabel(/first name/i).fill('Abdelrahman');
    await page.getByLabel(/last name/i).fill('Omar');
    await page.getByLabel(/birth date/i).fill('1998-01-01');
    await page.getByLabel(/mobile number/i).fill(uniqueMobile());
    await page.getByLabel(/email/i).fill(uniqueEmail());

    // Governorate -> dependent City.
    await page.getByLabel(/governorate/i).selectOption({ label: 'Cairo - القاهرة' });
    await expect(page.getByLabel(/^city/i)).toBeEnabled();
    await page.getByLabel(/^city/i).selectOption({ index: 1 });

    await page.getByLabel(/street/i).fill('Tahrir Street');
    await page.getByLabel(/building number/i).fill('12A');
    await page.getByLabel(/flat number/i).fill('3');

    const submit = page.getByRole('button', { name: /submit registration/i });
    await expect(submit).toBeEnabled();
    await submit.click();

    await expect(page.getByText(/you're registered/i)).toBeVisible();
    await expect(page.getByText(/reference id/i)).toBeVisible();

    // View the just-created registration by id.
    await page.getByRole('button', { name: /view registration/i }).click();
    await expect(page.getByText('Abdelrahman Omar')).toBeVisible();
    await expect(page.getByText('Tahrir Street, Bldg 12A, Flat 3')).toBeVisible();
  });

  test('find page reports a not-found id @happy', async ({ page }) => {
    await page.goto('/');
    await page.getByRole('button', { name: /find registration/i }).click();
    await page.getByLabel(/registration id/i).fill('00000000-0000-0000-0000-000000000000');
    await page.getByRole('button', { name: /^find$/i }).click();
    await expect(page.getByText(/no registration found/i)).toBeVisible();
  });
});
