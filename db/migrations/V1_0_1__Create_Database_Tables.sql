CREATE TABLE [dbo].[Todo]
(
	[Id]                BIGINT IDENTITY (1, 1) NOT NULL,
	[Description]       NVARCHAR(512)          NOT NULL,
	[Description_Lower] AS LOWER([Description]),
	[IsCompleted]       BIT                    NOT NULL,
	CONSTRAINT [PK_Todo_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [AK_Todo_Description] UNIQUE ([Description])
);
GO

CREATE INDEX IX_Todo_Description_Lower ON [dbo].[Todo] ([Description_Lower]);
GO
