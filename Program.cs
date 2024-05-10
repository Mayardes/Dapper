using System.Data;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=BaltaDapper;User Id=sa; Password=@Al126812";

using (var connection = new SqlConnection(connectionString))
{
    connection.Open();
    Console.WriteLine("Connected!");

    using (var command = new SqlCommand())
    {
        command.Connection = connection;
        command.CommandType = CommandType.Text;
        command.CommandText = "SELECT [Id], [Title] FROM [Category]";

        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"Id: {reader.GetGuid(0)} Name: {reader.GetString(1)}");
        }
    }
}
