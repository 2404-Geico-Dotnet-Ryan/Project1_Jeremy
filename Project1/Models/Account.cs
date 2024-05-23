using System.Transactions;

class Account
{
    /*
        What properties do I want in the Account?
        - AccountHolder - the customer's name
        - AccountNumber - the customer's bank account number and can function as the ID/unique identifier
        - TransactionType - Deposit or Withdrawal
        - Amount - or TransactionAmount - amount of Deposit or Withdrawal
        - Balance - or AccountBalance
    */

    public int AccountNumber { get; set; }
    public string? TransactionType { get; set; }
    public long TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public int UserAccountNumber { get; set; }


    /*
        Build constructors for these properies:
    */

    public Account(int accountNumber, string transactionType, long transactionDate, decimal amount, decimal balance, int userAccountNumber)
    {
        AccountNumber = accountNumber;
        TransactionType = transactionType;
        TransactionDate = transactionDate;
        Amount = amount;
        Balance = balance;
        UserAccountNumber = userAccountNumber;
    }

    public Account()
    {
    }

    public override string ToString()  // to give us a readable format in a string response
    {
        return "accountNumber: " + AccountNumber
        + ", amount: " + Amount
        + ", transactionType: " + TransactionType
        + ", transactionDate: " + TransactionDate
        + ", balance: " + Balance
        + ", userAccountNumber: " + UserAccountNumber.ToString() + "}";
    }

}

