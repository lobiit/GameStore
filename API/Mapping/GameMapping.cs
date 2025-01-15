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

    public static GameDto ToDto(this Game game)
    {
        return new(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }
}