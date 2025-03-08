

namespace TimeInTimeOut.Module.Core.Dto
{
    public class ComingAndgoingDtoRequest: IRequest<bool>
    {
        public int Id { get; set; }
        public DateTime OnlineTime { get; set; }
        public DateTime OflineTime { get; set; }

    }
 
}