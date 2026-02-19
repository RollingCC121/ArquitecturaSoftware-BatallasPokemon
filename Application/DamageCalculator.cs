using PokemonBattle.Domain;

namespace PokemonBattle.Application;

/// <summary>
/// Contrato para el cálculo del daño de un ataque sobre un Pokémon defensor.
/// </summary>
public interface IDamageCalculator
{
    /// <summary>
    /// Calcula el daño neto que inflige <paramref name="ataque"/> sobre <paramref name="defensor"/>
    /// pasando el contexto por todas las reglas registradas.
    /// </summary>
    /// <param name="ataque">Ataque a ejecutar.</param>
    /// <param name="defensor">Pokémon que recibe el ataque.</param>
    /// <returns>Daño final (>= 0).</returns>
    int CalcularDanio(Ataque ataque, Pokemon defensor);
}

/// <summary>
/// Implementación del calculador de daño.
/// Crea un <see cref="DamageContext"/>, lo inicializa y lo pasa secuencialmente
/// por cada <see cref="IDamageRule"/> registrada (pipeline funcional).
/// </summary>
public class DamageCalculator : IDamageCalculator
{
    private readonly IEnumerable<IDamageRule> _reglas;

    /// <summary>Inyecta la colección de reglas de daño a aplicar en orden.</summary>
    /// <param name="reglas">Reglas de daño (se aplican en el orden de enumeración).</param>
    public DamageCalculator(IEnumerable<IDamageRule> reglas)
    {
        _reglas = reglas;
    }

    /// <inheritdoc/>
    public int CalcularDanio(Ataque ataque, Pokemon defensor)
    {
        // Crea el contexto y lo inicializa con los datos base.
        var ctx = new DamageContext
        {
            Ataque     = ataque,
            Defensor   = defensor,
            BaseDamage = ataque.Danio,
            Resultado  = ataque.Danio,
            Modificador = 1.0,
            Acerto     = true,
        };

        // Pipeline funcional: cada regla transforma el contexto (estilo Aggregate/fold).
        _reglas.Aggregate(ctx, (context, regla) =>
        {
            regla.Apply(context);
            return context;
        });

        return ctx.Resultado;
    }
}
