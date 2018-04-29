SELECT 
	EmployeeID,
	COALESCE 
	(
		FirstName + ' ' + MiddleName + ' ' + LastName,
		FirstName + ' ' + LastName
	) AS [Full Name],
	Salary
FROM Employees
WHERE Salary BETWEEN 20000 AND 30000
ORDER BY Salary DESC
