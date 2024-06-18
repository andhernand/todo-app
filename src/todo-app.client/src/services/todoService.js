import axios from 'axios';

const client = axios.create();

export const getAllTodos = () => client.get('/api/todos');
export const createTodo = (todo) => client.post('/api/todos', todo);
export const updateTodo = (id, todo) => client.put(`/api/todos/${id}`, todo);
export const deleteTodo = (id) => client.delete(`/api/todos/${id}`);
