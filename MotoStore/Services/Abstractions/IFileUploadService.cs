using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MotoStore.Services.Abstractions
{
    public interface IFileUploadService
    {
        Task Upload(string path, string fileName, IFormFile file);
    }
}