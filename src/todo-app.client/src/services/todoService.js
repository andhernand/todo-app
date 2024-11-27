import axios from 'axios';

const client = axios.create();

export const getAllTodos = () => client.get('/api/v1/todos');
export const createTodo = (todo) => client.post('/api/v1/todos', todo);
export const updateTodo = (id, todo) => client.put(`/api/v1/todos/${id}`, todo);
export const deleteTodo = (id) => client.delete(`/api/v1/todos/${id}`);
