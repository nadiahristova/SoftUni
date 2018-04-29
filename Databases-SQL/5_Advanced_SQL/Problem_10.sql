SELECT 
	t.Name AS Town,
	d.Name AS Department,
	COUNT(EmployeeID) AS [Employees Count] 
FROM Departments d
	JOIN Employees e
	ON e.DepartmentID = d.DepartmentID
	JOIN Addresses a
	ON e.AddressID = a.AddressID
	JOIN Towns t
	ON a.TownID = t.TownID
GROUP BY t.Name, d.Name