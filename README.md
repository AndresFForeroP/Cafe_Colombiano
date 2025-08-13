# ☕ Colombian Coffee - Desktop App

Una aplicación de escritorio desarrollada en **C#** que permite explorar, filtrar y gestionar información técnica sobre las principales variedades de café cultivadas en Colombia. El sistema está construido bajo los principios **SOLID**, con una arquitectura **hexagonal** y enfoque de **vertical slicing**, asegurando escalabilidad, mantenibilidad y separación clara de responsabilidades.

## 📌 Propósito

Brindar una herramienta interactiva y funcional que permita:

- Visualizar fichas técnicas de variedades de café.
- Filtrar por atributos agronómicos y resistencias.
- Generar catálogos PDF personalizados.
- Administrar contenido mediante un panel CRUD.

---

## 👥 Integrantes

- **Andres Felipe Forero Perez** *(Líder de eqipo)*
- Hector Andrés Mejia Samoret
- Hadassa Raquel Galindo Rojas
- Juan Manuel Crispin Castellanos

---

## 🧱 Arquitectura

### 🔷 Hexagonal (Ports & Adapters)

- **Core Domain**: Entidades, lógica de negocio y servicios.
- **Application Layer**: Casos de uso organizados por verticales.
- **Adapters**: Interfaces para persistencia, consola y generación de PDFs.
- **Infrastructure**: Implementaciones concretas (EF, MySQL, PDF libs).

### 🟪 Vertical Slicing

Cada módulo funcional (e.g. Variedades, PDF, Administración) se organiza como una vertical independiente con su propio conjunto de:

- Entidades
- Casos de uso
- Controladores
- Repositorios

---

## 🧠 Principios SOLID

- **S**: Cada clase tiene una única responsabilidad (e.g. `VariedadService`, `PdfGenerator`).
- **O**: Nuevas variedades o filtros se pueden agregar sin modificar clases existentes.
- **L**: Las clases derivadas respetan el comportamiento esperado.
- **I**: Interfaces segregadas para cada tipo de funcionalidad (`ICatalogExporter`, `IVariedadRepository`).
- **D**: Inversión de dependencias mediante inyección de interfaces.

---

## ⚙️ Tecnologías

| Componente           | Tecnología                        |
| -------------------- | --------------------------------- |
| Lenguaje             | C# (.NET)                         |
| ORM                  | Entity Framework                  |
| Base de Datos        | MySQL                             |
| Frontend             | Consola (CLI)                     |
| PDF Generator        | IronPDF / Syncfusion / CraftMyPDF |
| Control de versiones | Git + GitHub (Git Flow)           |

---

## 📂 Estructura del Proyecto

```
<CAFE_COLOMBIANO>/
├── src/
│   ├── Modules/
|     ├── Usuario/
|		├── Domain/
|			├── Entities/
|				└── Usuario.cs
|		├── Application/
|			├── Interface/ 
│   		└── Services/
|		├── Infrastruture/
|			├── Repository/ 
|		├── UI/-> 
│   └── Shared/
│       ├── Configurations/
│       ├── Context/
│       └── Helpers/
├── Cafe_Colombiano.csproj
├── Cafe_Colombiano.sln
├── appsettings.json 
└── Program.cs
```

## 🔐 Funcionalidades Clave

- 📋 Catálogo técnico de variedades con filtros por:
  - Porte, tamaño de grano, altitud, rendimiento, resistencia.
- 🔍 Buscador avanzado con sugerencias inteligentes.
- 🖨️ Generación de PDF con vista previa.
- 🔧 Panel administrativo con autenticación y CRUD.
- ✅ Validaciones en frontend y base de datos.

---

## 📄 Requisitos No Funcionales

- LINQ + sentencias preparadas.
- Validaciones con triggers y procedimientos almacenados.
- Documentación clara por función.
- Interfaz accesible y limpia.

---

## 🚀 Cómo ejecutar

1. Clona el repositorio:

   ```powershell
   https://github.com/AndresFForeroP/Cafe_Colombiano.git
   ```

2. Configura el archivo appsetings segun tu base de datos.

3. Ejecuta desde la consola dotnet run.

---

## 📝 Tareas Fase 1

- **Hector Andrés Mejia Samoret** → UI de todos los menús  
- **Hadassa Raquel Galindo Rojas** → Generar el PDF  
- **Juan Manuel Crispin Castellanos** → Función ver catálogo  
- **Andres Felipe Forero Perez** → Filtrar variedades  

---

---

## 📝 Tareas Fase 2

- **Hector Andrés Mejia Samoret** → Gestion de contenido  
- **Hadassa Raquel Galindo Rojas** → Terminacion Generar el PDF  
- **Juan Manuel Crispin Castellanos** → Gestion de variedades  
- **Andres Felipe Forero Perez** → > Validacion de usuario, y terminar filtrado  

---