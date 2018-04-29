SELECT 
	e.EmployeeID,
	COALESCE 
	(
		e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName,
		e.FirstName + ' ' + e.LastName
	) AS [Full Name],
	a.AddressText AS 'Address'
FROM Employees e 
INNER JOIN Addresses a
ON e.AddressID = a.AddressID