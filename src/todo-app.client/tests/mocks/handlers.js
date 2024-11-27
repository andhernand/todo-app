import { http, HttpResponse } from 'msw';

export const handlers = [
  http.get('/api/v1/todos', () => {
    return HttpResponse.json(
      [
        { id: 1, description: 'take out the trash', isCompleted: false },
        { id: 2, description: 'do the laundry', isCompleted: false },
        { id: 3, description: 'clean bathroom', isCompleted: false },
      ],
      { status: 200 },
    );
  }),
];
