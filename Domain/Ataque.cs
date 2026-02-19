namespace PokemonBattle.Domain;

/// <summary>
/// Representa un ataque que un Pokémon puede ejecutar en batalla.
/// Inmutable: todos los campos se asignan en construcción.
/// </summary>
public class Ataque
{
    private readonly string _nombre;
    private readonly int _danio;
    private readonly int _precision;
    private readonly ITipo _tipo;

    // MINIMO TECNICO PARA COMPILAR: constructor necesario para instanciar Ataque.
    /// <summary>Inicializa un nuevo ataque con sus parámetros base.</summary>
    /// <param name="nombre">Nombre descriptivo del ataque.</param>
    /// <param name="danio">Daño base que inflige el ataque.</param>
    /// <param name="precision">Probabilidad de acierto en porcentaje (0-100).</param>
    /// <param name="tipo">Tipo elemental del ataque.</param>
    public Ataque(string nombre, int danio, int precision, ITipo tipo)
    {
        _nombre    = nombre;
        _danio     = danio;
        _precision = precision;
        _tipo      = tipo;
    }

    /// <summary>Nombre descriptivo del ataque.</summary>
    public string Nombre    => _nombre;

    /// <summary>Daño base que inflige el ataque antes de modificadores.</summary>
    public int    Danio     => _danio;

    /// <summary>Precisión del ataque expresada en porcentaje (0-100).</summary>
    public int    Precision => _precision;

    /// <summary>Tipo elemental al que pertenece el ataque.</summary>
    public ITipo  Tipo      => _tipo;
}
