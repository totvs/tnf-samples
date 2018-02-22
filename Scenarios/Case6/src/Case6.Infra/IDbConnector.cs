using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Tnf.Dependency;

namespace Case6.Infra
{
    public interface IDbConnector : ITransientDependency, IDisposable
    {
        DbCommand CreateCommand();
        DbCommand CreateCommand(string commandText, List<DbParameter> parameters, CommandType commandType = CommandType.Text);
        DbConnection CreateConnection();
        DbParameter CreateParameter(string name, object value);
        DbParameter CreateParameter(string name, object value, DbType type);
        DbDataReader CreateReader(string commandText, List<DbParameter> sqlParameters = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.SequentialAccess);
        IEnumerable<T> CreateReader<T>(string commandText, List<DbParameter> sqlParameters = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.SequentialAccess)
            where T : class, new();
        int ExecuteNonQuery(string commandText);
        int ExecuteNonQuery(string commandText, List<DbParameter> sqlParameters);
        int ExecuteReader(string commandText, List<DbParameter> sqlParameters);
        T ExecuteScalar<T>(string commandText, List<DbParameter> sqlParameters = null);
    }
}