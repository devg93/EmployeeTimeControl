using System.Net.NetworkInformation;
using Microsoft.Extensions.Logging;

namespace zShared.Services
{
    public sealed class PingIpChecker:IPingIpChecker
    {
        private readonly ILogger<PingIpChecker> logger;
        public PingIpChecker(ILogger<PingIpChecker> logger)
         => this.logger = logger;

        public async Task<bool> PingIp(string ipAddress)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(ipAddress);
                    logger.LogInformation($"Pinged {ipAddress}, Status: {reply.Status}, Roundtrip Time: {reply.RoundtripTime} ms");

                    return reply.Status == IPStatus.Success;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error pinging {ipAddress}: {ex.Message}");
                return false;
            }
        }


    }
}