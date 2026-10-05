
using Microsoft.EntityFrameworkCore;

namespace WebApi.Data;

public static class DataExtensions
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        await context.Database.MigrateAsync();
    }

}
