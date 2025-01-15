using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public record CreateGameDto(
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, ErrorMessage = "Name must not exceed 50 characters.")]
    string Name,
    
    int GenreId,
    
    [Range(1, 100, ErrorMessage = "Price must be between 1 and 100.")]
    decimal Price,
    
    DateOnly ReleaseDate
);
