﻿import { useState } from 'react';

const TodoForm = ({ dispatch }) => {
  const [task, setTask] = useState('');
  const [counter, setCounter] = useState(1);

  const handleSubmit = (e) => {
    e.preventDefault();
    setCounter(counter + 1); // fake an id incrementation. remove when you add calls to the api.
    dispatch({
      type: 'ADD_TODO',
      payload: { id: counter, description: task, isCompleted: false },
    });
    clearForm();
  };

  const clearForm = () => {
    setTask('');
  };

  return (
    <section>
      <form id="add-task-form" onSubmit={(e) => handleSubmit(e)}>
        <div className="form">
          <input
            id="task-input"
            type="text"
            value={task}
            className="form-input"
            placeholder="Add a task"
            onChange={(e) => setTask(e.target.value)}
          />
          <button type="submit" className="button">
            Submit
          </button>
        </div>
      </form>
    </section>
  );
};

export default TodoForm;