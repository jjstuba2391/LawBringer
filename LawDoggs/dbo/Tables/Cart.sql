CREATE TABLE [dbo].[Cart]
(
	[ID] uniqueidentifier DEFAULT newid(),
	[AspNetUserId] NVARCHAR(128) NULL,
	[DateCreated] DATETIME NOT NULL DEFAULT GetDate(), 
    [DateLastModified] DATETIME NOT NULL DEFAULT GetDate(), 
    [HelpTypeID] INT NOT NULL, 
    [Day] DATETIME NOT NULL, 
    [LawyerID] INT NOT NULL, 
    CONSTRAINT [PK_Cart] PRIMARY KEY ([ID]), 
    CONSTRAINT [FK_Cart_HelpType] FOREIGN KEY (HelpTypeID) REFERENCES HelpType(ID), 
    CONSTRAINT [FK_Cart_Lawyer] FOREIGN KEY (LawyerID) REFERENCES Lawyer(ID)
	
)
