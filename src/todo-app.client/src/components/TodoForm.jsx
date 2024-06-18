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
    <div>
      <form id="add-task-form" onSubmit={(e) => handleSubmit(e)}>
        <div className="form">
          <input
            id="task-input"
            type="text"
            ref={description}
            className="form-input"
            placeholder="Add a task"
          />
          <button type="submit" className="button icon-button">
            <i className="fa-solid fa-plus"></i>
          </button>
        </div>
      </form>
    </div>
  );
};

export default TodoForm;
