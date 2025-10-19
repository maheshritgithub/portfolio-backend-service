using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Portfolio.Service.Db;
using Portfolio.Service.Misc;
using Portfolio.Service.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Configure DbContext with SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var newConnectionString = new SqliteConnectionStringBuilder(connectionString)
    {
        ForeignKeys = true,
        Pooling = true,
        Cache = SqliteCacheMode.Default
    };

    options.UseSqlite(newConnectionString.ToString());
});

var enableCors = builder.Configuration.GetValue<bool>("AppConfig:EnableCors");

var corsUrls = builder.Configuration.GetSection("AppConfig:CorsUrls").Get<string[]>() ?? [];

if (enableCors && corsUrls.Length > 0)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Dev",
            builder => builder.WithOrigins(corsUrls)
                .AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyMethod()
                .WithExposedHeaders("Pagination-Metadata", "Content-Disposition")
        );
    });
}

// Register application services
builder.Services
    .AddScoped<Portfolio.Service.Contract.IUserService, UserService>()
    .AddScoped<Portfolio.Service.Contract.IUserDetailsService, UserDetailsService>()
    .AddScoped<Portfolio.Service.Contract.IExperienceService, ExperienceService>();

// Add Swagger/OpenAPI
// Add Swagger/OpenAPI
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "MyPortfolio Backend API",
        Version = "v1",
        Description = "ASP.NET Core Web API backend for the MyPortfolio application. " +
                      "Provides endpoints for managing profile, certificates, and projects for multiple users.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Mahesh Kumar S",
            Email = "ssmahesh001@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/mahesh-kumar-selvaraj-b866591ab/")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Include XML comments
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Create logger instance
var logger = app.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("=== Starting application ===");

// Apply migrations automatically at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        logger.LogInformation("Checking database and applying migrations...");

        db.Database.Migrate();

        logger.LogInformation("Database migration applied successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while applying database migrations.");
    }
}

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

if (enableCors && corsUrls.Length > 0)
    app.UseCors("Dev");

logger.LogInformation("Application startup complete. Listening for requests...");

app.Run();
