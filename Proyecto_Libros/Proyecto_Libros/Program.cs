using Proyecto_Libros.Clases;
using Proyecto_Libros.Controllers;
using System;
using System.Collections.Generic;
using System.IO;

namespace Proyecto_Libros
{
    internal class Program
    {

        //Variable que almacena opciones del menu
        private static string[] OpcionesMenu = new string[]
        {
            "1. Ingresar libros.",
            "2. Mover libros de ubicación",
            "3. Modificar libros",
            "4. Vender libros",
            "5. Generar archivo con listado de libros",
            "6. Generar factura de venta",
            "7. Generar archivo listado de Autores",
            "8. Generar listado de tipos",
            "9. Salir"
        };

        private static int x;
        private static int y;

        static void Main(string[] args)
        {
            bool Loop = true;
            int counter = 0;
            ConsoleKeyInfo Tecla;
            bool isFirst = true;

            while (Loop)
            {
                //Aqui se valida si es la primera vez que ingresa para saber que preguntar.
                if (!isFirst)
                {
                    string resp = Funciones.ConsoleStringYesOrNot("Deseas realizar otra operacion: {S/N): ");
                    if (resp.ToUpper() == "N")
                    {
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    }
                    Console.Clear();
                }
                else
                {
                    isFirst = false;
                }

                //se muestran las opciones del menu y se permite seleccionar capturando la seleccion via la tecla Enter
                Console.CursorVisible = false;
                Console.WriteLine("Seleccione una opcion del menu y presione ENTER" + System.Environment.NewLine);
                x = Console.CursorLeft;
                y = Console.CursorTop;
                string Resultado = Menu(OpcionesMenu, counter);

                while ((Tecla = Console.ReadKey(true)).Key != ConsoleKey.Enter)
                {
                    switch (Tecla.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (counter == OpcionesMenu.Length - 1) continue;
                            counter++;
                            break;
                        case ConsoleKey.UpArrow:
                            if (counter == 0) continue;
                            counter--;
                            break;
                        default:
                            break;
                    }
                    Console.CursorLeft = x;
                    Console.CursorTop = y;

                    Resultado = Menu(OpcionesMenu, counter);

                }

                //Se valida la opcion seleccionada para ejecutar accion correspondiente
                switch (counter)
                {
                    case 0:
                        Console.CursorVisible = true;
                        Funciones.RepetirCaracter("=");
                        Funciones.AlinearTitulos("|  INGRESO DE LIBROS  |");
                        var nombre = Funciones.ConsoleString("Escriba el nombre del libro: ");
                        var precio = Funciones.ConsoleDecimal("Escriba el precio del libro: ");
                        var cantidad = Funciones.ConsoleInteger("Escriba la cantidad del libro: ");
                        var tipo = Funciones.ConsoleString("Escriba el tipo del libro: ");
                        var ubi = Funciones.ConsoleString("Escriba la ubicacion del libro: ");
                        var autor = Funciones.ConsoleString("Escriba el autor del libro: ");

                        int resultado = LibroController.addLibro(nombre, precio, cantidad, tipo, ubi, autor);
                        Console.WriteLine($"Libro registrado satisfactoriamente con ID: {resultado} \n");
                        Funciones.RepetirCaracter("=");
                        break;
                    case 1:
                        Console.CursorVisible = true;
                        Funciones.RepetirCaracter("=");
                        Funciones.AlinearTitulos("|  MOVIMIENTO DE LIBRO  |");
                        Funciones.RepetirCaracter("=");
                        Funciones.AlinearTitulos("===LISTADO DE LIBROS===");
                        foreach (var l in LibroController.listarTodos())
                        {
                            Console.WriteLine($"{l.ID} - {l.Nombre} - Ubicacion: {l.Ubicacion}");
                        }
                        Funciones.RepetirCaracter("=");
                        var codigo = Funciones.ConsoleInteger("Escriba el codigo del libro: ");
                        var ubicacion = Funciones.ConsoleString("Escriba la ubicacion a mover: ");

                        bool result = LibroController.moveLibro(codigo, ubicacion);
                        if (result)
                        {
                            Console.WriteLine($"Libro con ID: {codigo} movido satisfactoriamente a la ubicacion: {ubicacion} \n");
                        }
                        else
                        {
                            Console.WriteLine($"Libro con ID: {codigo} no encontrado. \n");
                        }
                        Funciones.RepetirCaracter("=");
                        break;
                    case 2:
                        Console.CursorVisible = true;
                        Funciones.RepetirCaracter("=");
                        Funciones.AlinearTitulos("|  MODIFICACION DE LIBRO  |");
                        Funciones.RepetirCaracter("=");
                        Funciones.AlinearTitulos("===LISTADO DE LIBROS===");
                        foreach (var l in LibroController.listarTodos())
                        {
                            Console.WriteLine($"ID: {l.ID} | Nombre: {l.Nombre} | Precio: {l.Precio} | Cantidad: {l.Cantidad} | Ubicacion: {l.Ubicacion} | Tipo {l.Tipo} | Autor: {l.Autor}");
                        }
                        Funciones.RepetirCaracter("=");
                        var cod = Funciones.ConsoleInteger("Escriba el codigo del libro: ");
                        var libro = LibroController.GetLibroByID(cod);
                        if (libro == null)
                        {
                            Console.WriteLine($"Libro con ID: {cod} no encontrado. \n");
                        }
                        var nombreNew = Funciones.ConsoleString("Escriba el nuevo nombre del libro: ");
                        var precioNew = Funciones.ConsoleDecimal("Escriba el nuevo precio del libro: ");
                        var cantidadNew = Funciones.ConsoleInteger("Escriba la nueva cantidad del libro: ");
                        var tipoNew = Funciones.ConsoleString("Escriba el nuevo tipo del libro: ");
                        var ubiNew = Funciones.ConsoleString("Escriba la nueva ubicacion del libro: ");
                        var autorNew = Funciones.ConsoleString("Escriba el nuevo autor del libro: ");

                        bool resultadoNew = LibroController.updateLibro(cod, nombreNew, precioNew, cantidadNew, tipoNew, ubiNew, autorNew);
                        if (resultadoNew)
                        {
                            Console.WriteLine($"Libro con ID: {cod} actualizado satisfactoriamente. \n");
                        }
                        else
                        {
                            Console.WriteLine($"Libro con ID: {cod} no encontrado. \n");
                        }
                        Funciones.RepetirCaracter("=");
                        break;
                    case 3:
                        Console.CursorVisible = true;
                        List<VentaDetalle> detalle = new List<VentaDetalle>();
                        Funciones.RepetirCaracter("=");
                        Funciones.AlinearTitulos("|  VENTA DE LIBROS  |");
                        var cliente = Funciones.ConsoleString("Escriba el nombre del cliente: ");
                        bool IsFirstTime = true;

                        string preguntaAgregar = "";
                        do
                        {
                            if (!IsFirstTime)
                            {
                                preguntaAgregar = Funciones.ConsoleStringYesOrNot("\nDesea agregar otro libro (S/N):");
                            }
                            else
                            {
                                preguntaAgregar = "S";
                                IsFirstTime = false;
                            }

                            if (preguntaAgregar.ToUpper() == "S")
                            {

                                Funciones.RepetirCaracter("=");
                                Funciones.AlinearTitulos("===LISTADO DE LIBROS===");
                                foreach (var l in LibroController.listarTodos())
                                {
                                    Console.WriteLine($"{l.ID} - {l.Nombre} - Cantidad Disponible: {l.Cantidad.ToString("N2")} unidades");
                                }
                                Funciones.RepetirCaracter("=");

                                var codLib = Funciones.ConsoleInteger("Escriba el codigo del libro a vender: ");
                                var lib = LibroController.GetLibroByID(codLib);
                                if (lib == null)
                                {
                                    Console.WriteLine($"Libro con ID: {codLib} no encontrado. \n");
                                    break;
                                }
                                else if (lib.Cantidad == 0)
                                {
                                    Console.WriteLine($"Libro con ID: {codLib} no tiene inventario. \n");
                                    break;
                                }

                                var cantidadVenta = Funciones.ConsoleInteger("Escriba la cantidad a vender: ");
                                if (lib.Cantidad < cantidadVenta)
                                {
                                    Console.WriteLine($"Libro con ID: {codLib} no tiene la cantidad suficiente solicitada. Inventario actual: {lib.Cantidad.ToString("N2")} \n");
                                    break;
                                }
                                else
                                {
                                    lib.Cantidad = lib.Cantidad - cantidadVenta;
                                }


                                detalle.Add(new VentaDetalle
                                {
                                    Cantidad = cantidadVenta,
                                    LibroId = lib.ID,
                                    Nombre = lib.Nombre,
                                    Precio = lib.Precio,
                                    SubTotal = lib.Precio * cantidadVenta
                                });
                            }


                        } while (preguntaAgregar.ToUpper() != "N");

                        if (detalle != null && detalle.Count > 0)
                        {
                            int VentaId = VentaController.addVenta(cliente, detalle);
                            Console.WriteLine($"Venta realizada satisfactoriamente. Numero de factura: {VentaId} \n");
                            VentaController.GeneraFactura(VentaId);

                        }
                        Funciones.RepetirCaracter("=");
                        break;
                    case 4:
                        Console.CursorVisible = true;
                        string filePathLibros = AppDomain.CurrentDomain.BaseDirectory + "ArchivosGenerados";
                        var fileLibros = filePathLibros + "\\" + "ListadoLibros.txt";

                        if (!(Directory.Exists(filePathLibros)))
                            Directory.CreateDirectory(filePathLibros);

                        LibroController.GeneraArchivoListadoLibros(fileLibros);
                        break;
                    case 5:
                        Console.CursorVisible = true;
                        string filePathFactura = AppDomain.CurrentDomain.BaseDirectory + "ArchivosGenerados";
                        var fileFactura = filePathFactura + "\\" + "FacturaVenta.txt";

                        if (!(Directory.Exists(filePathFactura)))
                            Directory.CreateDirectory(filePathFactura);

                        Funciones.RepetirCaracter("=");
                        Funciones.AlinearTitulos("===LISTADO DE VENTAS===");
                        foreach (var v in VentaController.listarTodas())
                        {
                            Console.WriteLine($"{v.Id} - {v.Cliente} en Fecha: {v.Fecha}");
                        }
                        Funciones.RepetirCaracter("=");

                        var codFac = Funciones.ConsoleInteger("Escriba el codigo de la venta: ");
                        var ven = VentaController.GetVentaByID(codFac);
                        if (ven == null)
                        {
                            Console.WriteLine($"Venta con ID: {codFac} no encontrada. \n");
                            break;
                        }
                        else
                        {
                            VentaController.GeneraArchivoFactura(codFac, fileFactura);
                        }
                        break;
                    case 6:
                        Console.CursorVisible = true;
                        string filePathAutores = AppDomain.CurrentDomain.BaseDirectory + "ArchivosGenerados";
                        var fileAutores = filePathAutores + "\\" + "ListadoAutores.txt";

                        if (!(Directory.Exists(filePathAutores)))
                            Directory.CreateDirectory(filePathAutores);

                        LibroController.GeneraArchivoListadoAutores(fileAutores);
                        break;
                    case 7:
                        Console.CursorVisible = true;
                        string filePathTiposLibros = AppDomain.CurrentDomain.BaseDirectory + "ArchivosGenerados";
                        var fileTiposLibros = filePathTiposLibros + "\\" + "ListadoTiposLibros.txt";

                        if (!(Directory.Exists(filePathTiposLibros)))
                            Directory.CreateDirectory(filePathTiposLibros);

                        LibroController.GeneraArchivoListadoTiposLibros(fileTiposLibros);
                        break;
                    case 8:
                        Loop = false;
                        Console.WriteLine("Saliendo del sistema...");
                        break;

                }
            }

        }


        //Esto es para crear un menu principal con seleccion via el cursor del teclado
        private static string Menu(string[] items, int opcion)
        {
            string SeleccionActual = string.Empty;
            int destacado = 0;

            Array.ForEach(items, element =>
            {
                if (destacado == opcion)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(" > " + element + " < ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    SeleccionActual = element;
                }
                else
                {
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.CursorLeft = 0;
                    Console.WriteLine(element);
                }
                destacado++;
            });

            return SeleccionActual;
        }

        
    }
}
