using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Libros.Clases
{
    public  class Libro
    {
        public int ID { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; }
        public string Ubicacion { get; set; }
        public string Autor { get; set; }
        public string Nombre { get; set; }
    }
}
