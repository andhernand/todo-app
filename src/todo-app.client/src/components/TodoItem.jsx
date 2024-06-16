const TodoItem = ({ todo, dispatch }) => {
  const handleClick = (e) => {
    e.preventDefault();
    dispatch({
      type: 'UPDATE_TODO',
      payload: { ...todo, isCompleted: !todo.isCompleted },
    });
  };

  const handleDelete = (e) => {
    e.preventDefault();
    dispatch({
      type: 'DELETE_TODO',
      payload: todo,
    });
  };

  return (
    <li className="todo-item">
      <span className={todo.isCompleted ? 'todo-completed' : ''}>
        {todo.description}
      </span>
      <button onClick={(e) => handleClick(e)}>Complete</button>
      <button onClick={(e) => handleDelete(e)}>Delete</button>
    </li>
  );
};

export default TodoItem;
