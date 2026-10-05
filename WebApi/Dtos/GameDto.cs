using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos;

public record GameDto(
    int Id,
    [Required]
    [StringLength(25)]
    string Name,
    [StringLength(100)]
    string Description,
    int GenreId,
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
    decimal Price,
    DateOnly ReleaseDate
);