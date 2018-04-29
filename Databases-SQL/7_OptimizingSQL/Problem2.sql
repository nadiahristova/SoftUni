USE Homework
GO

SET STATISTICS TIME ON
GO

--Creating Index on Dates column
CREATE INDEX IDX_RandomData_Dates
ON RandomData([Random Date])

--Empty the SQL Server cache
CHECKPOINT 
GO 

DBCC DROPCLEANBUFFERS
GO

--We fire the same search
SELECT Id, [Random Date] AS [Date], [Random Text] AS [Text] FROM RandomData
WHERE [Random Date] BETWEEN '1995-06-15' AND '2000-06-15'
