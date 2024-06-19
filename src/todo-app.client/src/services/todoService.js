import axios from 'axios';

const client = axios.create();

export const getAllTodos = () => {
  return client.get('/api/todos').then((response) => {
    if (response.status === 200) {
      return response.data.todos;
    } else {
      throw new Error(response.statusText);
    }
  });
};

export const createTodo = (todo) => {
  return client.post('/api/todos', todo).then((response) => {
    return response.data;
  });
};

export const updateTodo = (id, todo) => client.put(`/api/todos/${id}`, todo);
export const deleteTodo = (id) => client.delete(`/api/todos/${id}`);
