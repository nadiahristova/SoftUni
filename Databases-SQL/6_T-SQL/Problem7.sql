--Problem 7.	Define function in the SoftUni database.
--Define a function in the database SoftUni that returns all Employee's names (first or 
--middle or last name) and all town's names that are comprised of given set of letters. 
--Example: 'oistmiahf' will return 'Sofia', 'Smith', but not 'Rob' and 'Guy'.


CREATE PROC usp_SelectNamesAndTowns
	AS
		SELECT Words FROM 
					(
						SELECT FirstName AS Words FROM Employees 
						UNION 
						SELECT MiddleName FROM Employees 
						UNION
						SELECT LastName FROM Employees
						UNION
						SELECT Name FROM Towns
					) AS AllWords
		WHERE Words IS NOT NULL
GO

ALTER FUNCTION ufn_IsValidWord(@randomWord nvarchar(50), @setOfLetters nvarchar(50))
RETURNS bit
	AS
		BEGIN
			DECLARE @wordLength int = LEN(@randomWord)
			DECLARE @setOfLettersLength int = LEN(@setOfLetters)
			DECLARE @letter nvarchar(1)
			DECLARE @i int = 1

			WHILE(@i <= @wordLength)
				BEGIN	
					SET @letter = SUBSTRING(@randomWord, @i, 1)	
					IF @setOfLetters NOT LIKE '%' + @letter + '%'	
						RETURN 0 
					SELECT @i += 1
				END

			RETURN 1
		END
GO


DECLARE @letters nvarchar(50) = 'oistmiahf'
CREATE TABLE #tmpTable
(
    Words VARCHAR(100)
)

CREATE TABLE [Output]
(	
	Id int PRIMARY KEY IDENTITY NOT NULL,
	[Words That Match The Requrements] nvarchar(50)
)

INSERT INTO #tmpTable (Words)
EXEC usp_SelectNamesAndTowns

DECLARE wCursor CURSOR READ_ONLY FOR SELECT Words FROM #tmpTable
	OPEN wCursor
		DECLARE @aWord nvarchar(50)
		FETCH NEXT FROM wCursor INTO @aWord

		WHILE @@FETCH_STATUS = 0
			BEGIN
				IF(dbo.ufn_IsValidWord(@aWord, @letters) = 1)
					INSERT INTO [Output] ([Words That Match The Requrements])
					VALUES (@aWord)
				FETCH NEXT FROM wCursor INTO @aWord
			END
	CLOSE wCursor
DEALLOCATE wCursor

DROP TABLE #tmpTable

SELECT * FROM [Output]
GO
