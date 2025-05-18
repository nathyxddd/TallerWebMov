using Serilog;
using TallerWebM.src.Data.Seeder;
using TallerWebM.src.Services.Interface;
using TallerWebM.src.Services.Implements;
using TallerWebM.src.Data;
using Microsoft.EntityFrameworkCore;
using TallerWebM.src.Services.Interfaces.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TallerWebM.src.Repository;
using TallerWebMov.src.Repository.Interfaces;
using TallerWebMov.src.Repository.Implements;

// Configura un logger utilizando Serilog
Log.Logger = new LoggerConfiguration()

    .CreateLogger(); // Se crea el logger configurado

try
{
    // Se registra el inicio del servidor.
    Log.Information("starting server.");

    // Se crea un nuevo objeto WebApplication para configurar el servidor.
    var builder = WebApplication.CreateBuilder(args);

    if (builder.Configuration["Cloudinary:Url"] == "")
    {
        Console.WriteLine("Debes definir la URL de Cloudinary primero");
        return;
    }

    var allowedOrigins = builder.Configuration.GetSection("CorsSettings:Allowed").Get<string[]>();

    if (allowedOrigins == null)
    {
        allowedOrigins = [];
    }
    builder.Services.AddCors(options =>
    {
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .WithHeaders(
                  "Content-Type",     
                  "Authorization",    
                  "Accept",           
                  "X-Requested-With"  
            )
            .AllowCredentials();
      });
    });

    builder.Services.AddDbContext<StoreContext>(options => {
        // Se define que la base de datos se almacenará en un archivo SQLite llamado app.db.
        options.UseSqlite("Data Source=app.db");
        // Se habilita el registro de datos sensibles.
        options.EnableSensitiveDataLogging();
    });

    builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
        };
    });

    // Se registran las dependencias necesarias para la inserción de datos.
    // Se inyecta la dependencia de UserSeeder en la aplicación.
    builder.Services.AddScoped<IUserSeeder,UserSeeder>();

    // Se inyecta la dependencia de ProductSeeder en la aplicación.
    builder.Services.AddScoped<IProductSeeder,ProductSeeder>();

    builder.Services.AddScoped<IProductCreationMapper, ProductCreationMapper>();

    builder.Services.AddScoped<IUserRepository, UserRepository>();

    builder.Services.AddScoped<IProductRepository, ProductRepository>();

    builder.Services.AddScoped<IRolesRepository, RoleRepository>();

    builder.Services.AddScoped<IPhotoService, PhotoService>();

    builder.Services.AddScoped<IProductService, ProductService>();

    builder.Services.AddScoped<IAuthenticateServices, AuthenticationService>();

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

    var storeContext = services.GetRequiredService<StoreContext>();

    storeContext.Database.EnsureCreated();

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

    Console.WriteLine(ex.Message);
    Log.Fatal(ex, "server terminated unexpectedly");
}
finally
{
    Console.WriteLine("xxxx ?");
    // Se cierra y limpia los recursos de Serilog cuando el servidor se apaga.
    Log.CloseAndFlush();
}