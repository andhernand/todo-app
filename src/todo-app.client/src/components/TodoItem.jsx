import { UPDATE_TODO, DELETE_TODO } from '../reducers/todoReducer';
import { updateTodo, deleteTodo } from '../services/todoService';

const TodoItem = ({ todo, dispatch }) => {
  const handleComplete = (e) => {
    e.preventDefault();

    updateTodo(todo.id, { ...todo, isCompleted: !todo.isCompleted }).then(
      (response) => {
        dispatch({
          type: UPDATE_TODO,
          payload: response.data,
        });
      },
    );
  };

  const handleEdit = (e) => {
    e.preventDefault();
    // TODO: Implement Editing.
  };

  const handleDelete = (e) => {
    e.preventDefault();

    deleteTodo(todo.id).then(() => {
      dispatch({
        type: DELETE_TODO,
        payload: todo,
      });
    });
  };

  return (
    <tr className="todo-item">
      <td>
        <span className={todo.isCompleted ? 'todo-completed' : ''}>
          {todo.description}
        </span>
      </td>
      <td>
        <div className="buttons">
          <button
            className="icon-button"
            type="button"
            onClick={(e) => handleComplete(e)}
          >
            {todo.isCompleted ? (
              <i className="fa-solid fa-toggle-on"></i>
            ) : (
              <i className="fa-solid fa-toggle-off"></i>
            )}
          </button>
          <button
            className="icon-button"
            type="button"
            onClick={(e) => handleEdit(e)}
          >
            <i className="fa-solid fa-pen-to-square"></i>
          </button>
          <button
            className="icon-button"
            type="button"
            onClick={(e) => handleDelete(e)}
          >
            <i className="fa-solid fa-trash-can"></i>
          </button>
        </div>
      </td>
    </tr>
  );
};

export default TodoItem;
