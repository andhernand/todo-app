# todo-app

A Todo application developed using a minimal API for the backend and [React](https://react.dev) + [Vite](https://vitejs.dev/) for the frontend.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Node.js](https://nodejs.org)

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/andhernand/todo-app.git
cd todo-app
```

### Restore, Build, and Run

```bash
dotnet restore todo-app.sln
dotnet build todo-app.sln --no-restore
dotnet run --project src/todo-app.server/todo-app.server.csproj
```

### Integration Testing

Integration tests utilize [Testcontainers](https://dotnet.testcontainers.org/). To run the tests, use the following command:

```bash
dotnet test todo-app.sln
```

## Contributing

Contributions are welcome! Please fork this repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
