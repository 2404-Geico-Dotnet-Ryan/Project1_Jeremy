class UserService
{
    UserRepo ur;

    public UserService(UserRepo ur)
    {
        this.ur = ur;
    }

    //Register
    public User? Register(User u)
    {
        //let's not let them register if the role is anything other than "user"
        if (u.Role != "user")
        {
            //reject them
            System.Console.WriteLine("Invalid Role - Please try again!");
            return null;
        }

        //let's not let them register if the username is already taken
        //get all users
        List<User> allUsers = ur.GetAllUsers();
        foreach (User user in allUsers)
        {
            if (user.AccountHolder == u.AccountHolder)
            {
                System.Console.WriteLine("Username already taken - Please try again!");
                return null; //reject them
            }
        }

        //if we don't care about any validation - this is but a simple/trivial service method
        return ur.AddUser(u);
    }

    //Login
    public User? Login(string accountHolder, string password)
    {
        //Get all users
        List<User> allUsers = ur.GetAllUsers();
        //check each user to see if we find a match
        foreach (User user in allUsers)
        {
            //if matching username and password, they 'login' -> return that user
            if (user.AccountHolder == accountHolder && user.Password == password)
            {
                //Yay! Login!
                return user;  //us returning the user will indicate success
            }
        }
        //if we made it this far, we didn't find a match, so...
        System.Console.WriteLine("Invalid AccountHolder/Pin combo.  Please try again!");
        return null;
    }

}