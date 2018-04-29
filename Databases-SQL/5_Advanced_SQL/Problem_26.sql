SELECT 
	d.Name AS Department, 
	e.JobTitle AS [Job Title], 
	MAX(e.FirstName) AS [First Name], 
	MIN(Salary) AS [Minimum Salary] 
FROM Departments d
JOIN Employees e
ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name, e.JobTitle

