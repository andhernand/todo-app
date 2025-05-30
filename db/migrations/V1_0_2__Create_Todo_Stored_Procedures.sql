﻿CREATE PROCEDURE [dbo].[usp_Todo_Insert](
	@Description NVARCHAR(64),
	@IsCompleted BIT,
	@TodoId BIGINT OUTPUT
)
AS
BEGIN
	-- Validate @Description
	IF @Description IS NULL OR LEN(@Description) <= 0
		BEGIN
			THROW 50000, 'The Description parameter must have a value.', 1;
		END

	-- Validate @IsCompleted
	IF @IsCompleted IS NULL
		BEGIN
			THROW 50001, 'The IsCompleted parameter must have a value.', 1;
		END

	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO [dbo].[Todo] ([Description], [IsCompleted])
			VALUES (@Description, @IsCompleted);

			SELECT @TodoId = SCOPE_IDENTITY();
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		THROW
	END CATCH
END
GO

CREATE PROCEDURE [dbo].[usp_Todo_GetById](
	@Id BIGINT
)
AS
BEGIN
	-- Validate @GolferId
	IF @Id IS NULL OR @Id <= 0
		BEGIN
			THROW 50002, 'The Id parameter must have a positive value.', 1;
		END

	SET NOCOUNT ON;

	SELECT T.[Id],
		   T.[Description],
		   T.[IsCompleted]
	FROM [dbo].[Todo] T
	WHERE T.[Id] = @Id;
END
GO

CREATE PROCEDURE [dbo].[usp_Todo_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT T.[Id],
		   T.[Description],
		   T.[IsCompleted]
	FROM [dbo].[Todo] T;
END
GO

CREATE PROCEDURE [dbo].[usp_Todo_Update](
	@Id BIGINT,
	@Description NVARCHAR(64),
	@IsCompleted BIT,
	@RowCount INT OUTPUT
)
AS
BEGIN
	-- Validate @Id
	IF @Id IS NULL OR @Id <= 0
		BEGIN
			THROW 50004, 'The Id parameter must have a positive value.', 1;
		END

	-- Validate @Description
	IF @Description IS NULL OR LEN(@Description) <= 0
		BEGIN
			THROW 50005, 'The Description parameter must have a value.', 1;
		END

	-- Validate @IsCompleted
	IF @IsCompleted IS NULL
		BEGIN
			THROW 50006, 'The IsCompleted parameter must have a value.', 1;
		END

	SET NOCOUNT ON;
	SET @RowCount = 0;

	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE [dbo].[Todo]
			SET [Description] = @Description,
				[IsCompleted] = @IsCompleted
			WHERE [Id] = @Id;

			SET @RowCount = @@ROWCOUNT;
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		THROW
	END CATCH
END
GO

CREATE PROCEDURE [dbo].[usp_Todo_Delete](
	@Id BIGINT,
	@RowCount INT OUTPUT
)
AS
BEGIN
	-- Validate @Id
	IF @Id IS NULL OR @Id <= 0
		BEGIN
			THROW 50007, 'The Id parameter must have a positive value.', 1;
		END

	SET NOCOUNT ON;
	SET @RowCount = 0;

	BEGIN TRY
		BEGIN TRANSACTION
			DELETE
			FROM [dbo].[Todo]
			WHERE [Id] = @Id;

			SET @RowCount = @@ROWCOUNT;
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		THROW
	END CATCH
END
GO
