class UserStorage
{
    /*
       Create temporary "User" info storage until we get it into SQL:
   */
    public Dictionary<int, User> users;

    public int accountNumberCounter = 1;

    public UserStorage()
    {
        User user1 = new("Lloyd Christmas", accountNumberCounter++, "password1", "user");
        User user2 = new("Harry Dunne", accountNumberCounter++, "password2", "user");
        User user3 = new("Mary Swanson", accountNumberCounter++, "password3", "user");
        User user4 = new("Sea Bass", accountNumberCounter++, "password4", "user");

        users = new();
        users.Add(user1.AccountNumber, user1);
        users.Add(user2.AccountNumber, user2);
        users.Add(user3.AccountNumber, user3);
        users.Add(user4.AccountNumber, user4);

    }

}