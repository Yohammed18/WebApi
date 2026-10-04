using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
// Add Entity Framework DbContext
builder.Services.AddDbContext<GameStoreContext>(options =>
{
    // Configure SQL Server connection
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
});

// builder.Services.AddSqlServer<GameStoreContext>(builder.Configuration.GetConnectionString("DefaultConnection")); 

var app = builder.Build();

// get home page
app.MapGet("/", () => "Welcome to the Game API!");

app.MapGamesEndpoints();

app.Run();
