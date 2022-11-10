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

        public StreamReader OpenReadStream(string fileName)
        {
            StreamReader rStream = new StreamReader(dbFolder + "/" + fileName + ".json");
            return rStream;
        }

        // stream writing was giving wrong results
        // changing to writealltext that removes the contents 
        // of the file completely and writes new contents.
        // TODO: because we have used stream reading for reading json
        // for consistency try to use stream writing.
        public void WriteToFile(string fileName, string json)
        {
            System.IO.File.WriteAllText(dbFolder + "/" + fileName + ".json", json);
        }
    }
}
