import { useState, useEffect } from 'react';
import { getTodos } from '@/services/todoService.jsx';

const useGetTodos = (initialData) => {
  const [todos, setTodos] = useState(initialData);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(false);

  useEffect(() => {
    setLoading(true);
    const response = getTodos();

    response
      .then((resp) => {
        return resp.data;
      })
      .then((data) => {
        setTodos(data.todos);
      })
      .catch(() => {
        setTodos([]);
        setError(true);
      })
      .finally(() => setLoading(false));
  }, []);

  return { todos, loading, error };
};

export default useGetTodos;
