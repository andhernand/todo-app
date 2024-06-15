import './App.css';
import Header from './components/Header';
import Footer from './components/Footer';
import TodoForm from './components/TodoForm';
import TodoList from './components/TodoList';
import UseGetTodos from '@/hooks/useGetTodos.jsx';

function App() {
  const { todos } = UseGetTodos([]);

  return (
    <div className="content">
      <Header />
      <TodoForm />
      <TodoList todos={todos} />
      <Footer />
    </div>
  );
}

export default App;
