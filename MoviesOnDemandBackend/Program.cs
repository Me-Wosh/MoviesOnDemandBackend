using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Middleware;
using MoviesOnDemandBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<MoviesOnDemandDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesOnDemandDB"));
});
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();