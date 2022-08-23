using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Libros.Clases
{
    public class Venta 
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        public List<VentaDetalle> ventaDetalles { get; set; }
    }
}
