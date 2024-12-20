using PokemonBattle.Domain.Models;

namespace PokemonBattle.Infrastructure.Clients;
public interface IPokemonApiClient
{
    Task<PokemonData> FetchPokemonAsync(int pokemonId);
    Task<TypeEffectiveness> GetTypeEffectivenessAsync(string typeName);
}
