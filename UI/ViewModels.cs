using PokemonBattle.Domain;

namespace PokemonBattle.UI;

// ─────────────────────────────────────────────────────────────────
// View Models: datos preparados para la UI, sin lógica de negocio.
// ─────────────────────────────────────────────────────────────────

/// <summary>ViewModel de pantalla de inicio. Contiene el título del juego.</summary>
public class StartVM
{
    /// <summary>Título a mostrar en pantalla de bienvenida.</summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Subtítulo o instrucción inicial.</summary>
    public string Subtitulo { get; set; } = string.Empty;
}

/// <summary>ViewModel del menú principal. Contiene las opciones disponibles.</summary>
public class MenuVM
{
    /// <summary>Lista de opciones del menú principal.</summary>
    public List<string> Opciones { get; set; } = new();
}

/// <summary>ViewModel del Pokédex. Contiene la lista de Pokémon para mostrar.</summary>
public class PokedexVM
{
    /// <summary>Lista de Pokémon a mostrar con su índice.</summary>
    public List<Pokemon> Pokemons { get; set; } = new();
}

/// <summary>ViewModel de batalla. Contiene el estado actual de la batalla.</summary>
public class BattleVM
{
    /// <summary>Nombre del Pokémon del jugador.</summary>
    public string NombreJugador { get; set; } = string.Empty;

    /// <summary>HP actual del Pokémon del jugador.</summary>
    public int HpJugador { get; set; }

    /// <summary>Nombre del Pokémon de la máquina.</summary>
    public string NombreOponente { get; set; } = string.Empty;

    /// <summary>HP actual del Pokémon de la máquina.</summary>
    public int HpOponente { get; set; }

    /// <summary>Log completo de la batalla para mostrar.</summary>
    public List<string> Log { get; set; } = new();
}

/// <summary>ViewModel de mensaje simple. Muestra un texto genérico al usuario.</summary>
public class MessageVM
{
    /// <summary>Mensaje a mostrar.</summary>
    public string Mensaje { get; set; } = string.Empty;
}
