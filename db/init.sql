IF (DB_ID(N'TodoApiDb') IS NULL)
	BEGIN
		PRINT 'Creating the database.';
		CREATE DATABASE [TodoApiDb];
	END
ELSE
	BEGIN
		PRINT 'Database already exists.';
	END
GO

IF NOT EXISTS(SELECT 1
			  FROM [master].[sys].[server_principals]
			  WHERE [name] = N'todoapi'
				AND [type] IN ('C', 'E', 'G', 'K', 'S', 'U'))
	BEGIN
		PRINT 'Creating the login.';
		CREATE LOGIN [todoapi] WITH PASSWORD = N'$(varApiPassword)', DEFAULT_DATABASE = [TodoApiDb];
	END
ELSE
	BEGIN
		PRINT 'Login already exists.';
	END
GO
