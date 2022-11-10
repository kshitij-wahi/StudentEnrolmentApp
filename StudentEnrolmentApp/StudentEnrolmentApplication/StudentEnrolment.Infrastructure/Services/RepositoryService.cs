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
        private readonly IFileHandlingService _fileHandlingService;
        private readonly IJsonHandlingService<T> _jsonHandlingService;

        public RepositoryService(IFileHandlingService fileHandlingService, IJsonHandlingService<T> jsonHandlingService)
        {
            _fileHandlingService = fileHandlingService;
            _jsonHandlingService = jsonHandlingService;
        }
        public async Task<List<T>> GetAsync(string fileName)
        {
            using FileStream openStream = _fileHandlingService.openReadStream(fileName);
            List<T> fetchedItems = await _jsonHandlingService.FetchFromJsonAsync(openStream);
            await openStream.DisposeAsync();
            return fetchedItems;
        }
            
        public async Task AddAsync(string fileName, T entity)
        {
            List<T> fetchedItems = await GetAsync(fileName);
            fetchedItems.Add(entity);
            using FileStream openStream = _fileHandlingService.openWriteStream(fileName);
            await _jsonHandlingService.AddToJsonAsync(openStream, fetchedItems);
            await openStream.DisposeAsync();
        }

        public async Task UpdateAsync(string fileName, List<T> entityList)
        {
            using FileStream openStream = _fileHandlingService.openWriteStream(fileName);
            await _jsonHandlingService.AddToJsonAsync(openStream, entityList);
            await openStream.DisposeAsync();
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
