using Shared.Dto;
using TimeInTimeOut.Module.Core.Domain.Entity;

namespace TimeInTimeOut.Module.Core.Abstractions
{
    public interface IcomingAndgoingRepositoryQeury
    {
        public Task<IEnumerable<ComingAndgoing>> GetAll();
        public Task<ResponseChecker<ComingAndgoing>> GetById(int id,string Param);
     

    }
}