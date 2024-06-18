import { useReducer, useEffect } from 'react';
import todoReducer, {
  initialState,
  INITIALIZE_TODOS,
} from './reducers/todoReducer';
import { getAllTodos } from './services/todoService';
import TodoForm from './components/TodoForm';
import TodoList from './components/TodoList';

function App() {
  const [state, dispatch] = useReducer(todoReducer, initialState);

  useEffect(() => {
    const fetchTodos = () => {
      getAllTodos().then((response) => {
        const allTodos = response.data.todos;
        dispatch({
          type: INITIALIZE_TODOS,
          payload: allTodos,
        });
      });
    };

    fetchTodos();
  }, []);

  return (
    <>
      <header>
        <h1>Tasks</h1>
      </header>
      <TodoForm dispatch={dispatch} />
      <TodoList todos={state.todos} dispatch={dispatch} />
    </>
  );
}

export default App;
