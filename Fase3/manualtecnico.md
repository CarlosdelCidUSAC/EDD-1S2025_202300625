# Manual Técnico del Proyecto EDD-1S2025_202300625

## Índice

1. [Introducción](#introducción)
2. [Estructura del Proyecto](#estructura-del-proyecto)
3. [Descripción General del Funcionamiento](#descripción-general-del-funcionamiento)
4. [Ventanas Principales](#ventanas-principales)
    - [Login](#login)
    - [Menú Administrador](#menú-administrador)
    - [Menú Usuario](#menú-usuario)
5. [Estructuras de Datos Utilizadas](#estructuras-de-datos-utilizadas)
6. [Gestión de Sesiones y Reportes](#gestión-de-sesiones-y-reportes)
7. [Respaldo y Recuperación de Datos](#respaldo-y-recuperación-de-datos)
8. [Consideraciones Técnicas](#consideraciones-técnicas)
9. [Detalles de Implementación](#detalles-de-implementación)
10. [Extensibilidad y Mantenimiento](#extensibilidad-y-mantenimiento)

---

## Introducción

Este documento describe el funcionamiento técnico del sistema desarrollado para la gestión de usuarios, vehículos, repuestos, servicios y facturación, utilizando estructuras de datos avanzadas y una interfaz gráfica basada en GTK#.

El programa está diseñado para ser modular, eficiente y seguro, facilitando la administración de información relevante para talleres automotrices o sistemas similares. Se prioriza la integridad de los datos, la trazabilidad de las operaciones y la facilidad de uso para distintos tipos de usuarios.

---

## Estructura del Proyecto

El proyecto está organizado en varias carpetas y archivos principales:

- **Program.cs**: Punto de entrada de la aplicación y gestor de instancias globales.
- **ventanas/**: Contiene las ventanas de la interfaz gráfica (Login, Menú, Visualizaciones, etc.).
- **Estructuras de datos**: Implementaciones de Blockchain, AVL, BST, listas, grafos y Merkle Tree.
- **Reportes**: Carpeta donde se almacenan los reportes generados.
- **reportedot**: Carpeta para archivos DOT de visualización de estructuras.

---

## Descripción General del Funcionamiento

1. **Inicio**: Al ejecutar el programa, se inicializa la aplicación GTK y se muestra la ventana de Login.
2. **Login**: El usuario ingresa sus credenciales. Según el tipo de usuario, se muestra el menú correspondiente (Administrador o Usuario).
3. **Menús**: Desde los menús se accede a las funcionalidades principales: carga masiva, inserción, visualización, creación de servicios, reportes, etc.
4. **Gestión de Datos**: Todas las operaciones se realizan sobre estructuras de datos en memoria, con respaldo y restauración desde archivos.
5. **Reportes**: El sistema permite generar reportes en formato JSON y visualizaciones gráficas de las estructuras.

El programa implementa un flujo de trabajo seguro y controlado, donde cada acción relevante queda registrada. La arquitectura modular permite agregar nuevas funcionalidades o modificar las existentes sin afectar el resto del sistema. Además, la interfaz gráfica está diseñada para ser intuitiva y adaptarse a las necesidades de los usuarios finales.

---

## Ventanas Principales

### Login

- Permite el acceso de usuarios al sistema.
- Valida credenciales y redirige al menú correspondiente.

### Menú Administrador

- **Carga Masiva**: Permite cargar datos desde archivos.
- **Insertar Usuario**: Inserta nuevos usuarios en la blockchain.
- **Visualizar Repuestos**: Muestra el árbol AVL de repuestos.
- **Crear Servicio**: Permite agregar nuevos servicios.
- **Reportes**: Genera archivos JSON y visualizaciones gráficas de las estructuras.

### Menú Usuario

- **Visualizar Vehículos**: Muestra los vehículos registrados.
- **Visualizar Repuestos**: Permite consultar repuestos disponibles.
- **Visualizar Facturas**: Acceso al historial de facturación.
- **Cerrar Sesión**: Guarda la sesión y regresa al login.

---

## Estructuras de Datos Utilizadas

- **Blockchain**: Para la gestión de usuarios.
- **Árbol AVL**: Para repuestos.
- **Lista Enlazada**: Para vehículos.
- **Árbol Binario de Búsqueda (BST)**: Para servicios.
- **Lista de Listas (Grafo)**: Para rutas o relaciones.
- **Merkle Tree**: Para la integridad de las facturas.

Cada estructura de datos fue seleccionada por sus ventajas específicas en cuanto a eficiencia, seguridad y facilidad de mantenimiento. Por ejemplo, la blockchain garantiza la inmutabilidad de los registros de usuarios, mientras que el Merkle Tree permite verificar la integridad de las facturas de manera eficiente.

---

## Gestión de Sesiones y Reportes

- Cada vez que un usuario inicia y cierra sesión, se registra en la lista `RegistroSesiones`.
- Al generar reportes, se crea un archivo JSON con el historial de sesiones y se generan visualizaciones gráficas de las estructuras de datos.
- Los reportes se almacenan en la carpeta `Reportes`.

El sistema también permite la auditoría de acciones y la generación de reportes personalizados, facilitando el análisis y la toma de decisiones.

---

## Respaldo y Recuperación de Datos

- Al cerrar sesión o salir del sistema, se realiza un respaldo automático de los datos principales (usuarios, repuestos, etc.).
- Al iniciar el sistema, se restauran los datos desde los archivos de respaldo si existen.

El mecanismo de respaldo utiliza serialización JSON para asegurar la portabilidad y facilidad de recuperación de la información.

---

## Consideraciones Técnicas

- **Lenguaje**: C# (.NET)
- **Interfaz gráfica**: GTK#
- **Serialización**: JSON para respaldo y reportes.
- **Visualización de estructuras**: Archivos DOT y generación de imágenes.
- **Plataforma**: Compatible con Linux.

El sistema está optimizado para ejecutarse en entornos Linux, aprovechando la robustez de .NET y la flexibilidad de GTK# para la interfaz gráfica.

---

## Detalles de Implementación

- **Modularidad**: Cada funcionalidad está implementada en clases y módulos independientes, facilitando el mantenimiento y la escalabilidad.
- **Gestión de errores**: Se implementan mecanismos de manejo de excepciones para garantizar la estabilidad del sistema ante entradas inválidas o fallos inesperados.
- **Seguridad**: El acceso a las funcionalidades está restringido según el tipo de usuario, y las credenciales se gestionan de forma segura.
- **Internacionalización**: El sistema puede adaptarse fácilmente a otros idiomas modificando los textos de la interfaz.

---

## Extensibilidad y Mantenimiento

El diseño del programa permite agregar nuevas estructuras de datos, ventanas o funcionalidades con un impacto mínimo en el resto del sistema. Se recomienda seguir las convenciones de codificación y documentación establecidas para facilitar el trabajo colaborativo y el mantenimiento a largo plazo.

---

**Nota:** Para modificar o extender el sistema, se recomienda revisar la inicialización de instancias en `Program.cs` y la lógica de cada ventana en la carpeta `ventanas/`. Además, es importante mantener actualizada la documentación técnica para reflejar cualquier cambio significativo en la arquitectura o funcionalidad del sistema.