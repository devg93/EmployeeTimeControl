
using System.Collections.Generic;
using System.Threading.Tasks;
using Break.Module.Core.Entity;

namespace Break.Module.Core.Astractions.Irepository
{
    public interface IbreakRepositoryQeury
    {
        public Task<BrakeTime> GetBreakByIdAsinc(int Id);
        public Task<List<BrakeTime>> GetAllBreaksAsync();

    }
}