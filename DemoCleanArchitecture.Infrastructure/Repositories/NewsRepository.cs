using AutoMapper;
using Azure.Core;
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

        public async Task<News> CreateAsync(News news)
        {
            Console.WriteLine(news);
            var sw = Stopwatch.StartNew();

            //var req = new Data.PersistenceModels.News
            //{
            //    MenuId = news.MenuId,
            //    Title = news.Title,
            //    Slug = news.Slug,
            //    Summary = news.Summary,
            //    Content = news.Content,
            //    ThumbnailUrl = news.ThumbnailUrl,
            //    Status = news.Status.ToString(),
            //    PublishedAt = news.PublishedAt,
            //};

            var req = _mapper.Map<Data.PersistenceModels.News>(news);

            var created = await _context.AddAsync(req);

            await _context.SaveChangesAsync();

            sw.Stop();

            Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");

            return await GetByIdAsync(created.Entity.Id);
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
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
            var query = await _context.News
                .Where(n => n.Id == id && !n.DeletedAt.HasValue)
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

        public async Task IncrementViewCountAsync(int id)
        {
            await _context.News
                .Where(n => n.Id == id)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(s => s.ViewCount, n => n.ViewCount + 1)
                );
        }

        public async Task<int> UpdateAsync(News news)
        {
            try
            {
                var sw = Stopwatch.StartNew();
                var res = await _context.News
                .Where(n => n.Id == news.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(n => n.MenuId, news.MenuId)
                    .SetProperty(n => n.Title, news.Title)
                    .SetProperty(n => n.Slug, news.Slug)
                    .SetProperty(n => n.Summary, news.Summary)
                    .SetProperty(n => n.Content, news.Content)
                    .SetProperty(n => n.ThumbnailUrl, news.ThumbnailUrl)
                    .SetProperty(n => n.Status, news.Status.ToString())
                    .SetProperty(n => n.PublishedAt, news.PublishedAt)
                    .SetProperty(n => n.ViewCount, news.ViewCount)
                    .SetProperty(n => n.UpdatedAt, news.UpdateAt)
                    .SetProperty(n => n.DeletedAt, news.DeleteAt)
                );
                sw.Stop();
                Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
                return res;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}
