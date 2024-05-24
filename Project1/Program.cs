using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

class Program
{
    static AccountService? accountService;
    static UserService? userService;
    static User? currentUser = null;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private static Account account;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    static void Main(string[] args)
    {
        string path = @"C:\Users\U1H950\Desktop\BankAppNuGetAddPackage.txt"; //add @ before path and quotes
        string connectionString = File.ReadAllText(path);
        //System.Console.WriteLine(connectionString); //remove later, this is just to read in this file.  dotnet run. 

        UserRepo ur = new(connectionString);
        userService = new(ur);

        AccountRepo ar = new(connectionString);
        accountService = new(ar);

        Thread.Sleep(1000); //just gives a pause in milliseconds before the next action

        LoginMenu();
    }

    private static void LoginMenu()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine(@"___________________________________");
        System.Console.WriteLine(@"|#######====================#######|");
        System.Console.WriteLine(@"|#(1)*UNITED STATES OF AMERICA*(1)#|");
        System.Console.WriteLine(@"|#**          /===\   ********  **#|");
        System.Console.WriteLine(@"|*# {G}      | ( ) |             #*|");
        System.Console.WriteLine(@"|#*  ******  | /v\ |    O N E    *#|");
        System.Console.WriteLine(@"|#(1)         \===/            (1)#|");
        System.Console.WriteLine(@"|##=========ONE DOLLAR===========##|");
        System.Console.WriteLine(@"------------------------------------");
        Console.ResetColor();
        System.Console.WriteLine(@"       Welcome to JT Bank!");
        System.Console.WriteLine("");
        bool keepGoing = true;
        while (keepGoing)
        {
            System.Console.WriteLine("Please Pick One of the Below Options:");
            System.Console.WriteLine("===============JT BANK===============");
            Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("[1] Login");
            System.Console.WriteLine("[2] Create Account");
            System.Console.WriteLine("[0] Quit");
            Console.ResetColor();
            System.Console.WriteLine("=====================================");

            Console.ForegroundColor = ConsoleColor.Cyan;
            int input = int.Parse(Console.ReadLine() ?? "0");
            Console.ResetColor();
            input = ValidateCmd(input, 2);
            keepGoing = DecideLoginOption(input);
        }
    }



    private static void MainMenu()
    {
        //Console.Clear();  //visually "clears" the console from any unrelated messages, warnings, etc. 
        // Main Menu - bank title, view balance, make deposit, make withdrawal, quit
        System.Console.WriteLine("Welcome to the JT Bank App!");
        bool keepGoing = true;
        while (keepGoing)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            Console.ResetColor();
            System.Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            System.Console.WriteLine("Please Pick One of the Below Options:");
            System.Console.WriteLine("===============JT BANK===============");
            System.Console.WriteLine("[1] View Available Funds");
            System.Console.WriteLine("[2] Make a Deposit");
            System.Console.WriteLine("[3] Withdrawl Funds");
            System.Console.WriteLine("[0] Logout");
            Console.ResetColor();
            System.Console.WriteLine("=====================================");

            Console.ForegroundColor = ConsoleColor.Cyan;
            int input = int.Parse(Console.ReadLine() ?? "0");
            Console.ResetColor();

            input = ValidateCmd(input, 3); //user input validation

            keepGoing = DecideNextOption(input); //using below SwitchCase
        }
    }

    private static bool DecideLoginOption(int input)
    {
        switch (input)
        {
            case 1:
                {
                    Login();
                    break;
                }
            case 2:
                {
                    Register();
                    break;
                }
            case 0:
            default:
                {
                    return false;
                }
        }
        return true;
    }


    private static bool DecideNextOption(int input)
    {
        switch (input)
        {
            case 1:
                {
                    RetrievingAvailableFunds();
                    break;
                }
            case 2:
                {
                    MakeDeposit();
                    break;
                }
            case 3:
                {
                    MakeWithdrawal();
                    break;
                }
            case 0:
            default:
                {
                    return false;
                }
        }

        return true;
    }

    private static void Login()
    {
        while (currentUser == null)
        {
            System.Console.WriteLine("Please Enter Your AccountHolder name: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string accountHolder = Console.ReadLine() ?? "";
            Console.ResetColor();

            System.Console.WriteLine("Please Enter Your Password: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string password = Console.ReadLine() ?? "";
            Console.ResetColor();

            //Setting the currentUser variable signifies Logging in. If Login() fails it will remain null.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            currentUser = userService.Login(accountHolder, password);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (currentUser == null)
                Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Login Failed. Please Try Again.");
            Console.ResetColor();
        }

        //Now that they are logged in -> send them to Main Menu.
        MainMenu();
        //When this MainMenu ends, so does this calling of Login() which means go
        //back to InitMenu().
    }

    private static void Register()
    {
        System.Console.WriteLine("Please Enter a New AccountHolder name: ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        string accountHolder = Console.ReadLine() ?? "";
        Console.ResetColor();


        System.Console.WriteLine("Please Enter a New Password: ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        string password = Console.ReadLine() ?? "";
        Console.ResetColor();


        //Lets not set an ID and assume their Role to be 'user'
        //My Register method chose a different tactic of passing in the whole User
        User? newUser = new(accountHolder, 0, password, "user");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        newUser = userService.Register(newUser); //should return the new User.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        if (newUser != null)
        {
            System.Console.WriteLine("New User Registered!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Registration Failed! Please Try Again!");
            Console.ResetColor();
        }
    }

    private static void RetrievingAvailableFunds()
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        Account getBalance = PromptUserForAccount();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        System.Console.WriteLine("Your available balance is:" + getBalance.Balance);
        System.Console.WriteLine("");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private static void MakeDeposit()
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        Account account = PromptUserForAccount();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        Console.ForegroundColor = ConsoleColor.Cyan;
        System.Console.WriteLine("Please enter amount to Deposit");
        decimal amount = decimal.Parse(Console.ReadLine() ?? "0");
        Console.ResetColor();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
        accountService.MakeDeposit(account, amount);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        System.Console.WriteLine("Your new balance is: " + account.Balance);
        System.Console.WriteLine("");

    }

    private static void MakeWithdrawal()
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        Account account = PromptUserForAccount();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        System.Console.WriteLine("Please enter amount of Withdrawal");
        Console.ForegroundColor = ConsoleColor.Cyan;
        decimal amount = decimal.Parse(Console.ReadLine() ?? "0");
        Console.ResetColor();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
        accountService.MakeWithdrawal(account, amount);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        System.Console.WriteLine("Your new balance is: " + account.Balance);
        System.Console.WriteLine("");
    }


    private static int ValidateCmd(int cmd, int maxOption)
    {
        while (cmd < 0 || cmd > maxOption)
        {
            System.Console.WriteLine("Invalid Command - Please Enter a command 1-" + maxOption + "; or 0 to Quit");
            Console.ForegroundColor = ConsoleColor.Cyan;
            cmd = int.Parse(Console.ReadLine() ?? "0");
            Console.ResetColor();
        }

        return cmd;
    }

    private static Account? PromptUserForAccount()
    {
        Account? retrievedAccount = null;
        while (retrievedAccount == null)
        {
            System.Console.WriteLine("Lets find your Account.");
            System.Console.WriteLine("Please enter an AccountNumber.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            int accountNumber = int.Parse(Console.ReadLine() ?? "0");
            Console.ResetColor();
            if (accountNumber == 0) return null;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            retrievedAccount = accountService.GetAccount(accountNumber);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        return retrievedAccount;
    }

}