using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using PdfCreatorApplication.Core.Utils.Exceptions;
using SiteCore.Utils.Exceptions;

namespace PdfCreatorApplication.Core.Utils.Database
{
    /// <summary>
    /// Contains methods to work with database
    /// </summary>
    [Serializable]
    public class SqlDbHelper : IDisposable
    {
        /// <summary>
        /// The logger
        /// </summary>

        /// <summary>
        /// The default connection
        /// </summary>
        private const string DefaultConnection = "DefaultConnection";

        /// <summary>
        /// The unique index violation
        /// </summary>
        private const int UniqueIndexViolation = 2601;

        /// <summary>
        /// The unique constraint violation
        /// </summary>
        private const int UniqueConstraintViolation = 2627;

        /// <summary>
        /// The foreign key constraint violation
        /// </summary>
        private const int ForeignKeyConstraintViolation = 547;

        /// <summary>
        /// The connection strings
        /// </summary>
        private static Dictionary<string, string> _connectionStrings;

        /// <summary>
        /// The SQL command parameters
        /// </summary>
        private List<SqlParameter> sqlCommandParameters;

        /// <summary>
        /// The connection
        /// </summary>
        private SqlConnection _connection;
        
        /// <summary>
        /// The connection
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
        }


        /// <summary>
        /// The SQL command timeout. 
        /// Set equals to zero means there is no timeout
        /// </summary>
        private const int SqlCommandTimeout = 0;

        /// <summary>
        /// Initializes the <see cref="SqlDbHelper"/> class.
        /// </summary>
        static SqlDbHelper()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDbHelper"/> class.
        /// </summary>
        public SqlDbHelper()
        {
            sqlCommandParameters = new List<SqlParameter>();
            _connection = new SqlConnection(GetConnectionString(DefaultConnection));
            _connection.Open();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDbHelper"/> class.
        /// </summary>
        /// <param name="connectionStringKey">The key of connection string.</param>
        public SqlDbHelper(string connectionStringKey)
        {
            sqlCommandParameters = new List<SqlParameter>();
            _connection = new SqlConnection(GetConnectionString(connectionStringKey));
            _connection.Open();
        }


        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private static void Initialize()
        {
            _connectionStrings = new Dictionary<string, string>();
            ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;
            foreach (ConnectionStringSettings connection in connectionStrings)
            {
                _connectionStrings.Add(connection.Name, connection.ConnectionString);
            }
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static string GetConnectionString(string key)
        {
            return (_connectionStrings.ContainsKey(key) ? _connectionStrings[key] : string.Empty);
        }



        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <remarks>
        /// Could be used in case of Insert, Update or Delete sql commands
        /// </remarks>
        /// <param name="sql">The SQL query.</param>
        /// <returns>Returns the number of rows affected</returns>
        public int ExecuteNonQuery(string sql)
        {
            int rowsAffected = 0;
            SqlCommand command = null;
            try
            {
                // Setup the attributes of SqlCommand
                command = new SqlCommand(sql, _connection)
                {
                    CommandTimeout = SqlCommandTimeout
                };
                SetupCommandParameters(command);

                // Execute the query
                rowsAffected = command.ExecuteNonQuery();

                //  Gathers post execution information
                sqlCommandParameters.Clear();
            }
            catch (Exception ex)
            {
                //  Gathers error information
                throw new CoreException(string.Format("Error of query execution.\n{0}\nExecuted SQL:\n{1}", ex.Message, sql), ex);
            }
            finally
            {
                //  Release of the resources & saving of the querylog info
                CloseCommand(command);
            }

            return rowsAffected;
        }


        /// <summary>
        /// Gets the data row collection.
        /// </summary>
        /// <param name="sql">The SQL query.</param>
        /// <returns>Data row collection as result of execution of SQL query</returns>
        public DataRowCollection GetDataRowCollection(string sql)
        {
            DataRowCollection dataRows = null;
            SqlDataAdapter adapter = null;
            DataTable dataTable = null;
            SqlCommand command = null;
            DateTime? queryStarted = null;
            DateTime? queryFinished = null;
            int rowsAffected = -1;
            try
            {
                // Setup the attributes of SqlCommand
                command = new SqlCommand(sql, _connection);
                command.CommandTimeout = SqlCommandTimeout;
                SetupCommandParameters(command);
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();

                queryStarted = DateTime.Now;
                // Execute the query
                adapter.Fill(dataTable);

                // Gets the results                              
                queryFinished = DateTime.Now;
                dataRows = dataTable.Rows;
                rowsAffected = (dataRows != null ? dataRows.Count : 0);
                sqlCommandParameters.Clear();
            }
            catch (Exception ex)
            {
                throw new CoreException(string.Format("Error getting of data.\n{0}\nExecuted SQL:\n{1}", ex.Message, sql), ex);
            }
            finally
            {
                CloseAll(command, adapter, dataTable);
            }

            return dataRows;
        }

        public IEnumerable<DataRow> GetDataRowEnumerable(string sql)
        {
            IEnumerable<DataRow> dataRows = null;
            SqlDataAdapter adapter = null;
            DataTable dataTable = null;
            SqlCommand command = null;
            DateTime? queryStarted = null;
            DateTime? queryFinished = null;
            int rowsAffected = -1;
            try
            {
                // Setup the attributes of SqlCommand
                command = new SqlCommand(sql, _connection);
                command.CommandTimeout = SqlCommandTimeout;
                SetupCommandParameters(command);
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();

                queryStarted = DateTime.Now;
                // Execute the query
                adapter.Fill(dataTable);

                // Gets the results                              
                queryFinished = DateTime.Now;
                dataRows = dataTable.AsEnumerable();
                rowsAffected = (dataRows != null ? dataRows.Count() : 0);
                sqlCommandParameters.Clear();
            }
            catch (Exception ex)
            {
                throw new CoreException(string.Format("Error getting of data.\n{0}\nExecuted SQL:\n{1}", ex.Message, sql), ex);
            }
            finally
            {
                CloseAll(command, adapter, dataTable);
            }

            return dataRows;
        }


        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        /// <exception cref="CoreException"></exception>
        public object ExecuteScalar(string sql)
        {
            object result = null;
            SqlCommand command = null;
            try
            {
                // Setup the attributes of SqlCommand
                command = new SqlCommand(sql, _connection)
                {
                    CommandTimeout = SqlCommandTimeout
                };
                SetupCommandParameters(command);

                // Executed SQL
                //Debug.WriteLine(SqlDebugHelper.CreateExecutableSqlStatement(sql, sqlCommandParameters));

                result = command.ExecuteScalar();

                //  Gathers post execution information
                sqlCommandParameters.Clear();

            }
            catch (SqlException ex)
            {
                if (ex.Number == UniqueIndexViolation)
                {
                    throw new UniqueIndexViolationException(ex, "Unique index violation occurred during query execution.\n{0}\nExecuted SQL:\n{1}", ex.Message, sql);
                }

                if (ex.Number == UniqueConstraintViolation)
                {
                    throw new UniqueConstraintViolationException(ex, "Unique constraint violation occurred during query execution.\n{0}\nExecuted SQL:\n{1}", ex.Message, sql);
                }

                if (ex.Number == ForeignKeyConstraintViolation)
                {
                    throw new ForeignKeyConstraintViolationException(ex, "Foreign key constraint violation occurred during query execution.\n{0}\nExecuted SQL:\n{1}", ex.Message, sql);
                }

                throw new CoreException(ex, "SQL Error of query execution.\n{0}\nExecuted SQL:\n{1}", ex.Message, sql);
            }
            catch (Exception ex)
            {
                //  Gathers error information
                throw new CoreException(ex, "Error of query execution.\n{0}\nExecuted SQL:\n{1}", ex.Message, sql);
            }
            finally
            {
                //  Release of the resources & saving of the querylog info
                CloseCommand(command);
            }

            return result;
        }

        /// <summary>
        /// Setups the command's parameters.
        /// </summary>
        /// <param name="command">The command.</param>
        private void SetupCommandParameters(SqlCommand command)
        {
            foreach (SqlParameter parameter in sqlCommandParameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        /// <summary>
        /// Adds the SQL command parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="size">The size.</param>
        /// <param name="value">The value.</param>
        public void AddSQLCommandParameter(string name, SqlDbType type, int size, object value)
        {
            sqlCommandParameters.Add(GetParameter(name, type, size, value));
        }

        /// <summary>
        /// Adds the SQL command parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public void AddSQLCommandParameter(string name, SqlDbType type, object value)
        {
            sqlCommandParameters.Add(GetParameter(name, type, value));
        }

        /// <summary>
        /// Adds the SQL command parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void AddSQLCommandParameter(string name, Object value)
        {
            sqlCommandParameters.Add(GetParameter(name, value));
        }


        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="type">The type.</param>
        /// <param name="size">The size.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private SqlParameter GetParameter(string parameterName, SqlDbType type, int size, object value)
        {
            var parameter = GetParameter(parameterName, type, value);
            parameter.Size = size;

            return parameter;
        }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private SqlParameter GetParameter(string parameterName, SqlDbType type, object value)
        {
            var parameter = GetParameter(parameterName, value);
            parameter.SqlDbType = type;

            return parameter;
        }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private SqlParameter GetParameter(string parameterName, object value)
        {
            var parameter = new SqlParameter
            {
                ParameterName = "@" + parameterName,
                Value = value ?? DBNull.Value
            };

            return parameter;
        }

        /// <summary>
        /// Closes all.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="adapter">The adapter.</param>
        private void CloseAll(SqlCommand command, SqlDataAdapter adapter)
        {
            CloseCommand(command);
            CloseDataAdapter(adapter);
        }

        /// <summary>
        /// Closes all.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="adapter">The adapter.</param>
        /// <param name="dataTable">The data table.</param>
        private void CloseAll(SqlCommand command, SqlDataAdapter adapter, DataTable dataTable)
        {
            CloseCommand(command);
            CloseDataAdapter(adapter);
            CloseDataTable(dataTable);
        }

        /// <summary>
        /// Closes the command.
        /// </summary>
        /// <param name="command">The command.</param>
        private void CloseCommand(SqlCommand command)
        {
            if (command != null)
            {
                sqlCommandParameters.Clear();
                command.Dispose();
            }
        }

        /// <summary>
        /// Closes the data adapter.
        /// </summary>
        /// <param name="adapter">The adapter.</param>
        private void CloseDataAdapter(SqlDataAdapter adapter)
        {
            if (adapter != null)
            {
                adapter.Dispose();
            }
        }

        /// <summary>
        /// Closes the data table.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        private void CloseDataTable(DataTable dataTable)
        {
            if (dataTable != null)
            {
                dataTable.Dispose();
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SqlDbHelper"/> class.
        /// </summary>
        ~SqlDbHelper()
        {
            Close();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        private void Close()
        {
            try
            {
                if (_connection != null && _connection.State != ConnectionState.Closed)
                    _connection.Close();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.ToString());
            }
        }



        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
            sqlCommandParameters = null;
            _connection = null;
            GC.SuppressFinalize(this);
        }
    }
}
