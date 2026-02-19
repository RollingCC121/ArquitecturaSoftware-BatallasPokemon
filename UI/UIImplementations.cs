namespace PokemonBattle.UI;

// ─────────────────────────────────────────────────────────────────
// Implementaciones de IUI<T> para consola.
// Cada clase sabe formatear y mostrar su ViewModel específico.
// ─────────────────────────────────────────────────────────────────

/// <summary>Muestra la pantalla de inicio del juego.</summary>
public class UIStart : IUI<StartVM>
{
    /// <inheritdoc/>
    public void Mostrar(StartVM vm)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine($"║  {vm.Titulo,-36}║");
        Console.WriteLine($"║  {vm.Subtitulo,-36}║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
    }
}

/// <summary>Muestra el menú principal con sus opciones numeradas.</summary>
public class UIMenu : IUI<MenuVM>
{
    /// <inheritdoc/>
    public void Mostrar(MenuVM vm)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("┌─ MENÚ PRINCIPAL ─────────────────────┐");
        vm.Opciones
          .Select((opcion, i) => $"│  [{i + 1}] {opcion}")
          .ToList()
          .ForEach(Console.WriteLine);
        Console.WriteLine("└──────────────────────────────────────┘");
        Console.ResetColor();
        Console.Write("Elige una opción: ");
    }
}

/// <summary>Muestra la lista de Pokémon del Pokédex con su índice y estadísticas básicas.</summary>
public class UIPokedex : IUI<PokedexVM>
{
    /// <inheritdoc/>
    public void Mostrar(PokedexVM vm)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n┌─ POKÉDEX ─────────────────────────────┐");
        vm.Pokemons
          .Select((p, i) => $"│  [{i + 1}] {p.Nombre,-12} Tipo: {p.Tipo.Id,-7} HP: {p.HP}")
          .ToList()
          .ForEach(Console.WriteLine);
        Console.WriteLine("└───────────────────────────────────────┘");
        Console.ResetColor();
    }
}

/// <summary>Muestra el log completo de la batalla.</summary>
public class UIBatalla : IUI<BattleVM>
{
    /// <inheritdoc/>
    public void Mostrar(BattleVM vm)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n╔═══════════════════════════════════════╗");
        Console.WriteLine($"║  {vm.NombreJugador,-18} HP: {vm.HpJugador,3}   ║");
        Console.WriteLine($"║  {vm.NombreOponente,-18} HP: {vm.HpOponente,3}   ║");
        Console.WriteLine("╠═══════════════════════════════════════╣");
        Console.ResetColor();
        vm.Log.ForEach(Console.WriteLine);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("╚═══════════════════════════════════════╝");
        Console.ResetColor();
    }
}

/// <summary>Muestra un mensaje simple al usuario.</summary>
public class UIMensaje : IUI<MessageVM>
{
    /// <inheritdoc/>
    public void Mostrar(MessageVM vm)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"\n  ➤ {vm.Mensaje}");
        Console.ResetColor();
    }
}
