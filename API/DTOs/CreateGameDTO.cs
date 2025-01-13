namespace API.DTOs;

public record CreateGameDTO(string Name, string Genre, decimal Price, DateOnly? ReleaseDate);