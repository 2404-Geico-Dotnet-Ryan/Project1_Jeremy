class AccountRepo
{

    //Create Account:
    AccountStorage accountStorage = new();
    public Accounts AddAccount(Accounts a)
    {
        a.AccountNumber = accountStorage.accountNumberCounter++;

        accountStorage.accounts.Add(a.AccountNumber, a);
        return a;
    }

    //Read Account:
    public Accounts? GetAccounts(int AccountNumber)
    {
        if (accountStorage.accounts.ContainsKey(AccountNumber))
        {
            Accounts selectedAccount = accountStorage.accounts[AccountNumber];
            return selectedAccount;
        }
        else
        {
            System.Console.WriteLine("Invalid Account Number, Please try again.");
            return null;
        }
    }

}