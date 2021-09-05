using System.Data.Common;

namespace Dapper.Logging
{
    public interface IDbConnectionFactory
    {
        DbConnection CreateConnection();
        DbConnection CreateConnection(string connString);
    }
    
    public interface IDbConnectionFactory<in T>
    {
        DbConnection CreateConnection(T context);
    }
}