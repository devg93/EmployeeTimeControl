using System.Collections.Generic;
using System.Threading.Tasks;
using Break.Module.Core.Entity;

namespace Break.Module.Core.Repository
{
    public interface IbreakRepository
    {
        public Task<BrakeTime> GetBreakByIdAsinc(int Id);
        public Task<List<BrakeTime>> GetAllBreaksAsync();
        public Task<string> CreateBreakAsync(BrakeTime  brakeTime);
    }
}