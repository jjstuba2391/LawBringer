CREATE TABLE [dbo].[Lawyer]
(
	[Id] INT IDENTITY NOT NULL,
	[Name] NVARCHAR(100),
	[City] NVARCHAR(100),
	[Price] MONEY,
	[Image] NVARCHAR(100),
	[HelpTypeID] INT, 
	[TimeSlot] DateTime,
	CONSTRAINT FK_Lawyer_HelpType FOREIGN KEY (HelpTypeID) REFERENCES HelpType(ID)
	
)
