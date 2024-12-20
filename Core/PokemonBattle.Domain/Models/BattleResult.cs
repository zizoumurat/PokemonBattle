namespace PokemonBattle.Domain.Models;
public class BattleResult
{
    public string Winner { get; set; }
    public string Log { get; set; }
    public int Pokemon1FinalHp { get; set; }
    public int Pokemon2FinalHp { get; set; }
}