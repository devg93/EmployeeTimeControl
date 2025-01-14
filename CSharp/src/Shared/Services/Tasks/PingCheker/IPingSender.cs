namespace Shared.Services.Tasks.PingCheker;

    public interface IPingSender
    {
        Task<bool> PingIp(string ipAddress);
    }
