using Inventario.Application.DTOs;
using Inventario.Application.Interfaces;
using Inventario.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Infraestructure.Services
{
    public class LoteService : ILoteService
    {

        private readonly ApplicationDbContext _context;

        public LoteService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<LoteDto>> ObtenerLotesAsync()
        {
            var lotes = await _context.Lotes
                .ToListAsync(); 

            var loteDtos = lotes.Select(l => new LoteDto
            {
                Id = l.Id,
                NumeroLote = l.NumeroLote,
                Descripcion = l.Descripcion,
                FechaIngreso=l.FechaIngreso
            }).ToList();

            return loteDtos;
        }
    }
}
