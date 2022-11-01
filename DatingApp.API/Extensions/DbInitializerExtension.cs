using DatingApp.API.Data;

internal static class DbInitializerExtension
{
    public static async Task<IApplicationBuilder> UseItToSeedSqlServerAsync(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<DataContext>();
            await Seed.SeedUsers(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration");
        }

        return app;
    }
}