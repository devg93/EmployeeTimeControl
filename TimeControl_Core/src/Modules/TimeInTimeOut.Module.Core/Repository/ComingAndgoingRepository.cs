using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore;
using TimeInTimeOut.Module.Core.Abstractions;
using TimeInTimeOut.Module.Core.DLA;
using TimeInTimeOut.Module.Core.Domain.Entity;

namespace TimeInTimeOut.Module.Core.Repository
{
    public class ComingAndgoingRepository : IcomingAndgoingRepository
    {
        private readonly DbInstaceTimeInOut dbInstaceTimeInOut;
        public ComingAndgoingRepository(DbInstaceTimeInOut dbInstaceTimeInOut)
        => this.dbInstaceTimeInOut = dbInstaceTimeInOut;

        public async Task<bool> Add(ComingAndgoing entity)
        {

            if (dbInstaceTimeInOut is null || dbInstaceTimeInOut.comingAndgoings is null)
            {
                throw new InvalidOperationException("Database instance or comingAndgoings collection is null.");
            }

            await dbInstaceTimeInOut.comingAndgoings.AddAsync(entity);
            await dbInstaceTimeInOut.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ComingAndgoing>> GetAll()
        => dbInstaceTimeInOut.comingAndgoings != null ?
        await dbInstaceTimeInOut.comingAndgoings.Include(ws => ws.OnlineTime).
        Include(ws => ws.OflineTime).ToListAsync() : new List<ComingAndgoing>();

        public async Task<ComingAndgoing> GetById(int id)
    {
        if (dbInstaceTimeInOut.comingAndgoings is null)
        {
            throw new InvalidOperationException("Database instance or comingAndgoings collection is null.");
        }

        var result = await dbInstaceTimeInOut.comingAndgoings.Include(ws => ws.OnlineTime)
            .Include(ws => ws.OflineTime).FirstOrDefaultAsync(ws => ws.Id == id);

        if (result is null)
        {
            throw new KeyNotFoundException($"Entity with id {id} not found.");
        }

        return result;
    }

        public Task<ComingAndgoing> Update(ComingAndgoing entity)
        {
            throw new NotImplementedException();
        }
    }
}