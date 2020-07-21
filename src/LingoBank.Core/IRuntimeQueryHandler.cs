using System.Threading.Tasks;

namespace LingoBank.Core
{
    public interface IRuntimeQueryHandler<TQuery, TResult> where TQuery : class, IRuntimeQuery<TResult>
    {
        Task<TResult> ExecuteAsync(TQuery query);
    }
}