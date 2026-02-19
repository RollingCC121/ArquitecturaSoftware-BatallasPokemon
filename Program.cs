using PokemonBattle.Application;
using PokemonBattle.Controller;
using PokemonBattle.Domain;
using PokemonBattle.Infrastructure;
using PokemonBattle.UI;

// ═══════════════════════════════════════════════════════════════════
// PROGRAM — Entrypoint y Composition Root
// Toda la inyección de dependencias se realiza aquí manualmente.
// No se usa ningún framework DI externo (cumple regla 12 del prompt).
// ═══════════════════════════════════════════════════════════════════

// ── Dominio ───────────────────────────────────────────────────────
IEfectividadTipos efectividad = new TablaEfectividadTipos();

// ── Infraestructura ───────────────────────────────────────────────
IPokedexRepository repository = new PokedexRepository();

// ── Aplicación: utilitarios ───────────────────────────────────────
IRandomProvider random = new RandomProvider();

// ── Aplicación: reglas de daño (pipeline en orden) ────────────────
IEnumerable<IDamageRule> reglas = new List<IDamageRule>
{
    new PrecisionRule(random),
    new TypeEffectivenessRule(efectividad),
};

// ── Aplicación: calculador de daño ───────────────────────────────
IDamageCalculator damageCalculator = new DamageCalculator(reglas);

// ── Aplicación: selectores ────────────────────────────────────────
IPokemonSelector pokemonSelector = new RandomPokemonSelector(repository, random);
IAttackSelector  attackSelector  = new RandomAttackSelector(random);

// ── Aplicación: servicios ─────────────────────────────────────────
IBatallaService batallaService = new BatallaService(damageCalculator, attackSelector);
IPokedexService pokedexService = new PokedexService(repository);

// ── UI: vistas ────────────────────────────────────────────────────
IUI<StartVM>   uiStart   = new UIStart();
IUI<MenuVM>    uiMenu    = new UIMenu();
IUI<PokedexVM> uiPokedex = new UIPokedex();
IUI<BattleVM>  uiBatalla = new UIBatalla();
IUI<MessageVM> uiMensaje = new UIMensaje();

// ── Aplicación: orquestador ───────────────────────────────────────
IJuegoOrchestrator orchestrator = new JuegoOrchestrator(
    batallaService,
    pokedexService,
    pokemonSelector,
    uiStart,
    uiMenu,
    uiPokedex,
    uiBatalla,
    uiMensaje);

// ── Controlador ───────────────────────────────────────────────────
var controller = new JuegoController(orchestrator);

// ── Arranque ──────────────────────────────────────────────────────
controller.Iniciar();
