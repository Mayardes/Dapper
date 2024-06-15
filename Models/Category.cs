namespace Balta_Dapper.Models;

public record Category(Guid? Id, string Title, string Url, string Summary, int Order, string Description, bool Featured);