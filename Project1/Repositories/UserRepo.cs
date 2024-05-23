using System.Data;
using Microsoft.Data.SqlClient;

class UserRepo
{
    private readonly string _connectionString;
    public UserRepo(string connString)
    {
        _connectionString = connString;
    }

    public User? AddUser(User u)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        //Create the SQL String
        string sql = "INSERT INTO dbo.[User] OUTPUT inserted.* VALUES (@AccountHolder, @Password, @Role)";

        //Set up SqlCommand Object and use its methods to modify the Parameterized Values
        using SqlCommand cmd = new(sql, connection);
        cmd.Parameters.AddWithValue("@AccountHolder", u.AccountHolder);
        cmd.Parameters.AddWithValue("@Password", u.Password);
        cmd.Parameters.AddWithValue("@Role", u.Role);

        //Execute the Query
        // cmd.ExecuteNonQuery(); //This executes a non-select SQL statement (inserts, updates, deletes)
        using SqlDataReader reader = cmd.ExecuteReader();

        //Extract the Results
        if (reader.Read())
        {
            //If Read() found data -> then extract it.
            User newUser = BuildUser(reader); //Helper Method for doing that repetitive task
            return newUser;
        }
        else
        {
            //Else Read() found nothing -> Insert Failed. :(
            return null;
        }
    }

    public User? GetUser(int accountNumber)
    {
        try
        {
            //Set up DB Connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //Create the SQL String
            string sql = "SELECT * FROM dbo.[User] WHERE AccountNumber = @AccountNumber";

            //Set up SqlCommand Object
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);

            //Execute the Query
            using var reader = cmd.ExecuteReader();

            //Extract the Results
            if (reader.Read())
            {
                //for each iteration -> extract the results to a User object -> add to list.
                User newUser = BuildUser(reader);
                return newUser;
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

    public List<User> GetAllUsers()
    {
        List<User> users = [];

        try
        {
            //Set up DB Connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //Create the SQL String
            string sql = "SELECT * FROM dbo.[User]";

            //Set up SqlCommand Object
            using SqlCommand cmd = new(sql, connection);

            //Execute the Query
            using var reader = cmd.ExecuteReader(); //flexing options here with the use of var.

            //Extract the Results
            while (reader.Read())
            {
                //for each iteration -> extract the results to a User object -> add to list.
                User newUser = BuildUser(reader);

                //don't return! Instead Add to List!
                users.Add(newUser);
            }

            return users;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            System.Console.WriteLine(e.StackTrace);
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }

    public User? UpdateUser(User updatedUser)
    {
        try
        {
            //Set up DB Connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //Create the SQL String
            string sql = "UPDATE dbo.[User] SET AccountHolder = @AccountHolder, AccountNumber = @AccountNumber, Password = @Password, Role = @Role OUTPUT inserted.* WHERE Id = @Id";

            //Set up SqlCommand Object
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@AccountHolder", updatedUser.AccountHolder);
            cmd.Parameters.AddWithValue("@AccountNumber", updatedUser.AccountNumber);
            cmd.Parameters.AddWithValue("@Password", updatedUser.Password);
            cmd.Parameters.AddWithValue("@Role", updatedUser.Role);

            //Execute the Query
            using var reader = cmd.ExecuteReader();

            //Extract the Results
            if (reader.Read())
            {
                //for each iteration -> extract the results to a User object -> add to list.
                User newUser = BuildUser(reader);
                return newUser;
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

    public User? DeleteUser(User u)
    {

        try
        {
            //Set up DB Connection
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            //Create the SQL String
            string sql = "DELETE FROM dbo.[User] OUTPUT deleted.* WHERE AccountNumber = @AccountNumber";

            //Set up SqlCommand Object
            using SqlCommand cmd = new(sql, connection);
            cmd.Parameters.AddWithValue("@AccountNumber", u.AccountNumber);

            //Execute the Query
            using var reader = cmd.ExecuteReader();

            //Extract the Results
            if (reader.Read())
            {
                //for each iteration -> extract the results to a User object -> add to list.
                User newUser = BuildUser(reader);
                return newUser;
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
    private static User BuildUser(SqlDataReader reader)
    {
        User newUser = new();
        newUser.AccountHolder = (string)reader["AccountHolder"];
        newUser.AccountNumber = (int)reader["AccountNumber"];
        newUser.Password = (string)reader["Password"];
        newUser.Role = (string)reader["Role"];

        return newUser;
    }

}
