using System;
using System.Threading.Tasks;
using LingoBank.Core.CommandHandlers;
using LingoBank.Core.Commands;
using LingoBank.Core.Exceptions;
using LingoBank.Core.Queries;
using LingoBank.Core.QueryHandlers;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace LingoBank.Core
{
    /// <summary>
    /// This class abstracts away the need to inject/execute command and query handlers directly into controllers.
    /// Supports the execution of commands and queries by simply passing them in as a parameter.
    /// </summary>
    public class Runtime : IRuntime
    {
        protected IServiceProvider ServiceProvider;
        private ILogger _logger = Log.ForContext<Runtime>();

        public Runtime(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// Executes a specified query, by fetching its handler dynamically at runtime, and returns the result, provided
        /// the query provided is in a valid state.
        /// A query returns an object or a list of objects from the applications database. Queries are read-only.
        /// </summary>
        /// <param name="runtimeQuery">The query with params that is to be executed.</param>
        /// <typeparam name="TResult">The result of executing the query, produced via the query handler.</typeparam>
        /// <exception cref="Exception">Throws an exception if an error occurs in the query handler - usually db related.</exception>
        public async Task<TResult> ExecuteQueryAsync<TResult>(IRuntimeQuery<TResult> runtimeQuery)
        {
            if (!runtimeQuery.Validate())
            {
                throw new RuntimeException("Invalid query. Check if any parameters are required, or if they're valid.");
            }

            Type handlerType = typeof(IRuntimeQueryHandler<,>)
                .MakeGenericType(runtimeQuery.GetType(), typeof(TResult));

            dynamic queryHandler;
            
            try
            {
                queryHandler = ServiceProvider.GetRequiredService(handlerType);
                _logger.Information($"[Runtime] Handler for query {runtimeQuery.GetType()} is ${queryHandler}");
            }
            catch (InvalidOperationException ex)
            {
                string message = ex.Message;
                _logger.Error("[Runtime] " + message);
                throw new RuntimeException(message, ex);
            }
            
            return await queryHandler.ExecuteAsync((dynamic) runtimeQuery);
        }

        /// <summary>
        /// Executes a specified command, by fetching its handler dynamically at runtime, provided
        /// the query provided is in a valid state.
        /// A command changes the application state, such as creating, updating or removing resources.
        /// </summary>
        /// <param name="command">The command that is to be executed.</param>
        /// <typeparam name="TCommand"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception">Throws an exception if an error occurs in the command handler - usually db related.</exception>
        public async Task ExecuteCommandAsync<TCommand>(TCommand command) where TCommand : class, IRuntimeCommand
        {
            if (!command.Validate())
            {
                throw new Exception("Invalid command. Check if any parameters are required, or if they're valid.");
            }

            if (command.HasExecuted)
            {
                _logger.Warning("[Runtime] The command has already been executed");
                throw new Exception("Command has already been executed");
            }
            
            Type handlerType = typeof(IRuntimeCommandHandler<>)
                .MakeGenericType(typeof(TCommand));
            
            try
            {
                IRuntimeCommandHandler<TCommand> handler = ServiceProvider.GetRequiredService(handlerType) as IRuntimeCommandHandler<TCommand>;
                _logger.Information("[Runtime] Handler is {handler}", handler);
                await handler?.ExecuteAsync((dynamic) command)!;
                command.HasExecuted = true;
            }
            catch (Exception exception)
            {
                string message = exception.Message;
                if (exception.InnerException != null) 
                {
                    message += exception.InnerException.Message;
                }
                _logger.Error($"[Runtime] {message}");
                throw new RuntimeException(message, exception);
            }
        }
    }
}