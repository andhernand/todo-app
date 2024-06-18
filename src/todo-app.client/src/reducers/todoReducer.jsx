export const initialState = {
  todos: [],
};

export const INITIALIZE_TODOS = 'INITIALIZE_TODOS';
export const ADD_TODO = 'ADD_TODO';
export const UPDATE_TODO = 'UPDATE_TODO';
export const DELETE_TODO = 'DELETE_TODO';

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
      };
    case DELETE_TODO:
      return {
        ...state,
        todos: state.todos.filter((todo) => todo.id !== payload.id),
      };
    default:
      return state;
  }
};

export default todoReducer;
