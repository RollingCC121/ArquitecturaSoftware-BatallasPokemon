# 🎮 Batallas Pokémon (Consola C#)

¡Bienvenido a **Batallas Pokémon**!  
Este es un juego de consola en C# donde puedes elegir tu Pokémon y enfrentarte a rivales en batallas por turnos, al estilo clásico de GameBoy, con un sistema de combate avanzado que incluye **tipos elementales**, **resistencias** y **efectos visuales**.

---

## 📋 Descripción del proyecto

El propósito de este proyecto es aplicar conceptos avanzados de **Programación Orientada a Objetos (POO)** y **Programación Funcional** en un entorno práctico y entretenido. A través de un juego de batallas Pokémon, se implementan y demuestran:

- ✅ **Encapsulación**: Campos privados con propiedades públicas controladas
- ✅ **Herencia**: Jerarquía de clases con comportamientos especializados
- ✅ **Polimorfismo**: Métodos virtuales y override para diferentes tipos de Pokémon
- ✅ **Sobrecarga (Overload)**: Múltiples versiones de métodos según parámetros
- ✅ **Abstracción**: Interfaces simples que ocultan la complejidad interna

El juego se ejecuta en la consola y cuenta con **menús ASCII estilo retro**, selección interactiva de personajes y batallas por turnos con un sistema de combate que considera tipos elementales y modificadores de daño.

---

## 📐 Diagrama UML del Sistema

A continuación se muestra el diagrama de clases UML que representa la arquitectura del sistema:

![Diagrama UML - Batallas Pokémon](images/Pokemon%20Damage%20Calculation-2026-02-19-002035.png)

*Diagrama de clases mostrando las relaciones entre Pokemon (clase base), sus clases derivadas (PokemonFuego, PokemonAgua, PokemonPlanta) y la clase Ataque. Se puede observar la herencia, composición y los métodos polimórficos implementados.*

---

## 🚀 ¿Cómo ejecutar el proyecto?

### Requisitos
- .NET 6.0 o superior
- Terminal compatible con UTF-8 y colores ANSI

### Pasos de ejecución

1. **Clona el repositorio:**
   ```bash
   git clone https://github.com/RollingCC121/BatallasPokemon.git
   cd BatallasPokemon
   ```

2. **Ejecuta el proyecto:**
   ```bash
   dotnet run
   ```

3. **¡Disfruta el juego!** Sigue las instrucciones en pantalla para jugar.

---

## 📁 Arquitectura y estructura de archivos

El proyecto está organizado en una arquitectura de capas con separación clara de responsabilidades:

### **📄 Program.cs** - Punto de entrada
**Responsabilidad:** Orquestación principal del flujo del juego

**Funcionalidad:**
- Control del bucle principal del juego
- Gestión del menú principal con 3 opciones:
  1. **Start**: Inicia una nueva batalla
  2. **Pokédex**: Muestra todos los Pokémon disponibles con sus estadísticas
  3. **Salir**: Cierra el programa
- Coordinación entre las diferentes capas del sistema

```csharp
// Flujo principal: Menú → Selección → Batalla → Resultado
Interfaz.MostrarMenuPrincipal();
Pokemon elegido = Pokedex.Pokemones[indice];
Batallas.IniciarBatalla(elegido);
```

---

### **🎯 Domain/pokemon.cs** - Modelo de dominio
**Responsabilidad:** Definición de las entidades centrales del juego

#### **Clase `Ataque`**
Representa un movimiento que un Pokémon puede realizar.

**Propiedades:**
- `Nombre` (string): Nombre del ataque (ej: "Lanzallamas")
- `Daño` (int): Daño base del ataque
- `Precisión` (int): Porcentaje de acierto (1-100)
- `Tipo` (string): Tipo elemental ("Fuego", "Agua", "Planta", "Normal")

**Concepto POO aplicado:** 
- ✅ **Encapsulación** con campos privados y propiedades de solo lectura

```csharp
public class Ataque {
    private string _nombre;
    private int _daño;
    // Propiedades públicas de solo lectura
    public string Nombre => _nombre;
}
```

---

#### **Clase `Pokemon` (Clase base)**
Representa un Pokémon genérico con sus características y comportamientos.

**Propiedades:**
- `Nombre` (string): Nombre del Pokémon
- `Tipo` (string): Tipo elemental del Pokémon
- `HP` (int): Puntos de vida (con validación para evitar valores negativos)
- `Ataques` (List<Ataque>): Lista de ataques disponibles

**Métodos clave:**

1. **`RecibirDanio(int cantidad)`** - Sobrecarga simple
   - Recibe solo la cantidad de daño
   - Usado para ataques tipo "Normal" que ignoran resistencias

2. **`virtual RecibirDanio(int cantidad, string tipoAtaque)`** - Método virtual
   - Recibe cantidad de daño y tipo del ataque
   - Puede ser sobrescrito por clases hijas para implementar resistencias
   - Base del sistema de polimorfismo

**Conceptos POO aplicados:**
- ✅ **Encapsulación**: Validación en el setter de HP
- ✅ **Sobrecarga (Overload)**: Dos versiones del método `RecibirDanio()`
- ✅ **Métodos virtuales**: Preparados para ser sobrescritos

```csharp
public int HP {
    get => _hp;
    set => _hp = value < 0 ? 0 : value; // Previene HP negativos
}

// SOBRECARGA: Dos versiones del mismo método
public void RecibirDanio(int cantidad) { ... }
public virtual void RecibirDanio(int cantidad, string tipoAtaque) { ... }
```

---

#### **Clases derivadas: `PokemonFuego`, `PokemonAgua`, `PokemonPlanta`**
Representan tipos específicos de Pokémon con resistencias elementales.

**Herencia:**
- Heredan todas las propiedades y métodos de `Pokemon`
- Especializan el tipo elemental en el constructor
- Sobrescriben el método `RecibirDanio()` para implementar resistencias

**Sistema de resistencias implementado:**

| Tipo Pokémon | Resistente a | Efecto |
|--------------|--------------|--------|
| 🔥 **Fuego** | 🌿 Planta | -50% de daño recibido |
| 💧 **Agua** | 🔥 Fuego | -50% de daño recibido |
| 🌿 **Planta** | 💧 Agua | -50% de daño recibido |

**Conceptos POO aplicados:**
- ✅ **Herencia**: Extienden la clase base `Pokemon`
- ✅ **Polimorfismo (Override)**: Sobrescriben `RecibirDanio()` con comportamiento especializado

**Ejemplo - PokemonFuego:**
```csharp
public class PokemonFuego : Pokemon {
    public override void RecibirDanio(int cantidad, string tipoAtaque) {
        if (tipoAtaque == "Planta")
            HP -= (int)(cantidad * 0.5); // Resistencia: solo 50% del daño
        else
            base.RecibirDanio(cantidad, tipoAtaque); // Comportamiento normal
    }
}
```

---

### **🎨 Application/interfaz.cs** - Capa de presentación
**Responsabilidad:** Interfaz de usuario y efectos visuales

**Métodos principales:**

#### **`MostrarMenuPrincipal()`**
- Muestra el menú principal con arte ASCII
- Diseño estilo retro con bordes y decoraciones
- Colores configurables (verde por defecto)

#### **`ElegirPokemon()`**
- Interfaz de selección de Pokémon
- Muestra información detallada:
  - Nombre, tipo y HP de cada Pokémon
  - Lista completa de ataques con daño y precisión
- Formato tabular con bordes ASCII
- Retorna el índice del Pokémon seleccionado

#### **`MostrarPantallaBatalla()`**
- Pantalla de combate estilo GameBoy clásico
- **Elementos visualizados:**
  - 🔴 Enemigo en la parte superior (color rojo)
  - 🔵 Jugador en la parte inferior (color cyan)
  - Barras de HP visuales y dinámicas
  - Indicadores numéricos de vida (actual/máxima)
  - Lista de ataques disponibles
  - Mensajes de batalla

#### **`MostrarBarraVida()`**
- Renderiza barras de HP con caracteres ASCII
- Representación visual: `[████████░░░░░░░░░░░░]`
- Caracteres usados:
  - `█` para HP actual (verde)
  - `░` para HP perdido (gris)
- Longitud configurable (por defecto 20 caracteres)

**Conceptos aplicados:**
- Separación de la lógica de presentación
- Uso de colores ANSI para mejorar la experiencia
- Diseño modular y reutilizable

---

### **🗄️ Infrastructure/pokedex.cs** - Capa de datos
**Responsabilidad:** Almacenamiento y gestión de datos de Pokémon

**Contenido:**
- **Lista estática de Pokémon**: Base de datos en memoria con inicialización directa
- **Pokémon disponibles:**
  1. **Charmander** (Fuego) - 100 HP
     - Lanzallamas (30 daño, 85% precisión, Fuego)
     - Ascuas (20 daño, 95% precisión, Fuego)
     - Placaje (15 daño, 100% precisión, Normal)
  
  2. **Squirtle** (Agua) - 110 HP
     - Pistola Agua (25 daño, 90% precisión, Agua)
     - Burbuja (20 daño, 95% precisión, Agua)
     - Placaje (15 daño, 100% precisión, Normal)
  
  3. **Bulbasaur** (Planta) - 105 HP
     - Latigazo (25 daño, 90% precisión, Planta)
     - Drenadoras (15 daño, 100% precisión, Planta)
     - Placaje (15 daño, 100% precisión, Normal)

**Método `MostrarTodos()`:**
- Lista todos los Pokémon con formato legible
- Muestra estadísticas completas de cada Pokémon
- Usado por la opción "Pokédex" del menú principal

**Concepto aplicado:**
- Inicialización con sintaxis moderna de C# (`new()`)
- Separación de datos del dominio

---

### **⚔️ batallas.cs** - Motor de combate
**Responsabilidad:** Lógica del sistema de batalla

#### **Método `IniciarBatalla(Pokemon jugador)`**
Gestiona el flujo completo de una batalla.

**Fases de la batalla:**

1. **Inicialización**
   - Selección aleatoria del enemigo (diferente al jugador)
   - Copia de HP inicial para ambos combatientes
   - Mensaje de inicio de batalla

2. **Bucle de combate** (mientras ambos tengan HP > 0)
   
   **Turno del jugador:**
   - Muestra pantalla de batalla actualizada
   - Jugador selecciona un ataque
   - Cálculo de precisión (aleatorio 1-100 vs precisión del ataque)
   - Si acierta:
     - Calcula daño con variabilidad aleatoria
     - **Aplicación de sobrecarga:**
       - Si ataque es "Normal": `enemigo.RecibirDanio(daño)`
       - Si ataque es elemental: `enemigo.RecibirDanio(daño, tipo)`
     - Descuenta HP y muestra mensaje de daño
   - Si falla: Muestra mensaje de fallo
   
   **Turno del enemigo:**
   - Selección aleatoria de ataque por IA
   - Misma lógica de precisión y daño
   - **Uso del mismo sistema de sobrecarga**
   
3. **Finalización**
   - Detecta victoria o derrota
   - Muestra mensaje correspondiente
   - Retorna al menú principal

#### **Método `CalcularDanio(int danioBase)`**
- Añade variabilidad al daño para mayor realismo
- Multiplicador aleatorio: 80% - 120% del daño base
- Evita que las batallas sean predecibles

**Conceptos POO aplicados:**
- ✅ **Uso de sobrecarga**: Llama a diferentes versiones de `RecibirDanio()`
- ✅ **Polimorfismo en acción**: El método correcto se ejecuta según el tipo de Pokémon

**Ejemplo del uso de sobrecarga:**
```csharp
if (ataqueElegido.Tipo == "Normal") {
    // SOBRECARGA: Versión con 1 parámetro
    enemigo.RecibirDanio(danioJugador);
} else {
    // SOBRECARGA: Versión con 2 parámetros
    enemigo.RecibirDanio(danioJugador, ataqueElegido.Tipo);
    // Aquí se activa el OVERRIDE según el tipo de Pokémon
}
```

---

### **🛠️ utils.cs** - Utilidades del sistema
**Responsabilidad:** Funciones auxiliares reutilizables

**Métodos disponibles:**

1. **`LeerOpcion(int min, int max)`**
   - Validación robusta de entrada numérica
   - Maneja errores y entradas inválidas
   - Bucle hasta obtener entrada válida
   - Muestra mensajes de error descriptivos

2. **`EsperarEnter()`**
   - Pausa el programa hasta presionar ENTER
   - Usado para dar tiempo al usuario de leer mensajes

3. **`Delay(int milisegundos)`**
   - Pausa temporal para efectos dramáticos
   - Mejora la experiencia de usuario

4. **`EscribirConDelay(string texto, int msPorLetra = 30)`**
   - Efecto de escritura progresiva (typewriter)
   - Letra por letra con delay configurable
   - Añade dramatismo a mensajes importantes

**Concepto aplicado:**
- Funciones puras y reutilizables
- Responsabilidad única para cada función

---

## 🎯 Conceptos de POO implementados

### 1️⃣ **Encapsulación**
**Definición:** Ocultar los detalles de implementación y exponer solo lo necesario.

**Implementación en el proyecto:**
- ✅ Campos privados con prefijo `_` (ej: `_nombre`, `_hp`, `_daño`)
- ✅ Propiedades públicas con getters de solo lectura
- ✅ Validación en setters (HP no puede ser negativo)
- ✅ Control de acceso mediante modificadores `public`/`private`

**Ejemplo:**
```csharp
private int _hp;
public int HP {
    get => _hp;
    set => _hp = value < 0 ? 0 : value; // Validación encapsulada
}
```

---

### 2️⃣ **Herencia**
**Definición:** Crear clases derivadas que heredan características de una clase base.

**Implementación en el proyecto:**
- ✅ Clase base: `Pokemon`
- ✅ Clases derivadas: `PokemonFuego`, `PokemonAgua`, `PokemonPlanta`
- ✅ Reutilización de código común
- ✅ Especialización de comportamiento por tipo

**Jerarquía:**
```
Pokemon (clase base)
├── PokemonFuego
├── PokemonAgua
└── PokemonPlanta
```

**Ejemplo:**
```csharp
public class PokemonFuego : Pokemon {
    public PokemonFuego(string nombre, int hp, List<Ataque> ataques)
        : base(nombre, "Fuego", hp, ataques) { }
}
```

---

### 3️⃣ **Polimorfismo (Override)**
**Definición:** Diferentes clases responden de manera distinta a la misma llamada de método.

**Implementación en el proyecto:**
- ✅ Método virtual `RecibirDanio()` en la clase base
- ✅ Override en cada clase hija para implementar resistencias elementales
- ✅ Comportamiento diferente según el tipo de Pokémon en tiempo de ejecución

**Flujo del polimorfismo:**
```
Ataque de tipo Planta → PokemonFuego
                      ↓
              RecibirDanio() override
                      ↓
              Daño reducido al 50%

Ataque de tipo Planta → PokemonAgua
                      ↓
              RecibirDanio() override
                      ↓
              Daño normal (100%)
```

**Ejemplo:**
```csharp
// Clase base
public virtual void RecibirDanio(int cantidad, string tipoAtaque) { ... }

// Override en PokemonFuego
public override void RecibirDanio(int cantidad, string tipoAtaque) {
    if (tipoAtaque == "Planta")
        HP -= (int)(cantidad * 0.5); // Comportamiento especializado
    else
        base.RecibirDanio(cantidad, tipoAtaque);
}
```
---

### 4️⃣ **Sobrecarga (Overload)**
**Definición:** Múltiples versiones de un método con diferentes parámetros.

**Implementación en el proyecto:**
- ✅ Dos versiones del método `RecibirDanio()` en la clase `Pokemon`
- ✅ Versión 1: `RecibirDanio(int cantidad)` - Para ataques simples
- ✅ Versión 2: `RecibirDanio(int cantidad, string tipoAtaque)` - Para ataques con tipo

**Uso estratégico:**
- Los ataques tipo "Normal" usan la sobrecarga simple (ignoran resistencias)
- Los ataques elementales usan la sobrecarga con tipo (activan resistencias)

**Ejemplo:**
```csharp
// Sobrecarga 1: Solo cantidad
public void RecibirDanio(int cantidad) {
    _hp -= cantidad;
}

// Sobrecarga 2: Cantidad y tipo
public virtual void RecibirDanio(int cantidad, string tipoAtaque) {
    _hp -= cantidad; // Comportamiento base
}
```
---

### 5️⃣ **Abstracción**
**Definición:** Simplificar la complejidad mostrando solo lo esencial.

**Implementación en el proyecto:**
- ✅ Interfaces simples para interactuar con objetos complejos
- ✅ El usuario no necesita saber cómo se calcula el daño internamente
- ✅ Los métodos públicos ocultan la lógica compleja

**Ejemplo:**
```csharp
// El usuario simplemente llama:
pokemon.RecibirDanio(50, "Fuego");

// Sin preocuparse por:
// - Cálculo de resistencias
// - Validación de HP
// - Actualización de estado interno
```

---

## 🔧 Programación funcional aplicada

### Características funcionales implementadas:

1. **Expresiones lambda** en operaciones aleatorias
```csharp
enemigo = Pokedex.Pokemones[rnd.Next(Pokedex.Pokemones.Count)];
Ataque ataqueEnemigo = enemigo.Ataques[rnd.Next(enemigo.Ataques.Count)];
```

2. **Propiedades con expresiones**
```csharp
public string Nombre => _nombre; // Expression-bodied property
public int HP {
    get => _hp;
    set => _hp = value < 0 ? 0 : value; // Expresión condicional
}
```

3. **Potencial para LINQ** (preparado para extensiones futuras)
```csharp
// Ejemplo de uso futuro:
var pokemonFuego = Pokedex.Pokemones.Where(p => p.Tipo == "Fuego");
var ataquesPotentes = pokemon.Ataques.Where(a => a.Daño > 25);
```

4. **Métodos puros sin efectos secundarios** (en utils.cs)
```csharp
private static int CalcularDanio(int danioBase) {
    double multiplicador = 0.8 + rnd.NextDouble() * 0.4;
    return (int)(danioBase * multiplicador);
}
```

---

## 🎮 ¿Cómo jugar?

### Guía paso a paso:

1. **🚀 Inicia el juego**
   - Ejecuta `dotnet run`
   - Se mostrará el menú principal con arte ASCII

2. **⚡ Selecciona "1. Start"**
   - Accede a la pantalla de selección de Pokémon

3. **🎯 Elige tu Pokémon estratégicamente:**
   
   | Pokémon | Tipo | HP | Ventaja contra | Desventaja contra |
   |---------|------|----|--------------|--------------------|
   | 🔥 **Charmander** | Fuego | 100 | 🌿 Planta | 💧 Agua |
   | 💧 **Squirtle** | Agua | 110 | 🔥 Fuego | 🌿 Planta |
   | 🌿 **Bulbasaur** | Planta | 105 | 💧 Agua | 🔥 Fuego |

4. **⚔️ Batalla contra el rival**
   - El sistema selecciona aleatoriamente un oponente
   - La batalla comienza automáticamente

5. **🎲 Toma decisiones estratégicas:**
   - **Considera el tipo del oponente** para elegir ataques efectivos
   - **Equilibra daño vs precisión:**
     - Ataques potentes tienen menor precisión
     - Ataques precisos hacen menos daño
   - **Usa ataques Normal** para ignorar resistencias (daño garantizado si acierta)

6. **📊 Observa la batalla:**
   - Barras de HP visuales y actualizadas en tiempo real
   - Mensajes de combate detallados
   - Indicadores de daño causado y recibido
   - Notificaciones de ataques fallidos

7. **🏆 Victoria o derrota:**
   - Gana al reducir el HP del enemigo a 0
   - Pierdes si tu HP llega a 0
   - Puedes reintentar cuantas veces quieras

---

## ⚡ Sistema de combate detallado

### 🎯 Mecánicas de batalla:

#### **Sistema de turnos:**
1. Turno del jugador (elige ataque manualmente)
2. Turno del enemigo (IA elige ataque aleatorio)
3. Repetir hasta que un Pokémon sea derrotado

#### **Cálculo de precisión:**
- Cada ataque tiene un % de precisión (85%-100%)
- Se genera un número aleatorio 1-100
- Si el número ≤ precisión → Ataque acierta
- Si el número > precisión → Ataque falla

#### **Cálculo de daño:**
```
Daño final = Daño base × Multiplicador aleatorio × Modificador de tipo
```
- **Multiplicador aleatorio**: 0.8 - 1.2 (80%-120% del daño base)
- **Modificador de tipo**: 
  - 1.0 (100%) para daño normal
  - 0.5 (50%) si el defensor resiste ese tipo
  - Ataques "Normal" siempre causan daño base (ignoran resistencias)

#### **Sistema de tipos elementales:**

```
🔥 Fuego  →  🌿 Planta  →  💧 Agua  →  🔥 Fuego
(débil)     (débil)       (débil)
```

**Tabla de efectividad:**

| Atacante ↓ / Defensor → | 🔥 Fuego | 💧 Agua | 🌿 Planta |
|-------------------------|---------|---------|-----------|
| 🔥 **Fuego** | Normal | Poco efectivo (50%) | Normal |
| 💧 **Agua** | Normal | Normal | Poco efectivo (50%) |
| 🌿 **Planta** | Poco efectivo (50%) | Normal | Normal |
| ⚪ **Normal** | Normal | Normal | Normal |

---

## 💡 Ejemplos de flujo de ejecución

### Ejemplo 1: Ataque normal (sin resistencia)
```
1. Jugador elige Charmander (Fuego, 100 HP)
2. Enemigo aleatorio: Bulbasaur (Planta, 105 HP)
3. Jugador usa "Lanzallamas" (30 daño, Fuego, 85% precisión)
4. Cálculo de precisión: Random(1-100) = 45 ≤ 85 → ¡Acierta!
5. Cálculo de daño: 30 × 1.15 = 34 daño
6. Aplicación de daño:
   - Tipo de ataque: "Fuego"
   - Tipo de defensor: "Planta"
   - Sin resistencia → Daño completo
   - HP de Bulbasaur: 105 → 71
7. Turno del enemigo...
```

### Ejemplo 2: Ataque con resistencia
```
1. Jugador elige Squirtle (Agua, 110 HP)
2. Enemigo: Charmander (Fuego, 100 HP)
3. Charmander usa "Lanzallamas" (30 daño, Fuego, 85% precisión)
4. ¡Acierta!
5. Daño calculado: 30 × 0.95 = 28 daño
6. Aplicación de daño:
   - Tipo de ataque: "Fuego"
   - Tipo de defensor: PokemonAgua
   - Override activado: if (tipoAtaque == "Fuego")
   - Resistencia aplicada: 28 × 0.5 = 14 daño final
   - HP de Squirtle: 110 → 96 ¡Resistió!
```

### Ejemplo 3: Ataque tipo Normal (ignora resistencias)
```
1. Cualquier Pokémon usa "Placaje" (15 daño, Normal, 100% precisión)
2. Siempre acierta (100% precisión)
3. Daño calculado: 15 × 1.10 = 16 daño
4. Aplicación de daño:
   - Tipo de ataque: "Normal"
   - Sobrecarga activada: RecibirDanio(16) // Sin tipo
   - No se activa override
   - Daño directo sin modificadores
```

---

## 📚 Tecnologías utilizadas

- **Lenguaje**: C# (.NET 6+)
- **Paradigmas**: Programación Orientada a Objetos + Programación Funcional
- **Consola**: Terminal con soporte UTF-8 y colores ANSI
- **Control de versiones**: Git & GitHub

---

## 👥 Autores

Jhon Daniel Gaviria
Jose David Velez
Andres Felipe Navarro

---

**¡Gracias por jugar! 🎮✨**


