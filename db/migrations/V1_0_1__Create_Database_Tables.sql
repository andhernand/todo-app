CREATE TABLE [dbo].[Todo]
(
	[Id]          BIGINT IDENTITY (1, 1) NOT NULL,
	[Description] NVARCHAR(512)          NOT NULL,
	[IsCompleted] BIT                    NOT NULL,
	CONSTRAINT [PK_Todo_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [AK_Todo_Description] UNIQUE ([Description])
);
GO
