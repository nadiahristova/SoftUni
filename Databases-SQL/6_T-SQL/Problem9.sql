--Problem 9.	Define a .NET aggregate function
--Define a .NET aggregate function StrConcat that takes as input a sequence of strings and 
--return a single string that consists of the input strings separated by ','. 

CREATE FUNCTION ufn_StrConcat(@input nvarchar(MAX))
RETURNS nvarchar(MAX)
	AS
		BEGIN
			DECLARE @strLen int = LEN(@input)
			DECLARE @i int = 1
			DECLARE @currentChar nvarchar(5) = ' '
			DECLARE @result nvarchar(MAX) = ''

			WHILE (@i <= @strLen)
				BEGIN
					SET @currentChar = SUBSTRING(@input, @i, 1)
					IF @currentChar = ' ' 
						BEGIN
							IF ((@i <> 1) AND (@i <> @strLen))
								BEGIN
									SET @currentChar = ', '
								END
						END
					SET @result = @result +	@currentChar
					SET @i += 1
				END
			RETURN @result
		END
GO

SELECT dbo.ufn_StrConcat(FirstName + ' ' + LastName)
FROM Employees

GO