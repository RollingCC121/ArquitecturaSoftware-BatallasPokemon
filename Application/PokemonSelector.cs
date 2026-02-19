using PokemonBattle.Domain;
using PokemonBattle.Infrastructure;

namespace PokemonBattle.Application;

/// <summary>
/// Contrato para seleccionar un Pokémon del Pokédex, opcionalmente excluyendo uno.
/// Usado por la máquina para elegir su Pokémon al inicio de la batalla.
/// </summary>
public interface IPokemonSelector
{
    /// <summary>
    /// Selecciona un Pokémon al azar excluyendo el de <paramref name="excluirId"/>.
    /// Precondición: debe haber al menos 2 Pokémon en el repositorio.
    /// </summary>
    /// <param name="excluirId">Id del Pokémon a excluir (el del jugador).</param>
    /// <returns>Un Pokémon diferente al excluido.</returns>
    Pokemon SeleccionarAleatorio(Guid excluirId);
}

/// <summary>
/// Selecciona un Pokémon aleatorio del repositorio, excluyendo el indicado.
/// Usa un enfoque funcional para filtrar y seleccionar.
/// </summary>
public class RandomPokemonSelector : IPokemonSelector
{
    private readonly IPokedexRepository _repository;
    private readonly IRandomProvider    _random;

    /// <summary>Inyecta repositorio y proveedor de aleatoriedad.</summary>
    public RandomPokemonSelector(IPokedexRepository repository, IRandomProvider random)
    {
        _repository = repository;
        _random     = random;
    }

    /// <inheritdoc/>
    public Pokemon SeleccionarAleatorio(Guid excluirId)
    {
        var candidatos = _repository.ObtenerTodos()
            .Where(p => p.Id != excluirId)
            .ToList();

        var indice = _random.NextInt(0, candidatos.Count);
        return candidatos[indice];
    }
}
