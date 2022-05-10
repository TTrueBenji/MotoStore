using System.IO;
using Microsoft.Extensions.Hosting;
using MotoStore.Services.Abstractions;

namespace MotoStore.Services
{
    public class DefaultAvatarService : IDefaultAvatarService
    {
        private string _path;
        public DefaultAvatarService(string path)
        {
            _path = path;
        }

        public string GetPathToDefaultAvatar(IHostEnvironment environment)
            => _path;
    }
}