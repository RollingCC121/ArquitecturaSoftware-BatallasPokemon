using PokemonBattle.Application;

namespace PokemonBattle.Controller;

/// <summary>
/// Controlador principal del juego. Punto de entrada entre el entrypoint (Program)
/// y el orquestador de la lógica de juego.
/// Sigue el patrón Controller de la arquitectura por capas.
/// </summary>
public class JuegoController
{
    private readonly IJuegoOrchestrator _orchestrator;

    /// <summary>Inyecta el orquestador del juego.</summary>
    /// <param name="orchestrator">Orquestador que maneja el flujo completo.</param>
    public JuegoController(IJuegoOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    /// <summary>
    /// Inicia la ejecución del juego delegando al orquestador.
    /// Es el único punto de entrada al flujo de juego.
    /// </summary>
    public void Iniciar()
    {
        _orchestrator.IniciarJuego();
    }
}
