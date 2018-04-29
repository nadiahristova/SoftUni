--Problem 8.	Using database cursor write a T-SQL
--Using database cursor write a T-SQL script that scans all employees and their 
--addresses and prints all pairs of employees that live in the same town.

CREATE PROC usp_SelectEmployees(@townName nvarchar(150))
	AS 
		SELECT e.FIrstName + ' ' + e.LastName AS [Full Name] FROM Employees e
		JOIN Addresses a
			ON a.AddressID = e.AddressID
		JOIN Towns t
			ON t.TownID = a.TownID
		WHERE t.Name = @townName
GO

CREATE TABLE #AllTownsTemp(TownName nvarchar(150))

INSERT INTO #AllTownsTemp(TownName)
	(SELECT Name FROM Towns)

	
DECLARE wCursor CURSOR READ_ONLY FOR SELECT TownName FROM #AllTownsTemp
	OPEN wCursor
		DECLARE @town nvarchar(50)
		FETCH NEXT FROM wCursor INTO @town

		WHILE @@FETCH_STATUS = 0
			BEGIN
				CREATE TABLE #AllEmployees(EmpName nvarchar(150))

				INSERT INTO #AllEmployees(EmpName)
				EXEC usp_SelectEmployees @town

				DECLARE @row nvarchar(128)
				DECLARE cur CURSOR FOR
				SELECT @town + ':' + ae.EmpName + ' <--> '  + aem.EmpName FROM #AllEmployees ae
					CROSS JOIN #AllEmployees aem
					WHERE ae.EmpName <> aem.EmpName

				OPEN cur
				FETCH NEXT FROM cur INTO @row;
				WHILE (@@FETCH_STATUS = 0)
					BEGIN   
						PRINT @row
						FETCH NEXT FROM cur INTO @row;
					END

				CLOSE cur;
				DEALLOCATE cur;

				FETCH NEXT FROM wCursor INTO @town
				DROP TABLE #AllEmployees
			END
	CLOSE wCursor
DEALLOCATE wCursor

DROP TABLE #AllTownsTemp