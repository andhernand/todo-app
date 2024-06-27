import { describe, expect, it } from 'vitest';
import { sortTodos } from '../../src/utilities/sortUtilities';

describe('sort utilities', () => {
  it('places completed at the bottom', () => {
    const todos = [
      { id: 1, description: 'clean room', isCompleted: true },
      { id: 2, description: 'clean carpet', isCompleted: false },
      { id: 3, description: 'do laundry', isCompleted: false },
    ];

    const sorted = sortTodos(todos);
    expect(sorted[2].isCompleted).toBe(true);
  });
});
