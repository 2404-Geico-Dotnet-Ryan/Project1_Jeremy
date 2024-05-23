class AccountStorage
{
    /*
        Create temporary "Account" info storage until we get it into SQL:
    */
    public Dictionary<int, Account> account;

    // create a baseline for the first AccountNumber:
    public int accountNumberCounter = 1;

    /*
        Create a constructor for this storage:
    */
    public AccountStorage()
    {
        Account account1 = new(accountNumberCounter++, "withdrawal", 0, 400, 2000, 1);
        Account account2 = new(accountNumberCounter++, "withdrawal", 0, 100, 1050, 2);
        Account account3 = new(accountNumberCounter++, "deposit", 0, 500, 1280, 3);
        Account account4 = new(accountNumberCounter++, "withdrawal", 0, 10, 12450, 4);

        account = new();
        account.Add(account1.AccountNumber, account1);
        account.Add(account2.AccountNumber, account2);
        account.Add(account3.AccountNumber, account3);
        account.Add(account4.AccountNumber, account4);

    }

}