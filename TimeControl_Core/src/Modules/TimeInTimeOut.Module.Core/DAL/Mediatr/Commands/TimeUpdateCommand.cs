
using MediatR;

namespace TimeInTimeOut.Module.Core.DAL.Mediatr.Commands
{
    public class TimeUpdateCommand:IRequest<bool>
    {
         public int UserId { get; set; }
         public char Param{ get;set;}
    }
}