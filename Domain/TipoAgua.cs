namespace PokemonBattle.Domain;

/// <summary>Tipo elemental Agua. Es fuerte contra Fuego y débil contra Planta.</summary>
public class TipoAgua : ITipo
{
    /// <inheritdoc/>
    public string Id => "Agua";
}
