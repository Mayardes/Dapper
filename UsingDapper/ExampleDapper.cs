using Balta_Dapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Balta_Dapper.UsingDapper;

public class ExampleDapper
{
    public void OpenConnection(string connectionString)
    {
        Console.WriteLine("Example using Dapper:");
        using var connection = new SqlConnection(connectionString);
        var categories = connection.Query<Category>("SELECT [Id], [Title] FROM Category");

        foreach(var category in categories)
        {
            Console.WriteLine($"Id: {category.Id}, Title {category.Title}");
        }
    }
}