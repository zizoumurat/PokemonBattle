using PokemonBattle.Domain.Models;

namespace PokemonBattle.Application.Interfaces;
public interface IPokemonService
{
    Task<Pokemon> GetPokemonDataAsync(int pokemonId);
    Task<BattleResult> SimulateBattleAsync(Pokemon pokemon1, Pokemon pokemon2);
}
