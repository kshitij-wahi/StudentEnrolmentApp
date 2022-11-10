using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Infrastructure.Services.Interfaces
{
    public interface IJsonHandlingService<T> where T : class
    {
        Task<List<T>> FetchFromJsonAsync(FileStream jsonFile);
        Task AddToJsonAsync(FileStream jsonFile, List<T> listOfEntities);
    }
}
