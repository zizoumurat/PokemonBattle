using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace PokemonBattle.Domain.Models;

public record Pokemon
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public List<string> Types { get; init; } = new();
    public Dictionary<string, int> BaseStats { get; init; } = new();
}

public record PokemonData
{
    [JsonProperty("id")]
    public int Id { get; init; }

    [JsonProperty("name")]
    public string Name { get; init; } = string.Empty;

    [JsonProperty("types")]
    public List<PokemonTypeSlot> Types { get; init; } = new();

    [JsonProperty("stats")]
    public List<PokemonStat> Stats { get; init; } = new();

    [JsonProperty("moves")]
    public List<PokemonMove> Moves { get; init; } = new();
}

public record PokemonTypeSlot
{
    public int Slot { get; init; }
    public PokemonType Type { get; init; } = new();
}

public record PokemonType
{
    public string Name { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
}

public record PokemonStat
{
    [JsonProperty("base_stat")]
    public int BaseStat { get; init; }
    public int Effort { get; init; }
    public StatDetails Stat { get; init; } = new();
}

public record StatDetails
{
    public string Name { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
}

public record PokemonMove
{
    public MoveDetails Move { get; init; } = new();
}

public record MoveDetails
{
    public string Name { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
}
