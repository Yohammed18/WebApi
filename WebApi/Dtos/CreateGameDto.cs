namespace WebApi.Dtos;

public record  CreateGameDto
(
    string Name,
    string Description,
    string Genre,
    decimal Price,
    DateTime ReleaseDate
);
