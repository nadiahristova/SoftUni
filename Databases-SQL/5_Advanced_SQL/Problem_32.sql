BEGIN TRAN
DELETE  Employees
WHERE DepartmentID = 
	(SELECT DepartmentID FROM Departments
	 WHERE Name = 'Sales')

SELECT * FROM Employees e
	JOIN Departments d
		ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales'
ROLLBACK TRAN