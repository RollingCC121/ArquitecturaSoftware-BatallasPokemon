namespace PokemonBattle.Application;

/// <summary>
/// Abstracción de generación de números aleatorios.
/// Permite sustituir la implementación en tests sin modificar lógica de negocio.
/// </summary>
public interface IRandomProvider
{
    /// <summary>
    /// Genera un entero aleatorio en el rango [minIncl, maxExcl).
    /// Precondición: minIncl &lt; maxExcl.
    /// </summary>
    /// <param name="minIncl">Valor mínimo inclusivo.</param>
    /// <param name="maxExcl">Valor máximo exclusivo.</param>
    /// <returns>Entero aleatorio en el rango especificado.</returns>
    int NextInt(int minIncl, int maxExcl);
}

/// <summary>
/// Implementación de <see cref="IRandomProvider"/> basada en <see cref="System.Random"/>.
/// </summary>
public class RandomProvider : IRandomProvider
{
    private readonly Random _random = new();

    /// <inheritdoc/>
    public int NextInt(int minIncl, int maxExcl) => _random.Next(minIncl, maxExcl);
}
