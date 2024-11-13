using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// -------------------------------- Banking System ----------------------------------

public class BankingSystem
{


    private List<User> users = new List<User>();
    private User currentUser;
    // Basic Interest Rate is 5%
    private const decimal INTEREST_RATE = 0.05m;



    // ---------------------------------- StartUp Menu --------------------------------------
    public void Start()
    {
        while (true)
        {
            if (currentUser == null)
            {
                ShowLoginMenu();
            }
            else
            {
                ShowMainMenu();
            }
        }
    }


    // ------------------------------------- When user not loggen in Menu------------------------

    private void ShowLoginMenu()
    {
        Console.Clear();
        Console.WriteLine("========== Banking System ==========");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Exit");

        string choice = GetUserInput("Enter your choice: ", new[] { "1", "2", "3" });
        switch (choice)
        {
            case "1":
                Login();
                break;
            case "2":
                Register();
                break;
            case "3":
                Environment.Exit(0);
                break;
        }
    }

    // -------------------------- Main Menu When user logged In -------------------------------

    private void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine($"Welcome, {currentUser.Username}!");
        Console.WriteLine("1. Open New Account");
        Console.WriteLine("2. Deposit");
        Console.WriteLine("3. Withdraw");
        Console.WriteLine("4. Check Balance");
        Console.WriteLine("5. View Statement");
        Console.WriteLine("6. Calculate Interest");
        Console.WriteLine("7. Logout");

        string choice = GetUserInput("Enter your choice: ", new[] { "1", "2", "3", "4", "5", "6", "7" });
        switch (choice)
        {
            case "1":
                OpenAccount();
                break;
            case "2":
                ProcessDeposit();
                break;
            case "3":
                ProcessWithdrawal();
                break;
            case "4":
                CheckBalance();
                break;
            case "5":
                ViewStatement();
                break;
            case "6":
                CalculateInterest();
                break;
            case "7":
                Logout();
                break;
        }
    }

    // ------------------------- Register Function --------------------------

    private void Register()
    {
        Console.WriteLine("Enter username:");
        string username = GetUserInput();

        if (users.Any(u => u.Username == username))
        {
            Console.WriteLine("Username already exists!");
            PressAnyKeyToContinue();
            return;
        }

        Console.WriteLine("Enter password:");
        string password = GetUserInput();

        users.Add(new User { Username = username, Password = password });
        Console.WriteLine("Registration successful!");
        PressAnyKeyToContinue();
    }


    // ---------------------------------- Login Function --------------------------------

    private void Login()
    {
        if (users.Count <= 0)
        {
            Console.WriteLine("No user exists!");
            PressAnyKeyToContinue();
            return;
        }
        else
        {
            Console.WriteLine("Enter username:");
            string username = GetUserInput();
            Console.WriteLine("Enter password:");
            string password = GetUserInput();

            currentUser = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (currentUser == null)
            {
                Console.WriteLine("Invalid credentials!");
                PressAnyKeyToContinue();
            }
        }

    }


    // ----------------------------------- Account Opening. --------------------------------

 

    private void OpenAccount()
    {
        Console.WriteLine("Enter account holder name:");
        string holderName = GetUserInput();

        Console.WriteLine("Select account type (1 for Savings || 2 for Checking):");
        string typeInput = GetUserInput("Enter 1 for Savings or 2 for Checking: ", new[] { "1", "2" });

        AccountType type = typeInput == "1" ? AccountType.Savings : AccountType.Checking;

        Console.WriteLine("Enter initial deposit amount:");
        decimal initialDeposit = GetDecimalInput("Enter a valid amount: ");

        while(initialDeposit < 150)
        {
            Console.WriteLine("Initial deposit must be greater than 150.");
            initialDeposit = GetDecimalInput("Enter a valid amount: ");

        }

        string accountNumber = Guid.NewGuid().ToString().Substring(0, 8);
        var account = new Account(accountNumber, holderName, type, initialDeposit);

        // Add initial deposit transaction
        account.Transactions.Add(new Transaction
        {
            TransactionId = Guid.NewGuid().ToString(),
            Date = DateTime.Now,
            Type = TransactionType.Deposit,
            Amount = initialDeposit
        });

        currentUser.Accounts.Add(account);
        Console.WriteLine($"Account created successfully! Account Number: {accountNumber}");
        PressAnyKeyToContinue();
    }
    // ------------------------------------- Choose An Account Option -------------------------------

    private Account SelectAccount()
    {
        if (!currentUser.Accounts.Any())
        {
            Console.WriteLine("No accounts found!");
            PressAnyKeyToContinue();
            return null;
        }

        Console.WriteLine("Available accounts:");
        foreach (var account in currentUser.Accounts)
        {
            Console.WriteLine($"{account.AccountNumber} - {account.AccountHolderName} ({account.Type})");
        }

        Console.WriteLine("Please type the account number you want to select:");
        string accountNumber = GetUserInput("Enter account number: ", currentUser.Accounts.Select(a => a.AccountNumber).ToArray());

        var selectedAccount = currentUser.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        if (selectedAccount == null)
        {
            Console.WriteLine("Account not found. Please try again.");
        }

        return selectedAccount;
    }


    //--------------------------------------- DEPOSIT FUNCTION -------------------------------------
    private void ProcessDeposit()
    {
        var account = SelectAccount();
        if (account == null) return;

        decimal amount = GetDecimalInput("Enter deposit amount: ", amount => amount > 0);
        account.Balance += amount;
        account.Transactions.Add(new Transaction
        {
            TransactionId = Guid.NewGuid().ToString(),
            Date = DateTime.Now,
            Type = TransactionType.Deposit,
            Amount = amount
        });

        Console.WriteLine("Deposit successful!");
        Console.WriteLine($"Current balance: ${account.Balance:F2}");
        PressAnyKeyToContinue();
    }

    // ------------------------------------------ WITHDRAWAL FUNCTION -------------------------------

    private void ProcessWithdrawal()
    {
        var account = SelectAccount();
        if (account == null) return;

        decimal amount = GetDecimalInput($"Enter withdrawal amount (Available balance: ${account.Balance:F2}): ",
                                         amount => amount > 0 && amount <= account.Balance);

        account.Balance -= amount;
        account.Transactions.Add(new Transaction
        {
            TransactionId = Guid.NewGuid().ToString(),
            Date = DateTime.Now,
            Type = TransactionType.Withdrawal,
            Amount = amount
        });

        Console.WriteLine("Withdrawal successful!");
        Console.WriteLine($"Current balance: ${account.Balance:F2}");
        PressAnyKeyToContinue();
    }


    // --------------------------------------- CHECK BALANCE ----------------------------------

    private void CheckBalance()
    {
        var account = SelectAccount();
        if (account == null) return;

        Console.WriteLine($"Current balance: ${account.Balance:F2}");
        PressAnyKeyToContinue();
    }

    // --------------------------------- VIEW STATEMENT --------------------------------------

    private void ViewStatement()
    {
        var account = SelectAccount();
        if (account == null) return;

        Console.WriteLine($"Statement for Account: {account.AccountNumber}");
        Console.WriteLine("Date\t\tType\t\tAmount");
        Console.WriteLine("----------------------------------------");

        foreach (var transaction in account.Transactions.OrderByDescending(t => t.Date))
        {
            Console.WriteLine($"{transaction.Date}\t{transaction.Type}\t${transaction.Amount:F2}");
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Current Balance: ${account.Balance:F2}");
        PressAnyKeyToContinue();
    }

    // ---------------------------------- Interest Calculation ------------------------------

    private void CalculateInterest()
    {
       
        var savingsAccounts = currentUser.Accounts.Where(a => a.Type == AccountType.Savings);

        if (!savingsAccounts.Any())
        {
            Console.WriteLine($"{currentUser} doesn't have a saving account. Please open a saving account first.");
        }
        else
        {
            foreach (var account in savingsAccounts)
            {
                // Calculate months since last interest calculation
                var monthsSinceLastCalculation = ((DateTime.Now.Year - account.LastInterestCalculation.Year) * 12) +
                                                 DateTime.Now.Month - account.LastInterestCalculation.Month;

                if (monthsSinceLastCalculation >= 1)
                {
                    // Calculate monthly interest
                    decimal monthlyInterestRate = INTEREST_RATE / 12;
                    decimal interest = account.Balance * monthlyInterestRate * monthsSinceLastCalculation;

                    // Update account balance and last calculation date
                    account.Balance += interest;
                    account.LastInterestCalculation = DateTime.Now;

                    // Add a transaction for the interest
                    account.Transactions.Add(new Transaction
                    {
                        TransactionId = Guid.NewGuid().ToString(),
                        Date = DateTime.Now,
                        Type = TransactionType.Deposit,
                        Amount = interest
                    });

                    Console.WriteLine($"Interest of ${interest:F2} added to account {account.AccountNumber}");
                }
                else
                {
                    Console.WriteLine($"No interest due for account {account.AccountNumber}. Interest is added monthly.");
                }
            }
        }

        PressAnyKeyToContinue();
    }

    // ------------------------------------ LOG USER OUT ----------------------------------

    private void Logout()
    {
        currentUser = null;
        Console.WriteLine("Logged out successfully!");
        PressAnyKeyToContinue();
    }


    // ------------------------------------ HELPER INPUT FUNCTION --------------------------------

    // Handles Input Invalid Errors and Take correct user Input...........
    private string GetUserInput(string prompt = "", string[] validOptions = null)
    {
        string input;
        do
        {
            input = Console.ReadLine();
            if (validOptions != null && !validOptions.Contains(input))
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        } while (validOptions != null && !validOptions.Contains(input));
        return input;
    }

    private T GetUserInputEnum<T>(string[] validOptions)
    {
        string input = GetUserInput();
        return (T)Enum.Parse(typeof(T), validOptions[Array.IndexOf(validOptions, input)]);
    }




    private decimal GetDecimalInput(string prompt, Func<decimal, bool> validator = null)
    {
        decimal value;
        bool isValid;
        do
        {
            Console.Write(prompt);
            isValid = decimal.TryParse(Console.ReadLine(), out value);
            if (!isValid || (validator != null && !validator(value)))
            {
                Console.WriteLine("Invalid input. Please enter a valid amount.");
            }
        } while (!isValid || (validator != null && !validator(value)));
        return value;
    }

    private void PressAnyKeyToContinue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

}