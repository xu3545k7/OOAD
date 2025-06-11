import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:5164' // 後端 API URL
});

export const login = (username, password) =>
    api.post('/Account/login', { username, password });

export const register = (username, password, role) =>
    api.post('/Account/register', { username, password, role });
