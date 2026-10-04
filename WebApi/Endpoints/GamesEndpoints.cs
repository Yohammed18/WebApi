using WebApi.Dtos;

namespace WebApi.Endpoints;

public static class GamesEndpoints
{
    // games list 
    public static readonly List<GameDto> games =
    [
        new GameDto(1, "Call of Duty", "War game", "Fighting", 19.99m, new DateTime(2022, 1, 1)),
        new GameDto(2, "GTA V", "Open-world game", "Action", 29.99m, new DateTime(2022, 2, 1)),
        new GameDto(3, "The Witcher 3", "RPG game", "Fantasy", 39.99m, new DateTime(2022, 3, 1))
    ];

    const string EndpointName = "GetGameById"; 

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // get all games
        group.MapGet("", () => games);

        // get game by id
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return Results.NotFound("Game not found");
            }
            return Results.Ok(game);
        }).WithName(EndpointName);


        // add a new game 
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new GameDto(
                games.Count + 1,
                newGame.Name,
                newGame.Description,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(EndpointName, new { id = game.Id }, game);
        });



        // update a game PUT
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            // get the game to update
            var game = games.FirstOrDefault(g => g.Id == id);

            // check if the game exists
            if (game == null)
            {
                return Results.NotFound("Game not found");
            }


            games.Remove(game); // remove the old game
            GameDto updated = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Description,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            games.Add(updated);// add the updated game

            return Results.Ok(updated); // return the updated game
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