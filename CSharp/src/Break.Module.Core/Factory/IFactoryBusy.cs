
namespace Break.Module.Core.Factory
{
    public abstract class IFactoryBusy
    {
      public abstract object GetRepository(string repositoryType);
    }
}