using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Infrastructure.Services.Interfaces
{
    public interface IJsonHandlingService<T> where T : class
    {
        IEnumerable<T> FetchFromJson(string fileName);
        void AddToJson(string fileName, List<T> list);
    }
}
