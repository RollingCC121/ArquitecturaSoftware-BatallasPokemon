using PokemonBattle.Domain;

namespace PokemonBattle.Infrastructure;

/// <summary>
/// Implementación en memoria del repositorio del Pokédex.
/// Contiene datos de muestra suficientes para demostrar el sistema.
/// </summary>
/// <remarks>
/// MINIMO TECNICO PARA COMPILAR: datos en memoria sin persistencia externa.
/// </remarks>
public class PokedexRepository : IPokedexRepository
{
    private readonly List<Pokemon> _pokemons;

    /// <summary>Inicializa el repositorio con un conjunto de Pokémon predefinidos.</summary>
    public PokedexRepository()
    {
        var fuego  = new TipoFuego();
        var agua   = new TipoAgua();
        var planta = new TipoPlanta();

        _pokemons = new List<Pokemon>
        {
            new(Guid.NewGuid(), "Charmander", 100, fuego,
                new List<Ataque>
                {
                    new("Ascuas",       40, 100, fuego),
                    new("Lanzallamas",  90,  85, fuego),
                    new("Arañazo",      40, 100, new TipoPlanta()), // ataque secundario
                }),

            new(Guid.NewGuid(), "Squirtle", 100, agua,
                new List<Ataque>
                {
                    new("Pistola Agua", 40, 100, agua),
                    new("Surf",         90,  85, agua),
                    new("Placaje",      40, 100, new TipoPlanta()),
                }),

            new(Guid.NewGuid(), "Bulbasaur", 100, planta,
                new List<Ataque>
                {
                    new("Látigo Cepa",  45, 100, planta),
                    new("Rayo Solar",   90,  85, planta),
                    new("Tackle",       40, 100, new TipoPlanta()),
                }),

            new(Guid.NewGuid(), "Flareon", 110, fuego,
                new List<Ataque>
                {
                    new("Llamarada",    95,  80, fuego),
                    new("Colmillo Ígneo",65, 95, fuego),
                }),

            new(Guid.NewGuid(), "Vaporeon", 110, agua,
                new List<Ataque>
                {
                    new("Hidrobomba",   95,  80, agua),
                    new("Acua Jet",     40, 100, agua),
                }),

            new(Guid.NewGuid(), "Leafeon", 110, planta,
                new List<Ataque>
                {
                    new("Hoja Aguda",   55, 100, planta),
                    new("Energibola",   80, 100, planta),
                }),
        };
    }

    /// <inheritdoc/>
    public List<Pokemon> ObtenerTodos() => _pokemons;

    /// <inheritdoc/>
    public Pokemon ObtenerPorIndice(int indice) => _pokemons[indice];
}
