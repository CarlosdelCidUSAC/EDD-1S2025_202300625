# Manual Técnico del Sistema de Gestión de Taller Automotriz

## Introducción Técnica

El sistema de gestión de taller automotriz es una aplicación desarrollada en C# utilizando el framework **Gtk#** para la creación de interfaces gráficas. Este programa está diseñado para facilitar la administración de usuarios, vehículos, servicios, repuestos y facturas en un entorno de taller mecánico. A continuación, se describen los componentes principales y su funcionamiento técnico.

### Arquitectura del Sistema

El sistema está compuesto por varias clases y estructuras de datos que interactúan entre sí para proporcionar las funcionalidades requeridas. Entre los elementos principales se encuentran:

1. **Clases Principales**:
    - `Program`: Es el punto de entrada del programa. Aquí se inicializan las estructuras de datos globales y se lanza la ventana de inicio de sesión.
    - `MenuUsuarioWindow` y `MenuAdminWindow`: Son las ventanas principales para los usuarios y administradores, respectivamente. Estas ventanas permiten acceder a las funcionalidades específicas según el tipo de usuario.

2. **Estructuras de Datos**:
    - `listaUsuarios`: Lista enlazada para gestionar los usuarios registrados.
    - `ListaVehiculos`: Lista enlazada para almacenar y gestionar los vehículos.
    - `ArbolRepuestos`: Árbol binario para organizar los repuestos disponibles.
    - `AVLServicios`: Árbol AVL para gestionar los servicios ofrecidos.
    - `ArbolBFacturas`: Árbol B para almacenar y organizar las facturas generadas.

3. **Ventanas Secundarias**:
    - Ventanas como `RegistroVehiculoWindow`, `VisualizacionServicioWindow`, y `CancelarFacturaWindow` permiten realizar operaciones específicas como registrar vehículos, visualizar servicios o cancelar facturas.

### Flujo de Ejecución

1. **Inicio del Programa**:
    - El programa comienza en la clase `Program`, donde se inicializan las estructuras de datos y se lanza la ventana de inicio de sesión (`LoginWindow`).

2. **Interacción con el Usuario**:
    - Dependiendo del tipo de usuario (administrador o cliente), se muestra la ventana correspondiente (`MenuAdminWindow` o `MenuUsuarioWindow`).
    - Cada ventana contiene botones que permiten acceder a funcionalidades específicas, como registrar vehículos, generar servicios, o visualizar repuestos.

3. **Gestión de Datos**:
    - Los datos se almacenan en estructuras dinámicas como listas enlazadas y árboles, lo que permite un acceso eficiente y organizado.
    - Se implementan métodos para realizar operaciones como inserción, búsqueda, actualización y eliminación en estas estructuras.

4. **Exportación y Reportes**:
    - El sistema permite exportar registros de usuarios en formato JSON y generar reportes gráficos de las estructuras de datos utilizando herramientas de visualización.

### Tecnologías Utilizadas

- **Lenguaje de Programación**: C#.
- **Framework de Interfaz Gráfica**: Gtk#.
- **Serialización de Datos**: `System.Text.Json` para exportar datos en formato JSON.
- **Estructuras de Datos**: Listas enlazadas, Árboles AVL, Árboles B y Árboles Binarios.

Este diseño modular y eficiente permite que el sistema sea escalable y fácil de mantener, proporcionando una solución robusta para la gestión de talleres automotrices.  