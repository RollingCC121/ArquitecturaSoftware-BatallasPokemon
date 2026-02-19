using PokemonBattle.Domain;

namespace PokemonBattle.Application;

/// <summary>
/// Regla de daño que opera sobre un <see cref="DamageContext"/>.
/// Patrón Chain-of-Responsibility: cada regla modifica el contexto y lo pasa adelante.
/// </summary>
public interface IDamageRule
{
    /// <summary>
    /// Aplica la regla al contexto de daño.
    /// Precondición: ctx no debe ser nulo.
    /// Postcondición: ctx puede tener modificado Acerto, Modificador o Resultado.
    /// </summary>
    /// <param name="ctx">Contexto de cálculo de daño.</param>
    void Apply(DamageContext ctx);
}

// ─────────────────────────────────────────────────────────────────
// PrecisionRule
// ─────────────────────────────────────────────────────────────────

/// <summary>
/// Regla que determina si el ataque acierta usando la precisión del ataque
/// y un número aleatorio generado por <see cref="IRandomProvider"/>.
/// Si no acierta, establece Acerto = false y Resultado = 0.
/// </summary>
public class PrecisionRule : IDamageRule
{
    private readonly IRandomProvider _random;

    /// <summary>Inyecta el proveedor de aleatoriedad.</summary>
    /// <param name="random">Fuente de números aleatorios.</param>
    public PrecisionRule(IRandomProvider random)
    {
        _random = random;
    }

    /// <inheritdoc/>
    public void Apply(DamageContext ctx)
    {
        var tirada = _random.NextInt(1, 101); // 1..100
        ctx.Acerto    = tirada <= ctx.Ataque.Precision;
        ctx.Resultado = ctx.Acerto ? ctx.Resultado : 0;
    }
}

// ─────────────────────────────────────────────────────────────────
// TypeEffectivenessRule
// ─────────────────────────────────────────────────────────────────

/// <summary>
/// Regla que aplica el modificador de efectividad de tipo al daño base.
/// Depende de <see cref="IEfectividadTipos"/> para obtener el multiplicador.
/// Solo aplica si el ataque acertó (ctx.Acerto == true).
/// </summary>
public class TypeEffectivenessRule : IDamageRule
{
    private readonly IEfectividadTipos _efectividad;

    /// <summary>Inyecta la tabla de efectividad de tipos.</summary>
    /// <param name="efectividad">Servicio de cálculo de efectividad.</param>
    public TypeEffectivenessRule(IEfectividadTipos efectividad)
    {
        _efectividad = efectividad;
    }

    /// <inheritdoc/>
    public void Apply(DamageContext ctx)
    {
        // Enfoque funcional: expresión condicional sin bloque if imperativo.
        ctx.Modificador = ctx.Acerto
            ? _efectividad.ObtenerModificador(ctx.Ataque.Tipo, ctx.Defensor.Tipo)
            : ctx.Modificador;

        ctx.Resultado = ctx.Acerto
            ? (int)(ctx.BaseDamage * ctx.Modificador)
            : 0;
    }
}
