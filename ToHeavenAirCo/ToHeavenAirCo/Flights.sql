CREATE TABLE [dbo].[Flights]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Date] DATE NOT NULL, 
    [Time] TIME NOT NULL, 
	[Departure] NCHAR(20) NOT NULL, 
	[Arrival] NCHAR(20) NOT NULL, 
	[Plane] INT NOT NULL,
		CONSTRAINT [FK_Flights_Planes] FOREIGN KEY ([Plane]) REFERENCES [Planes]([Id])
)
