using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modules.Break.Module.Core.Astractions.Irepository.Ibusy;
using Modules.Break.Module.Core.Entity;


namespace Modules. Break.Module.Core.Repository.Busy
{
    public class busyRepositoryQeury : IbusyRepositoryQeury
    {
        public Task<busyChecker> GetBusyByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}