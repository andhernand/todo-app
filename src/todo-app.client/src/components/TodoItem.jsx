const TodoItem = ({ todo }) => {
  const { id, description, isCompleted } = todo;

  const handleChange = (e) => {
    e.preventDefault();
  };

  return (
    <li>
      <input
        id={`${id}-complete`}
        type="checkbox"
        checked={isCompleted}
        onChange={(e) => handleChange(e)}
      />
      <span>{description}</span>
    </li>
  );
};

export default TodoItem;
