import axios from 'axios';
import MockAdapter from 'axios-mock-adapter';

export const apiClient = axios.create({ baseURL: 'https://api.biggest-app.local' });

// Development-time mocks
const mock = new MockAdapter(apiClient, { delayResponse: 400 });

mock.onGet('/ping').reply(200, { message: 'pong' });

mock.onGet('/items').reply(200, Array.from({ length: 15 }, (_, i) => ({ id: String(i + 1), title: `Item #${i + 1}` })));

mock.onAny().passThrough();