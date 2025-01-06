using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Break.Module.Core.Astractions.Irepository.Ibusy;
using Break.Module.Core.Entity;

namespace Break.Module.Core.Repository.Busy
{
    public class busyRepositoryQeury : IbusyRepositoryQeury
    {
        public Task<busyChecker> GetBusyByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}