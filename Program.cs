using Microsoft.EntityFrameworkCore;
using StreamVid.Data;
using StreamVid.Services;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Register services
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IMovieService, MovieService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
