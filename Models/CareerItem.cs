namespace Balta_Dapper.Models;

public class CareerItem
{
    public Guid Id {get; set;}
    public Guid CareerId {get; set;} 
    public Guid CourseId {get; set;} 
    public string Title {get; set;} 
    public string Description {get; set;}
    public Course Course {get; set;}
}