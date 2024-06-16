import { useReducer } from 'react';
import todoReducer from './reducers/todoReducer.jsx';
import TodoForm from './components/TodoForm';
import TodoList from './components/TodoList';

function App() {
  const [state, dispatch] = useReducer(todoReducer, { todos: [] });

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
