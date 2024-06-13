CREATE PROCEDURE [dbo].[usp_Todo_Insert](
	@Description NVARCHAR(512),
	@IsCompleted BIT,
	@TodoId BIGINT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;

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
	SET NOCOUNT ON;

	-- Validate @GolferId
	IF @Id IS NULL OR @Id <= 0
		BEGIN
			THROW 50002, 'The Id parameter must have a positive value.', 1;
		END

	SELECT t.[Id],
		   t.[Description],
		   t.[IsCompleted]
	FROM [dbo].[Todo] t
	WHERE t.[Id] = @Id;
END
GO

CREATE PROCEDURE [dbo].[usp_Todo_GetByDescription](
	@Description NVARCHAR(512)
)
AS
BEGIN
	SET NOCOUNT ON;

	-- Validate @Description
	IF @Description IS NULL OR LEN(@Description) <= 0
		BEGIN
			THROW 50003, 'The Description parameter must have a value.', 1;
		END

	DECLARE @Description_Lower NVARCHAR(512) = LOWER(@Description);

	SELECT t.[Id],
		   t.[Description],
		   t.[IsCompleted]
	FROM [dbo].[Todo] t
	WHERE t.[Description_Lower] LIKE '%' + @Description_Lower + '%';
END
GO

CREATE PROCEDURE [dbo].[usp_Todo_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT t.[Id],
		   t.[Description],
		   t.[IsCompleted]
	FROM [dbo].[Todo] t;
END
GO

CREATE PROCEDURE [dbo].[usp_Todo_Update](
	@Id BIGINT,
	@Description NVARCHAR(512),
	@IsCompleted BIT,
	@RowCount INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;

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

	IF EXISTS (SELECT 1 FROM [dbo].[Todo] WHERE [Id] = @Id)
		BEGIN
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
	ELSE
		BEGIN
			SET @RowCount = 0;
		END
END
GO

CREATE PROCEDURE [dbo].[usp_Todo_Delete](
	@Id BIGINT,
	@RowCount INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;

	-- Validate @Id
	IF @Id IS NULL OR @Id <= 0
		BEGIN
			THROW 50007, 'The Id parameter must have a positive value.', 1;
		END

	IF EXISTS (SELECT 1 FROM [dbo].[Todo] WHERE [Id] = @Id)
		BEGIN
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
	ELSE
		BEGIN
			SET @RowCount = 0;
		END
END
GO

CREATE PROC [dbo].[usp_Todo_ExistsById](
	@Id BIGINT
)
AS
BEGIN
	SET NOCOUNT ON;

	-- Validate @Id
	IF @Id IS NULL OR @Id <= 0
		BEGIN
			THROW 50008, 'The Id parameter must have a positive value.', 1;
		END

	SELECT COUNT(1)
	FROM [dbo].[Todo]
	WHERE [Id] = @Id;
END
GO
