using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.Entidades
{
    public  class Lote
    {

        public int Id { get; set; }
        public string NumeroLote { get; set; }

        public string Descripcion { get; set; }
        public DateTime FechaIngreso { get; set; }

        // Relación de uno a muchos con PreciosProductoPorLote
        public ICollection<ProductoPrecioLote> Precios { get; set; }
    }
}
