import { describe, expect, it, vi } from 'vitest';
import { render, screen } from '@testing-library/react';
import TodoList from '../../src/components/TodoList';

describe('Todo List', () => {
  it('renders multiple todos', () => {
    const dispatchMock = vi.fn();
    const todos = [
      { id: 3, description: 'todo list task', isCompleted: false },
      { id: 5, description: 'another todo list task', isCompleted: true },
    ];

    render(<TodoList todos={todos} dispatch={dispatchMock()} />);

    const todoOne = screen.getByText(todos[0].description);
    expect(todoOne).toBeInTheDocument();

    const todoTwo = screen.getByText(todos[1].description);
    expect(todoTwo).toBeInTheDocument();
  });
});
