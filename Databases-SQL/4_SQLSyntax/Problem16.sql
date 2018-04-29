SELECT 
	EmployeeID,
	COALESCE 
	(
		FirstName + ' ' + MiddleName + ' ' + LastName,
		FirstName + ' ' + LastName
	) AS [Full Name],
	Salary
FROM Employees
WHERE Salary >= 50000
ORDER BY Salary DESC