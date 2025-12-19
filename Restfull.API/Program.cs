using Microsoft.EntityFrameworkCore;
using Restfull.Infrastructure.Data;
using Restfull.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        builder => builder
            .WithOrigins("https://localhost:5001", "http://localhost:5000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Add Swagger вместо OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

// Add services
builder.Services.AddScoped<ResourceManager>();

var app = builder.Build();

app.UseCors(policy =>
{
    policy.WithOrigins("https://localhost:5001", "http://localhost:5000")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials();
});

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "RESTfull API is running! Go to /swagger for API documentation");

app.Run();