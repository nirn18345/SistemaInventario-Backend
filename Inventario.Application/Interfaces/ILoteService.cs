using Inventario.Application.DTOs;
using Inventario.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Interfaces
{
    public interface ILoteService
    {
        Task<List<LoteDto>> ObtenerLotesAsync();
    }
}
