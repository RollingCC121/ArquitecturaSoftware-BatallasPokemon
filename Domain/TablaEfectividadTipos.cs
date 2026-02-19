namespace PokemonBattle.Domain;

/// <summary>
/// Tabla de efectividad de tipos implementada con un diccionario funcional.
/// Cubre las relaciones Fuego/Agua/Planta según el triángulo clásico de Pokémon.
/// </summary>
public class TablaEfectividadTipos : IEfectividadTipos
{
    /// <summary>
    /// Tabla de modificadores indexada por (tipoAtacante, tipoDefensor).
    /// Los pares no presentes devuelven 1.0 (neutro).
    /// </summary>
    private readonly Dictionary<(string Atacante, string Defensor), double> _tabla = new()
    {
        { ("Fuego",  "Planta"), 2.0 },
        { ("Fuego",  "Agua"),   0.5 },
        { ("Agua",   "Fuego"),  2.0 },
        { ("Agua",   "Planta"), 0.5 },
        { ("Planta", "Agua"),   2.0 },
        { ("Planta", "Fuego"),  0.5 },
    };

    /// <inheritdoc/>
    public double ObtenerModificador(ITipo atacante, ITipo defensor) =>
        _tabla.TryGetValue((atacante.Id, defensor.Id), out var mod) ? mod : 1.0;
}
