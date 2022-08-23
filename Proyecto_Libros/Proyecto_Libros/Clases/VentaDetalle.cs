using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Libros.Clases
{
    public class VentaDetalle
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
    }
}
