SELECT 
	e.EmployeeID,
	COALESCE 
	(
		e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName,
		e.FirstName + ' ' + e.LastName
	) AS [Employee Name],
	COALESCE 
	(
		em.FirstName + ' ' + em.MiddleName + ' ' + em.LastName,
		em.FirstName + ' ' + em.LastName
	) AS [Manager Name]
FROM Employees e
LEFT OUTER JOIN Employees em
ON e.ManagerID = em.EmployeeID
