using System.Data;
using Microsoft.Data.SqlClient;

namespace Balta_Dapper.UsingRawADO.Net;

public class Example
{
   public void OpenConnection(string connectionString)
   {
        Console.WriteLine("Shows a example communications using ADO.net.");
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.Text;
        command.CommandText = "SELECT [Id], [Title] FROM [Category]";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Id: {reader.GetGuid(0)}, Title: {reader.GetString(1)}");
        }
    }
}