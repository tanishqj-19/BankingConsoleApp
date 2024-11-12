using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ----------------------------- ACCOUNT MODEL --------------------------------------
public class Account
{
    public string AccountNumber { get; set; }
    public string AccountHolderName { get; set; }
    public AccountType Type { get; set; }
    public decimal Balance { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    public DateTime LastInterestCalculation { get; set; }

    public Account(string accountNumber, string accountHolderName, AccountType type, decimal initialDeposit)
    {
        AccountNumber = accountNumber;
        AccountHolderName = accountHolderName;
        Type = type;
        Balance = initialDeposit;
        LastInterestCalculation = DateTime.Now;
    }
}

// -------------------------------------- ACCOUNT TYPE -----------------------------------

public enum AccountType
{
    Savings,
    Checking
}
