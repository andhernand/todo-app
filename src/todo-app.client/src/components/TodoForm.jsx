import { useState, useRef } from 'react';

const TodoForm = ({ dispatch }) => {
  const description = useRef(null);
  const [counter, setCounter] = useState(1);

  const handleSubmit = (e) => {
    e.preventDefault();
    setCounter(counter + 1); // fake an id incrementation. remove when you add calls to the api.
    dispatch({
      type: 'ADD_TODO',
      payload: {
        id: counter,
        description: description.current.value,
        isCompleted: false,
      },
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
            <i className="fas fa-plus"></i>
          </button>
        </div>
      </form>
    </div>
  );
};

export default TodoForm;
