using Newtonsoft.Json;
using StudentEnrolment.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentEnrolment.Infrastructure.Services
{
    public class JsonHandlingService<T> : IJsonHandlingService<T> where T : class
    {
        private readonly IFileHandlingService _fileHandlingService;

        public JsonHandlingService(IFileHandlingService fileHandlingService)
        {
            _fileHandlingService = fileHandlingService;
        }
        public List<T> FetchFromJson(string fileName)
        {
            List<T>? fetchedItems = new List<T>();
            using (StreamReader openStream = _fileHandlingService.OpenReadStream(fileName))
            {
                string json = openStream.ReadToEnd();
                fetchedItems = JsonConvert.DeserializeObject<List<T>>(json);
            }
            return fetchedItems;
        }

        
        public void AddToJson(string fileName, List<T> list)
        {
            string studentDetailsJson = JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented);
            _fileHandlingService.WriteToFile(fileName, studentDetailsJson);
        }
    }
}
