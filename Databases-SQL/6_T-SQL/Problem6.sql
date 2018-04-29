--Problem 6.	Create table Logs.
--Create another table – Logs (LogID, AccountID, OldSum, NewSum). Add a trigger to the Accounts table that 
--enters a new entry into the Logs table every time the sum on an account changes.

CREATE TABLE Logs(
					LogID int IDENTITY PRIMARY KEY NOT NULL,
					AccountID int NOT NULL,
					OldSum money DEFAULT 0,
					NewSum money NOT NULL
				)
GO

CREATE TRIGGER tr_MoneyMovement ON Accounts FOR UPDATE
	AS
		INSERT INTO Logs(AccountID, OldSum, NewSum)
		SELECT d.Id, d.Balance, i.Balance
		FROM INSERTED i
			JOIN DELETED d
			ON d.Id = i.Id
GO

CREATE TRIGGER tr_MoneyMovement1 ON Accounts FOR UPDATE
	AS
		DECLARE @accID int, @oldSum money, @newSum money
		SELECT @accID = d.Id FROM DELETED d
		SELECT @oldSum = d.Balance FROM DELETED d
		SELECT @newSum = i.Balance FROM INSERTED i
		INSERT INTO Logs(AccountID, OldSum, NewSum)
		VALUES (@accID, @oldSum, @newSum)
		PRINT 'Trigger 1 has been fired.'
GO

EXEC usp_WithdrawMoney 4, 500