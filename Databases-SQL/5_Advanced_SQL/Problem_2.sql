SELECT FirstName, LastName, Salary FROM Employees
WHERE Salary <= ( SELECT MIN(Salary) FROM Employees ) * 1.1
ORDER BY Salary DESC