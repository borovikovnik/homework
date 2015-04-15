CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	[Client] INT NOT NULL,
	[Date] DATE NOT NULL, 
    [Time] TIME NOT NULL, 
	[Flight] INT NOT NULL,
	[ReservedSeats] INT NOT NULL,
	[Cost] INT NOT NULL,
	[State] NCHAR(10) NULL,
		CONSTRAINT [FK_Orders_Flights] FOREIGN KEY ([Flight]) REFERENCES [Flights]([Id]),
		CONSTRAINT [FK_Orders_Clients] FOREIGN KEY ([Client]) REFERENCES [Clients]([Id])
)
