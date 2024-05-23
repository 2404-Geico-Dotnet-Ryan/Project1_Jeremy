class User
{
    public string AccountHolder { get; set; }
    public int AccountNumber { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    public User()
    {
        AccountHolder = "";
        Password = "";
        Role = "";
    }

    public User(string accountHolder, int accountNumber, string password, string role)
    {
        AccountHolder = accountHolder;
        AccountNumber = accountNumber;
        Password = password;
        Role = role;
    }

    public override string ToString()
    {
        return "accountHolder:" + AccountHolder
        + ", accountNumber:" + AccountNumber
        + ", password:" + Password + "'";
    }

}