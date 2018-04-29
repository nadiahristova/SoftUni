SELECT COUNT(EmployeeID) AS [Sales Employee Count] FROM Employees e
JOIN Departments d
ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales'