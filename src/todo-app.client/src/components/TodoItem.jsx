﻿import {
  DELETE_TODO,
  SET_EDITING_TODO,
  UPDATE_TODO,
} from '../reducers/todoReducer';
import { deleteTodo, updateTodo } from '../services/todoService';
import { parseValidationError } from '../utilities/parseValidationErrors';

const TodoItem = ({ todo, dispatch }) => {
  const handleIsCompleted = (e) => {
    e.preventDefault();
    updateTodo(todo.id, { ...todo, isCompleted: !todo.isCompleted })
      .then((response) => {
        if (response.status === 200) {
          dispatch({
            type: UPDATE_TODO,
            payload: response.data,
          });
        }
      })
      .catch((e) => {
        const response = e.response;
        if (response.status === 404 && response.data) {
          setErrors(response.data.errors);
        }
        if (response.status === 400 && response.data) {
          setErrors(response.data.errors);
        }
      });
  };

  const handleEdit = (e) => {
    e.preventDefault();
    dispatch({ type: SET_EDITING_TODO, payload: todo });
  };

  const handleDelete = (e) => {
    e.preventDefault();
    deleteTodo(todo.id)
      .then((response) => {
        if (response.status === 204) {
          dispatch({
            type: DELETE_TODO,
            payload: todo,
          });
        }
      })
      .catch((e) => {
        const response = e.response;
        if (response.status === 404) {
          setErrors(response.data.errors);
        }
      });
  };

  const setErrors = (errors) => {
    const error = parseValidationError(errors);
    console.error(error);
  };

  return (
    <tr className="todo-item">
      <td>
        <span
          className={
            todo.isCompleted ? 'todo-description complete' : 'todo-description'
          }
        >
          {todo.description}
        </span>
      </td>
      <td>
        <div className="buttons">
          <button
            className="todo-icon-button"
            type="button"
            onClick={(e) => handleIsCompleted(e)}
          >
            {todo.isCompleted ? (
              <svg className="svg-icon-button" viewBox="0 0 576 512">
                <title>completed</title>
                <path d="M192 64C86 64 0 150 0 256S86 448 192 448H384c106 0 192-86 192-192s-86-192-192-192H192zm192 96a96 96 0 1 1 0 192 96 96 0 1 1 0-192z" />
              </svg>
            ) : (
              <svg className="svg-icon-button" viewBox="0 0 576 512">
                <title>incomplete</title>
                <path d="M384 128c70.7 0 128 57.3 128 128s-57.3 128-128 128H192c-70.7 0-128-57.3-128-128s57.3-128 128-128H384zM576 256c0-106-86-192-192-192H192C86 64 0 150 0 256S86 448 192 448H384c106 0 192-86 192-192zM192 352a96 96 0 1 0 0-192 96 96 0 1 0 0 192z" />
              </svg>
            )}
          </button>
          <button
            className="todo-icon-button"
            type="button"
            onClick={(e) => handleEdit(e)}
          >
            <svg className="svg-icon-button" viewBox="0 0 512 512">
              <title>edit</title>
              <path d="M471.6 21.7c-21.9-21.9-57.3-21.9-79.2 0L362.3 51.7l97.9 97.9 30.1-30.1c21.9-21.9 21.9-57.3 0-79.2L471.6 21.7zm-299.2 220c-6.1 6.1-10.8 13.6-13.5 21.9l-29.6 88.8c-2.9 8.6-.6 18.1 5.8 24.6s15.9 8.7 24.6 5.8l88.8-29.6c8.2-2.7 15.7-7.4 21.9-13.5L437.7 172.3 339.7 74.3 172.4 241.7zM96 64C43 64 0 107 0 160V416c0 53 43 96 96 96H352c53 0 96-43 96-96V320c0-17.7-14.3-32-32-32s-32 14.3-32 32v96c0 17.7-14.3 32-32 32H96c-17.7 0-32-14.3-32-32V160c0-17.7 14.3-32 32-32h96c17.7 0 32-14.3 32-32s-14.3-32-32-32H96z" />
            </svg>
          </button>
          <button
            className="todo-icon-button"
            type="button"
            onClick={(e) => handleDelete(e)}
          >
            <svg className="svg-icon-button" viewBox="0 0 448 512">
              <title>delete</title>
              <path d="M135.2 17.7C140.6 6.8 151.7 0 163.8 0H284.2c12.1 0 23.2 6.8 28.6 17.7L320 32h96c17.7 0 32 14.3 32 32s-14.3 32-32 32H32C14.3 96 0 81.7 0 64S14.3 32 32 32h96l7.2-14.3zM32 128H416V448c0 35.3-28.7 64-64 64H96c-35.3 0-64-28.7-64-64V128zm96 64c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16z" />
            </svg>
          </button>
        </div>
      </td>
    </tr>
  );
};

export default TodoItem;
