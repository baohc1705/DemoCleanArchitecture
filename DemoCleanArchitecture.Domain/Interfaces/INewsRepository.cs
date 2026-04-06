using DemoCleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Domain.Interfaces
{
    public interface INewsRepository
    {
        Task<News> GetByIdAsync(int id);
        Task<IEnumerable<News>> GetAllAsync();
        Task<News> CreateAsync(News news);
        Task<int> DeleteAsync(int id);
        Task<int> UpdateAsync(News news);
        Task IncrementViewCountAsync(int id);
    }
}
