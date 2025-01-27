using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using TimeInTimeOut.Module.Core.Abstractions;
using TimeInTimeOut.Module.Core.DAL;
using TimeInTimeOut.Module.Core.Domain.Entity;
using TimeInTimeOut.Module.Core.Dto;

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
        public async Task<ResponseChecker<ComingAndgoing>> GetById(int id)
        {
            if (dbInstaceTimeInOut.comingAndgoings is null)
            {
                return new ResponseChecker<ComingAndgoing>
                {
                    IsSuccess = false,
                    Message = "Database instance or comingAndgoings collection is null."
                };
            }

            var result = await dbInstaceTimeInOut.comingAndgoings
                .Include(ws => ws.OnlineTime)
                .Include(ws => ws.OflineTime)
                .FirstOrDefaultAsync(ws => ws.Id == id);

            if (result is null)
            {
                return new ResponseChecker<ComingAndgoing>
                {
                    IsSuccess = false,
                    Message = "Data not found."
                };
            }

            return new ResponseChecker<ComingAndgoing>
            {
                IsSuccess = true,
                Data = result
            };
        }


        public Task<ComingAndgoing> Update(ComingAndgoing entity)
        {
            throw new NotImplementedException();
        }
    }
}