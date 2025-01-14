using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.DTOs
{
    public class LoteDto
    {

        public int Id { get; set; }
        public string NumeroLote { get; set; }

        public string Descripcion { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
