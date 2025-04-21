using Serilog;

using Microsoft.EntityFrameworkCore;

using TallerWebM.src.Data;
using TallerWebM.src.Data.Seeder;

Log.Logger = new LoggerConfiguration()

    .CreateLogger();

try
{
    Log.Information("starting server.");
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddDbContext<StoreContext>(options => {
        options.UseSqlite("Data Source=app.db");
        options.EnableSensitiveDataLogging();
    });
    
    builder.Services.AddScoped<IUserSeeder,UserSeeder>();
    builder.Services.AddScoped<IProductSeeder,ProductSeeder>();
    builder.Services.AddControllers();
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithMachineName();
    });

    var app = builder.Build();
    using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var UserSeeder = services.GetRequiredService<IUserSeeder>();
    var ProductSeeder = services.GetRequiredService<IProductSeeder>();

    
    UserSeeder.Seed();
    ProductSeeder.Seed();
    

}
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "server terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}