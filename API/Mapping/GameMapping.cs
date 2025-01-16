using API.DTOs;
using API.Entities;

namespace API.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto game)
    {

        return new Game()
        {  
            GenreId = game.GenreId,
            Name = game.Name,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        return new(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }
    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {
        return new(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }
    
    public static Game ToEntity(this UpdateGameDTO game, int id)
    {

        return new Game()
        {  Id = id,
            GenreId = game.GenreId,
            Name = game.Name,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }
}