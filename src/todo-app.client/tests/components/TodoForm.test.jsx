import { describe, expect, it, vi } from 'vitest';
import { render, screen } from '@testing-library/react';
import TodoForm from '../../src/components/TodoForm';

describe('Todo Form', () => {
  it('renders form when no editingTodo exists', () => {
    const dispatchMock = vi.fn();
    const editingTodo = null;

    render(<TodoForm dispatch={dispatchMock} editingTodo={editingTodo} />);

    const textInput = screen.getByPlaceholderText('Add a task');
    expect(textInput).toBeInTheDocument();

    const submitButton = screen.getByText('submit');
    expect(submitButton).toBeInTheDocument();
  });

  it('renders form when editingTodo exists', () => {
    const dispatchMock = vi.fn();
    const editingTodo = {
      id: 1,
      description: 'editing task',
      isCompleted: false,
    };

    render(<TodoForm dispatch={dispatchMock} editingTodo={editingTodo} />);

    const textInput = screen.getByDisplayValue(editingTodo.description);
    expect(textInput).toBeInTheDocument();

    const submitButton = screen.getByText('submit');
    expect(submitButton).toBeInTheDocument();

    const cancelButton = screen.getByText('cancel');
    expect(cancelButton).toBeInTheDocument();
  });
});
