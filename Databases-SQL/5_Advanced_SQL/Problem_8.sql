SELECT COUNT(EmployeeID) AS [Employees without Manager] 
FROM Employees
WHERE ManagerID IS NULL