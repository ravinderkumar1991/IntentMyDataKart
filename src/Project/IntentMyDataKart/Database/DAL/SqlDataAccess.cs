using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntentMyDataKart.Database.DAL
{
    public interface IDataAccess : IDisposable
    {
        bool SaveData(string storedProcedureName, Dictionary<string, object> parameters);
        IDataReader RetrieveDataWithParameter(string storedProcedureName, Dictionary<string, string> parameters);
        IDataReader RetrieveData(string storedProcedureName);
        IDataReader RetrieveData(string storedProcedureName, Dictionary<string, object> parameters);
        string RetrieveScaler(string procedureName, System.Collections.Generic.Dictionary<string, string> dictParam);
        string RetrieveScalarData(string storedProcedureName, Dictionary<string, object> parameters);
        bool SaveDataWithObject(string storedProcedureName, Dictionary<string, object> parameters);
        bool SaveDataWithXml(string storedProcedureName, Dictionary<string, object> parameters);

        DataTable RetrieveDataInDataTable(string storedProcedureName, Dictionary<string, object> parameters);
        DataSet RetrieveDataInDataSet(string storedProcedureName, Dictionary<string, object> parameters);

        int AdhockQuery(string Query);

    }
    public class SqlDataAccess : IDisposable, IDataAccess
    {
        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        IDataReader reader;
        string connectionString = string.Empty;

        public IConfiguration AppSetting { get; }
        public SqlDataAccess()
        {

            AppSetting = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

            connectionString = AppSetting["ConnectionStrings:ConString"];
        }


        int IDataAccess.AdhockQuery(string query)
        {
            int i = 0;

            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand(query);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandTimeout = 60000;

            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            i = sqlCommand.ExecuteNonQuery();



            return i;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IDataReader IDataAccess.RetrieveData(string storedProcedureName, Dictionary<string, object> parameters)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand(storedProcedureName);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 60000;
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> key in parameters)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(key.Key, key.Value));
                }
            }
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            return reader = sqlCommand.ExecuteReader();

        }

        DataTable IDataAccess.RetrieveDataInDataTable(string storedProcedureName, Dictionary<string, object> parameters)
        {
            sqlConnection = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            try
            {

                sqlCommand = new SqlCommand(storedProcedureName);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60000;
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> key in parameters)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter(key.Key, key.Value));
                    }
                }
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlCommand;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return dt;
        }

        IDataReader IDataAccess.RetrieveDataWithParameter(string storedProcedureName, Dictionary<string, string> parameters)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand(storedProcedureName);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 60000;
            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> key in parameters)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(key.Key, key.Value));
                }
            }
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            return reader = sqlCommand.ExecuteReader();

        }
        String IDataAccess.RetrieveScalarData(string storedProcedureName, Dictionary<string, object> parameters)
        {
            string scalar;
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand(storedProcedureName);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 60000;
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> key in parameters)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(key.Key, key.Value));
                }
            }
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            return scalar = (string)sqlCommand.ExecuteScalar();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        bool IDataAccess.SaveData(string storedProcedureName, Dictionary<string, object> parameters)
        {
            try
            {
                using (sqlConnection = new SqlConnection(connectionString))
                {
                    sqlCommand = new SqlCommand(storedProcedureName);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 60000;
                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> key in parameters)
                        {
                            sqlCommand.Parameters.Add(new SqlParameter(key.Key, key.Value));
                        }
                    }
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        string IDataAccess.RetrieveScaler(string procedureName, Dictionary<string, string> dictParam)
        {

            object identity = 0;
            try
            {
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = procedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60000;
                sqlConnection = new SqlConnection(connectionString);
                foreach (var keyValue in dictParam)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(keyValue.Key, keyValue.Value));
                }
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                identity = sqlCommand.ExecuteScalar();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return identity.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <returns></returns>
        IDataReader IDataAccess.RetrieveData(string storedProcedureName)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand(storedProcedureName);
            sqlCommand.CommandTimeout = 60000;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            return reader = sqlCommand.ExecuteReader();
        }
        ~SqlDataAccess()
        {
            Dispose();
        }
        public void Dispose()
        {
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }
            if (sqlConnection != null)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            GC.SuppressFinalize(this);
        }


        bool IDataAccess.SaveDataWithObject(string storedProcedureName, Dictionary<string, object> parameters)
        {
            try
            {
                using (sqlConnection = new SqlConnection(connectionString))
                {
                    sqlCommand = new SqlCommand(storedProcedureName);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 60000;
                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> key in parameters)
                        {
                            sqlCommand.Parameters.Add(new SqlParameter(key.Key, key.Value));
                        }
                    }
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        bool IDataAccess.SaveDataWithXml(string storedProcedureName, Dictionary<string, object> parameters)
        {
            try
            {
                using (sqlConnection = new SqlConnection(connectionString))
                {
                    sqlCommand = new SqlCommand(storedProcedureName);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 60000;
                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> key in parameters)
                        {
                            sqlCommand.Parameters.Add(new SqlParameter(key.Key, key.Value));
                        }
                    }
                    sqlCommand.Connection = sqlConnection;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        DataSet IDataAccess.RetrieveDataInDataSet(string storedProcedureName, Dictionary<string, object> parameters)
        {
            sqlConnection = new SqlConnection(connectionString);
            DataSet ds = new DataSet();
            try
            {

                sqlCommand = new SqlCommand(storedProcedureName);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60000;
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> key in parameters)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter(key.Key, key.Value));
                    }
                }
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlCommand;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return ds;
        }
    }
}
