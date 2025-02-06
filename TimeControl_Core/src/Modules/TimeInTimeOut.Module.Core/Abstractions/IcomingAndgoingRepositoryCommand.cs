
using TimeInTimeOut.Module.Core.Domain.Entity;

namespace TimeInTimeOut.Module.Core.Abstractions
{
    public interface IcomingAndgoingRepositoryCommand
    {
        public Task<bool> CreateTime(ComingAndgoing entity);
        public Task<bool> UpdateTimeAsync(int  UserId,char param);
        public Task<bool> Delete(int id);
    }
}