SELECT t.Name AS Town, COUNT(*) FROM Towns t
JOIN Addresses a
ON a.TownID = t.TownID
JOIN Employees e
ON e.AddressID = a.AddressID
JOIN Employees em
ON em.EmployeeID = e.ManagerID
GROUP BY t.Name
