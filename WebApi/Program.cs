using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Endpoints;
using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

Genre[] seedGenres =
[
    new() { Name = "Action" },
    new() { Name = "Adventure" },
    new() { Name = "RPG" },
    new() { Name = "Strategy" },
    new() { Name = "Racing" },
    new() { Name = "Sports" }
];

builder.Services.AddSqlServer<GameStoreContext>(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    optionsAction: options => options
        .UseSeeding((context, _) =>
        {
            if (!context.Set<Genre>().Any())
            {
                context.Set<Genre>().AddRange(seedGenres);
                context.SaveChanges();                      // ← was missing
            }
        })
        .UseAsyncSeeding(async (context, _, ct) =>          // ← async twin
        {
            if (!await context.Set<Genre>().AnyAsync(ct))
            {
                context.Set<Genre>().AddRange(seedGenres);
                await context.SaveChangesAsync(ct);
            }
        })
);

var app = builder.Build();

await app.MigrateDatabaseAsync();

app.MapGet("/", () => "Welcome to the Game API!");
app.MapGamesEndpoints();

app.Run();