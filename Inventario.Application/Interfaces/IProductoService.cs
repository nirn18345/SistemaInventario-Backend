using Inventario.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Application.Interfaces
{
    public interface IProductoService
    {
        Task<int> AgregarProductoAsync(ProductoDto productoDto);
        Task<PaginacionDto<ProductoDto>> ObtenerProductosPaginadosAsync(int page, int size, string sortBy, string search);
        Task<ProductoDto> ObtenertProductosAsync(int id);
        Task<bool> ModificarProductoAsync(int id, ProductoDto productDto);
        Task<bool> EliminarProductoAsync(int id);

    }
}
