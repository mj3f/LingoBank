using System.Threading.Tasks;

namespace LingoBank.Core
{
    public interface IRuntimeCommandHandler<TCommand> where TCommand : class, IRuntimeCommand
    {
        Task<RuntimeCommandResult> ExecuteAsync(TCommand command);
    }
}