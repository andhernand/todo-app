import TodoItem from '@/components/TodoItem.jsx';

const TodoList = ({ todos, dispatch }) => {
  return (
    <article className="todo-list">
      <table>
        <tbody>
          {todos.map((todo) => (
            <TodoItem key={todo.id} todo={todo} dispatch={dispatch} />
          ))}
        </tbody>
      </table>
    </article>
  );
};

export default TodoList;
