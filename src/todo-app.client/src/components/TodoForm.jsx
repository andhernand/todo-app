import { useRef, useEffect } from 'react';
import {
  ADD_TODO,
  UPDATE_TODO,
  CLEAR_EDITING_TODO,
} from '../reducers/todoReducer';
import { createTodo, updateTodo } from '../services/todoService';

const TodoForm = ({ dispatch, editingTodo }) => {
  const description = useRef(null);

  useEffect(() => {
    if (editingTodo) {
      description.current.value = editingTodo.description;
    } else {
      description.current.value = '';
    }
  }, [editingTodo]);

  const handleSubmit = (e) => {
    e.preventDefault();

    if (editingTodo) {
      updateTodo(editingTodo.id, {
        description: description.current.value,
        isCompleted: editingTodo.isCompleted,
      }).then((response) => {
        dispatch({
          type: UPDATE_TODO,
          payload: response.data,
        });
      });
    } else {
      createTodo({
        description: description.current.value,
        isCompleted: false,
      })
        .then((todo) => {
          dispatch({
            type: ADD_TODO,
            payload: todo,
          });
        })
        .catch((e) => {
          const errors = parseErrors(e.response.data.errors);
          const errorMessage = `${e.response.data.title}\n${errors}`;
          alert(errorMessage);
        });
    }

    e.target.reset();
  };

  const parseErrors = (errors) => {
    let result = '';
    for (const key in errors) {
      if (Object.prototype.hasOwnProperty.call(errors, key)) {
        const messages = errors[key];
        messages.forEach((message) => {
          result += `${key}: ${message}\n`;
        });
      }
    }
    return result.trim(); // Remove the trailing newline character
  };

  const handleCancel = () => {
    dispatch({ type: CLEAR_EDITING_TODO });
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
    </form>
  );
};

export default TodoForm;
