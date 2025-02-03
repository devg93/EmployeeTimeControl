
using TimeInTimeOut.Module.Core.Domain.Entity;

namespace TimeInTimeOut.Module.Core.Abstractions
{
    public interface IcomingAndgoingRepositoryCommand
    {
        public Task<bool> Add(ComingAndgoing entity);
        public Task<bool> Update(ComingAndgoing entity);
        public Task<bool> Delete(int id);
    }
}