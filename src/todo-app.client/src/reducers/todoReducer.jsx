export const initialState = {
  todos: [],
  editingTodo: null,
};

export const INITIALIZE_TODOS = 'INITIALIZE_TODOS';
export const ADD_TODO = 'ADD_TODO';
export const UPDATE_TODO = 'UPDATE_TODO';
export const DELETE_TODO = 'DELETE_TODO';
export const SET_EDITING_TODO = 'SET_EDITING_TODO';
export const CLEAR_EDITING_TODO = 'CLEAR_EDITING_TODO';

const todoReducer = (state = initialState, { type, payload } = {}) => {
  switch (type) {
    case INITIALIZE_TODOS:
      return { ...state, todos: payload };
    case ADD_TODO:
      return { ...state, todos: [...state.todos, payload] };
    case UPDATE_TODO:
      return {
        ...state,
        todos: state.todos.map((todo) =>
          todo.id === payload.id ? payload : todo,
        ),
        editingTodo: null,
      };
    case DELETE_TODO:
      if (state.editingTodo && state.editingTodo.id === payload.id) {
        return {
          ...state,
          todos: state.todos.filter((todo) => todo.id !== payload.id),
          editingTodo: null,
        };
      } else {
        return {
          ...state,
          todos: state.todos.filter((todo) => todo.id !== payload.id),
        };
      }
    case SET_EDITING_TODO:
      return {
        ...state,
        editingTodo: payload,
      };
    case CLEAR_EDITING_TODO:
      return {
        ...state,
        editingTodo: null,
      };
    default:
      return state;
  }
};

export default todoReducer;
