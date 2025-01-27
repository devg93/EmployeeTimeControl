using Shared.Dto;
using TimeInTimeOut.Module.Core.Domain.Entity;
using TimeInTimeOut.Module.Core.Dto;

namespace TimeInTimeOut.Module.Core.Abstractions
{
    public interface IcomingAndgoingRepository
    {
        public Task<IEnumerable<ComingAndgoing>> GetAll();
        public Task<ResponseChecker<ComingAndgoing>> GetById(int id);

        public Task<bool> Add(ComingAndgoing entity);
        public Task<ComingAndgoing> Update(ComingAndgoing entity);
        public Task<bool> Delete(int id);

    }
}