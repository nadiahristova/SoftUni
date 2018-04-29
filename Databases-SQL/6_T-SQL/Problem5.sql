--Problem 5.	Add two more stored procedures WithdrawMoney and DepositMoney.
--Add two more stored procedures WithdrawMoney (AccountId, money) and DepositMoney (AccountId, money) 
--that operate in transactions.

CREATE PROC usp_WithdrawMoney(@accountId int, @moneyToWithdraw money)
	AS
		BEGIN TRAN
		IF (EXISTS(SELECT * From Accounts Where Id = @accountId)) AND
			((SELECT Balance FROM Accounts WHERE Id = @accountId) - @moneyToWithdraw >= 0)
			BEGIN
				UPDATE Accounts
				SET Balance = Balance - @moneyToWithdraw 
				WHERE Id = @accountId
			END
		ELSE 
			ROLLBACK TRAN

		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION
GO

EXEC usp_WithdrawMoney 4, 500

CREATE PROC usp_DepositMoney(@accountId int, @moneyToWithdraw money)
	AS
		BEGIN TRAN
		IF EXISTS(SELECT * From Accounts Where Id = @accountId)
			BEGIN
				UPDATE Accounts
				SET Balance = Balance + @moneyToWithdraw 
				WHERE Id = @accountId
			END
		ELSE 
			ROLLBACK TRAN

		IF @@TRANCOUNT > 0
			COMMIT TRANSACTION
GO

EXEC usp_DepositMoney 4, 500