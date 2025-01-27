
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Modules.Break.Module.Core.Astractions.Irepository;
using Shared.Dto;
using Shared.Services.ModuleCommunication.Contracts;

namespace Break.Module.Core.ServicesCommunication
{
    public class SendDataToTimeInTimeOutModule:ISendServiceToTimeInTimeOutModule
    {
        private readonly IbreakRepositoryQeury getservice;
        public SendDataToTimeInTimeOutModule(IbreakRepositoryQeury getservice)
        =>this.getservice=getservice;

        public async Task<ResponseChecker<BrakeTimeDto>> GetByIdAsync(int id)
        {
            var brakeTime = await getservice.GetBreakByIdAsinc(id);
            if (brakeTime == null || brakeTime.Data == null)
            {
                return new ResponseChecker<BrakeTimeDto>
                {
                    IsSuccess = false,
                    Message = "Brake time data not found"
                };
            }

            return new ResponseChecker<BrakeTimeDto>
            {
                IsSuccess = true,
                Data = new BrakeTimeDto
                {
                    Id = brakeTime.Data.Id,
                    StartTime = brakeTime.Data.StartTime?.Select(tm => new DateTimeWorkScheduleDto
                    {
                        StartTime = tm.StartTime
                    }).ToList(),
                    EndTime = brakeTime.Data.EndTime?.Select(tm => new DateTimeWorkScheduleDto
                    {
                        EndTime = tm.EndTime
                    }).ToList()
                }
            };
        }
    }
}