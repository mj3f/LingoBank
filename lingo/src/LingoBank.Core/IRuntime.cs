using System;
using System.Threading.Tasks;
using LingoBank.Core.Commands;
using LingoBank.Core.Queries;

namespace LingoBank.Core
{
    public interface IRuntime
    {
        Task<TResult> ExecuteQueryAsync<TResult>(IRuntimeQuery<TResult> runtimeQuery);
        
        Task ExecuteCommandAsync<TCommand>(TCommand command) where TCommand : class, IRuntimeCommand;
    }
}