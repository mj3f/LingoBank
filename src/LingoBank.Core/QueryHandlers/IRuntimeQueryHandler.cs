using System.Threading.Tasks;
using LingoBank.Core.Queries;

namespace LingoBank.Core.QueryHandlers
{
    public interface IRuntimeQueryHandler<TQuery, TResult> where TQuery : class, IRuntimeQuery<TResult>
    {
        Task<TResult> ExecuteAsync(TQuery query);
    }
}