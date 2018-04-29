--Problem 4.	Create a stored procedure that uses the function from the previous example.
--Your task is to create a stored procedure that uses the function from the previous example to give an 
--interest to a person's account for one month. It should take the AccountId and the interest rate as parameters.


ALTER FUNCTION dbo.ufn_CalcLoan(@sum money, @statedAnnualInterestRate float, @numberOfMonths int)
	RETURNS money
	AS
		BEGIN
			DECLARE @effectiveAnnualInterestRate float = @statedAnnualInterestRate
			WHILE(@numberOfMonths > 0)
				BEGIN
					SELECT @sum = @sum*(1 + @effectiveAnnualInterestRate)
					SELECT @effectiveAnnualInterestRate = 
						POWER((1 + @statedAnnualInterestRate/@numberOfMonths) - 1, @numberOfMonths)
					SELECT @numberOfMonths = @numberOfMonths -1
				END
			RETURN @sum
		END
GO

ALTER PROC usp_ReturnInterest(@accountID int, @intrestRate float)
	AS 
		SELECT 
			p.Id, 
			p.FirstName, 
			p.LastName,  
			dbo.ufn_CalcLoan(a.Balance, @intrestRate, 1) - a.Balance AS [Money gained after 1 month]
		FROM Persons p
		JOIN Accounts a 
			ON a.PersonID = p.Id
		WHERE p.Id = @accountID
GO

EXEC usp_ReturnInterest 2, 0.05
