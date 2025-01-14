using API.DTOs;

namespace API.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName ="GetGame";
    
    private static readonly List<GameDto> games =
    [
        new(
            1,
            "Street Fighter 11",
            "Fighting",
            19.99M,
            new DateOnly(1999, 04, 09)
        ),
        new(
            2,
            "E Football",
            "Sports",
            49.99M,
            new DateOnly(2025, 04, 09)
        ),
        new(
            3,
            "FIFA 24",
            "Sports",
            59.79M,
            new DateOnly(2024, 12, 09)
        ),
    ];

    public static WebApplication MapGameEndpoints(this WebApplication app)
    {
        app.MapGet("games", () => games).WithName("Games");

        app.MapGet("games/{id:int}", (int id) =>
        {
            GameDto? game = games.Find(g => g.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndpointName);
        app.MapGet("/", () => "Hello World!");

        app.MapPost("games", (CreateGameDTO newGame) =>
        {
            GameDto game = new(
                games.Capacity + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);
    
            return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
        });


        app.MapPut("games/{id:int}", (int id, UpdateGameDTO updateGame) =>
        {
            var index = games.FindIndex(g => g.Id == id);

            games[index] = new GameDto(
                id,
                updateGame.Name,
                updateGame.Genre,
                updateGame.Price,
                updateGame.ReleaseDate
            );
    
            return Results.NoContent();
        });

        app.MapDelete("games/{id:int}", (int id) =>
        {
            var index = games.RemoveAll(g => g.Id == id);
    
            return Results.NoContent();
        });
        return app;
    }
}
