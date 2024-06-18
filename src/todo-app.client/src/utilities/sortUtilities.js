export const sortTodos = (todos) => {
  return [...todos].sort((a, b) => {
    return a.isCompleted === b.isCompleted ? 0 : a.isCompleted ? 1 : -1;
  });
};
