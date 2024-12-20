using Newtonsoft.Json;

namespace PokemonBattle.Domain.Models;
public class TypeApiResponse
{
    [JsonProperty("damage_relations")]
    public DamageRelations DamageRelations { get; set; }
}

public class DamageRelations
{
    [JsonProperty("double_damage_to")]
    public List<TypeReference> DoubleDamageTo { get; set; }

    [JsonProperty("half_damage_to")]
    public List<TypeReference> HalfDamageTo { get; set; }

    [JsonProperty("no_damage_to")]
    public List<TypeReference> NoDamageTo { get; set; }
}

public class TypeReference
{
    [JsonProperty("name")]
    public string Name { get; set; }
}
