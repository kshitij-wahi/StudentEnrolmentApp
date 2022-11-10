using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Infrastructure.Services.Interfaces
{
    public interface IFileHandlingService
    {
        FileStream openReadStream(string fileName);
        FileStream openWriteStream(string fileName);
    }
}
