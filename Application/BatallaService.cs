using PokemonBattle.Domain;

namespace PokemonBattle.Application;

/// <summary>
/// Contrato del servicio de batalla por turnos entre dos Pokémon.
/// </summary>
public interface IBatallaService
{
    /// <summary>
    /// Ejecuta el turno del jugador humano aplicando el ataque elegido.
    /// </summary>
    /// <param name="jugador">Pokémon del jugador humano que ataca.</param>
    /// <param name="oponente">Pokémon de la máquina que recibe el daño.</param>
    /// <param name="ataqueElegido">Ataque elegido por el jugador para este turno.</param>
    /// <param name="resultado">Resultado acumulado de la batalla para registrar el log.</param>
    void EjecutarTurnoJugador(Pokemon jugador, Pokemon oponente, Ataque ataqueElegido, ResultadoBatalla resultado);

    /// <summary>
    /// Ejecuta el turno de la máquina seleccionando un ataque automáticamente.
    /// </summary>
    /// <param name="oponente">Pokémon de la máquina que ataca.</param>
    /// <param name="jugador">Pokémon del jugador humano que recibe el daño.</param>
    /// <param name="resultado">Resultado acumulado de la batalla para registrar el log.</param>
    void EjecutarTurnoMaquina(Pokemon oponente, Pokemon jugador, ResultadoBatalla resultado);
}

/// <summary>
/// Implementación del servicio de batalla por turnos.
/// Ejecuta acciones individuales de ataque; el loop de batalla vive en el orquestador.
/// La máquina selecciona ataques con <see cref="IAttackSelector"/>.
/// El daño pasa por <see cref="IDamageCalculator"/> con sus reglas.
/// </summary>
public class BatallaService : IBatallaService
{
    private readonly IDamageCalculator _damageCalculator;
    private readonly IAttackSelector   _attackSelector;

    /// <summary>Inyecta el calculador de daño y el selector de ataques.</summary>
    public BatallaService(IDamageCalculator damageCalculator, IAttackSelector attackSelector)
    {
        _damageCalculator = damageCalculator;
        _attackSelector   = attackSelector;
    }

    /// <inheritdoc/>
    public void EjecutarTurnoJugador(Pokemon jugador, Pokemon oponente, Ataque ataqueElegido, ResultadoBatalla resultado)
    {
        var danio = _damageCalculator.CalcularDanio(ataqueElegido, oponente);
        oponente.RecibirDanio(danio);

        if (danio > 0)
        {
            resultado.Log.Add($"   {jugador.Nombre} usa {ataqueElegido.Nombre} -> {danio} de daño a {oponente.Nombre}.");
        }
        else
        {
            resultado.Log.Add($"   {jugador.Nombre} usa {ataqueElegido.Nombre} -> ¡Falló!");
        }

        if (oponente.HP <= 0)
        {
            resultado.Log.Add($"💀 {oponente.Nombre} no puede continuar.");
        }
        else
        {
            resultado.Log.Add($"   {oponente.Nombre} tiene {oponente.HP} HP restantes.");
        }
    }

    /// <inheritdoc/>
    public void EjecutarTurnoMaquina(Pokemon oponente, Pokemon jugador, ResultadoBatalla resultado)
    {
        var ataque = _attackSelector.SeleccionarAtaque(oponente);
        var danio  = _damageCalculator.CalcularDanio(ataque, jugador);

        jugador.RecibirDanio(danio);

        if (danio > 0)
        {
            resultado.Log.Add($"   {oponente.Nombre} usa {ataque.Nombre} -> {danio} de daño a {jugador.Nombre}.");
        }
        else
        {
            resultado.Log.Add($"   {oponente.Nombre} usa {ataque.Nombre} -> ¡Falló!");
        }

        if (jugador.HP <= 0)
        {
            resultado.Log.Add($"💀 {jugador.Nombre} no puede continuar.");
        }
        else
        {
            resultado.Log.Add($"   {jugador.Nombre} tiene {jugador.HP} HP restantes.");
        }
    }
}
