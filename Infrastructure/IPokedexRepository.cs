using PokemonBattle.Domain;

namespace PokemonBattle.Infrastructure;

/// <summary>
/// Contrato de acceso a datos del Pokédex.
/// Abstrae el origen de los datos (memoria, base de datos, archivo, etc.).
/// </summary>
public interface IPokedexRepository
{
    /// <summary>Devuelve todos los Pokémon disponibles en el repositorio.</summary>
    /// <returns>Lista completa de Pokémon.</returns>
    List<Pokemon> ObtenerTodos();

    /// <summary>
    /// Devuelve el Pokémon en la posición indicada.
    /// Precondición: indice debe ser válido dentro del rango de la lista.
    /// </summary>
    /// <param name="indice">Índice basado en 0.</param>
    /// <returns>El Pokémon en esa posición.</returns>
    Pokemon ObtenerPorIndice(int indice);
}
