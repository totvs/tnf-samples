using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Case6.Infra
{
    public class DbConnector : IDisposable, IDbConnector
    {
        private readonly DbProviderFactory dbProviderFactory;
        private DbCommand command;
        private DbConnection connection;
        private DbTransaction transaction;
        private bool manuallyControlConnection;
        private bool manuallyControlTransaction;

        public DbConnector(DbProviderFactory dbProviderFactory)
        {
            this.dbProviderFactory = dbProviderFactory;
        }

        public void CreateConnection()
        {
            connection = dbProviderFactory.CreateConnection();
        }

        public void ManuallyControlConnection(bool manuallyControlConnection)
        {
            this.manuallyControlConnection = manuallyControlConnection;
        }

        public void ManuallyControlTransaction(bool manuallyControlTransaction)
        {
            this.manuallyControlTransaction = manuallyControlTransaction;
        }

        public bool BeginTransaction()
        {
            try
            {
                if (transaction == null)
                {
                    transaction = connection.BeginTransaction();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CommitTransaction()
        {
            try
            {
                if (transaction == null)
                {
                    return false;
                }
                else
                {
                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RoolbackTransaction()
        {
            try
            {
                if (transaction == null)
                {
                    return false;
                }
                else
                {
                    transaction.Rollback();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DbCommand CreateCommand()
        {
            if (connection == null && !manuallyControlConnection)
                CreateConnection();

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

            if (!manuallyControlConnection)
                command.Connection.Open();

            command.CommandText = commandText;
            return command.ExecuteReader();
        }

        public T ExecuteScalar<T>(string commandText, List<DbParameter> sqlParameters = null)
        {
            using (var cmd = CreateCommand(commandText, sqlParameters))
            {
                if (!manuallyControlConnection)
                    cmd.Connection.Open();

                object result = cmd.ExecuteScalar();

                if (!manuallyControlConnection)
                    cmd.Connection.Close();

                if (result == null || result.GetType() == typeof(DBNull))
                    return default(T);

                if (!typeof(T).IsGenericType)
                {
                    result = Convert.ChangeType(result, typeof(T));
                }

                return (T)result;
            }
        }

        public int ExecuteNonQuery(string commandText)
        {
            if (command == null)
                command = CreateCommand();

            using (command)
            {
                if (!manuallyControlConnection)
                    command.Connection.Open();

                if (!manuallyControlTransaction)
                    BeginTransaction();

                int result = -1;

                try
                {
                    command.CommandText = commandText;
                    result = command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    if (!manuallyControlTransaction)
                        RoolbackTransaction();
                    throw;
                }

                if (!manuallyControlTransaction)
                    CommitTransaction();

                if (!manuallyControlConnection)
                    command.Connection.Close();

                return result;
            }
        }

        public int ExecuteNonQuery(string commandText, List<DbParameter> sqlParameters)
        {
            using (var cmd = CreateCommand(commandText, sqlParameters))
            {
                if (!manuallyControlConnection)
                    cmd.Connection.Open();

                if (!manuallyControlTransaction)
                    BeginTransaction();

                int result = -1;

                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    if (!manuallyControlTransaction)
                        RoolbackTransaction();
                    throw;
                }

                if (!manuallyControlTransaction)
                    CommitTransaction();

                if (!manuallyControlConnection)
                    cmd.Connection.Close();

                return result;
            }
        }

        public int ExecuteReader(string commandText, List<DbParameter> sqlParameters)
        {
            using (var cmd = CreateCommand(commandText, sqlParameters))
            {
                if (!manuallyControlConnection)
                    cmd.Connection.Open();
                int result = cmd.ExecuteNonQuery();
                if (!manuallyControlConnection)
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

            if (transaction != null)
                transaction.Dispose();

            command = null;
            connection = null;
        }
    }
}
