namespace PokemonBattle.Domain;

/// <summary>
/// Contrato que representa un tipo elemental de Pokémon o ataque.
/// Toda entidad con tipo debe implementar esta interfaz.
/// </summary>
public interface ITipo
{
    /// <summary>Identificador único del tipo (ej. "Fuego", "Agua", "Planta").</summary>
    string Id { get; }
}
