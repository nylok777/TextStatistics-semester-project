using Szovegelemzo.Data;
using Szovegelemzo.Logic;
using Szovegelemzo.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddCors();
builder.Services.AddSingleton<TextDataRepository>();
builder.Services.AddSingleton<ITextAnalyzer, TextAnalyzer>();
var app = builder.Build();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapGet("/", () => "Hello World!");

app.UseCors(x => x
.AllowCredentials()
.AllowAnyMethod()
.AllowAnyHeader()
.WithOrigins("http://localhost:5500"));

app.Run();
