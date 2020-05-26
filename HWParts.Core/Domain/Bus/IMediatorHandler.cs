using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Events;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
