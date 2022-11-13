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
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        private readonly IJsonHandlingService<T> _jsonHandlingService;

        public RepositoryService(IJsonHandlingService<T> jsonHandlingService)
        {
            _jsonHandlingService = jsonHandlingService;
        }
        public IEnumerable<T> Get(string fileName)
        {
            IEnumerable<T> fetchedItems = _jsonHandlingService.FetchFromJson(fileName);
            if (fetchedItems != null)
            {
                return fetchedItems;
            }
            return null;
        }

        public void Add(string fileName, T entity)
        {
            List<T> fetchedItems = _jsonHandlingService.FetchFromJson(fileName).ToList();
            fetchedItems.Add(entity);
            _jsonHandlingService.AddToJson(fileName, fetchedItems);
        }

        public void Update(string fileName, List<T> entities)
        {
            _jsonHandlingService.AddToJson(fileName, entities);
        }

        //public async Task UpdateAsync(string fileName, T entity)
        //{
        //    List<T> fetchedItems = await GetAsync(fileName);
        //    var singleStudent = FindItem(studentDetail.StudentId, fetchedItems);

        //    fetchedItems.Add(entity);
        //    using FileStream openStream = _fileHandlingService.openReadStream(fileName);
        //    await _jsonHandlingService.AddToJsonAsync(openStream, fetchedItems);
        //    await openStream.DisposeAsync();
        //}

    }
}
