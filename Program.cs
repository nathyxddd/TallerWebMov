using Serilog;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Data;
using TallerWebM.src.Data.Seeder;

// Configura un logger utilizando Serilog
Log.Logger = new LoggerConfiguration()

    .CreateLogger(); // Se crea el logger configurado

try
{
    // Se registra el inicio del servidor.
    Log.Information("starting server.");

    // Se crea un nuevo objeto WebApplication para configurar el servidor.
    var builder = WebApplication.CreateBuilder(args);

    // Se configura el contexto de la base de datos utilizando SQLite.
    builder.Services.AddDbContext<StoreContext>(options => {
        // Se define que la base de datos se almacenará en un archivo SQLite llamado app.db.
        options.UseSqlite("Data Source=app.db");
        // Se habilita el registro de datos sensibles.
        options.EnableSensitiveDataLogging();
    });

    // Se registran las dependencias necesarias para la inserción de datos.
    // Se inyecta la dependencia de UserSeeder en la aplicación.
    builder.Services.AddScoped<IUserSeeder,UserSeeder>();

    // Se inyecta la dependencia de ProductSeeder en la aplicación.
    builder.Services.AddScoped<IProductSeeder,ProductSeeder>();

    // Se registran los controladores de la aplicación.
    builder.Services.AddControllers();

    // Se configura el uso de Serilog para el registro de logs.
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithMachineName();
    });

    // Se construye la aplicación con las configuraciones anteriores.
    var app = builder.Build();

    // Se crea un ámbito de servicio para obtener los servicios registrados y ejecutar la inserción de datos.
    using (var scope = app.Services.CreateScope())
{
    // Se obtiene al proveedor de servicios.
    var services = scope.ServiceProvider;

    // Se obtiene el servicio IUserSeeder.
    var UserSeeder = services.GetRequiredService<IUserSeeder>();

    // Se obtiene el servicio IProductSeeder.
    var ProductSeeder = services.GetRequiredService<IProductSeeder>();

    // Se ejecuta la inserción de datos de usuarios.
    UserSeeder.Seed();

    // Se ejecuta la inserción de datos de productos.
    ProductSeeder.Seed();


}
    // Mapea las rutas para los controladores de la aplicación.
    app.MapControllers();

    // Inicia la aplicación.
    app.Run();
}

// Se captura cualquier excepción que ocurra durante la ejecución.
catch (Exception ex)
{
    // Se registra un mensaje de error fatal si el servidor termina inesperadamente.
    Log.Fatal(ex, "server terminated unexpectedly");
}
finally
{
    // Se cierra y limpia los recursos de Serilog cuando el servidor se apaga.
    Log.CloseAndFlush();
}