CREATE TABLE [dbo].[Clients]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [Name] NCHAR(20) NOT NULL, 
    [Phone] NCHAR(10) NOT NULL, 
    [CreditCard] NCHAR(10) NOT NULL,
	[Pasport] NCHAR(20) NOT NULL
)