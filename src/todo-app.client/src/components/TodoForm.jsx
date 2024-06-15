import { useState } from 'react';

const TodoForm = () => {
  const [task, setTask] = useState('');
  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(`Your new task: ${task}`);
    clearForm();
  };

  const clearForm = () => {
    setTask('');
  };

  return (
    <section className="addTask">
      <form id="addTaskForm" onSubmit={(e) => handleSubmit(e)}>
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
      </form>
    </section>
  );
};

export default TodoForm;
