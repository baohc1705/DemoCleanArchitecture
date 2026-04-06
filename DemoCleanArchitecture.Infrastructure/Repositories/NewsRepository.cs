using AutoMapper;
using DemoCleanArchitecture.Domain.Entities;
using DemoCleanArchitecture.Domain.Enums;
using DemoCleanArchitecture.Domain.Interfaces;
using DemoCleanArchitecture.Infrastructure.Data;
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

    public class NewsRepository : INewsRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public NewsRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            var sw = Stopwatch.StartNew();
            var query = await _context.News
                .Where(n => !n.DeletedAt.HasValue)
                .Select(n => new Domain.Entities.News
                {
                    Id = n.Id,
                    Title = n.Title,
                    Status = Enum.Parse<NewsStatus>(n.Status),
                    PublishedAt = n.PublishedAt,
                })
                .ToListAsync();
            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
            return query;
        }

        public async Task<News> GetByIdAsync(int id)
        {
            var sw = Stopwatch.StartNew(); 
            var query =  await _context.News
                .Where(n => n.Id == id)
                .OrderBy(n => n.PublishedAt)
                .Select(n => new Domain.Entities.News
                {
                    Id = n.Id,
                    MenuId = n.MenuId,
                    Title = n.Title,
                    Slug = n.Slug,
                    Summary = n.Summary,
                    Content = n.Content,
                    ThumbnailUrl = n.ThumbnailUrl,
                    Status = Enum.Parse<NewsStatus>(n.Status),
                    PublishedAt = n.PublishedAt,
                    ViewCount = n.ViewCount,
                    Menu = new Domain.Entities.Menu
                    {
                        Id = n.Menu.Id,
                        Name = n.Menu.Name,
                        Slug = n.Menu.Slug,
                        IsActive = n.Menu.IsActive
                    }
                })
                .FirstOrDefaultAsync();
            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
            return query;
        }
    }
}
