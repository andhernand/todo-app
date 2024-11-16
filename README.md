# todo-app

This project is a simple Todo application that allows users to manage their tasks efficiently. The backend is built with a minimal API using [.NET](https://dotnet.microsoft.com), while the frontend is developed with [React](https://react.dev) and [Vite](https://vitejs.dev/).

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Node.js](https://nodejs.org)

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/andhernand/todo-app.git
cd todo-app
```

### Setup Environment Variables

Create a .env file in the root directory of the project. This file contains the required environment variables for building and running the application and tests.

**NOTE:** The **TODO_API_USER_NAME** variable must remain as shown because it is hardcoded in the `init.sql` script.

```text
TODO_API_SA_PASSWORD=YOURSTRONGSAPASSWORDHERE
TODO_API_USER_NAME="todoapi"
TODO_API_USER_PASSWORD=YOURSTRONGUSERPASSWORDHERE
```

### Start MSSQL Server with Docker

Make sure Docker Desktop is running, then execute the following command to start the MSSQL server container:

```bash
docker-compose up -d
```

**Note:** Flyway by Redgate is used to manage and execute database migrations in this project.

### Restore, Build, and Run

```bash
dotnet restore todo-app.sln
dotnet build todo-app.sln --no-restore
dotnet run --project src/todo-app.server/todo-app.server.csproj
```

### Database Access with Dapper

This project uses [Dapper](https://github.com/DapperLib/Dapper) for database access.

### Input Validation with FluentValidation

[FluentValidation](https://docs.fluentvalidation.net) is used to handle input validation.

### Integration Testing

Integration tests utilize [Testcontainers](https://dotnet.testcontainers.org/) to create MSSQL databases for all integration tests. To run the tests, use the following command:

```bash
dotnet test todo-app.sln
```

## Contributing

Contributions are welcome! Please fork this repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
