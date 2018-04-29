SELECT 
	e.FirstName + ' ' + LastName AS [Full Name], 
	e.Salary, 
	d.Name AS Department 
FROM Employees e
JOIN Departments d
ON e.DepartmentID = d.DepartmentID
WHERE Salary = 
	(
		SELECT MIN(Salary) FROM Employees
		WHERE DepartmentID = d.DepartmentID
	)