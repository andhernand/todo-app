/* eslint-disable @stylistic/js/quotes */

import { describe, expect, it } from 'vitest';
import { parseValidationError } from '../../src/utilities/parseValidationErrors';

describe('parse validation error', () => {
  it('parses single error for single property', () => {
    const errors = {
      Description: ["'Description' may not be empty."],
    };

    const error = parseValidationError(errors);
    expect(error).toBe("'Description' may not be empty.");
  });

  it('parses multiple errors for single property', () => {
    const errors = {
      Description: [
        "'Description' may not be empty.",
        "'Description' already exists.",
      ],
    };

    const error = parseValidationError(errors);
    expect(error).toBe(
      "'Description' may not be empty.\n'Description' already exists.",
    );
  });

  it('parses single errors for multiple property', () => {
    const errors = {
      Description: ["'Description' may not be empty."],
      Id: ["'Id' must be a positive number."],
    };

    const error = parseValidationError(errors);
    expect(error).toBe(
      "'Description' may not be empty.\n'Id' must be a positive number.",
    );
  });
});
