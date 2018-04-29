--Problem 1.	Create a database with two tables
--Persons (id (PK), first name, last name, SSN) and Accounts (id (PK), person id (FK), balance). 
--Insert few records for testing. 
--Write a stored procedure that selects the full names of all persons.

CREATE TABLE Persons(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	FirstName nvarchar(30) NOT NULL,
	LastName nvarchar(30) NOT NULL,
	SSN nvarchar(25) NOT NULL)

CREATE TABLE Accounts(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	PersonID int NOT NULL,
	Balance money DEFAULT 0 NOT NULL,
	CONSTRAINT FK_Accounts_Persons
	FOREIGN KEY (PersonID) REFERENCES Persons(Id))

CREATE PROC dbo.usp_SelectFullNamesOfPersons
	AS
		SELECT FirstName + ' ' + LastName AS [Full Name] FROM Persons
GO

EXEC dbo.usp_SelectFullNamesOfPersons