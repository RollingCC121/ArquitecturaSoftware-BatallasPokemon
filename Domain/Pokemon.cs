namespace PokemonBattle.Domain;

/// <summary>
/// Entidad central del dominio. Representa un Pokémon con sus estadísticas y ataques.
/// HP es mutable únicamente a través de <see cref="RecibirDanio"/>.
/// </summary>
public class Pokemon
{
    private readonly Guid          _id;
    private readonly string        _nombre;
    private          int           _hp;
    private readonly ITipo         _tipo;
    private readonly List<Ataque>  _ataques;

    // MINIMO TECNICO PARA COMPILAR: constructor necesario para instanciar Pokemon.
    /// <summary>Inicializa un Pokémon con todos sus atributos de dominio.</summary>
    /// <param name="id">Identificador único del Pokémon.</param>
    /// <param name="nombre">Nombre del Pokémon.</param>
    /// <param name="hp">Puntos de vida iniciales.</param>
    /// <param name="tipo">Tipo elemental del Pokémon.</param>
    /// <param name="ataques">Lista de ataques disponibles.</param>
    public Pokemon(Guid id, string nombre, int hp, ITipo tipo, List<Ataque> ataques)
    {
        _id      = id;
        _nombre  = nombre;
        _hp      = hp;
        _tipo    = tipo;
        _ataques = ataques;
    }

    /// <summary>Identificador único del Pokémon.</summary>
    public Guid                  Id      => _id;

    /// <summary>Nombre del Pokémon.</summary>
    public string                Nombre  => _nombre;

    /// <summary>Puntos de vida actuales. No puede ser menor que 0.</summary>
    public int                   HP      => _hp;

    /// <summary>Tipo elemental del Pokémon.</summary>
    public ITipo                 Tipo    => _tipo;

    /// <summary>Lista de ataques disponibles (solo lectura).</summary>
    public IReadOnlyList<Ataque> Ataques => _ataques.AsReadOnly();

    /// <summary>
    /// Aplica daño al Pokémon. HP no desciende por debajo de 0.
    /// Postcondición: HP >= 0.
    /// </summary>
    /// <param name="danio">Cantidad de daño a aplicar.</param>
    public void RecibirDanio(int danio)
    {
        _hp = Math.Max(0, _hp - danio);
    }
}
