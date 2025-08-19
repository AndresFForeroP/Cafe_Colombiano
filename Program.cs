/*
# ☕ Colombian Coffee - Desktop App

Una aplicación de escritorio desarrollada en C# que permite explorar, filtrar y gestionar información técnica sobre
las principales variedades de café cultivadas en Colombia. El sistema está construido bajo los principios SOLID, 
con una arquitectura hexagonal y enfoque de vertical slicing, asegurando escalabilidad, mantenibilidad y separación 
clara de responsabilidades.

## 👥 Integrantes

- Andres Felipe Forero Perez(Líder de eqipo)
- Hector Andrés Mejia Samoret
- Hadassa Raquel Galindo Rojas
- Juan Manuel Crispin Castellanos

Campuslands 8 de agosto 2025
---

*/
using Cafe_Colombiano.src.Modules.Usuario.Ui;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var menu = new MenuPrincipal();
        await menu.Iniciar();   
    }
}