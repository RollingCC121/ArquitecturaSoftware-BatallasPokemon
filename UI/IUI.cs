namespace PokemonBattle.UI;

/// <summary>
/// Contrato genérico para mostrar un ViewModel tipado en la capa de presentación.
/// Cada implementación concreta sabe cómo renderizar un tipo específico de VM.
/// </summary>
/// <typeparam name="T">Tipo del ViewModel que esta UI sabe mostrar.</typeparam>
public interface IUI<T>
{
    /// <summary>
    /// Muestra el ViewModel al usuario a través de la interfaz de salida (consola, UI, etc.).
    /// Precondición: vm no debe ser nulo.
    /// </summary>
    /// <param name="vm">Datos del ViewModel a renderizar.</param>
    void Mostrar(T vm);
}
