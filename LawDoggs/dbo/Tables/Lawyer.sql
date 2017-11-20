CREATE TABLE [dbo].[Lawyer]
(
	[Id] INT IDENTITY NOT NULL,
	[Name] NVARCHAR(100),
	[City] NVARCHAR(100),
	[Price] MONEY,
	[Image] NVARCHAR(100),
    CONSTRAINT [PK_Lawyer] PRIMARY KEY ([Id])
	
)
