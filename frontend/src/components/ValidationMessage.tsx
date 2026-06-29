interface ValidationMessageProps {
  id: string;
  message?: string;
}

/** Inline, screen-reader-announced validation message linked to a field via id. */
export function ValidationMessage({ id, message }: ValidationMessageProps) {
  if (!message) {
    return null;
  }

  return (
    <p className="validation-message" id={id} role="alert">
      {message}
    </p>
  );
}
