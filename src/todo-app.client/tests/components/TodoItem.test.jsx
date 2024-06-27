import { describe, expect, it, vi, beforeEach } from 'vitest';
import { render, screen } from '@testing-library/react';
import TodoItem from '../../src/components/TodoItem';

describe('Todo Item', () => {
  let dispatchMock;
  let todo;

  beforeEach(() => {
    dispatchMock = vi.fn();
    todo = { id: 1, description: 'tests', isCompleted: false };
  });

  it('renders todo description', () => {
    render(
      <table>
        <tbody>
          <TodoItem todo={todo} dispatch={dispatchMock} />
        </tbody>
      </table>,
    );

    const description = screen.getByText(todo.description);
    expect(description).toBeInTheDocument();
  });

  it('renders correct button when todo is incomplete', () => {
    render(
      <table>
        <tbody>
          <TodoItem todo={todo} dispatch={dispatchMock} />
        </tbody>
      </table>,
    );

    const inCompleteButton = screen.getByText('incomplete');
    expect(inCompleteButton).toBeInTheDocument();
  });

  it('renders correct button when todo is complete', () => {
    const completedTodo = { ...todo, isCompleted: true };

    render(
      <table>
        <tbody>
          <TodoItem todo={completedTodo} dispatch={dispatchMock} />
        </tbody>
      </table>,
    );

    const completeButton = screen.getByText('completed');
    expect(completeButton).toBeInTheDocument();
  });

  it('renders the edit button', () => {
    render(
      <table>
        <tbody>
          <TodoItem todo={todo} dispatch={dispatchMock} />
        </tbody>
      </table>,
    );

    const editButton = screen.getByText('edit');
    expect(editButton).toBeInTheDocument();
  });

  it('renders the delete button', () => {
    render(
      <table>
        <tbody>
          <TodoItem todo={todo} dispatch={dispatchMock} />
        </tbody>
      </table>,
    );

    const deleteButton = screen.getByText('delete');
    expect(deleteButton).toBeInTheDocument();
  });
});
