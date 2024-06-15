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
        var categories = connection.Query<Category>("SELECT [Id], [Title], [Url], [Summary], [Order], [Description], [Featured] FROM [Category]");

        foreach(var category in categories)
        {
            Console.WriteLine($"Id: {category.Id}, Title {category.Title}");
        }

        Console.WriteLine("Executing inserting data:");
        var inserting = Inserting(connection);
        Console.WriteLine($"rows affected: {inserting}");

        Console.WriteLine("Executing updating data:");

        Guid id = new Guid("f9899100-f578-4e2d-87cd-febb620bb255");
        string description = "new description";

        var updating = Updating(connection, description, id);
        Console.WriteLine($"rows affected: {updating}"); 
    }

    private static int Inserting(SqlConnection sqlConnection)
    {
        string insert = @"INSERT INTO [Category] VALUES (@id, @title, @url, @summary, @order, @description, @featured)";

        var category = new Category(Id: Guid.NewGuid(), Title: "Amazon AWS Storage", Url: "https://aws.amazon.com/", Summary: "Storage Cloud", Order: 0, Description: "Service for stored data", Featured: false);

        return sqlConnection.Execute(insert, new { category.Id, category.Title, category.Url, category.Summary, category.Order,category.Description, category.Featured});
    }

    private static int Updating(SqlConnection sqlConnection, string description, Guid id)
    {
        string update = "UPDATE [Category] SET Description = @Description WHERE Id = @Id";

        return sqlConnection.Execute(update, new {Description = description, Id = id});
    }
}