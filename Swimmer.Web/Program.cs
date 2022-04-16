using Swimmer.Infrastructure;

/*var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();*/

const string Path = @"C:\Users\Max\OneDrive\Документы\Соревнования\20-21 ноября 21\Стартовый оригинал 1 день.xlsx";
using var file = File.Open(Path, FileMode.Open);
var service = new ExcelCompetitionImportService();
var competition = service.ParseFile(file);

