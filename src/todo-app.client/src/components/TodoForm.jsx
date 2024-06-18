import { useRef } from 'react';
import { ADD_TODO } from '../reducers/todoReducer';
import { createTodo } from '../services/todoService';

const TodoForm = ({ dispatch }) => {
  const description = useRef(null);

  const handleSubmit = (e) => {
    e.preventDefault();

    createTodo({
      description: description.current.value,
      isCompleted: false,
    }).then((response) => {
      dispatch({
        type: ADD_TODO,
        payload: response.data,
      });
    });

    e.target.reset();
  };

  return (
    <form id="add-task-form" onSubmit={(e) => handleSubmit(e)}>
      <div className="form">
        <input
          id="task-input"
          type="text"
          ref={description}
          className="form-input"
          placeholder="Add a task"
        />
        <button type="submit" className="button">
          <svg className="svg-icon-button" viewBox="0 0 448 512">
            <title>submit</title>
            <path d="M256 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H48c-17.7 0-32 14.3-32 32s14.3 32 32 32H192V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H400c17.7 0 32-14.3 32-32s-14.3-32-32-32H256V80z" />
          </svg>
        </button>
      </div>
    </form>
  );
};

export default TodoForm;
