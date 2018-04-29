SELECT DISTINCT 
		EmployeeID, 
		FirstName + ' ' + LastName AS [Full Name],
		Salary
FROM Employees