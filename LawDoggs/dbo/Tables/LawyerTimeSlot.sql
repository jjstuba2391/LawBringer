CREATE TABLE [dbo].[LawyerTimeSlot]
(
	LawyerID INT NOT NULL,
	[DateTime] DATETIME NOT NULL, 
    CONSTRAINT [PK_LawyerTimeSlot] PRIMARY KEY ([LawyerID], [DateTime]), 
    CONSTRAINT [FK_LawyerTimeSlot_Lawyer] FOREIGN KEY (LawyerID) REFERENCES Lawyer(ID) 
	
)
