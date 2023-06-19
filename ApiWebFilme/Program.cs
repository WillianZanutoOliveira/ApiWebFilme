using ApiWebFilme.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Configura��es do banco de dados
builder.Services.AddDbContext<FilmesContext>(options =>
{
    options.UseSqlite("Data Source=filme.db");
});

builder.Services.AddMvc();
builder.Services.AddScoped<IFilmesRepository, FilmesRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
public partial class Program { }