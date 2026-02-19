namespace PokemonBattle.Domain;

/// <summary>
/// Contrato para calcular el modificador de efectividad entre dos tipos.
/// Precondición: atacante y defensor no deben ser nulos.
/// Postcondición: devuelve un double > 0.0 (ej. 0.5, 1.0, 2.0).
/// </summary>
public interface IEfectividadTipos
{
    /// <summary>
    /// Obtiene el multiplicador de daño según la relación entre el tipo atacante y el defensor.
    /// </summary>
    /// <param name="atacante">Tipo del ataque que se usa.</param>
    /// <param name="defensor">Tipo del Pokémon que recibe el ataque.</param>
    /// <returns>Modificador de daño: 2.0 (súper efectivo), 1.0 (normal), 0.5 (poco efectivo).</returns>
    double ObtenerModificador(ITipo atacante, ITipo defensor);
}
