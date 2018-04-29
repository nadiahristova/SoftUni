SELECT EmployeeID, 
	COALESCE 
	(
		FirstName + ' ' + MiddleName + ' ' + LastName,
		FirstName + ' ' + LastName
	) AS [Full Name],
	FirstName + '.' + LastName + '@softuni.bg' AS [Full Email Address]
FROM Employees
ORDER BY FirstName