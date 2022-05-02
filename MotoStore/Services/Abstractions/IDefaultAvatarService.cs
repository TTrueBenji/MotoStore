using Microsoft.Extensions.Hosting;

namespace MotoStore.Services.Abstractions
{
    public interface IDefaultAvatarService
    {
        string GetPathToDefaultAvatar(IHostEnvironment environment);
    }
}