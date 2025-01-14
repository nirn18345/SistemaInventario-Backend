using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Interfaces
{
    public interface ICredencialesService
    {
        Task<string> ValidarCredencialesAsync(string username, string password);
    }
}
