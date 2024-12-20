namespace PokemonBattle.Domain.Models;
public class TypeEffectiveness
{
    public List<string> DoubleDamageTo { get; init; } = new();
    public List<string> HalfDamageTo { get; init; } = new();
    public List<string> NoDamageTo { get; init; } = new();
}