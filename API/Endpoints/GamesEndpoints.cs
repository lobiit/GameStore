using API.Data;
using API.DTOs;
using API.Entities;
using API.Mapping;

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

    public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("games");
        
        group.MapGet("/", () => games).WithName("Games");

        group.MapGet("/{id:int}", (int id) =>
        {
            GameDto? game = games.Find(g => g.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndpointName);
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            
            Game game = newGame.ToEntity();
            game.Genre = dbContext.Genres.Find(newGame.GenreId);

            dbContext.Games.Add(game);
            dbContext.SaveChanges();
    
            return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game.ToDto());
        });


        group.MapPut("/{id:int}", (int id, UpdateGameDTO updateGame) =>
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

        group.MapDelete("/{id:int}", (int id) =>
        {
            var index = games.RemoveAll(g => g.Id == id);
    
            return Results.NoContent();
        });
        return group;
    }
}
