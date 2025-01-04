namespace zShared.Services
{
    public interface IPingIpChecker
    {
        Task<bool> PingIp(string ipAddress);
    }
}