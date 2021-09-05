using System;
using System.Data.Common;

namespace Dapper.Logging.Hooks
{
    internal class WrappedConnectionFactory<T> : IHookedDbConnectionFactory<T>
    {
        private readonly Func<DbConnection> _factory;
        private readonly Func<string,DbConnection> _connStrFactory;
        public WrappedConnectionFactory(Func<DbConnection> factory) => _factory = factory;
        public WrappedConnectionFactory(Func<string,DbConnection> factory) => _connStrFactory = factory;

        public DbConnection CreateConnection(ISqlHooks<T> hooks, T context) => 
            new WrappedConnection<T>(_factory(), hooks, context);
        public DbConnection CreateConnection(ISqlHooks<T> hooks, T context,string connString) => 
            new WrappedConnection<T>(_connStrFactory(connString), hooks, context);
    }
}