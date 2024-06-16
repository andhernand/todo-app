import TodoItem from '@/components/TodoItem.jsx';

const TodoList = ({ todos, dispatch }) => {
  return (
    <section className="todo-list">
      <ul>
        {todos.map((todo) => (
          <TodoItem key={todo.id} todo={todo} dispatch={dispatch} />
        ))}
      </ul>
    </section>
  );
};

export default TodoList;
