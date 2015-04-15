--Примеры с простыми запросами:
-- 1.	Выбрать всю информацию обо всех самолетах.
SELECT * FROM [Planes]
-- 2.	Выбрать самолеты типа Boeing.
SELECT * FROM [Planes] WHERE [Type] = N'Boing'
-- 3.	Выбрать номера самолетов, у которых число мест >=100 и упорядочить их по количеству мест.
SELECT [Type] FROM [Planes] WHERE [FreePlace] >= 100 ORDER BY [FreePlace] 
-- 4.	Выбрать номера и экипаж всех быстрых (быстрее скорости звука) самолетов, у которых в номере встречается число 19. Список упорядочить по номеру самолета.
SELECT [Type], [Pilot] FROM [Planes] WHERE [FTS] = 1 AND (CHARINDEX(N'19', [Id]) != 0) ORDER BY [Id] 
-- 5.	Выдать номера рейсов, фамилию заказчика и время отлета, улетевших с 11:00 до 23:00 1 июля 2014 года.
SELECT [Fights].[Id], [Clients].[Name], [Flights].[Date] FROM [Flights], [Clients] WHERE
	(DATEPART(HOUR, [Orders].[Time]) <= 23) AND
	(DATEPART(HOUR, [Orders].[Time]) >= 11) AND
	([Orders].[Date] = '2014-07-01')  
--Примеры со сложными запросами:
-- 1.	Посчитать общую сумму оплаты по всем выполненным заказам
SELECT SUM([Cost]) FROM [Orders] WHERE [Orders].[State] = N'closed' OR [Orders].[State] = N'returned'
-- 2.	Получить список экипажей, отсортированный по количеству выполненных заказов
SELECT [Planes].[Pilot] FROM [Planes], [Flights], [Orders] WHERE
	[Flights].[Plane] = [Planes].[Id] AND
	[Orders].[Flight] = [Flights].[Id] AND
	[Orders].[State] = N'closed'
	GROUP BY [Planes].[Id]
	ORDER BY COUNT([Planes].[Id])
-- 3.	Выбрать постоянных клиентов (не менее 5 выполненных заказов)
SELECT [Clients].[Id], [Clients].[Name] FROM [Clients], [Orders] WHERE
	[Orders].Client = [Clients].[Id] AND [Orders].[State] = N'closed'
	GROUP BY [Clients].[Id]
	HAVING COUNT([Clients].[Id]) >= 5
--Примеры на редактирование:
-- 1.	Удалить клиента Попова.
DELETE FROM [Clients] WHERE [Clients].[Name] = N'Попов'
-- 2.	Удалить клиента Гейтса и все его заказы.
DELETE FROM [Orders] 
	WHERE [Orders].[Client] IN
		(SELECT [Clients].[Id] FROM [Clients] WHERE [Clients].[Name] = N'Гейтс')
DELETE FROM [Clients] WHERE [Clients].[Name] = N'Гейтс'
-- 3.	Заменить номер самолета ‘121212’ на ‘521212’.
UPDATE [Planes]
	SET [Planes].[Id] = N'521212'
	WHERE [Planes].[Id] = N'121212'