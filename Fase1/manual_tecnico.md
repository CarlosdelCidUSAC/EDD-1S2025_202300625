# Manual Técnico del Proyecto

## Descripción General

Este proyecto es una aplicación de escritorio desarrollada en C# utilizando la biblioteca Gtk# para la interfaz gráfica de usuario. La aplicación permite la gestión de usuarios, vehículos, repuestos, servicios y facturas. Además, incluye funcionalidades para cargar archivos y generar reportes gráficos.

## Estructura del Proyecto

El proyecto está compuesto por varias ventanas y clases que manejan diferentes aspectos de la aplicación. A continuación, se describen las principales clases y sus funcionalidades.

### Clases Principales

#### Program.cs

Esta es la clase principal que inicia la aplicación. Contiene las siguientes propiedades estáticas que representan las listas y estructuras de datos utilizadas en la aplicación:

- `UsuariosLista listaUsuarios`: Lista de usuarios.
- `VehiculoListaDoble listaVehiculos`: Lista doblemente enlazada de vehículos.
- `RepuestosListaCircular listaRepuestos`: Lista circular de repuestos.
- `SeviciosCola colaServicios`: Cola de servicios.
- `FacturasPila pilaFacturas`: Pila de facturas.
- `BitacoraMatrizDispersa bitacora`: Matriz dispersa para la bitácora.

El método `Main` inicializa la aplicación Gtk y muestra la ventana de inicio de sesión (`LoginWindow`) y la ventana principal del menú (`MenuWindow`).

```csharp
static void Main()
{
    Application.Init();
    LoginWindow win = new LoginWindow();
    win.ShowAll();

    MenuWindow menu = new MenuWindow();
    menu.ShowAll();

    Application.Run();
}

```
#### UsuariosLista.cs

Esta clase implementa una lista simple para gestionar los usuarios. Cada nodo de la lista (`NodoUsuario`) contiene información sobre un usuario, como su ID, nombre, apellido, correo electrónico y contraseña. La clase proporciona métodos para agregar, buscar, actualizar, borrar e imprimir usuarios, así como para liberar la memoria utilizada por la lista.

#### VehiculosListaDoble.cs

Esta clase implementa una lista doblemente enlazada para gestionar los vehículos. Cada nodo de la lista (`NodoVehiculo`) contiene información sobre un vehículo, como su ID, ID de usuario, marca, año y placa. La clase proporciona métodos para agregar, buscar, borrar e imprimir vehículos, así como para liberar la memoria utilizada por la lista.

#### RepuestosListaCircular.cs

Esta clase implementa una lista circular para gestionar los repuestos. Cada nodo de la lista (`NodoRepuestos`) contiene información sobre un repuesto, como su ID, nombre, detalle y costo. La clase proporciona métodos para agregar, buscar, borrar e imprimir repuestos, así como para liberar la memoria utilizada por la lista.

#### ServiciosCola.cs

Esta clase implementa una cola para gestionar los servicios. Cada nodo de la cola (`NodoServicio`) contiene información sobre un servicio, como su ID, ID de servicio, ID de vehículo, detalles y costo. La clase proporciona métodos para encolar, desencolar, buscar e imprimir servicios, así como para liberar la memoria utilizada por la cola.

#### FacturasPila.cs

Esta clase implementa una pila para gestionar las facturas. Cada nodo de la pila (`NodoFactura`) contiene información sobre una factura, como su ID, ID de servicio, ID de vehículo, detalles, costo y orden. La clase proporciona métodos para apilar, desapilar, buscar e imprimir facturas, así como para liberar la memoria utilizada por la pila.

#### BitacoraMatrizDispersa.cs

Esta clase implementa una matriz dispersa para gestionar la bitácora. Cada celda de la matriz (`NodoCelda`) contiene información sobre un evento, como sus detalles y coordenadas (x, y). La clase proporciona métodos para insertar y graficar eventos en la matriz, así como para verificar si la matriz está vacía.

### Ventanas Principales

#### LoginWindow.cs

Esta ventana permite a los usuarios iniciar sesión en la aplicación. Contiene campos para ingresar el nombre de usuario y la contraseña, y un botón para validar las credenciales. Si las credenciales son correctas, se muestra la ventana del menú principal.

#### MenuWindow.cs

Esta ventana actúa como el menú principal de la aplicación, proporcionando acceso a las diferentes funcionalidades, como la gestión de usuarios, vehículos, repuestos, servicios y facturas. Contiene botones que abren las ventanas correspondientes a cada funcionalidad.

#### UsuarioIngresoWindow.cs

Esta ventana permite ingresar nuevos usuarios al sistema. Contiene campos para ingresar el ID, nombre, apellido, correo electrónico y teléfono del usuario, y un botón para guardar la información.

#### UsuarioGestionWindow.cs

Esta ventana permite gestionar los usuarios existentes. Contiene campos para buscar usuarios por ID, actualizar su información y eliminar usuarios. También muestra la información del usuario buscado.

#### VehiculoIngresoWindow.cs

Esta ventana permite ingresar nuevos vehículos al sistema. Contiene campos para ingresar el ID del vehículo, ID del usuario, marca, modelo y placa, y un botón para guardar la información.

#### RepuestoIngresoWindow.cs

Esta ventana permite ingresar nuevos repuestos al sistema. Contiene campos para ingresar el ID del repuesto, nombre, detalles y costo, y un botón para guardar la información.

#### ServicioIngresoWindow.cs

Esta ventana permite ingresar nuevos servicios al sistema. Contiene campos para ingresar el ID del servicio, ID del vehículo, ID del repuesto, detalles y costo, y un botón para guardar la información.

#### GenerarServicioWindow.cs

Esta ventana permite crear nuevos servicios combinando información de vehículos y repuestos. Contiene campos para ingresar el ID del servicio, ID del repuesto, ID del vehículo, detalles y costo, y un botón para guardar la información.

#### GenerarFacturaWindow.cs

Esta ventana permite generar facturas para los servicios realizados. Muestra la información de la factura, como el ID, ID de la orden y el total a pagar. Si no hay facturas generadas, muestra un mensaje indicando que no hay facturas disponibles.

#### VentanaEmergente.cs

Esta ventana permite seleccionar entre diferentes opciones de gestión, como usuarios, vehículos, repuestos y servicios. Contiene botones que abren las ventanas correspondientes a cada opción seleccionada.

### Modo Unsafe

El modo `unsafe` en C# permite realizar operaciones de bajo nivel que no están verificadas por el entorno de ejecución de .NET. En este proyecto, el modo `unsafe` se utiliza para manipular punteros y realizar operaciones de memoria directa, lo cual puede ser útil para optimizar el rendimiento en ciertas partes críticas del código. Sin embargo, es importante usar este modo con precaución, ya que puede llevar a errores difíciles de depurar y problemas de seguridad.

Para habilitar el modo `unsafe`, se debe agregar la palabra clave `unsafe` en la declaración del método o del bloque de código que lo requiera. Además, es necesario configurar el proyecto para permitir código no seguro en las propiedades del compilador.

```csharp
unsafe
{
    // Código que manipula punteros
}
```

### Graphviz

Graphviz es una herramienta de software para la visualización de grafos. En este proyecto, Graphviz se utiliza para generar representaciones gráficas de las estructuras de datos, como la matriz dispersa de la bitácora. Esto facilita la comprensión y el análisis visual de los datos.

Para integrar Graphviz en la aplicación, se generan archivos en formato DOT que describen los grafos. Luego, estos archivos se procesan con Graphviz para producir imágenes visuales de los grafos.

Ejemplo de un archivo DOT:

```
digraph G {
    node1 -> node2;
    node2 -> node3;
}
```

### Gtk#

Gtk# es una biblioteca gráfica para C# que permite crear interfaces de usuario utilizando el toolkit GTK. En este proyecto, Gtk# se utiliza para desarrollar todas las ventanas y componentes de la interfaz gráfica de la aplicación. Esto incluye ventanas de inicio de sesión, menús, formularios de ingreso y gestión de datos, entre otros.

