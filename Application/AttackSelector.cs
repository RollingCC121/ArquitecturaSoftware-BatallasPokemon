using PokemonBattle.Domain;
namespace PokemonBattle.Application;

/// <summary>
/// Contrato para seleccionar un ataque de un Pokémon atacante.
/// </summary>
public interface IAttackSelector
{
    /// <summary>
    /// Selecciona un ataque del Pokémon dado.
    /// Precondición: atacante debe tener al menos 1 ataque.
    /// </summary>
    /// <param name="atacante">Pokémon que va a atacar.</param>
    /// <returns>El ataque seleccionado.</returns>
    Ataque SeleccionarAtaque(Pokemon atacante);
}

/// <summary>
/// Selecciona un ataque al azar de la lista de ataques del Pokémon.
/// Utilizado por la IA para elegir su ataque en cada turno.
/// </summary>
public class RandomAttackSelector : IAttackSelector
{
    private readonly IRandomProvider _random;

    /// <summary>Inyecta el proveedor de aleatoriedad.</summary>
    public RandomAttackSelector(IRandomProvider random)
    {
        _random = random;
    }

    /// <inheritdoc/>
    public Ataque SeleccionarAtaque(Pokemon atacante)
    {
        var indice = _random.NextInt(0, atacante.Ataques.Count);
        return atacante.Ataques[indice];
    }
}
