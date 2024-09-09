import { useEffect, useState } from 'react';
import {
  ADD_TODO,
  UPDATE_TODO,
  CLEAR_EDITING_TODO,
} from '../reducers/todoReducer';
import { createTodo, updateTodo } from '../services/todoService';
import { parseValidationError } from '../utilities/parseValidationErrors';

const TodoForm = ({ dispatch, editingTodo }) => {
  const [task, setTask] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  useEffect(() => {
    if (editingTodo) {
      setTask(editingTodo.description);
    } else {
      clearForm();
    }
  }, [editingTodo]);

  const handleSubmit = (e) => {
    e.preventDefault();

    if (editingTodo) {
      const todo = {
        id: editingTodo.id,
        description: task,
        isCompleted: editingTodo.isCompleted,
      };

      updateTodo(editingTodo.id, todo)
        .then((response) => {
          if (response.status === 200) {
            dispatch({
              type: UPDATE_TODO,
              payload: response.data,
            });
            clearForm();
          }
        })
        .catch((error) => {
          const response = error.response;
          if (response.status === 404 && response.data) {
            setErrors(response.data.errors);
          }
          if (response.status === 400 && response.data) {
            setErrors(response.data.errors);
          }
        });
    } else {
      const todo = { description: task, isCompleted: false };

      createTodo(todo)
        .then((response) => {
          if (response.status === 201) {
            const created = response.data;
            dispatch({
              type: ADD_TODO,
              payload: created,
            });
            clearForm();
          }
        })
        .catch((e) => {
          const response = e.response;
          if (response.status === 400 && response.data) {
            setErrors(response.data.errors);
          }
        });
    }
  };

  const handleCancel = (e) => {
    e.preventDefault();
    dispatch({ type: CLEAR_EDITING_TODO });
    clearForm();
  };

  const clearForm = () => {
    setTask('');
    setErrorMessage('');
  };

  const handleCloseAlert = (e) => {
    e.preventDefault();
    setErrorMessage('');
    if (!editingTodo) {
      setTask('');
    }
  };

  const setErrors = (errors) => {
    const error = parseValidationError(errors);
    setErrorMessage(error);
  };

  return (
    <form id="add-task-form" onSubmit={(e) => handleSubmit(e)}>
      <div className="form">
        <input
          id="task-input"
          type="text"
          className="form-input"
          placeholder="Add a task"
          value={task}
          onChange={(e) => setTask(e.target.value)}
        />
        <div className="buttons">
          <button type="submit" className="button">
            <svg className="svg-icon-button" viewBox="0 0 448 512">
              <title>submit</title>
              <path d="M256 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H48c-17.7 0-32 14.3-32 32s14.3 32 32 32H192V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H400c17.7 0 32-14.3 32-32s-14.3-32-32-32H256V80z" />
            </svg>
          </button>
          {editingTodo && (
            <button
              type="button"
              className="button"
              onClick={(e) => handleCancel(e)}
            >
              <svg className="svg-icon-button" viewBox="0 0 512 512">
                <title>cancel</title>
                <path d="M256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z" />
              </svg>
            </button>
          )}
        </div>
      </div>
      {errorMessage && (
        <div className="alert">
          <span className="close-btn" onClick={(e) => handleCloseAlert(e)}>
            &times;
          </span>
          {errorMessage}
        </div>
      )}
    </form>
  );
};

export default TodoForm;
