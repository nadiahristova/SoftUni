--Problem 2.	Create a stored procedure
--Your task is to create a stored procedure that accepts a number as a parameter and returns 
--all persons who have more money in their accounts than the supplied number.

CREATE PROC usp_ReturnPersonsWithMoneyMoreThenGiven(@minMoney money)
	AS
		SELECT p.Id, p.FirstName, p.LastName, p.SSN, a.Balance FROM Persons p
		JOIN Accounts a
		ON a.PersonID = p.Id
		WHERE a.Balance > @minMoney
		ORDER BY a.Balance DESC
GO

EXEC dbo.usp_ReturnPersonsWithMoneyMoreThenGiven 10000