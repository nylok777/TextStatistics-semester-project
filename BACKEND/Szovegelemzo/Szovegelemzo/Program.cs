using Szovegelemzo.Logic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITextAnalyzer, TextAnalyzer>();
var app = builder.Build();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapGet("/", () => "Hello World!");

app.Run();
