using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Globalization;


class Program
{
    static void Main(string[] args)
    {
        var bankingSystem = new BankingSystem();
        bankingSystem.Start();
    }
}