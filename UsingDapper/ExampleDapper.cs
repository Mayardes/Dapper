using System.Data;
using System.Net.NetworkInformation;
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

        Console.WriteLine("Executing inserting many data:");
        var insertMany = InsertMany(connection);
        Console.WriteLine($"rows affected: {insertMany}");

        Console.WriteLine("Executing stored procedure:");
        var storedProcedure = ExecuteStoredProcedure(connection);
        Console.WriteLine($"rows affected: {storedProcedure}");

        Console.WriteLine("Executing stored procedure:");
        ExecuteReadingStoredProcedure(connection);

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

    private static int InsertMany(SqlConnection sqlConnection)
    {
        string insertMany = "INSERT INTO [Category] VALUES (@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

        var googleCloudy = new Category(Id: Guid.NewGuid(), Title: "Google Cloud", Url: "https://cloud.google.com/", Summary:"Google Storage", Order: 1, Description: "Google Drive, part of Google Workspace, lets you securely store, intelligently organize and collaborate on files and folders from anywhere, on any device", Featured: false);
        var azureDevops = new Category(Id: Guid.NewGuid(), Title: "Azure Devops", Url: "https://azure.microsoft.com/", Summary: "Azure Devops", Order: 1, Description: "Azure DevOps is an end-to-end software development platform", Featured: true);

        return sqlConnection.Execute(insertMany, new [] { 
            new {googleCloudy.Id, googleCloudy.Title, googleCloudy.Url, googleCloudy.Summary, googleCloudy.Order, googleCloudy.Description, googleCloudy.Featured}, 
            new {azureDevops.Id, azureDevops.Title, azureDevops.Url, azureDevops.Summary, azureDevops.Order, azureDevops.Description, azureDevops.Featured}});
    }

    private static int ExecuteStoredProcedure(SqlConnection sqlConnection)
    {
        var execute = "EXEC [spDeleteStudent] @StudentId";
        var parameter = new { StudentId = new Guid("be022541-1e54-494c-bf94-40a71e9f0b36") };

        return sqlConnection.Execute(execute, parameter);
    }

    private static void ExecuteReadingStoredProcedure(SqlConnection connection)
    {
        string execute = "EXEC [spGetCoursesByCategory] @CategoryId";
        var parameter = new { CategoryId = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4")};

        var categories = connection.Query(execute, parameter);

        foreach(var category in categories)
        {
            Console.WriteLine($"Id: {category.Id}, Title {category.Title}");
        }

    }
}