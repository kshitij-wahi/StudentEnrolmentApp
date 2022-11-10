using StudentEnrolment.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrolment.Infrastructure.Services
{
    public class FileHandlingService : IFileHandlingService
    {
        private string dbFolder = "JsonDb";

        public FileStream openReadStream(string fileName)
        {
            FileStream openRStream = System.IO.File.OpenRead(dbFolder + "/" + fileName + ".json");
            return openRStream;
        }
        public FileStream openWriteStream(string fileName)
        {
            FileStream openWStream = System.IO.File.OpenWrite(dbFolder + "/" + fileName + ".json");
            return openWStream;
        }
    }
}
