using System.Runtime;
using System.Transactions;
using Microsoft.VisualBasic;

class AccountService
{
    /* Services:
        - Retrieve Available Funds
        - Make a Deposit
        - Make a Withdrawal
    */

    AccountRepo ar;

    public AccountService(AccountRepo ar)
    {
        this.ar = ar;
    }

    public Account? GetBalance(int accountNumber)
    {
        //return balance -- but isn't this going to return all account info? or does that matter if the RetrievingAvailableFunds method 
        //in the program file is only going to display the balance in the writeline? 
        return ar.GetAccount(accountNumber);
    }

    public Account? MakeDeposit(Account account, decimal amount)
    {

        //update balance: balance += amount;
        //update date/time:  DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        //return new balance;
        account.Balance += amount;
        account.TransactionDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        ar.UpdateAccount(account);
        return account;
    }

    public Account? MakeWithdrawal(Account account, decimal amount)
    {
        //balance -= amount;
        //should I include a way to prevent a withdrawal that is > current balance?
        //update date/time:  DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        //return balance;
        account.Balance -= amount;
        account.TransactionDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        ar.UpdateAccount(account);
        return account;
    }

    public Account? GetAccount(int accountNumber)
    {
        return ar.GetAccount(accountNumber);
    }
}

