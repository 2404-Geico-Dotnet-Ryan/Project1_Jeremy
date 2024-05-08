using System.Transactions;

class Accounts
{
    public int AccountNumber { get; set; }
    public double Amount { get; set; }
    //public long TransactionDate { get; set; }
    public string? Merchant { get; set; }
    public string? Type { get; set; }
    public double Balance { get; set; }


    public Accounts(int AccountNumber, double Amount, string Merchant, string Type, double Balance)
    {
        AccountNumber = accountNumber;
        Amount = amount;
        //TransactionDate = transactionDate;
        Merchant = merchant;
        Type = type;
        Balance = balance;
    }

    public override string ToString()
    {
        return "accountNumber: " + AccountNumber
        + ", amount: " + Amount
        //+ ", transactionDate: " + TransactionDate
        + ", merchant: " + Merchant
        + ", type: " + Type
        + ", balance: " + Balance + "}";
    }

}

