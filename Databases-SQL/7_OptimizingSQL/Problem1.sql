CREATE DATABASE Homework
GO

USE Homework
GO

SET STATISTICS TIME ON
GO

CREATE VIEW v_rndView
AS
SELECT RAND() rndResult
GO

CREATE FUNCTION ufn_RandomNumGen(@start int, @end int)
RETURNS int
AS
	BEGIN
		DECLARE @ranNum int, @rnd float
		SELECT @rnd = rndResult
		FROM v_rndView

		SET @ranNum = @start + CONVERT(INT, @start + (@end-@start+1)*@rnd)
		RETURN @ranNum
	END
GO


CREATE TABLE RandomData (
						Id int PRIMARY KEY IDENTITY,
						[Random Date] date NOT NULL,
						[Random Text] nvarchar(max)
						)


DECLARE @currRow int = 1, @numOFRows int = 1250000, @randomDate date, @randomText nvarchar(50) 
-- I am generating only 1 250 000 random rows because SQL Express and my comp cannot handle generating 10 000 000 entries at once
WHILE(@currRow <= @numOFRows) 
	BEGIN
		SET @randomDate = DateAdd(d, ROUND(DateDiff(d, '1990-01-01', '2010-12-31') * RAND(CHECKSUM(NEWID())), 0),
      DATEADD(second,CHECKSUM(NEWID())%48000, '1990-01-01'))
		SET @randomText = LEFT(CONVERT(varchar(255), NEWID()), dbo.ufn_RandomNumGen(5,50))
		INSERT INTO RandomData([Random Date], [Random Text])
		VALUES(@randomDate, @randomText)
		SET @currRow += 1
	END

--I'll insert the table into itself 3 times and we'll get our 10 000 000 rows of data
DECLARE @statement nvarchar(50) = 'INSERT INTO RandomData([Random Date], [Random Text]) SELECT [Random Date], [Random Text] FROM RandomData', 
		@times int = 3, @t int = 0
	WHILE(@t < @times)
		BEGIN
			EXEC (@statement)
			SET @t += 1
		END

SELECT Id, [Random Date] AS [Date], [Random Text] AS [Text] FROM RandomData
WHERE [Random Date] BETWEEN '1995-06-15' AND '2000-06-15'


