using TimeInTimeOut.Module.Core.Domain.Entity;

namespace TimeInTimeOut.Module.Core.Repository
{
    public interface IcomingAndgoingRepository
    {
        public Task<IEnumerable<ComingAndgoing>> GetAll();
        public Task<ComingAndgoing> GetById(int id);

        public Task<bool> Add(ComingAndgoing entity);
        public Task<ComingAndgoing> Update(ComingAndgoing entity);
        public Task<bool> Delete(int id);

    }
}