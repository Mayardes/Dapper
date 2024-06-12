using Balta_Dapper.UsingDapper;
using Balta_Dapper.UsingRawADO.Net;



const string connectionString = "Server=localhost;Database=balta;User Id=SA; Password=@Al126812; TrustServerCertificate=true";

//This example shows a simple communication with Database using ADO.net as library.
#region ConnectionUsingADOnet

var example = new Example();
example.OpenConnection(connectionString);

#endregion


#region ConnectionUsingDapper

var exampleDapper = new ExampleDapper();
exampleDapper.OpenConnection(connectionString);

#endregion

