using AutoMapper;
using AutoMapper.QueryableExtensions;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Application.Interfaces;
using DemoCleanArchitecture.Domain.Entities;
using DemoCleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Infrastructure.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public MenuRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            var sw = Stopwatch.StartNew();
            var query = await _context.Menus
                .Where(m => m.IsActive && m.DeletedAt == null)
                .OrderBy(m => m.DisplayOrder)
                .Select(m => new Domain.Entities.Menu
                {
                    Id = m.Id,
                    Name = m.Name,
                    Slug = m.Slug,
                    IsActive = m.IsActive,
                })
                .AsNoTracking()
                .ToListAsync();
            //var query = await _context.Menus
            //    .Where(m => m.IsActive && m.DeletedAt == null)
            //    .OrderBy(m => m.DisplayOrder)
            //    .Select(m => new Domain.Entities.Menu
            //    {
            //        Id = m.Id,
            //        Name = m.Name,
            //        Slug = m.Slug,
            //        DisplayOrder = m.DisplayOrder,
            //        IsActive = m.IsActive,
            //        ParentId = m.ParentId,
            //        News = m.News
            //        .Select(n => new Domain.Entities.News { Id = n.Id }).ToList(),
            //        Children = m.InverseParent.Select(c => new Domain.Entities.Menu
            //        {
            //            Id = c.Id,
            //            Name = c.Name,
            //            Slug = c.Slug,
            //            IsActive = c.IsActive
            //        }).ToList()
            //    })
            //    .AsNoTracking()
            //    .ToListAsync();

            //var data = await _context.Menus
            //     .Include(m => m.News)
            //     .Include(m => m.InverseParent)
            //     .Where(m => m.IsActive && m.DeletedAt == null)
            //     .OrderBy(m => m.DisplayOrder)
            //     .AsNoTracking()
            //     .ToListAsync();
            //var query = _mapper.Map<IEnumerable<Domain.Entities.Menu>>(data);

            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
            return query;

        }
    }
}
