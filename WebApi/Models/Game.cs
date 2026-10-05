using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int GenreId { get; set; }

    [Precision(5, 2)]
    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
}