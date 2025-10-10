var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

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
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
