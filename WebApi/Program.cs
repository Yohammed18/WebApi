using WebApi.Dtos;
using WebApi.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var app = builder.Build();

// get home page
app.MapGet("/", () => "Welcome to the Game API!");

app.MapGamesEndpoints();

app.Run();
