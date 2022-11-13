using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Infrastructure.Services.Interfaces
{
    public interface IRepositoryService<T> where T : class
    {
        IEnumerable<T> Get(string fileName);
        void Add(string fileName, T entity);
        void Update(string fileName, List<T> entities);
    }
}
