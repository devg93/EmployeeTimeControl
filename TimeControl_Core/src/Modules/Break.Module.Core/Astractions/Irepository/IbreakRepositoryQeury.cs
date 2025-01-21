
using System.Collections.Generic;
using System.Threading.Tasks;
using Modules.Break.Module.Core.Entity;


namespace Modules.Break.Module.Core.Astractions.Irepository;

    public interface IbreakRepositoryQeury
    {
        public Task<BrakeTime> GetBreakByIdAsinc(int Id);
        public Task<List<BrakeTime>> GetAllBreaksAsync();

    }
