using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Core.Events;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
