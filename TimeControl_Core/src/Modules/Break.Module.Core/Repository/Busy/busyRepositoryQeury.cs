using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modules.Break.Module.Core.Astractions.Irepository.Ibusy;
using Modules.Break.Module.Core.DLA;
using Modules.Break.Module.Core.Entity;


namespace Modules.Break.Module.Core.Repository.Busy
{
    public class busyRepositoryQeury : IbusyRepositoryQeury
    {
        private readonly DbInstace _db;
        busyRepositoryQeury(DbInstace db)
        => _db = db;

        public async Task<bool> GetBusyByIdAsync(int id)
        {
            var res = _db.BusyCheckers != null ?
            await _db.BusyCheckers.FirstOrDefaultAsync(bs => bs.Id == id) : null;
            return res?.busy ?? false;
        }
    }
}