import { describe, expect, it } from 'vitest';
import { render, screen, waitFor } from '@testing-library/react';
import App from '../src/App';

describe('App', () => {
  it('renders header', async () => {
    render(<App />);
    await waitFor(() => screen.getByText(/clean bathroom/i));

    const heading = screen.getByText(/Tasks/i);
    expect(heading).toBeInTheDocument();
  });

  it('renders form', async () => {
    render(<App />);
    await waitFor(() => screen.getByText(/clean bathroom/i));

    const input = screen.getByPlaceholderText(/Add a task/i);
    expect(input).toBeInTheDocument();
  });

  it('renders todo', async () => {
    render(<App />);
    await waitFor(() => screen.getByText(/clean bathroom/i));

    const todo = screen.getByText(/take out the trash/i);
    expect(todo).toBeInTheDocument();
  });
});
