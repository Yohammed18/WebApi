namespace WebApi.Dtos;

public record class UpdateGameDto
(
    string Name,
    string Description,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
