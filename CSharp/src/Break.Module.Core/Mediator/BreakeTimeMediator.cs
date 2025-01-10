using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Break.Module.Core.Astractions.Iservices;
using Break.Module.Core.Dto;



namespace Break.Module.Core.Mediator;

    public class BreakeTimeMediator:IBreakeTimeMediator
    {
        private readonly IBrakeTimeService brakeTimeService;
        public BreakeTimeMediator(IBrakeTimeService brakeTimeService)
        => this.brakeTimeService = brakeTimeService;

        public async Task<bool> UpdateAsync(int userId, bool pingResponseStatus)
        {
             var workSchedule = new BrakeTimeDtoReqvest
            {
                UserId = userId,
                StartTime = pingResponseStatus ? new List<DateTime>() : new List<DateTime> { DateTime.Now },
                EndTime = pingResponseStatus ? new List<DateTime> { DateTime.Now } : new List<DateTime>()
            };

            await brakeTimeService.addService(workSchedule, pingResponseStatus);
            return true;
        }
    }
