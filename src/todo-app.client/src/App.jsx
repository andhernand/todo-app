import { useReducer, useEffect } from 'react';
import todoReducer, {
  initialState,
  INITIALIZE_TODOS,
} from './reducers/todoReducer';
import { getAllTodos } from './services/todoService';
import TodoForm from './components/TodoForm';
import TodoList from './components/TodoList';
import { sortTodos } from './utilities/sortUtilities';

function App() {
  const [state, dispatch] = useReducer(todoReducer, initialState);
  const sortedTodos = sortTodos(state.todos);

  useEffect(() => {
    const fetchTodos = () => {
      getAllTodos()
        .then((response) => {
          if (response.status === 200) {
            dispatch({
              type: INITIALIZE_TODOS,
              payload: response.data,
            });
          }
        })
        .catch((e) => {
          const response = e.response;
          console.error(response.statusText);
        });
    };

    fetchTodos();
  }, []);

  return (
    <>
      <header>
        <h1>Tasks</h1>
      </header>
      <TodoForm editingTodo={state.editingTodo} dispatch={dispatch} />
      <TodoList todos={sortedTodos} dispatch={dispatch} />
    </>
  );
}

export default App;
