using System;
using Break.Module.Core.Repository.Busy;
namespace Break.Module.Core.Factory
{
    public class FactoryBusy
    {
        public object GetRepository(Dbcommand repositoryType)
        {
            return repositoryType switch
            {
                Dbcommand.Query => new busyRepositoryQeury(),
                Dbcommand.Command => new busyRepositoryCommand(),
                _ => throw new ArgumentException("Invalid repository type", nameof(repositoryType))
            };
        
        }
    }
}
