using Proyecto_Libros.Clases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Libros.Controllers
{
    public class VentaController 
    {
        static List<Venta> ventas { get; set; } = new List<Venta>();

        public static int GetNextId()
        {
            return ventas.Count == 0 ? 1 : ventas.Max(v => v.Id) + 1;
        }

        public static int addVenta(string Cliente, List<VentaDetalle> ventaDetalles)
        {
            var Id = GetNextId();
            var totalVenta = ventaDetalles.Sum(x => x.SubTotal);
            ventas.Add(new Venta
            {
                Cliente = Cliente,
                Id = Id,
                Total = totalVenta,
                ventaDetalles = ventaDetalles,
                Fecha = DateTime.Now
            });

            return Id;
        }

        public static List<Venta> listarTodas()
        {
            return ventas;
        }

        public static Venta? GetVentaByID(int ID)
        {
            return ventas.SingleOrDefault(v => v.Id == ID);
        }

        public static void GeneraFactura(int id)
        {
            var venta = GetVentaByID(id);
            Funciones.RepetirCaracter("=");
            Funciones.AlinearTitulos("|  Factura  |");
            Console.WriteLine($"Cliente..: {venta.Cliente}");
            Funciones.RepetirCaracter("=");
            Funciones.AlinearTitulos("|  DETALLE  |");
            Console.WriteLine("Articulo                Cantidad                           Precio                    Total");
            foreach (var i in venta.ventaDetalles)
            {
                Console.Write($"{i.Nombre.Trim().PadRight(8)}\t\t{i.Cantidad.ToString("N2").PadLeft(8)}\t\t\t{i.Precio.ToString("C2").PadLeft(8)}\t\t{i.SubTotal.ToString("C2").PadLeft(8)}\n");
            }
            Funciones.RepetirCaracter("=");
            Console.WriteLine($"TOTAL..: {venta.Total.ToString("C2").PadLeft(12)}");
            Funciones.RepetirCaracter("=");
        }

        public static void GeneraArchivoFactura(int id, string file)
        {
            try
            {
                StreamWriter sw = new StreamWriter(file);
                var venta = VentaController.GetVentaByID(id);
                var title = "";

                sw.WriteLine("***********FACTURA***********");
                sw.WriteLine("==============================================================");
                sw.WriteLine($"Cliente......:{venta.Cliente}");
                sw.WriteLine($"Fecha........:{venta.Fecha}");
                sw.WriteLine("==============================================================");
                sw.WriteLine("***********DETALLE***********");
                sw.WriteLine("Articulo                Cantidad                           Precio                    Total");
                foreach (var i in venta.ventaDetalles)
                {
                    sw.Write($"{i.Nombre.Trim().PadRight(8)}\t\t{i.Cantidad.ToString("N2").PadLeft(8)}\t\t\t{i.Precio.ToString("C2").PadLeft(8)}\t\t{i.SubTotal.ToString("C2").PadLeft(8)}\n");
                }
                sw.WriteLine("==============================================================");
                sw.WriteLine($"TOTAL..: {venta.Total.ToString("C2").PadLeft(12)}");
                sw.WriteLine("==============================================================");


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
