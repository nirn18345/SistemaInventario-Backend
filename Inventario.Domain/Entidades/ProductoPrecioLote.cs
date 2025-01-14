using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.Entidades
{
    public  class ProductoPrecioLote
    {
        public int Id { get; set; }

        // Claves foráneas
        public int ProductoID { get; set; }
        public int LoteID { get; set; }

        // Propiedades
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // Relación con Producto
        public Producto Producto { get; set; }

        // Relación con Lote
        public Lote Lote { get; set; }
    }
}
