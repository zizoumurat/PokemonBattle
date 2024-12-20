using PokemonBattle.Application.Interfaces;
using PokemonBattle.Application.Services;
using PokemonBattle.Infrastructure.Clients;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient("PokemonApiClient", client =>
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30); 
});


builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IPokemonApiClient, PokomonApiClient>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
