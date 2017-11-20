CREATE TABLE [dbo].[Order] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [TrackingNumber]     CHAR (8)        DEFAULT (left(newid(),(8))) NOT NULL,
    [Email]              NVARCHAR (512)  NOT NULL,
    [CustomerName]       NVARCHAR (512)  NOT NULL,
    [ShippingAddress1]   NVARCHAR (1000) NOT NULL,
    [ShippingCity]       NVARCHAR (1000) NOT NULL,
    [ShippingState]      NVARCHAR (100)  NOT NULL,
    [ShippingPostalCode] NVARCHAR (10)   NOT NULL,
    [Total]              MONEY           NOT NULL,
    [ServiceCharge]      MONEY           NOT NULL,
    [Tax]                MONEY           NOT NULL,
    [DateCreated]        DATETIME        DEFAULT (getdate()) NOT NULL,
    [DateLastModified]   DATETIME        DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC),
	
    [HelpTypeID] INT NOT NULL, 
    [Day] DATETIME NOT NULL, 
    [LawyerID] INT NOT NULL, 
	
    CONSTRAINT [FK_Order_HelpType] FOREIGN KEY (HelpTypeID) REFERENCES HelpType(ID), 
    CONSTRAINT [FK_Order_Lawyer] FOREIGN KEY (LawyerID) REFERENCES Lawyer(ID)
);


