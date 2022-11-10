using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Infrastructure.Services.Interfaces
{
    public interface IFileHandlingService
    {
        StreamReader OpenReadStream(string fileName);
        void WriteToFile(string fileName, string json);
    }
}
