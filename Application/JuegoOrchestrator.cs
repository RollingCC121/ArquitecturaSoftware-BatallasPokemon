using PokemonBattle.Domain;
namespace PokemonBattle.Application;
using PokemonBattle.UI;

/// <summary>
/// Contrato del orquestador principal del juego.
/// Coordina la navegación entre menú, Pokédex y batalla.
/// </summary>
public interface IJuegoOrchestrator
{
    /// <summary>Inicia el flujo completo del juego desde el principio.</summary>
    void IniciarJuego();

    /// <summary>Muestra el menú principal y espera la elección del jugador.</summary>
    void MostrarMenuPrincipal();

    /// <summary>Muestra la lista completa del Pokédex.</summary>
    void MostrarPokedex();

    /// <summary>
    /// Inicia la batalla con el Pokémon del jugador ya seleccionado.
    /// La máquina selecciona su Pokémon aleatoriamente.
    /// </summary>
    /// <param name="jugador">Pokémon elegido por el jugador.</param>
    void IniciarBatalla(Pokemon jugador);
}

/// <summary>
/// Implementación del orquestador del juego.
/// Coordina los servicios de dominio con la UI para guiar al jugador.
/// </summary>
public class JuegoOrchestrator : IJuegoOrchestrator
{
    private readonly IBatallaService    _batallaService;
    private readonly IPokedexService    _pokedexService;
    private readonly IPokemonSelector   _pokemonSelector;
    private readonly IUI<StartVM>       _uiStart;
    private readonly IUI<MenuVM>        _uiMenu;
    private readonly IUI<PokedexVM>     _uiPokedex;
    private readonly IUI<BattleVM>      _uiBatalla;
    private readonly IUI<MessageVM>     _uiMensaje;

    /// <summary>
    /// Inyecta todos los servicios y vistas necesarios para orquestar el juego.
    /// </summary>
    public JuegoOrchestrator(
        IBatallaService   batallaService,
        IPokedexService   pokedexService,
        IPokemonSelector  pokemonSelector,
        IUI<StartVM>      uiStart,
        IUI<MenuVM>       uiMenu,
        IUI<PokedexVM>    uiPokedex,
        IUI<BattleVM>     uiBatalla,
        IUI<MessageVM>    uiMensaje)
    {
        _batallaService  = batallaService;
        _pokedexService  = pokedexService;
        _pokemonSelector = pokemonSelector;
        _uiStart         = uiStart;
        _uiMenu          = uiMenu;
        _uiPokedex       = uiPokedex;
        _uiBatalla       = uiBatalla;
        _uiMensaje       = uiMensaje;
    }

    /// <inheritdoc/>
    public void IniciarJuego()
    {
        _uiStart.Mostrar(new StartVM
        {
            Titulo    = "POKEMON BATTLE",
            Subtitulo = "¡Prepárate para la batalla!",
        });

        MostrarMenuPrincipal();
    }

    /// <inheritdoc/>
    public void MostrarMenuPrincipal()
    {
        var continuar = true;

        // Diccionario funcional: mapeo de opción → acción, sin switch imperativo.
        var acciones = new Dictionary<string, Action>
        {
            ["1"] = () =>
            {
                MostrarPokedex();
                var pokemon = SolicitarSeleccionPokemon();
                IniciarBatalla(pokemon);
            },
            ["2"] = () =>
            {
                MostrarPokedex();
                _uiMensaje.Mostrar(new MessageVM { Mensaje = "Presiona Enter para volver al menú..." });
                Console.ReadLine();
            },
            ["3"] = () =>
            {
                _uiMensaje.Mostrar(new MessageVM { Mensaje = "¡Hasta pronto, entrenador!" });
                continuar = false;
            },
        };

        while (continuar)
        {
            _uiMenu.Mostrar(new MenuVM
            {
                Opciones = new List<string> { "Iniciar Batalla", "Ver Pokédex", "Salir" },
            });

            var input  = Console.ReadLine()?.Trim() ?? string.Empty;
            var ejecutar = acciones.TryGetValue(input, out var accion)
                ? accion
                : () => _uiMensaje.Mostrar(new MessageVM { Mensaje = "Opción no válida. Intenta de nuevo." });

            ejecutar();
        }
    }

    /// <inheritdoc/>
    public void MostrarPokedex()
    {
        var pokemons = _pokedexService.ObtenerTodos();
        _uiPokedex.Mostrar(new PokedexVM { Pokemons = pokemons });
    }

    /// <inheritdoc/>
    public void IniciarBatalla(Pokemon jugador)
    {
        var oponente = _pokemonSelector.SeleccionarAleatorio(jugador.Id);
        _uiMensaje.Mostrar(new MessageVM
        {
            Mensaje = $"La máquina eligió a {oponente.Nombre} ({oponente.Tipo.Id}). ¡A pelear!",
        });

        var resultado = new ResultadoBatalla { Log = new List<string>() };
        resultado.Log.Add($"Batalla iniciada: {jugador.Nombre} vs {oponente.Nombre}");
        var indiceUltimoLogMostrado = 0;
        indiceUltimoLogMostrado = MostrarNuevosEventos(resultado, indiceUltimoLogMostrado);

        var turno = 1;
        while (jugador.HP > 0 && oponente.HP > 0)
        {
            resultado.Log.Add($"\n── Turno {turno} ──────────────────────────");
            indiceUltimoLogMostrado = MostrarNuevosEventos(resultado, indiceUltimoLogMostrado);
            MostrarEstadoActualBatalla(jugador, oponente);

            Console.WriteLine($"\nTu turno, {jugador.Nombre}. Elige un ataque:");
            for (var i = 0; i < jugador.Ataques.Count; i++)
            {
                var ataque = jugador.Ataques[i];
                Console.WriteLine($"[{i + 1}] {ataque.Nombre} | Daño: {ataque.Danio} | Precisión: {ataque.Precision}% | Tipo: {ataque.Tipo.Id}");
            }

            Ataque ataqueElegido;
            while (true)
            {
                Console.Write($"Selecciona ataque (1-{jugador.Ataques.Count}): ");
                var input = Console.ReadLine()?.Trim() ?? string.Empty;
                var valido = int.TryParse(input, out var indice) && indice >= 1 && indice <= jugador.Ataques.Count;
                if (valido)
                {
                    ataqueElegido = jugador.Ataques[indice - 1];
                    break;
                }

                _uiMensaje.Mostrar(new MessageVM { Mensaje = "Ataque inválido. Intenta de nuevo." });
            }

            _batallaService.EjecutarTurnoJugador(jugador, oponente, ataqueElegido, resultado);
            indiceUltimoLogMostrado = MostrarNuevosEventos(resultado, indiceUltimoLogMostrado);
            if (oponente.HP <= 0)
            {
                break;
            }

            _batallaService.EjecutarTurnoMaquina(oponente, jugador, resultado);
            indiceUltimoLogMostrado = MostrarNuevosEventos(resultado, indiceUltimoLogMostrado);

            _uiMensaje.Mostrar(new MessageVM { Mensaje = "Presiona Enter para continuar al siguiente turno..." });
            Console.ReadLine();
            turno++;
        }

        if (jugador.HP > 0)
        {
            resultado.Ganador = jugador;
            resultado.Perdedor = oponente;
        }
        else
        {
            resultado.Ganador = oponente;
            resultado.Perdedor = jugador;
        }

        resultado.Log.Add($"\nGanador: {resultado.Ganador.Nombre}");
        indiceUltimoLogMostrado = MostrarNuevosEventos(resultado, indiceUltimoLogMostrado);

        MostrarEstadoActualBatalla(jugador, oponente);

        _uiMensaje.Mostrar(new MessageVM { Mensaje = "Presiona Enter para volver al menú..." });
        Console.ReadLine();
    }

    private static int MostrarNuevosEventos(ResultadoBatalla resultado, int indiceInicial)
    {
        for (var i = indiceInicial; i < resultado.Log.Count; i++)
        {
            Console.WriteLine(resultado.Log[i]);
        }

        return resultado.Log.Count;
    }

    private void MostrarEstadoActualBatalla(Pokemon jugador, Pokemon oponente)
    {
        _uiBatalla.Mostrar(new BattleVM
        {
            NombreJugador  = jugador.Nombre,
            HpJugador      = jugador.HP,
            NombreOponente = oponente.Nombre,
            HpOponente     = oponente.HP,
            Log            = new List<string>(),
        });
    }

    // ─────────────────────────────────────────────────────────────
    // Método privado de apoyo: solicitar al jugador que elija Pokémon.
    // MINIMO TECNICO PARA COMPILAR: lectura de consola necesaria para
    // permitir al jugador elegir su Pokémon. No existe IInput en el UML.
    // ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Solicita al jugador que elija un Pokémon del Pokédex por índice.
    /// Repite la solicitud hasta recibir un índice válido.
    /// </summary>
    private Pokemon SolicitarSeleccionPokemon()
    {
        var pokemons = _pokedexService.ObtenerTodos();
        Pokemon? seleccion = null;

        while (seleccion is null)
        {
            Console.Write($"\nElige tu Pokémon (1-{pokemons.Count}): ");
            var input  = Console.ReadLine()?.Trim() ?? string.Empty;
            var valido = int.TryParse(input, out var num) && num >= 1 && num <= pokemons.Count;

            seleccion = valido
                ? _pokedexService.ObtenerPorIndice(num - 1)
                : (Pokemon?)null;

            if (valido)
            {
                _uiMensaje.Mostrar(new MessageVM { Mensaje = $"Elegiste a {seleccion!.Nombre}." });
            }
            else
            {
                _uiMensaje.Mostrar(new MessageVM { Mensaje = "Número fuera de rango. Intenta de nuevo." });
            }

        }

        return seleccion;
    }
}
