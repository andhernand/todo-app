CREATE USER [todoapi] FOR LOGIN [todoapi] WITH DEFAULT_SCHEMA = [dbo];
GO

CREATE ROLE [todoapipermissions];
GO

ALTER ROLE [todoapipermissions] ADD MEMBER [todoapi];
GO

GRANT SELECT, UPDATE, INSERT, DELETE, EXECUTE ON SCHEMA::DBO TO [todoapipermissions];
GO
