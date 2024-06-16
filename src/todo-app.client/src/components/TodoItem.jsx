const TodoItem = ({ todo, dispatch }) => {
  const handleComplete = (e) => {
    e.preventDefault();
    dispatch({
      type: 'UPDATE_TODO',
      payload: { ...todo, isCompleted: !todo.isCompleted },
    });
  };

  const handleEdit = (e) => {
    e.preventDefault();
    // TODO: Implement Editing.
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
      <div className="buttons">
        <button
          className="icon-button"
          type="button"
          onClick={(e) => handleComplete(e)}
        >
          <i className="fas fa-check"></i>
        </button>
        <button
          className="icon-button"
          type="button"
          onClick={(e) => handleEdit(e)}
        >
          <i className="fas fa-edit"></i>
        </button>
        <button
          className="icon-button"
          type="button"
          onClick={(e) => handleDelete(e)}
        >
          <i className="fas fa-trash"></i>
        </button>
      </div>
    </li>
  );
};

export default TodoItem;
