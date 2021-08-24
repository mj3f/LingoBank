using System;
using System.Threading.Tasks;
using LingoBank.Core.CommandHandlers;
using LingoBank.Core.Commands;
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
        /// </summary>
        /// <param name="runtimeQuery"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <exception cref="Exception"></exception>
        public async Task<TResult> ExecuteQueryAsync<TResult>(IRuntimeQuery<TResult> runtimeQuery)
        {
            // Check if query has been provided.
            _ = runtimeQuery ?? throw new ArgumentNullException(nameof(runtimeQuery));

            if (!runtimeQuery.Validate())
            {
                throw new Exception("Invalid query. Check if any parameters are required, or if they're valid.");
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
                // Could not construct QueryHandler.
                // string message = $"Unable to instantiate handler for query type: {runtimeQuery.GetType().FullName}.\r\nCheck that the query’s handler and associated dependencies have been registered in the RuntimeContainer.";
                _logger.Error("[Runtime] " + message);
                throw new Exception(message, ex);
            }
            
            return await queryHandler.ExecuteAsync((dynamic) runtimeQuery);
        }

        /// <summary>
        /// Executes a specified command, by fetching its handler dynamically at runtime, provided
        /// the query provided is in a valid state.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="withMetadataCallback"></param>
        /// <typeparam name="TCommand"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task ExecuteCommandAsync<TCommand>(TCommand command) where TCommand : class, IRuntimeCommand
        {
            // Check if query has been provided.
            _ = command ?? throw new ArgumentNullException(nameof(command));

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

                command.HasExecuted = true;
            }
            catch (Exception exception)
            {
                string message = exception.Message;
                if (exception.InnerException != null) 
                {
                    message += exception.InnerException.Message;
                }
                // string message = $"Unable to instantiate handler for command type: {typeof(TCommand).FullName}.\r\nCheck that the command’s handler and associated dependencies have been registered in the RuntimeContainer.";
                _logger.Error($"[Runtime] {message}");
                throw new Exception(message, exception);
            }
        }
    }
}