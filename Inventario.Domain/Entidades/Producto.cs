using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.Entidades
{
    public class Producto
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        

        public int Stock {  get; set; }


        public string Estado {  get; set; }

        public string Imagen {  get; set; } 

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacio {  get; set; } 

        public bool Activo {  get; set; }

        public ICollection<ProductoPrecioLote> Precios { get; set; }



    }
}
