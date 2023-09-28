using Microsoft.Data.Sqlite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// A class which makes accessing databases tidier by removing the low-level implementation details
    /// </summary>
    public class DbContext : IDisposable
    {
        /// <summary>
        /// The current connection to the database
        /// </summary>
        private SqliteConnection Connection { get; set; }

        /// <summary>
        /// Creates a new DbContext
        /// </summary>
        /// <param name="connectionString"></param>
        public DbContext(string connectionString)
        {
            Connection = new SqliteConnection(connectionString);

            Batteries.Init(); // Sets the SQLite provider
            Connection.Open();
        }

        /// <summary>
        /// Creates a new DbParameter using the specified name and value
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbParameter GenerateParameter(string parameterName, object value)
        {
            DbParameter parameter = new SqliteParameter(parameterName, value);
            return parameter;
        }

        /// <summary>
        /// Gets a single database row that matches the given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DbRow GetDataRow(string sql)
        {
            return GetDataRow(sql, new DbParameter[0]);
        }

        /// <summary>
        /// Gets a single database row that matches the given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<DbRow> GetDataRowAsync(string sql)
        {
            return await GetDataRowAsync(sql, new DbParameter[0]);
        }

        /// <summary>
        /// Gets a single database row that matches the given sql and parameters
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DbRow GetDataRow(string sql, IEnumerable<DbParameter> parameters)
        {
            using (DbDataReader dataReader = GenerateDataReader(sql, parameters))
            {
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    DbRow dataRow = new DbRow(dataReader);
                    return dataRow;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets a single database row that matches the given sql and parameters
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<DbRow> GetDataRowAsync(string sql, IEnumerable<DbParameter> parameters)
        {
            using (DbDataReader dataReader = await GenerateDataReaderAsync(sql, parameters).ConfigureAwait(false))
            {
                if (dataReader.HasRows)
                {
                    await dataReader.ReadAsync().ConfigureAwait(false);

                    DbRow dataRow = new DbRow(dataReader);

                    return dataRow;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets any database rows that match the given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<DbRow> GetDataRows(string sql)
        {
            return GetDataRows(sql, new DbParameter[0]);
        }

        /// <summary>
        /// Gets any database rows that match the given sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<List<DbRow>> GetDataRowsAsync(string sql)
        {
            return await GetDataRowsAsync(sql, new DbParameter[0]).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets any database rows that match the given sql and parameters
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<DbRow> GetDataRows(string sql, IEnumerable<DbParameter> parameters)
        {
            using (DbDataReader dataReader = GenerateDataReader(sql, parameters))
            {
                if (dataReader.HasRows)
                {
                    List<DbRow> rows = new List<DbRow>();

                    while (dataReader.Read())
                    {
                        DbRow dataRow = new DbRow(dataReader);
                        rows.Add(dataRow);
                    }

                    return rows;
                }

                return new List<DbRow>();
            }
        }

        /// <summary>
        /// Gets any database rows that match the given sql and parameters
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<List<DbRow>> GetDataRowsAsync(string sql, IEnumerable<DbParameter> parameters)
        {
            using (DbDataReader dataReader = await GenerateDataReaderAsync(sql, parameters).ConfigureAwait(false))
            {
                if (dataReader.HasRows)
                {
                    List<DbRow> rows = new List<DbRow>();

                    while (await dataReader.ReadAsync().ConfigureAwait(false))
                    {
                        DbRow dataRow = new DbRow(dataReader);
                        rows.Add(dataRow);
                    }
                    
                    return rows;
                }

                return new List<DbRow>();
            }
        }

        /// <summary>
        /// Disposes of the DbContext and any managed resources
        /// </summary>
        public void Dispose()
        {
            Connection?.Dispose();
        }

        /// <summary>
        /// Uses the SQLite connection to send a SQL query to the database synchronously and returns a DbDataReader to read the results
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private DbDataReader GenerateDataReader(string sql, IEnumerable<DbParameter> parameters)
        {
            DbCommand command = new SqliteCommand(sql, Connection)
            {
                CommandType = CommandType.Text
            };

            foreach (DbParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            DbDataReader dataReader = command.ExecuteReader();

            return dataReader;
        }

        /// <summary>
        /// Uses the SQLite connection to send a SQL query to the database asynchronously and returns a DbDataReader to read the results
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private async Task<DbDataReader> GenerateDataReaderAsync(string sql, IEnumerable<DbParameter> parameters)
        {
            await Connection.OpenAsync().ConfigureAwait(false);

            DbCommand command = new SqliteCommand(sql, Connection)
            {
                CommandType = CommandType.Text
            };

            foreach (DbParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            DbDataReader dataReader = await command.ExecuteReaderAsync().ConfigureAwait(false);

            return dataReader;
        }
    }
}
