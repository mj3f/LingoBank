using System;
using System.Threading.Tasks;

namespace LingoBank.Core
{
    public interface IRuntime
    {
        Task<TResult> ExecuteQueryAsync<TResult>(IRuntimeQuery<TResult> runtimeQuery);
        
        Task ExecuteCommandAsync<TCommand>(TCommand command, Action<RuntimeCommandResult> withMetadataCallback = null) where TCommand : class, IRuntimeCommand;
    }
}