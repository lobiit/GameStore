using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public record CreateGameDto(
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, ErrorMessage = "Name must not exceed 50 characters.")]
    string Name,
    
    [Required(ErrorMessage = "Genre is required.")]
    [StringLength(20, ErrorMessage = "Genre must not exceed 20 characters.")]
    string Genre,
    
    [Range(1, 100, ErrorMessage = "Price must be between 1 and 100.")]
    decimal Price,
    
    DateOnly? ReleaseDate
);
