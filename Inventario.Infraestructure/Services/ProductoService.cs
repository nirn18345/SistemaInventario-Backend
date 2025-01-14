using Inventario.Application.DTOs;
using Inventario.Application.Interfaces;
using Inventario.Domain.Entidades;
using Inventario.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Inventario.Infraestructure.Services
{
    public class ProductoService: IProductoService
    {

        private readonly ApplicationDbContext _context;

        public ProductoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AgregarProductoAsync(ProductoDto ProductoDto)
        {
           
            var producto = new Producto
            {
                Nombre = ProductoDto.Nombre,
                Descripcion = ProductoDto.Descripcion,
                Estado = ProductoDto.Estado,
                Imagen = ProductoDto.Imagen,
                Activo = true,
                FechaCreacion = DateTime.UtcNow,
                FechaModificacio = DateTime.UtcNow
            };

           
            var preciosPorLote = new List<ProductoPrecioLote>();
            int stockTotal = 0; 

            foreach (var lotePrecio in ProductoDto.ProductoPrecios)
            {
                
                var lote = await _context.Lotes
                    .FirstOrDefaultAsync(l => l.Id == lotePrecio.LoteID);

                if (lote == null)
                {
                    throw new Exception($"El lote con ID {lotePrecio.LoteID} no existe.");
                }

               
                var precioProductoPorLote = new ProductoPrecioLote
                {
                    ProductoID = producto.Id,   
                    LoteID = lote.Id,           
                    Precio = lotePrecio.Precio,
                    Cantidad = lotePrecio.Cantidad, 
                    FechaInicio = lotePrecio.FechaInicio,
                    FechaFin = lotePrecio.FechaFin
                };

               
                preciosPorLote.Add(precioProductoPorLote);

                
                stockTotal += lotePrecio.Cantidad;
            }

            
            producto.Stock = stockTotal;

            
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync(); 

           
            preciosPorLote.ForEach(p => p.ProductoID = producto.Id);

           
            await _context.ProductoPrecioLote.AddRangeAsync(preciosPorLote);
            await _context.SaveChangesAsync(); 

            return producto.Id; 
        }

        public async Task<PaginacionDto<ProductoDto>> ObtenerProductosPaginadosAsync(int page, int size, string sortBy, string search)
        {
            var query = _context.Productos
                .Include(p => p.Precios)
                .ThenInclude(pp => pp.Lote)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p =>
                    p.Nombre.Contains(search) ||
                    p.Descripcion.Contains(search) 
                    );
            }

            query = sortBy.ToLower() switch
            {
            
                "date" => query.OrderByDescending(p => p.FechaCreacion),
                "Nombre" => query.OrderBy(p => p.Nombre),
                _ => query.OrderBy(p => p.Nombre),
            };

            var totalRecords = await query.CountAsync();

            var products = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            var productoDtos = products.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Stock = p.Stock,
                Estado = p.Estado,
                Imagen = p.Imagen,
                ProductoPrecios = p.Precios.Select(pp => new ProductoPrecioLoteDto
                {
                    ProductoID = pp.ProductoID,
                    LoteID = pp.LoteID,
                    Precio = pp.Precio,
                    Cantidad=pp.Cantidad,
                    FechaInicio = pp.FechaInicio,
                    FechaFin = pp.FechaFin,
                    NumeroLote = pp.Lote.NumeroLote,
                    Descripcion=pp.Lote.Descripcion,
                }).ToList()
            }).ToList();

            return new PaginacionDto<ProductoDto>
            {
                TotalRegistros = totalRecords,
                Items = productoDtos
            };
        }
        public async Task<ProductoDto> ObtenertProductosAsync(int id)
        {
            var producto = await _context.Productos
                 .Where(p => p.Id == id)
                .Include(p => p.Precios)  
                .FirstOrDefaultAsync();

            if (producto == null)
                return null;

            var productoResponse = new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Stock = producto.Stock,
                Estado = producto.Estado,
                Imagen = producto.Imagen,
                ProductoPrecios = producto.Precios.Select(pp => new ProductoPrecioLoteDto
                {
                    ProductoID = pp.ProductoID,
                    LoteID = pp.LoteID,
                    Precio = pp.Precio,
                    Cantidad = pp.Cantidad,
                    FechaInicio = pp.FechaInicio,
                    FechaFin = pp.FechaFin
                }).ToList() 
            };

            return productoResponse;
        }

        public async Task<bool> ModificarProductoAsync(int id, ProductoDto ProductoDto)
        {
            try {

                var product = await _context.Productos.FindAsync(id);

                if (product == null)
                    return false;


                product.Nombre = ProductoDto.Nombre;
                product.Descripcion = ProductoDto.Descripcion;
                product.Estado = ProductoDto.Estado;
                product.Imagen = ProductoDto.Imagen;
                product.FechaModificacio = DateTime.UtcNow;


                int stockTotal = 0;


                foreach (var lotePrecio in ProductoDto.ProductoPrecios)
                {

                    var precioExistente = await _context.ProductoPrecioLote
                        .FirstOrDefaultAsync(pp => pp.ProductoID == product.Id && pp.LoteID == lotePrecio.LoteID);

                    if (precioExistente != null)
                    {

                        precioExistente.Precio = lotePrecio.Precio;
                        precioExistente.Cantidad = lotePrecio.Cantidad;
                        precioExistente.FechaInicio = lotePrecio.FechaInicio;
                        precioExistente.FechaFin = lotePrecio.FechaFin;
                    }
                    else
                    {

                        var nuevoPrecio = new ProductoPrecioLote
                        {
                            ProductoID = product.Id,
                            LoteID = lotePrecio.LoteID,
                            Precio = lotePrecio.Precio,
                            Cantidad = lotePrecio.Cantidad,
                            FechaInicio = lotePrecio.FechaInicio,
                            FechaFin = lotePrecio.FechaFin
                        };
                        await _context.ProductoPrecioLote.AddAsync(nuevoPrecio);
                    }


                    stockTotal += lotePrecio.Cantidad;
                }


                product.Stock = stockTotal;


                await _context.SaveChangesAsync();

                return true;

            }
            catch(Exception e)
            {
                return false;
            }
           
           
        }


        public async Task<bool> EliminarProductoAsync(int id)
        {
            var product = await _context.Productos.FindAsync(id);

            if (product == null)
                return false;

            product.Activo = false;
            product.FechaModificacio = DateTime.UtcNow;

            _context.Productos.Update(product);
            await _context.SaveChangesAsync();

            return true;
        }

       
    }
}
