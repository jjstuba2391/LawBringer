CREATE TABLE [dbo].[LawyerHelpType]
(
	LawyerID INT NOT NULL,
	HelpTypeId INT NOT NULL, 
    CONSTRAINT [PK_LawyerHelpType] PRIMARY KEY ([LawyerID], [HelpTypeId]), 
    CONSTRAINT [FK_LawyerHelpType_Lawyer] FOREIGN KEY (LawyerID) REFERENCES Lawyer(ID), 
    CONSTRAINT [FK_LawyerHelpType_HelpType] FOREIGN KEY (HelpTypeID) REFERENCES HelpType(ID),
)
