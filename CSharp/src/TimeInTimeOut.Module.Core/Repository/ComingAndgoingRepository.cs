using TimeInTimeOut.Module.Core.Domain.Entity;

namespace TimeInTimeOut.Module.Core.Repository
{
    public class ComingAndgoingRepository : IcomingAndgoingRepository
    {
        public Task<bool> Add(ComingAndgoing entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComingAndgoing>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ComingAndgoing> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ComingAndgoing> Update(ComingAndgoing entity)
        {
            throw new NotImplementedException();
        }
    }
}