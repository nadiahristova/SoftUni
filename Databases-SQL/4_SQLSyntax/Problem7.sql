SELECT COALESCE 
	(
		FirstName + ' ' + MiddleName + ' ' + LastName,
		FirstName + ' ' + LastName
	) AS [Full Name]
FROM Employees
ORDER BY FirstName

--We use COALESCE because in the Employee table sometimes the field MiddleName-- 
--is left unfilled. This causes the SELECT FirstName + ' ' + MiddleName + ' ' + LastName-- 
--statement to return NULL. With COALESCE if the first statement in the brackets results-- 
--in NULL, T-SQL moves on to the next statement listed in the brackets until it finds a-- 
--valid information.--