using ApiWebFilme.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FilmesContext>(options =>
{
    options.UseSqlite("Data Source=filme.db");
});

builder.Services.AddSingleton<AwardIntervalCalculator>();
builder.Services.AddScoped<IFilmesRepository, FilmesRepository>();
builder.Services.AddScoped<IObterPremiosRepository, ObterPremiosRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();

public partial class Program { }
