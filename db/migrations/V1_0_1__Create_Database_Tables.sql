CREATE TABLE [dbo].[Todo]
(
	[Id]          BIGINT IDENTITY (1, 1) NOT NULL,
	[Description] NVARCHAR(64)           NOT NULL,
	[IsCompleted] BIT                    NOT NULL,
	CONSTRAINT [PK_Todo_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
