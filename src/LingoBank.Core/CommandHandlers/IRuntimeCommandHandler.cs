using System.Threading.Tasks;
using LingoBank.Core.Commands;

namespace LingoBank.Core.CommandHandlers
{
    public interface IRuntimeCommandHandler<TCommand> where TCommand : class, IRuntimeCommand
    {
        Task ExecuteAsync(TCommand command);
    }
}