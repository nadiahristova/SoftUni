SELECT 
	em.FirstName, 
	em.LastName, 
	COUNT(e.ManagerID) AS [Employees Count] 
FROM Employees e
JOIN Employees em
ON em.EmployeeID = e.ManagerID
GROUP BY em.FirstName, em.LastName
HAVING COUNT(e.ManagerID) = 5