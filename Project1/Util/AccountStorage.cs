class AccountStorage
{

    public Dictionary<int, Accounts> accounts;
    public int accountNumberCounter = 1;

    public AccountStorage()
    {
        Accounts account1 = new(accountNumberCounter++, 400.00, "Lowes", "debit", 1000.00);
        Accounts account2 = new(accountNumberCounter++, 100.00, "Publix", "debit", 950.50);
        Accounts account3 = new(accountNumberCounter++, 500.00, "Payroll", "credit", 1280.40);
        Accounts account4 = new(accountNumberCounter++, 10.30, "Taco Bell", "debit", 12450.30);

        accounts = [];
        accounts.Add(account1.AccountNumber, account1);
        accounts.Add(account2.AccountNumber, account2);
        accounts.Add(account3.AccountNumber, account3);
        accounts.Add(account4.AccountNumber, account4);

    }

}