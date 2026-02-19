using PokemonBattle.Domain;

namespace PokemonBattle.Application;

/// <summary>
/// Resultado completo de una batalla entre dos Pokémon.
/// Contiene el ganador, el perdedor y un log de los eventos por turno.
/// </summary>
public class ResultadoBatalla
{
    /// <summary>Pokémon que ganó la batalla (HP > 0 al finalizar).</summary>
    public Pokemon       Ganador  { get; set; } = null!;

    /// <summary>Pokémon que perdió la batalla (HP == 0 al finalizar).</summary>
    public Pokemon       Perdedor { get; set; } = null!;

    /// <summary>Log de mensajes del desarrollo de la batalla, un mensaje por evento relevante.</summary>
    public List<string>  Log      { get; set; } = new();
}
