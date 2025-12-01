using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Portfolio.Service.Db;
using Portfolio.Service.Misc;
using Portfolio.Service.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to DI
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "MyPortfolio Backend API",
        Version = "v1",
        Description = "ASP.NET Core Web API backend for the MyPortfolio application.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Mahesh Kumar S",
            Email = "ssmahesh001@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/mahesh-kumar-selvaraj-b866591ab/")
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// SQLite DB
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var sqliteBuilder = new SqliteConnectionStringBuilder(connString)
    {
        ForeignKeys = true
    };
    opt.UseSqlite(sqliteBuilder.ToString());
});

// CORS
var enableCors = builder.Configuration.GetValue<bool>("AppConfig:EnableCors");
var corsUrls = builder.Configuration.GetSection("AppConfig:CorsUrls").Get<string[]>() ?? [];

if (enableCors && corsUrls.Length > 0)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Dev", policy =>
        {
            policy.WithOrigins(corsUrls)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials()
                  .WithExposedHeaders("Pagination-Metadata", "Content-Disposition");
        });
    });
}

// DI Services
builder.Services
    .AddScoped<Portfolio.Service.Contract.IUserService, UserService>()
    .AddScoped<Portfolio.Service.Contract.IUserDetailsService, UserDetailsService>()
    .AddScoped<Portfolio.Service.Contract.IExperienceService, ExperienceService>()
    .AddScoped<Portfolio.Service.Contract.IProjectService, ProjectService>()
    .AddScoped<Portfolio.Service.Contract.IResumeService, ResumeService>();

var app = builder.Build();

// Logging
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("=== Starting application on Render ===");

// Migrate DB
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        logger.LogInformation("Applying migrations...");
        db.Database.Migrate();
        logger.LogInformation("Migrations applied successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error applying migrations");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

// CORS
if (enableCors && corsUrls.Length > 0)
    app.UseCors("Dev");

app.UseAuthorization();

// Map API controllers
app.MapControllers();



app.Run();
