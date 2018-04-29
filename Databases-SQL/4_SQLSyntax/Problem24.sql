SELECT 
	e.EmployeeID,
	COALESCE 
	(
		e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName,
		e.FirstName + ' ' + e.LastName
	) AS [Full Name],
	d.Name AS [Department Name],
	e.HireDate
FROM Employees e
LEFT OUTER JOIN Departments d
ON e.DepartmentID = d.DepartmentID
WHERE 
	(d.Name = 'Sales' OR d.Name = 'Finance') 
	AND 
	e.HireDate BETWEEN '1995-01-01' AND '2005-01-01'