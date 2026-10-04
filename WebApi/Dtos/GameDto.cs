namespace WebApi.Dtos;

public record  GameDto(
    int Id,
    string Name,
    string Description,
    string Genre,
    decimal Price,
    DateTime ReleaseDate
);
