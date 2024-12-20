using Microsoft.AspNetCore.Mvc;
using PokemonBattle.Application.Interfaces;

namespace PokemonBattle.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BattleController : ControllerBase
{
    private readonly IPokemonService _pokemonService;

    public BattleController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet("battle/{pokemonId1}/{pokemonId2}")]
    public async Task<ActionResult> SimulateBattle(int pokemonId1, int pokemonId2)
    {
        var pokemon1 = await _pokemonService.GetPokemonDataAsync(pokemonId1);

        if (pokemon1 == null)
        {

            return NotFound("Pokemon1 not found");
        }

        var pokemon2 = await _pokemonService.GetPokemonDataAsync(pokemonId2);

        if (pokemon2 == null)
        {

            return NotFound("Pokemon2 not found");
        }

        var result = await _pokemonService.SimulateBattleAsync(pokemon1, pokemon2);


        return Ok(result);
    }
}