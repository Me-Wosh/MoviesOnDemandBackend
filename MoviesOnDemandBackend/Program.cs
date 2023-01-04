using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MoviesOnDemandBackend.Entities;
using MoviesOnDemandBackend.Middleware;
using MoviesOnDemandBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<MoviesOnDemandDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesOnDemandDB"));
});
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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