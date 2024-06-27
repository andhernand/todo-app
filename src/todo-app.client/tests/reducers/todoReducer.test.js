import { describe, it, expect } from 'vitest';
import todoReducer, {
  initialState,
  INITIALIZE_TODOS,
  ADD_TODO,
  UPDATE_TODO,
  DELETE_TODO,
  SET_EDITING_TODO,
  CLEAR_EDITING_TODO,
} from '../../src/reducers/todoReducer';

describe('todo reducer', () => {
  it('INITIALIZE_TODOS sets all todos', () => {
    const newState = todoReducer(initialState, {
      type: INITIALIZE_TODOS,
      payload: [
        { id: 1, description: 'task 1', isCompleted: true },
        { id: 2, description: 'task 2', isCompleted: false },
      ],
    });

    expect(newState).toEqual({
      todos: [
        { id: 1, description: 'task 1', isCompleted: true },
        { id: 2, description: 'task 2', isCompleted: false },
      ],
      editingTodo: null,
    });
  });

  it('ADD_TODO adds a todo to the state', () => {
    const newState = todoReducer(initialState, {
      type: ADD_TODO,
      payload: { id: 1, description: 'task 1', isCompleted: true },
    });

    expect(newState).toEqual({
      todos: [{ id: 1, description: 'task 1', isCompleted: true }],
      editingTodo: null,
    });
  });

  it('UPDATE_TODO updates the todo in the state', () => {
    const newState = todoReducer(
      {
        todos: [{ id: 1, description: 'task 1', isCompleted: true }],
        editingTodo: null,
      },
      {
        type: UPDATE_TODO,
        payload: { id: 1, description: 'task updated', isCompleted: false },
      },
    );

    expect(newState).toEqual({
      todos: [{ id: 1, description: 'task updated', isCompleted: false }],
      editingTodo: null,
    });
  });

  it('DELETE_TODO while editing clears editing and removes a todo from the state', () => {
    const newState = todoReducer(
      {
        todos: [{ id: 1, description: 'task 1', isCompleted: true }],
        editingTodo: { id: 1, description: 'task 1', isCompleted: true },
      },
      {
        type: DELETE_TODO,
        payload: { id: 1 },
      },
    );

    expect(newState).toEqual({
      todos: [],
      editingTodo: null,
    });
  });

  it('DELETE_TODO removes a todo from the state', () => {
    const newState = todoReducer(
      {
        todos: [{ id: 1, description: 'task 1', isCompleted: true }],
        editingTodo: null,
      },
      {
        type: DELETE_TODO,
        payload: { id: 1 },
      },
    );

    expect(newState).toEqual({
      todos: [],
      editingTodo: null,
    });
  });

  it('SET_EDITING_TODO assigns the editing todo', () => {
    const newState = todoReducer(initialState, {
      type: SET_EDITING_TODO,
      payload: { id: 99, description: 'editing', isCompleted: false },
    });

    expect(newState).toEqual({
      todos: [],
      editingTodo: { id: 99, description: 'editing', isCompleted: false },
    });
  });

  it('CLEAR_EDITING_TODO clears the editing todo', () => {
    const newState = todoReducer(
      {
        todos: [],
        editingTodo: { id: 99, description: 'editing', isCompleted: false },
      },
      {
        type: CLEAR_EDITING_TODO,
      },
    );

    expect(newState).toEqual({
      todos: [],
      editingTodo: null,
    });
  });
});
