using Proyecto_Libros.Clases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Libros.Controllers
{
    public  class LibroController 
    {
        static List<Libro> libros { get; set; }=new List<Libro>();

        public static int GetNextId()
        {
            return libros.Count == 0 ? 1 : libros.Max(l => l.ID) + 1;
        }

        public static int addLibro(string Nombre, decimal Precio, int Cantidad, string Tipo, string Ubicacion, string Autor)
        {
            int ID = GetNextId();
            libros.Add(new Libro
            {
                ID = ID,
                Nombre = Nombre,
                Precio = Precio,
                Cantidad = Cantidad,
                Tipo = Tipo,
                Ubicacion = Ubicacion,
                Autor = Autor
            });

            return ID;
        }

        public static bool moveLibro(int ID, string nuevaUbicacion)
        {

            var libro = libros.SingleOrDefault(l => l.ID == ID);
            if (libro == null)
            {
                return false;
            }
            libro.Ubicacion = nuevaUbicacion;
            return true;
        }

        public static bool updateLibro(int ID, string Nombre, decimal Precio, int Cantidad, string Tipo, string Ubicacion, string Autor)
        {
            var libro = libros.SingleOrDefault(l => l.ID == ID);
            if (libro == null)
            {
                return false;
            }

            libro.Nombre = Nombre;
            libro.Precio = Precio;
            libro.Cantidad = Cantidad;
            libro.Tipo = Tipo;
            libro.Ubicacion = Ubicacion;
            libro.Autor = Autor;

            return true;
        }

        public static List<Libro> listarTodos()
        {
            return libros;
        }

        public static Libro? GetLibroByID(int ID)
        {
           return libros.SingleOrDefault(x => x.ID == ID);
        }

        public static void GeneraArchivoListadoLibros(string file)
        {
            try
            {
                StreamWriter sw = new StreamWriter(file);

                sw.WriteLine("LISTADO DE LIBROS");
                sw.WriteLine("=========================================");
                foreach (var l in LibroController.listarTodos())
                {
                    sw.WriteLine($"Nombre.....:{l.Nombre}");
                    sw.WriteLine($"Precio.....:{l.Precio.ToString("C2")}");
                    sw.WriteLine($"Cantidad...:{l.Cantidad.ToString("N2")}");
                    sw.WriteLine($"Tipo.......:{l.Tipo}");
                    sw.WriteLine($"Ubicacion..:{l.Ubicacion}");
                    sw.WriteLine($"Autor......:{l.Autor}");
                    sw.WriteLine("=========================================");
                }

                sw.Close();
                Console.WriteLine("Archivo creado!!");
                System.Diagnostics.Process.Start("notepad.exe", file);
            }
            catch (Exception ex)
            {
                ErrorHandler.registerError(ex);
                Console.WriteLine("Ha ocurrido un error: " + ex);
            }
        }

        public static void GeneraArchivoListadoAutores(string file)
        {
            try
            {
                StreamWriter sw = new StreamWriter(file);

                sw.WriteLine("LISTADO DE AUTORES");
                sw.WriteLine("=========================================");
                foreach (var l in LibroController.listarTodos())
                {
                    sw.WriteLine($"Autor......:{l.Autor}");
                    sw.WriteLine("=========================================");
                }

                sw.Close();
                Console.WriteLine("Archivo creado!!");
                System.Diagnostics.Process.Start("notepad.exe", file);
            }
            catch (Exception ex)
            {
                ErrorHandler.registerError(ex);
                Console.WriteLine("Ha ocurrido un error: " + ex);
            }
        }

        public static void GeneraArchivoListadoTiposLibros(string file)
        {
            try
            {
                StreamWriter sw = new StreamWriter(file);

                sw.WriteLine("LISTADO DE TIPOS DE LIBROS");
                sw.WriteLine("=========================================");
                foreach (var l in LibroController.listarTodos())
                {
                    sw.WriteLine($"Tipo......:{l.Tipo}");
                    sw.WriteLine("=========================================");
                }

                sw.Close();
                Console.WriteLine("Archivo creado!!");
                System.Diagnostics.Process.Start("notepad.exe", file);
            }
            catch (Exception ex)
            {
                ErrorHandler.registerError(ex);
                Console.WriteLine("Ha ocurrido un error: " + ex);
            }
        }



    }
}
