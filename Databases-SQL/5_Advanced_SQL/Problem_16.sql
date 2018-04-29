CREATE VIEW LogginsToday
AS SELECT * FROM Users u
WHERE CONVERT(nvarchar(20), u.LastLogin,104) = 
	CONVERT(nvarchar(20), GETDATE(),104)
GO
