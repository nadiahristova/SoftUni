using ATM.Data;
using ATM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ATM.ConsoleClient
{
    class ATMMachineMain
    {
        static void Main()
        {
            Console.Write("Enter Card Number: ");
            string cardNum  = Console.ReadLine();
            Console.Write("PIN: ");
            string pin  = Console.ReadLine();
            Console.Write("Amount of money to withdrawal: ");
            decimal withdrawaledMoney = decimal.Parse(Console.ReadLine());

            var ctx = new ATMEntities();
            using (var dbContextTransaction = ctx.Database.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                var card = ctx.CardAccounts.FirstOrDefault(a => a.CardNumber == cardNum);
                if (card == null || card.CardPIN != pin)
                {                    
                    dbContextTransaction.Rollback();
                    throw new Exception("Invalid input data.");
                }

                decimal moneyAfterWithdrawal = card.CardCash - withdrawaledMoney;
                if(moneyAfterWithdrawal < 0)
                {
                    dbContextTransaction.Rollback();
                    throw new Exception("Insufficient amount of money.");
                }

                card.CardCash = moneyAfterWithdrawal;
                ctx.TransactionHistories.Add(new TransactionHistory()
                {
                    CardNumber = card.CardNumber,
                    TransactionDate = DateTime.Now,
                    Amount = withdrawaledMoney
                });
                ctx.SaveChanges();

                dbContextTransaction.Commit();
            }
        }

    }
}

//REPEATABLE READ: A query in the current transaction cannot read data modified by another transaction 
//that has not yet committed, thus preventing dirty reads. In addition, no other transactions can modify 
//data being read by the current transaction until it completes, eliminating nonrepeatable reads. 
//However, if another transaction inserts new rows that match the search condition in the current 
//transaction, in between the current transaction accessing the same data twice, phantom rows can 
//appear in the second read.
//https://www.simple-talk.com/sql/t-sql-programming/questions-about-t-sql-transaction-isolation-levels-you-were-too-shy-to-ask/