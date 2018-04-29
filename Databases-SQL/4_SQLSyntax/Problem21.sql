SELECT 
	e.EmployeeID,
	COALESCE 
	(
		e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName,
		e.FirstName + ' ' + e.LastName
	) AS [Employee Name],
	a.AddressText AS 'Address',
	COALESCE 
	(
		em.FirstName + ' ' + em.MiddleName + ' ' + em.LastName,
		em.FirstName + ' ' + em.LastName
	) AS [Managed By]
FROM Employees e
JOIN Employees em
ON em.EmployeeID = e.ManagerID
INNER JOIN Addresses a
ON e.AddressID = a.AddressID