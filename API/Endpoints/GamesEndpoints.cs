using API.Data;
using API.DTOs;
using API.Entities;
using API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";
    

    public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");

        group.MapGet("/",
            (GameStoreContext dbContext) => dbContext.Games.Include(game => game.Genre)
                .Select(game => game.ToGameSummaryDto()).AsNoTracking());

        group.MapGet("/{id:int}", (int id, GameStoreContext dbContext) =>
        {
            Game? game = dbContext.Games.Find(id);
            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        }).WithName(GetGameEndpointName);
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();
            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
        });


        group.MapPut("/{id:int}", (int id, UpdateGameDTO updateGame, GameStoreContext dbContext) =>
        {
            var existingGame = dbContext.Games.Find(id);

            dbContext.Entry(existingGame).CurrentValues.SetValues(updateGame.ToEntity(id));

            dbContext.SaveChanges();
            return Results.NoContent();
        });

        group.MapDelete("/{id:int}", (int id, GameStoreContext dbContext) =>
        {
            dbContext.Games.Where(game => game.Id == id).ExecuteDelete();
            return Results.NoContent();
        });
        return group;
    }
}