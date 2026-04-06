using DemoCleanArchitecture.Domain.Entities;

namespace DemoCleanArchitecture.Domain.Interfaces
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllAsync();
        Task<IEnumerable<Menu>> GetAllWithNewsAsync();
        Task<Menu> GetByIdAsync(int id);
        Task<Menu> GetByIdWithNewsAsync(int id);
        Task<Menu> CreateAsync(Menu menu);
        Task<int> DeleteAsync(int id);
        Task<int> UpdateAsync(int id, Menu menu);
        Task<bool> HasActiveNewsAsync(int Id);
    }
}
