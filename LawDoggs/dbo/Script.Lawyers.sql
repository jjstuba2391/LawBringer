/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO HelpType (Description, StandardPrice) VALUES
--('Divorce', 500), 
--('Traffic Violation', 100), 
--('Criminal', 250),
('Corporate',300)

INSERT INTO Lawyer (Name, City, Price, Image)
VALUES --('Frank', 'Chicago', 100, '/Images/Lawyer1.jpg'),
--('Tom', 'Chicago', 80, '/Images/Lawyer2.jpg'),
--('Dave', 'Chicago', 100,'/Images/Lawyer4.jpg'),
--('Katie', 'Ney York City', 70, '/Images/Lawyer3.jpg'),
('Saul', 'Chicago', 500,'/Images/Lawyer5.jpg')


INSERT INTO LawyerTimeSlot(LawyerID,DateTime)
VALUES (3002,'12/13/2017'),(3002,'12/14/2017'),(3002,'12/15/2017'),(3002,'12/16/2017'),(3002,'12/17/2017'),(3002,'12/18/2017'),(3002,'12/19/2017'),(3002,'12/20/2017'),(3002,'12/21/2017'),(3002,'12/22/2017')
--(1,'12/13/2017'),(1,'12/14/2017'),(1,'12/16/2017'),(1,'12/17/2017'),(1,'12/18/2017'),(1,'12/22/2017'),(1,'12/25/2017'),(2,'12/13/2017'),(2,'12/14/2017'),(2,'12/16/2017'),(2,'12/17/2017'),(2,'12/18/2017'),(2,'12/22/2017'),(2,'12/25/2017'),
--(2002,'12/13/2017'),(2002,'12/14/2017'),(2002,'12/16/2017'),(2002,'12/17/2017'),(2002,'12/18/2017'),(2002,'12/22/2017'),(2002,'12/25/2017'),(2005,'12/13/2017'),(2005,'12/14/2017'),(2005,'12/16/2017'),(2005,'12/17/2017'),(2005,'12/18/2017'),(2005,'12/22/2017'),(2005,'12/25/2017')


DECLARE @lawyerID INT
--SET @lawyerID = (SELECT TOP 1 ID FROM Lawyer WHERE Name = 'Frank')
SET @lawyerID = (SELECT TOP 1 ID FROM Lawyer WHERE Name = 'Saul')
DECLARE @helpTypeID INT 
--SET @helpTypeID = (SELECT TOP 1 ID FROM HelpType WHERE [Description] LIKE 'Divorce')
--SET @helpTypeID = (SELECT TOP 1 ID FROM HelpType WHERE [Description] LIKE 'Corporate')

--SET @helpTypeID = (SELECT TOP 1 ID FROM HelpType WHERE [Description] LIKE 'Criminal')

INSERT INTO LawyerHelpType(LawyerID, HelpTypeId)
VALUES(@lawyerID, @helpTypeID)