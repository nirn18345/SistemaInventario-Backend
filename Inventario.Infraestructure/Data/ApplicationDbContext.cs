using Inventario.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Infraestructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger) : base(options) {
            _logger = logger;
        }
        public DbSet<Producto> Productos {  get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<ProductoPrecioLote> ProductoPrecioLote { get; set; }
        public DbSet<Usuario> Usuarios {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Filtro global para excluir productos eliminados
            modelBuilder.Entity<Producto>().HasQueryFilter(p => p.Activo);
            modelBuilder.Entity<ProductoPrecioLote>().Property(p => p.Precio).HasPrecision(18, 2);

            modelBuilder.Entity<ProductoPrecioLote>()
           .HasOne(pp => pp.Producto)
           .WithMany(p => p.Precios)
           .HasForeignKey(pp => pp.ProductoID);

            modelBuilder.Entity<ProductoPrecioLote>()
                .HasOne(pp => pp.Lote)
                .WithMany(l => l.Precios)
                .HasForeignKey(pp => pp.LoteID);

            // Configuración de las fechas
            modelBuilder.Entity<ProductoPrecioLote>()
                .Property(pp => pp.FechaInicio)
                .HasColumnType("datetime");

            modelBuilder.Entity<ProductoPrecioLote>()
                .Property(pp => pp.FechaFin)
                .HasColumnType("datetime");
        }


        public override int SaveChanges()
        {
            _logger.LogInformation("Se guardaron los cambios en la base de datos...");
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Se guardaron los cambios asincronicos en la base de datos.");
            return base.SaveChangesAsync(cancellationToken);
        }


    }
}
