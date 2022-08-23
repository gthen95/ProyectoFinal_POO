using Proyecto_Libros.Clases;
using Proyecto_Libros.Controllers;
using System;
using System.Collections.Generic;
using System.IO;

namespace Proyecto_Libros
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0;
            bool isFirst = true;
            
            do
            {
                if (!isFirst)
                {
                    string resp = Funciones.ConsoleStringYesOrNot("Deseas realizar otra operacion: {S/N): ");


                    if (resp.ToUpper() == "N")
                    {
                        break;
                    }
                    Console.Clear();
                }
                else
                {
                    isFirst = false;
                }

                Funciones.RepetirCaracter("-"); 
                Funciones.AlinearTitulos("***Menu Principal***");
                Console.WriteLine($"1. Ingresar libros.");
                Console.WriteLine($"2. Mover libros de ubicación");
                Console.WriteLine($"3. Modificar libros");
                Console.WriteLine($"4. Vender libros");
                Console.WriteLine($"5. Generar archivo con listado de libros");
                Console.WriteLine($"6. Generar factura de venta");
                Console.WriteLine($"7. Generar archivo listado de Autores");
                Console.WriteLine($"8. Generar listado de tipos");
                Console.WriteLine($"9. Salir");
                Funciones.RepetirCaracter("-");
                Console.Write($"Elige tu opcion....: ");
                var data = Console.ReadLine();

                if (data != null)
                {
                    opcion = int.Parse(data);
                }

                switch (opcion)
                {
                    case 1:

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
                    case 2:

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

                        bool result = LibroController.moveLibro(codigo,ubicacion);
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
                    case 3:

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
                        var libro= LibroController.GetLibroByID(cod);
                        if (libro==null)
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
                    case 4:

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

                        if(detalle != null && detalle.Count>0)
                        {
                            int VentaId = VentaController.addVenta(cliente, detalle);
                            Console.WriteLine($"Venta realizada satisfactoriamente. Numero de factura: {VentaId} \n");
                            VentaController.GeneraFactura(VentaId);
                            
                        }
                        Funciones.RepetirCaracter("=");
                        break;
                    case 5:


                        string filePathLibros = AppDomain.CurrentDomain.BaseDirectory + "ArchivosGenerados";
                        var fileLibros = filePathLibros + "\\" + "ListadoLibros.txt";

                        if (!(Directory.Exists(filePathLibros)))
                            Directory.CreateDirectory(filePathLibros);

                        LibroController.GeneraArchivoListadoLibros(fileLibros);
                        
                        break;
                    case 6:


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
                    case 7:

                        string filePathAutores = AppDomain.CurrentDomain.BaseDirectory + "ArchivosGenerados";
                        var fileAutores = filePathAutores + "\\" + "ListadoAutores.txt";

                        if (!(Directory.Exists(filePathAutores)))
                            Directory.CreateDirectory(filePathAutores);

                        LibroController.GeneraArchivoListadoAutores(fileAutores);

                        break;
                    case 8:

                        string filePathTiposLibros = AppDomain.CurrentDomain.BaseDirectory + "ArchivosGenerados";
                        var fileTiposLibros = filePathTiposLibros + "\\" + "ListadoTiposLibros.txt";

                        if (!(Directory.Exists(filePathTiposLibros)))
                            Directory.CreateDirectory(filePathTiposLibros);

                        LibroController.GeneraArchivoListadoTiposLibros(fileTiposLibros);

                        break;
                    case 9:
                        opcion = 9;
                        break;
                    default:
                        Console.WriteLine("Elige una opcion del menu");
                        break;
                }
            }
            while (opcion != 9);



        }
    }
}
