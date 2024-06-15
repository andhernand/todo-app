import axios from 'axios';

const api = axios.create();

const getTodos = () => api.get('/api/todos');

export { getTodos };
