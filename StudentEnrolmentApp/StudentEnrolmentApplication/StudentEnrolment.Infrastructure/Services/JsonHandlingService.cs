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
        public async Task<List<T>> FetchFromJsonAsync(FileStream jsonFile)
        {
            List<T>? fetchedElements = await JsonSerializer.DeserializeAsync<List<T>>(jsonFile);
            return fetchedElements;
        }

        public async Task AddToJsonAsync(FileStream jsonFile, List<T> listOfEntities)
        {
            await JsonSerializer.SerializeAsync(jsonFile, listOfEntities);
        }
    }
}
