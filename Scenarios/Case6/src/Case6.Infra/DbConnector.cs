using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Tnf.Configuration;

namespace Case6.Infra
{
    public class DbConnector : IDisposable, IDbConnector
    {
        private readonly DbProviderFactory dbProviderFactory;
        private readonly ITnfConfiguration tnfConfiguration;
        private DbCommand command;
        private DbConnection connection;

        public DbConnector(DbProviderFactory dbProviderFactory, ITnfConfiguration tnfConfiguration)
        {
            this.dbProviderFactory = dbProviderFactory;
            this.tnfConfiguration = tnfConfiguration;

            this.connection = CreateConnection();
        }

        public DbConnection CreateConnection()
        {
            var connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = tnfConfiguration.DefaultNameOrConnectionString;
            return connection;
        }

        public DbCommand CreateCommand()
        {
            command = connection.CreateCommand();
            return command;
        }

        public DbCommand CreateCommand(string commandText, List<DbParameter> parameters, CommandType commandType = CommandType.Text)
        {
            if (command == null)
                command = CreateCommand();

            command.CommandType = commandType;
            command.CommandText = commandText;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            return command;
        }

        public DbDataReader CreateReader(string commandText, List<DbParameter> sqlParameters = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.SequentialAccess)
        {
            if (command == null)
                command = CreateCommand(commandText, sqlParameters, commandType);

            command.Connection.Open();

            command.CommandText = commandText;
            return command.ExecuteReader();
        }

        public T ExecuteScalar<T>(string commandText, List<DbParameter> sqlParameters = null)
        {
            using (var cmd = CreateCommand(commandText, sqlParameters))
            {
                cmd.Connection.Open();
                object result = cmd.ExecuteScalar();
                cmd.Connection.Close();

                if (result == null || result.GetType() == typeof(DBNull))
                    return default(T);
                else
                {
                    if (!typeof(T).IsGenericType)
                    {
                        result = Convert.ChangeType(result, typeof(T));
                    }
                    return (T)result;
                }
            }
        }

        public int ExecuteNonQuery(string commandText)
        {
            if (command == null)
                command = CreateCommand();

            using (command)
            {
                command.Connection.Open();
                command.CommandText = commandText;
                int result = command.ExecuteNonQuery();
                command.Connection.Close();
                return result;
            }
        }

        public int ExecuteNonQuery(string commandText, List<DbParameter> sqlParameters)
        {
            using (var cmd = CreateCommand(commandText, sqlParameters))
            {
                cmd.Connection.Open();
                int result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return result;
            }
        }

        public int ExecuteReader(string commandText, List<DbParameter> sqlParameters)
        {
            using (var cmd = CreateCommand(commandText, sqlParameters))
            {
                cmd.Connection.Open();
                int result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return result;
            }
        }

        public DbParameter CreateParameter(string name, object value)
        {
            if (command == null)
                command = CreateCommand();

            var param = command.CreateParameter();
            param.ParameterName = name;
            param.Value = value == null ? DBNull.Value : value;

            command.Parameters.Add(param);
            return param;
        }

        public DbParameter CreateParameter(string name, object value, DbType type)
        {
            if (command == null)
                command = CreateCommand();

            var param = command.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            param.DbType = type;
            return param;
        }

        public IEnumerable<T> CreateReader<T>(string commandText, List<DbParameter> sqlParameters = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.SequentialAccess) 
            where T : class, new()
        {
            List<T> collection = new List<T>();

            using (var reader = CreateReader(commandText, sqlParameters, commandType, commandBehavior))
            {
                while (reader.Read())
                {
                    collection.Add(reader.MapTo<T>());
                }
            }

            return collection;
        }

        public void Dispose()
        {
            if (command != null)
                command.Dispose();

            connection.Close();
            connection.Dispose();

            command = null;
            connection = null;
        }
    }
}
