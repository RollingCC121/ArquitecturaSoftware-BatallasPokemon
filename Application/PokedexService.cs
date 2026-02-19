using PokemonBattle.Domain;
using PokemonBattle.Infrastructure;

namespace PokemonBattle.Application;

/// <summary>
/// Contrato del servicio de consulta del Pokédex.
/// Capa de aplicación que encapsula la lógica de acceso al repositorio.
/// </summary>
public interface IPokedexService
{
    /// <summary>Devuelve todos los Pokémon disponibles.</summary>
    List<Pokemon> ObtenerTodos();

    /// <summary>
    /// Devuelve el Pokémon en el índice indicado.
    /// Precondición: índice válido dentro de la lista.
    /// </summary>
    Pokemon ObtenerPorIndice(int indice);
}

/// <summary>
/// Implementación del servicio de Pokédex que delega al repositorio.
/// </summary>
public class PokedexService : IPokedexService
{
    private readonly IPokedexRepository _repository;

    /// <summary>Inyecta el repositorio de datos.</summary>
    public PokedexService(IPokedexRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public List<Pokemon> ObtenerTodos() => _repository.ObtenerTodos();

    /// <inheritdoc/>
    public Pokemon ObtenerPorIndice(int indice) => _repository.ObtenerPorIndice(indice);
}
