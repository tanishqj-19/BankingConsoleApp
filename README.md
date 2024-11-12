# C# Console Banking Application

This is a simple console-based banking application implemented in C#. It provides core banking functionalities, such as user registration and login, account opening, transaction processing, statement generation, and interest calculation.


## File Structure 

## Account.cs

Defines the `Account` class, representing a bank account with the following properties:

- **AccountNumber**: Unique identifier for the account.
- **AccountHolderName**: Name of the account holder.
- **Type**: Enum value representing the type of account (Savings, Checking).
- **Balance**: The current balance of the account.
- **Transactions**: List of transactions associated with the account.
- **LastInterestCalculation**: Stores the date of the last interest calculation for savings accounts.
- **Constructor**:
   - The constructor initializes an account with essential details.

---
## BankingSystem.cs

Contains the `BankingSystem` class, responsible for managing the core functionalities of the banking application. It includes methods for:

- Opening Accounts
- Selecting Accounts
- Processing Deposits
- Processing Withdrawals
- Calculating Interest
- Checking Balance
- User Input Functions: Implemented helper functions to make sure code reusability.

Acts as a bridge between the user and the `Account` and `Transaction` functionalities.

---

## Transaction.cs

Defines the `Transaction` class, representing a transaction on an account. Properties include:

- **TransactionId**: Unique identifier for each transaction.
- **Date**: The date the transaction took place.
- **Type**: Enum representing the type of transaction (Deposit, Withdrawal).
- **Amount**: The amount involved in the transaction.

---

## User.cs

Defines the `User` class, representing a bank customer with the following properties:

- **UserId**: Unique identifier for each user.
- **Name**: The name of the user.
- **Accounts**: A list of accounts owned by the user.

This class provides a structure to associate multiple accounts with a single user.

---

## Program.cs

The entry point of the application. It contains the `Main` method that:

- Initializes the `BankingSystem`

## Approach

The application is structured around the following key components:

1. **Models**:
   - `User`: Represents a user of the banking system, with a username, password, and a list of accounts.
   - `Account`: Represents a bank account, with an account number, account holder name, account type (Savings or Checking), balance, and a list of transactions.
   - `Transaction`: Represents a financial transaction, with a transaction ID, date, type (Deposit or Withdrawal), and amount.
   - `AccountType` and `TransactionType`: Enum types to represent the different account types and transaction types, respectively.

2. **Banking System Class**:
   - `BankingSystem`: This is the main class that handles the core functionality of the banking application.
   - `Start()`: The main entry point of the application, which handles the user flow between the login menu and the main menu.
   - `ShowLoginMenu()` and `ShowMainMenu()`: These methods display the respective menus and handle user input.
   - **User Management**:
     - `Register()`: Allows users to register by creating a username and password.
     - `Login()`: Verifies user credentials and sets the `currentUser` variable.
   - **Account Management**:
     - `OpenAccount()`: Creates a new account with a unique account number, account holder name, account type, and initial deposit.
     - `SelectAccount()`: Displays the user's accounts and allows them to select an account.
   - **Transactions**:
     - `ProcessDeposit()` and `ProcessWithdrawal()`: Handle the deposit and withdrawal operations, updating the account balance and creating a new transaction.
   - **Statement Generation**:
     - `ViewStatement()`: Displays the transaction history and current balance for a selected account.
   - **Interest Calculation**:
     - `CalculateInterest()`: Calculates and applies the monthly interest for all of the user's savings accounts.
   - **Logout**:
     - `Logout()`: Clears the `currentUser` variable, effectively logging the user out.

3. **Helper Methods**:
   - `GetUserInput()`: Handles user input, validating against a list of allowed options if provided.
   - `GetUserInputEnum<T>()`: Converts user input to the appropriate enum value.
   - `GetDecimalInput()`: Validates decimal input, including an optional custom validator function.
   - `PressAnyKeyToContinue()`: Prompts the user to press a key before clearing the screen and displaying the next menu.

The application's functionality is implemented as described in the "Features" section above.


## Features

1. **User Management**:
   - Registration and login system
   - Password verification
   - Session management using `currentUser`

2. **Account Management**:
   - Create new accounts (Savings/Checking)
   - Generate unique account numbers
   - Track account balances
   - Support multiple accounts per user

3. **Transactions**:
   - Deposit and withdrawal functionality
   - Transaction history logging
   - Overdraft protection
   - Unique transaction IDs

4. **Interest Calculation**:
   - Monthly interest calculation for savings accounts
   - Interest rate set at 5% annually
   - Tracks last interest calculation date

5. **Statement Generation**:
   - Detailed transaction history
   - Current balance display
   - Formatted output

## Usage

1. Create a new C# Console Application in Visual Studio.
2. Copy the code from the `banking-app` artifact into your `Program.cs` file.
3. Run the application.
4. Follow the console menu to:
   - Register a new user
   - Login with credentials
   - Create accounts
   - Perform transactions
   - View statements and balances

## License

This project is licensed under the [MIT License](LICENSE).
