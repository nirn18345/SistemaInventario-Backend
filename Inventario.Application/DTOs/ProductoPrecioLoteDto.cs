using Inventario.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.DTOs
{
    public class ProductoPrecioLoteDto
    {

        public int ProductoID { get; set; }
        public int LoteID { get; set; }


        public string? NumeroLote { get; set; }

        public string? Descripcion { get; set; }
        // Propiedades
        public decimal Precio { get; set; }

        public int Cantidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        
        
    }
}
