using System.Reflection;
using DbUp;

// var connectionString = args.FirstOrDefault() ??
//                        Environment.GetEnvironmentVariable("DBUP_CONNECTION_STRING");

var connectionString = "Server=127.0.0.1,1433;Database=RealEstateDatabase2;User=sa;Password=Test123.!;TrustServerCertificate=true;";

EnsureDatabase.For.SqlDatabase(connectionString);

var builder =
    DeployChanges.To.SqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), (scriptName) => !scriptName.Contains("ManualRun"))
        .LogToConsole();

var result = builder.Build().PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
    #if DEBUG
    Console.ReadLine();
    #endif
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();
