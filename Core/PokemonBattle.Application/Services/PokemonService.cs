using PokemonBattle.Application.Interfaces;
using PokemonBattle.Domain.Models;
using PokemonBattle.Infrastructure.Clients;
using System.Text;

namespace PokemonBattle.Application.Services;
public class PokemonService : IPokemonService
{
    private readonly IPokemonApiClient _pokemonApiClient;

    public PokemonService(IPokemonApiClient pokemonApiClient)
    {
        _pokemonApiClient = pokemonApiClient;
    }

    public async Task<Pokemon> GetPokemonDataAsync(int pokemonId)
    {
        var response = await _pokemonApiClient.FetchPokemonAsync(pokemonId);

        if (response == null)
            return null;

        var pokemon = new Pokemon
        {
            Id = response.Id,
            Name = response.Name,
            Types = response.Types.Select(t => t.Type.Name).ToList(),
            BaseStats = response.Stats.ToDictionary(s => s.Stat.Name, s => s.BaseStat)
        };

        return pokemon;
    }

    public async Task<BattleResult> SimulateBattleAsync(Pokemon pokemon1, Pokemon pokemon2)
    {
        int pokemon1Hp = pokemon1.BaseStats["hp"];
        int pokemon2Hp = pokemon2.BaseStats["hp"];

        StringBuilder log = new();
        log.AppendLine("Battle begins!");

        var firstPokemon = pokemon1.BaseStats["speed"] >= pokemon2.BaseStats["speed"] ? pokemon1 : pokemon2;
        var secondPokemon = firstPokemon == pokemon1 ? pokemon2 : pokemon1;

        int firstPokemonHp = firstPokemon == pokemon1 ? pokemon1Hp : pokemon2Hp;
        int secondPokemonHp = firstPokemon == pokemon1 ? pokemon2Hp : pokemon1Hp;

        while (firstPokemonHp > 0 && secondPokemonHp > 0)
        {
            firstPokemonHp = await PerformAttackAsync(firstPokemon, secondPokemon, log);
            if (firstPokemonHp <= 0) break;

            secondPokemonHp = await PerformAttackAsync(secondPokemon, firstPokemon, log);
            if (secondPokemonHp <= 0) break;
        }

        string winner = firstPokemonHp > 0 ? firstPokemon.Name : secondPokemon.Name;

        return new BattleResult
        {
            Winner = winner,
            Log = log.ToString(),
            Pokemon1FinalHp = firstPokemon == pokemon1 ? firstPokemonHp : secondPokemonHp,
            Pokemon2FinalHp = firstPokemon == pokemon1 ? secondPokemonHp : firstPokemonHp
        };
    }

    private async Task<int> PerformAttackAsync(Pokemon attacker, Pokemon defender, StringBuilder log)
    {
        int damage = await CalculateDamageAsync(attacker, defender);
        defender.BaseStats["hp"] -= damage;

        log.AppendLine($"{attacker.Name} dealt {damage} damage to {defender.Name}. {defender.Name} has {defender.BaseStats["hp"]} HP left.");
        return defender.BaseStats["hp"];
    }

    private async Task<int> CalculateDamageAsync(Pokemon attacker,Pokemon defender)
    {
        // Tür avantajını hesapla
        double typeMultiplier = await CalculateTypeAdvantageAsync(attacker.Types, defender.Types);

        // Hasar hesaplama
        int attack = attacker.BaseStats["attack"];
        int defense = defender.BaseStats["defense"];
        int baseDamage = attack - (defense / 2);

        // Tür avantajını uygula
        int damage = (int)(baseDamage * typeMultiplier);

        // Minimum hasar 1 olmalı
        return Math.Max(damage, 1);
    }

    private async Task<double> CalculateTypeAdvantageAsync(List<string> attackerTypes,List<string> defenderTypes)
    {
        double multiplier = 1.0;

        foreach (var attackerType in attackerTypes)
        {
            var effectiveness = await _pokemonApiClient.GetTypeEffectivenessAsync(attackerType);

            if (effectiveness == null) continue;

            foreach (var defenderType in defenderTypes)
            {
                if (effectiveness.DoubleDamageTo.Contains(defenderType))
                {
                    multiplier *= 2.0;
                }
                else if (effectiveness.HalfDamageTo.Contains(defenderType))
                {
                    multiplier *= 0.5;
                }
                else if (effectiveness.NoDamageTo.Contains(defenderType))
                {
                    multiplier *= 0.0;
                }
            }
        }

        return multiplier;
    }

}
