SELECT d.Name AS Department, e.JobTitle AS [Job Title], AVG(Salary) AS [Average Salary] FROM Departments d
JOIN Employees e
ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name, e.JobTitle