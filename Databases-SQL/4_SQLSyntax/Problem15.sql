SELECT 
	EmployeeID,
	COALESCE 
	(
		FirstName + ' ' + MiddleName + ' ' + LastName,
		FirstName + ' ' + LastName
	) AS [Full Name],
	JobTitle
FROM Employees
WHERE ManagerID IS NULL
ORDER BY FirstName