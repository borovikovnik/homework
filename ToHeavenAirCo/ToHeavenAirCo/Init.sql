insert into dbo.[Clients] 
([Name],		[Phone],	[CreditCard],	[Pasport]) values 
(N'Иванов',		'12345678', N'1684MMM',		'1234567OP'),
(N'Петров',		'98761',	'',				'dfsdff54'),
(N'Сидоров',	'654489',	'3456789',		'3456789@.'),
(N'Diffrouxe',	'498488',	'00000000',		'4567890'),
(N'Иванова',	'',			'9f9w9e8f',		'55efw988')

GO

insert into dbo.[Flights]
([Date],		[Time],		[Departure],	[Arrival],	[Plane]) values 
('2044-12-01',	'14:17:00', 'Point A',		'Point B',	'1231123-g'),
('1910-02-15',	'15:17:00', 'Mars',			'Earth',	'2123233-c'),
('2000-10-10',	'16:17:00', 'Dirt',			'Diamonds',	'4345532-c'),
('2000-02-14',	'17:17:00', 'Mac',			'Windows',	'4563523-c'),
('2015-04-12',	'01:17:00', 'Barselona',	'Ukraine',	'4332555-p')

GO

insert into dbo.[Orders]
([Client],		[Date],			[Time],		[Flight],		[ReservedSeats],	[Cost],	[State]) values 
(N'Петров',		'2044-12-01',	'14:17:00',	'1231123-g',	'1',				'300',	'Pending'),
(N'Иванов',		'1910-02-15',	'15:17:00',	'2123233-c',	'1',				'200',	'Pending'),
(N'Сидоров',	'2000-10-10',	'16:17:00',	'4345532-c',	'42',				'9930', 'Complite')

GO

insert into dbo.[Planes]
([Type],		[FreePlase],	[Pilot],		[FTS]) values 
(N'Кукурузник',	'15',			N'Степанов',	FALSE),
(N'Ту-54',		'43',			N'Нечаев',		FALSE),
('TARDIS',		'9999',			N'Доктор',		TRUE),
('Nighthawk',	'1',			N'Смитт',		TRUE)
