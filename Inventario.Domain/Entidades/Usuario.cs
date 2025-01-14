using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.Entidades
{
    public  class Usuario
    {
        public string Id { get; set; }
        public string NombreUsuario {  get; set; }

        public string Clave {  get; set; }

        public string Rol {  get; set; }

    }
}
