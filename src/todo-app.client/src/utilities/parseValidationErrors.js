export const parseValidationError = (errors) => {
  let result = '';

  for (const key in errors) {
    if (Object.prototype.hasOwnProperty.call(errors, key)) {
      const messages = errors[key];
      messages.forEach((message) => {
        result += `${message}\n`;
      });
    }
  }

  return result.trim();
};
