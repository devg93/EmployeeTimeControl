using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using TimeInTimeOut.Module.Core.Abstractions;
using TimeInTimeOut.Module.Core.DAL;
using TimeInTimeOut.Module.Core.Domain.Entity;
using TimeInTimeOut.Module.Core.Dto;

namespace TimeInTimeOut.Module.Core.Repository
{
    public class ComingAndgoingRepository : IcomingAndgoingRepositoryCommand, IcomingAndgoingRepositoryQeury
    {
        private readonly DbInstaceTimeInOut dbInstaceTimeInOut;
        public ComingAndgoingRepository(DbInstaceTimeInOut dbInstaceTimeInOut)
        => this.dbInstaceTimeInOut = dbInstaceTimeInOut;

        public async Task<bool> CreateTime(ComingAndgoing entity)
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
        //***********************************************************************
        public async Task<IEnumerable<ComingAndgoing>> GetAll()
        => dbInstaceTimeInOut.comingAndgoings != null ?
        await dbInstaceTimeInOut.comingAndgoings.
        Include(ws => ws.OnlineTime).
        Include(ws => ws.OfflineTime).ToListAsync() : new List<ComingAndgoing>();



        //***********************************************************************


        public async Task<ResponseChecker<ComingAndgoing>> GetById(int id, string Param)
        {
            if (dbInstaceTimeInOut.comingAndgoings is null)
            {
                return new ResponseChecker<ComingAndgoing>
                {
                    IsSuccess = false,
                    Message = "Database instance or comingAndgoings collection is null."
                };
            }

            try
            {
                if (dbInstaceTimeInOut.comingAndgoings == null)
                {
                    return new ResponseChecker<ComingAndgoing>
                    {
                        IsSuccess = false,
                        Message = "DateTimeTimeInTimeOuts collection is null."
                    };
                }


                switch (Param)
                {
                    case "OnlineTime":
                        var ResOnlineTime = await dbInstaceTimeInOut.comingAndgoings
                             .Where(o => o.Id == id)
                             .Select(o => o.OnlineTime)
                             .FirstOrDefaultAsync();
                        return new ResponseChecker<ComingAndgoing>
                        {
                            IsSuccess = true,
                            Message = "Data retrieved successfully.",
                            Data = new ComingAndgoing { OnlineTime = ResOnlineTime }
                        };
                    case "OfflineTime":
                        var ResOfflineTime = await dbInstaceTimeInOut.comingAndgoings
                             .Where(o => o.Id == id)
                             .Select(o => o.OnlineTime)
                             .FirstOrDefaultAsync();
                        return new ResponseChecker<ComingAndgoing>
                        {
                            IsSuccess = true,
                            Message = "Data retrieved successfully.",
                            Data = new ComingAndgoing { OnlineTime = ResOfflineTime }
                        };

                    case "ResBreake":
                        var ResBreake = await dbInstaceTimeInOut.comingAndgoings

                             .FirstOrDefaultAsync();
                        return new ResponseChecker<ComingAndgoing>
                        {
                            IsSuccess = true,
                            Message = "Data retrieved successfully.",
                            Data = ResBreake
                        };
                    default:
                        return new ResponseChecker<ComingAndgoing>
                        {
                            IsSuccess = false,
                            Message = "Invalid parameter."
                        };
                }

            }
            catch (Exception ex)
            {
                return new ResponseChecker<ComingAndgoing>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<bool> UpdateTimeAsync(int userId, char param)
        {
           
            if (dbInstaceTimeInOut.comingAndgoings == null)
            {
                return false;
            }
            
            var entity = await dbInstaceTimeInOut.comingAndgoings
                .FirstOrDefaultAsync(us => us.Id == userId);

      
            if (entity == null)
            {
                return false;
            }

          
            switch (param)
            {
                case 'O':
                    entity.OnlineTime ??= new List<DateTime>(); 
                    entity.OnlineTime.Add(DateTime.Now);
                    break;

                case 'F':
                    entity.OfflineTime ??= new List<DateTime>(); 
                    entity.OfflineTime.Add(DateTime.Now);
                    break;

                default:
                    return false; 
            }

            dbInstaceTimeInOut.comingAndgoings.Update(entity);
            await dbInstaceTimeInOut.SaveChangesAsync();
            return true;
        }

        //***********************************************************************

    }
}