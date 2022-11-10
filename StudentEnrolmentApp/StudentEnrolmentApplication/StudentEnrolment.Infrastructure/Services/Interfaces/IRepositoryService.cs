using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Infrastructure.Services.Interfaces
{
    public interface IRepositoryService<T> where T : class
    {
        Task<List<T>> GetAsync(string fileName);
        Task AddAsync(string fileName, T listOfEntities);
        Task UpdateAsync(string fileName, List<T> entityList);
    }
}
