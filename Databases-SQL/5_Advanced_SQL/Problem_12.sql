SELECT 
	e.FirstName + ' '  + e.LastName AS Employee, 
	ISNULL(em.FirstName + ' ' + em.LastName, '(no manager)') AS Manager
FROM Employees e
LEFT OUTER JOIN Employees em
ON e.ManagerID = em.EmployeeID 