SELECT TOP 5
	EmployeeID,
	COALESCE 
	(
		FirstName + ' ' + MiddleName + ' ' + LastName,
		FirstName + ' ' + LastName
	) AS [Full Name],
	JobTitle,
	Salary
FROM Employees
ORDER BY Salary DESC