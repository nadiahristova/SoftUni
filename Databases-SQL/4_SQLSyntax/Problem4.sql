SELECT 
	d.Name AS [Department Name], 
	e.FirstName + ' ' + e.LastName AS [Managed By],
	e.JobTitle AS [With Job Title]
FROM Departments d
JOIN Employees e
ON e.EmployeeID = d.ManagerID
ORDER BY e.Salary DESC


SELECT *
FROM Departments