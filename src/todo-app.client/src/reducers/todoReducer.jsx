const todoReducer = (state, action) => {
  switch (action.type) {
    case 'ADD_TODO':
      return { ...state, todos: [...state.todos, action.payload] };
    case 'UPDATE_TODO':
      return {
        ...state,
        todos: state.todos.map((todo) =>
          todo.id === action.payload.id ? action.payload : todo,
        ),
      };
    case 'DELETE_TODO':
      return {
        ...state,
        todos: state.todos.filter((todo) => todo.id !== action.payload.id),
      };
    default:
      return state;
  }
};

export default todoReducer;
