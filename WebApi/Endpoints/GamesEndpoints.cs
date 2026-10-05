using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Endpoints;

public static class GamesEndpoints
{
    // games list 
    public static readonly List<GameDto> games =
    [

    ];

    const string EndpointName = "GetGameById";


    /// <summary>
    /// Maps the game endpoints for the web application.
    /// </summary>
    /// <param name="app">The web application instance.</param>
    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // get all games
        group.MapGet("/", async (GameStoreContext context) =>
            await context.Games
                .Select(g => new GameDto(
                    g.Id,
                    g.Name,
                    g.Description,
                    g.GenreId,
                    g.Price,
                    g.ReleaseDate))
                .AsNoTracking()
                .ToListAsync());



        // get game by id
        group.MapGet("/{id}", async (GameStoreContext context, int id) =>
        {
            var game = await context.Games.FindAsync(id);

            if (game == null)
            {
                return Results.NotFound("Game not found");
            }
            return Results.Ok(game);

        }).WithName(EndpointName);


        // add a new game 
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext context) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                Description = newGame.Description,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            await context.Games.AddAsync(game);
            await context.SaveChangesAsync();


            GameDetailsDto gameDetail = new GameDetailsDto(
                game.Id,
                game.Name,
                game.Description,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );

            return Results.CreatedAtRoute(EndpointName, new { id = game.Id }, gameDetail);
        });



        // update a game PUT
        _ = group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            // get the game to update
            var game = games.FirstOrDefault(g => g.Id == id);

            // check if the game exists
            if (game == null)
            {
                return Results.NotFound("Game not found");
            }


            games.Remove(game); // remove the old game
                                // GameDto updated = new GameDto(

            // );

            // games.Add(updated);// add the updated game

            return Results.Ok(); // return the updated game
        });


        // delete a game
        group.MapDelete("/{id}", (int id) =>
        {
            int deletedCount = games.RemoveAll(g => g.Id == id); // returns 1 if a game was deleted, 0 otherwise

            if (deletedCount == 0)
                return Results.NotFound($"Game with ID {id} not found");
            else
                return Results.Content("Game deleted successfully");
        });

    }

}