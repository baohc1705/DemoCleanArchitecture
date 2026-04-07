using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using DemoCleanArchitecture.Application.Common.DTOs;
using DemoCleanArchitecture.Domain.Entities;
using DemoCleanArchitecture.Domain.Enums;
using DemoCleanArchitecture.Domain.Interfaces;
using DemoCleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<Menu> CreateAsync(Menu menu)
        {
            var req = new Data.PersistenceModels.Menu
            {
                Name = menu.Name,
                Slug = menu.Slug,
                DisplayOrder = menu.DisplayOrder,
                IsActive = menu.IsActive,
                ParentId = menu.ParentId,
            };

            await _context.AddAsync(req);
            await _context.SaveChangesAsync();
            return await GetByIdWithNewsAsync(req.Id);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _context.Menus
                .Where(m => m.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            var sw = Stopwatch.StartNew();
            var query = await _context.Menus
                .Where(m => m.IsActive && m.DeletedAt == null)
                .Select(m => new Domain.Entities.Menu
                {
                    Id = m.Id,
                    Name = m.Name,
                    Slug = m.Slug,
                    IsActive = m.IsActive,
                })
                .AsNoTracking()
                .ToListAsync();
            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
            return query;
        }

        // Cũng có thể sử dụng SP

        public async Task<IEnumerable<Menu>> GetAllWithNewsAsync()
        {
            var sw = Stopwatch.StartNew();

            var query = await _context.Menus
                .Where(m => m.IsActive && m.DeletedAt == null)
                .Select(m => new Domain.Entities.Menu
                {
                    Id = m.Id,
                    Name = m.Name,
                    Slug = m.Slug,
                    DisplayOrder = m.DisplayOrder,
                    IsActive = m.IsActive,
                    ParentId = m.ParentId,
                    News = m.News
                    .Select(n => new Domain.Entities.News 
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Status = Enum.Parse<NewsStatus>(n.Status),
                        PublishedAt = n.PublishedAt,

                    }).ToList(),
                   
                })
                .AsNoTracking()
                .ToListAsync();

            // EF sử dụng include lấy luôn các dữ liệu không cần thiết
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

        public async Task<Menu> GetByIdAsync(int id)
        {
            var sw = Stopwatch.StartNew();

            var query = await _context.Menus
                .Where(m => m.Id == id && m.IsActive)
                .Select(m => new Domain.Entities.Menu
                {
                    Id = m.Id,
                    Name = m.Name,
                    Slug = m.Slug,
                    IsActive = m.IsActive,

                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
            return query;
        }

        public async Task<Menu> GetByIdWithNewsAsync(int id)
        {
            var sw = Stopwatch.StartNew();

            var query = await _context.Menus
                .Where(m => m.Id == id && m.IsActive)
                .Select(m => new Domain.Entities.Menu
                {
                    Id = m.Id,
                    Name = m.Name,
                    Slug = m.Slug,
                    DisplayOrder = m.DisplayOrder,
                    IsActive = m.IsActive,
                    ParentId = m.ParentId,
                    News = m.News
                    .Select(n => new Domain.Entities.News { Id = n.Id }).ToList(),
                    Children = m.InverseParent.Select(c => new Domain.Entities.Menu
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Slug = c.Slug,
                        IsActive = c.IsActive
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
            return query;
        }

        public async Task<bool> HasActiveNewsAsync(int Id)
        {
            return await _context.News
                .AnyAsync(n => n.MenuId == Id && n.Status.Equals(NewsStatus.Published));
        }

        public async Task<int> UpdateAsync(int id, Menu menu)
        {
            var sw = Stopwatch.StartNew();

            var query = await _context.Menus
                .Where(m => m.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(m => m.Name, menu.Name)
                    .SetProperty(m => m.Slug, menu.Slug)
                    .SetProperty(m => m.DisplayOrder, menu.DisplayOrder)
                    .SetProperty(m => m.IsActive, menu.IsActive)
                    .SetProperty(m => m.ParentId, menu.ParentId)
                );

            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
            return query;
           
        }
    }
}
