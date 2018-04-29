SELECT d.Name, AVG(Salary) FROM Departments d
JOIN Employees e 
ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name