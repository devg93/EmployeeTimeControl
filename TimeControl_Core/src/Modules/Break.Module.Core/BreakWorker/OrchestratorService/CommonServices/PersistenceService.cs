

namespace Break.Module.Core.BreakWorker.CommonServices.OrchestratorService
{
    public class PersistenceService : IPersistenceService
    {
        private readonly IbreakRepositoryQeury _breakRepositoryQeury;
        private readonly ISendServiceToBreakModule _sendServiceToBreakModule;
        private readonly IbusyRepositoryQeury _busyRepositoryQeury;
        private readonly IServicesFactory _servicesFactory;

        public PersistenceService(
            IbreakRepositoryQeury breakRepositoryQeury,
            ISendServiceToBreakModule sendServiceToBreakModule,
            IbusyRepositoryQeury busyRepositoryQeury, IServicesFactory servicesFactory)
        {
            _breakRepositoryQeury = breakRepositoryQeury;
            _sendServiceToBreakModule = sendServiceToBreakModule;
            _busyRepositoryQeury = busyRepositoryQeury;
            _servicesFactory = servicesFactory;

        }

        public async Task<ResponseChecker<BrakeTime>> FetchExistingBrakeTime(int id)
        => await _breakRepositoryQeury.GetBreakByIdAsinc(id);

        public async Task<ResponseChecker<ComingAndGoingDto>> FetchServiceTimeInTimeOut(int id)
        => await _sendServiceToBreakModule.GetByIdAsync(id);

        public async Task<bool> GetBusyStatus(int userId)
        {
            var busyChecker = await _busyRepositoryQeury.GetBusyByIdAsync(userId);
            return busyChecker ? true : false;
        }

        public async Task<bool> CreateBusyStatus(int Userid, bool status)
        {
            var busyChecker = await _servicesFactory.GetBusyRepositoryCommand().CreateBusy(Userid, status);
            return busyChecker;
        }

        public async Task<bool> UpdateBusyStatus(int id, bool status)
        {

            await _servicesFactory.GetBusyRepositoryCommand().UpdateBusy(id, status);
            return await _servicesFactory.GetBusyRepositoryCommand().Save();
        }



        public async Task<string> CreateBreakAsync(BrakeTime brakeTime)
        {
            await _servicesFactory.GetBreakRepositoryCommand().CreateBreakAsync(brakeTime);
            return await _servicesFactory.GetBreakRepositoryCommand().Save();
        }

        public async Task<string> UbdateBreakAsync(int Id, byte param)
        => await _servicesFactory.GetBreakRepositoryCommand().UbdateBreakAsync(Id, 1);


        public async Task<ResponseChecker<BrakeTime>> GetBreakByIdAsinc(int Id)
        => await _servicesFactory.GetibreakRepositoryQeury().GetBreakByIdAsinc(Id);




        public async Task<List<BrakeTime>> GetAllBreaksAsync()
        => await _servicesFactory.GetibreakRepositoryQeury().GetAllBreaksAsync();


    }

    public interface IPersistenceService
    {
        Task<ResponseChecker<BrakeTime>> FetchExistingBrakeTime(int id);
        Task<ResponseChecker<ComingAndGoingDto>> FetchServiceTimeInTimeOut(int id);
        Task<bool> GetBusyStatus(int userId);
        Task<bool> CreateBusyStatus(int Userid, bool status);
        Task<bool> UpdateBusyStatus(int id, bool status);
        Task<string> CreateBreakAsync(BrakeTime brakeTime);
        Task<string> UbdateBreakAsync(int Id, byte param);
        Task<ResponseChecker<BrakeTime>> GetBreakByIdAsinc(int Id);
        Task<List<BrakeTime>> GetAllBreaksAsync();
    }
}