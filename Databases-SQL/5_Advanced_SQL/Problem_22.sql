INSERT INTO Users(FullName, Username, [Password], LastLogin)
	SELECT (e.FirstName + ' ' + e.LastName),
		(Left(e.FirstName, 1) + LOWER(e.LastName)),
		(SUBSTRING(e.FirstName, 0, 2) + LOWER(e.LastName) + '007'),
		(NULL)
	FROM Employees e