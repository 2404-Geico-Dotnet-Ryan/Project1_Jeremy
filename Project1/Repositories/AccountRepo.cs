using System.Data;
using Microsoft.Data.SqlClient;

class AccountRepo
{
    private readonly string _connectionString;
    public AccountRepo(string connString)
    {
        _connectionString = connString;
    }
    /*
        Create an access layer by which a new Account can be made and added to storage 
        or an existing Account can be read from storage:
    */

    //Create Account:
    public Account? AddUser(Account a)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        //Create the SQL String
        string sql = "INSERT INTO dbo.Account OUTPUT inserted.* VALUES (@TransactionType, @TransactionDate, @Amount, @Balance)";

        //Set up SqlCommand Object and use its methods to modify the Parameterized Values
        using SqlCommand cmd = new(sql, connection);
        cmd.Parameters.AddWithValue("@TransactionType", a.TransactionType);
        cmd.Parameters.AddWithValue("@TransactionDate", a.TransactionDate);
        cmd.Parameters.AddWithValue("@Amount", a.Amount);
        cmd.Parameters.AddWithValue("@Balance", a.Balance);

        //Execute the Query
        // cmd.ExecuteNonQuery(); //This executes a non-select SQL statement (inserts, updates, deletes)
        using SqlDataReader reader = cmd.ExecuteReader();

        //Extract the Results
        if (reader.Read())
        {
            //If Read() found data -> then extract it.
            Account newAccount = BuildAccount(reader); //Helper Method for doing that repetitive task
            return newAccount;
        }
        else
        {
            //Else Read() found nothing -> Insert Failed. :(
            return null;
        }
    }

    //Read Account:
    public Account? GetAccount(int accountNumber) //retrieving Account, returning Account, message if not found:
    {
        try
        {
            //Set up DB Connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //Create the SQL String
            string sql = "SELECT * FROM dbo.Account WHERE AccountNumber = @AccountNumber";

            //Set up SqlCommand Object
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

            //Execute the Query
            using var reader = cmd.ExecuteReader();

            //Extract the Results
            if (reader.Read())
            {
                //for each iteration -> extract the results to a User object -> add to list.
                Account newAccount = BuildAccount(reader);
                return newAccount;
            }

            return null; //Didnt find anyone :(

        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    public Account? UpdateAccount(Account updatedAccount)
    {
        //Assuming that the AccountNumber is consistent with an AccountNumber that exists
        //then we just have to update the Account for said AccountNumber within the Dictionary.
        try
        {
            //Set up DB Connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //Create the SQL String
            string sql = "UPDATE dbo.Account SET TransactionType = @TransactionType, TransactionDate = @TransactionDate, Amount = @Amount, Balance = @Balance, UserAccountNumber = @UserAccountNumber OUTPUT inserted.* WHERE AccountNumber = @AccountNumber";

            //Set up SqlCommand Object
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@AccountNumber", updatedAccount.AccountNumber);
            cmd.Parameters.AddWithValue("@TransactionType", updatedAccount.TransactionType);
            cmd.Parameters.AddWithValue("@TransactionDate", updatedAccount.TransactionDate);
            cmd.Parameters.AddWithValue("@Amount", updatedAccount.Amount);
            cmd.Parameters.AddWithValue("@Balance", updatedAccount.Balance);
            cmd.Parameters.AddWithValue("@UserAccountNumber", updatedAccount.UserAccountNumber);

            //Execute the Query
            using var reader = cmd.ExecuteReader();

            //Extract the Results
            if (reader.Read())
            {
                //for each iteration -> extract the results to a User object -> add to list.
                Account newAccount = BuildAccount(reader);
                return newAccount;
            }

            return null; //Didnt find anyone :(

        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    public Account? DeleteAccount(Account a)
    {
        try
        {
            //Set up DB Connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //Create the SQL String
            string sql = "DELETE FROM dbo.Account OUTPUT deleted.* WHERE AccountNumber = @AccountNumber";

            //Set up SqlCommand Object
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@AccountNumber", a.AccountNumber);

            //Execute the Query
            using var reader = cmd.ExecuteReader();

            //Extract the Results
            if (reader.Read())
            {
                //for each iteration -> extract the results to a User object -> add to list.
                Account newAccount = BuildAccount(reader);
                return newAccount;
            }

            return null; //Didnt find anyone :(

        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
            return null;
        }
    }

    //Helper Method - used above in AddUser and GetAllUser
    private static Account BuildAccount(SqlDataReader reader)
    {
        Account newAccount = new();
        newAccount.AccountNumber = (int)reader["AccountNumber"];
        newAccount.TransactionType = (string)reader["TransactionType"];
        newAccount.TransactionDate = (long)reader["TransactionDate"];
        newAccount.Amount = (decimal)reader["Amount"];
        newAccount.Balance = (decimal)reader["Balance"];
        newAccount.UserAccountNumber = (int)reader["UserAccountNumber"];

        return newAccount;
    }
}