using System;
using System.Data.Common;
using Dapper.Logging.Configuration;
using Dapper.Logging.Hooks;
using Microsoft.Extensions.Logging;

namespace Dapper.Logging
{
    public class ContextlessLoggingFactory : IDbConnectionFactory
    {
        private readonly LoggingHook<Empty> _hooks;
        private readonly WrappedConnectionFactory<Empty> _factory;
        private readonly WrappedConnectionFactory<Empty> _connStrFactory;

        public ContextlessLoggingFactory(
            ILogger<IDbConnectionFactory> logger, 
            DbLoggingConfiguration config, 
            Func<DbConnection> factory)
        {
            _hooks = new LoggingHook<Empty>(logger, config);
            _factory = new WrappedConnectionFactory<Empty>(factory);
        }
        
        public ContextlessLoggingFactory(
            ILogger<IDbConnectionFactory> logger, 
            DbLoggingConfiguration config, 
            Func<string,DbConnection> factory)
        {
            _hooks = new LoggingHook<Empty>(logger, config);
            _connStrFactory = new WrappedConnectionFactory<Empty>(factory);
        }
        
        [Obsolete]
        public ContextlessLoggingFactory(
            ILogger<DbConnection> logger, 
            DbLoggingConfiguration config, 
            Func<DbConnection> factory)
        {
            _hooks = new LoggingHook<Empty>(logger, config);
            _factory = new WrappedConnectionFactory<Empty>(factory);
        }
        
        public DbConnection CreateConnection() => 
            _factory.CreateConnection(_hooks, Empty.Object);

        public DbConnection CreateConnection(string connString) => _connStrFactory.CreateConnection(_hooks, Empty.Object,connString);
    }
}