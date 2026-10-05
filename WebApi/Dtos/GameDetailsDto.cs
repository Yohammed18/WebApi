namespace WebApi.Dtos;

public record GameDetailsDto
(
    int Id,
    string Name,
    string Description,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);