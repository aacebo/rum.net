using Npgsql;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton((_) =>
{
    return new NpgsqlDataSourceBuilder("Host=localhost;Database=main;Username=admin;Password=admin").Build();
});

var app = builder.Build();

app.MapControllers();
app.Run();