using Newtonsoft.Json;
using PokemonBattle.Domain.Models;

namespace PokemonBattle.Infrastructure.Clients;
public class PokomonApiClient : IPokemonApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PokomonApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<PokemonData> FetchPokemonAsync(int pokemonId)
    {
        var httpClient = _httpClientFactory.CreateClient("PokemonApiClient");

        var response = await httpClient.GetAsync($"pokemon/{pokemonId}");
        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();
        var pokemonData = JsonConvert.DeserializeObject<PokemonData>(content);

        return pokemonData;
    }

    public async Task<TypeEffectiveness> GetTypeEffectivenessAsync(string typeName)
    {
        var httpClient = _httpClientFactory.CreateClient("PokemonApiClient");

        var response = await httpClient.GetAsync($"https://pokeapi.co/api/v2/type/{typeName.ToLower()}");
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<TypeApiResponse>(content);

        return new TypeEffectiveness
        {
            DoubleDamageTo = data.DamageRelations.DoubleDamageTo.Select(t => t.Name).ToList(),
            HalfDamageTo = data.DamageRelations.HalfDamageTo.Select(t => t.Name).ToList(),
            NoDamageTo = data.DamageRelations.NoDamageTo.Select(t => t.Name).ToList()
        };
    }
}