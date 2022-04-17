using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MotoStore.Services.Abstractions;

namespace MotoStore.Services
{
    public class FileUploadService : IFileUploadService
    {
        public async Task Upload(string path, string fileName, IFormFile file)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                
                await using var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
                await file.CopyToAsync(stream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    }
}