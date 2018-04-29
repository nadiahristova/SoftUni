SELECT TOP 1 t.Name AS Name, COUNT(e.EmployeeID) AS [Number of Employees] FROM Towns t
JOIN Addresses a
ON a.TownID = t.TownID
JOIN Employees e
ON e.AddressID = a.AddressID
GROUP BY t.Name
ORDER BY [Number of Employees] DESC