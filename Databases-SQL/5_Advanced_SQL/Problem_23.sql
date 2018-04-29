UPDATE Users SET [Password] = NULL
	WHERE DATEDIFF(day, LastLogin, CAST('2010-03-10' AS DATE)) > 0