using Microsoft.Extensions.Hosting;
using System;

try
{
    Host.CreateDefaultBuilder(args)
        .Build()
        .Run();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(ex.ToString());
}
