namespace PokemonBattle.Application.DTOs;

public class PokemonDto
{
    public string Name { get; set; }
    public int Hp { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
    public string[] Types { get; set; }
}