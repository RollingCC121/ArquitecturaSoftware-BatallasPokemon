using PokemonBattle.Domain;

namespace PokemonBattle.Application;

/// <summary>
/// Contexto mutable que circula a través del pipeline de reglas de daño.
/// Cada <see cref="IDamageRule"/> puede leer y modificar este contexto.
/// </summary>
public class DamageContext
{
    /// <summary>Ataque que se está ejecutando en este cálculo.</summary>
    public Ataque  Ataque      { get; set; } = null!;

    /// <summary>Pokémon que recibe el ataque.</summary>
    public Pokemon Defensor    { get; set; } = null!;

    /// <summary>Daño base antes de aplicar modificadores (igual a Ataque.Danio).</summary>
    public int     BaseDamage  { get; set; }

    /// <summary>Modificador acumulado de efectividad de tipo (empieza en 1.0).</summary>
    public double  Modificador { get; set; } = 1.0;

    /// <summary>Indica si el ataque acertó según la regla de precisión.</summary>
    public bool    Acerto      { get; set; } = true;

    /// <summary>Resultado final del daño tras aplicar todas las reglas.</summary>
    public int     Resultado   { get; set; }
}
