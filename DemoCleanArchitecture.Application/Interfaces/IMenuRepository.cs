using DemoCleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Interfaces
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllAsync();

    }
}
