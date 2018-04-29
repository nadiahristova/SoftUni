--Problem 3.	Create a function with parameters
--Your task is to create a function that accepts as parameters – sum, yearly interest 
--rate and number of months. It should calculate and return the new sum. Write a SELECT 
--to test whether the function works as expected.

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

SELECT dbo.ufn_CalcLoan(1000, 0.12, 12);