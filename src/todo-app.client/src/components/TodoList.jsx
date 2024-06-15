import TodoItem from '@/components/TodoItem.jsx';

const TodoList = ({ todos }) => {
  return (
    <section className="todoList">
      <ul>
        {todos.map((todo) => (
          <TodoItem key={todo.id} todo={todo} />
        ))}
      </ul>
    </section>
  );
};

export default TodoList;
