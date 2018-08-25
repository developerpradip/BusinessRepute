using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
//using HouzzHelper;

namespace AppHelper
{
    public class DBHandler
    {
        protected string dbServername = string.Empty;
        protected string databaseName = string.Empty;
        protected string dbUser = string.Empty;
        protected string dbPassword = string.Empty;
        protected bool bStoreInDatabase = false;
        //private DBHelper _dbHelper = null;
        private SqlConnection _sqlConnection = null;


        #region MSAccess

        public DBHandler()
        {
            //_dbHelper = new DBHelper();
        }

        //public int ExecuteScalarQuery(string query)
        //{
        //    string sqlCommand = query;
        //    int result = 0;
        //    object returnVal = _dbHelper.ExecuteScalar(sqlCommand);
        //    if (returnVal != null )
        //    {
        //        Int32.TryParse(returnVal.ToString(), out result);
        //    }
        //    return result;
        //}

        //public int ExecuteScalarQuery(string query, DBParameterCollection paramColl)
        //{
        //    string sqlCommand = query;
        //    DBParameterCollection paramCollection = paramColl;
        //    object returnVal =_dbHelper.ExecuteScalar(sqlCommand, paramCollection);
        //    if (returnVal != null)
        //    {
        //        return (int)returnVal;
        //    }
        //    else
        //        return 0;
        //}

        //public int ExecuteInsertQuery(string query, DBParameterCollection paramcoll)
        //{
        //    int recordsAffected = 0;
        //    string sqlCommand = query;
        //    DBParameterCollection paramCollection = paramcoll;
        //    IDbTransaction transaction = _dbHelper.BeginTransaction();

        //    try
        //    {
        //        IDataReader objScalar = _dbHelper.ExecuteDataReader(sqlCommand, paramCollection, transaction, CommandType.Text);

        //        if (objScalar != null)
        //        {
        //            recordsAffected = objScalar.RecordsAffected; ;
        //            objScalar.Close();
        //            objScalar.Dispose();
        //        }
        //        _dbHelper.CommitTransaction(transaction);
        //    }
        //    catch (Exception ex)
        //    {
        //        _dbHelper.RollbackTransaction(transaction);
        //        YelpTrace.Write(ex);
        //    }

        //    return recordsAffected;
        //}

        //public int ExecuteInsertQuery(string query)
        //{
        //    int recordsAffected = 0;
        //    string sqlCommand = query;
        //    IDbTransaction transaction = _dbHelper.BeginTransaction();
        //    DBParameterCollection dbParamCollection = new DBParameterCollection();
        //    try
        //    {
        //        IDataReader objScalar = _dbHelper.ExecuteDataReader(sqlCommand, dbParamCollection, transaction, CommandType.Text);

        //        if (objScalar != null)
        //        {
        //            recordsAffected = objScalar.RecordsAffected; ;
        //            objScalar.Close();
        //            objScalar.Dispose();
        //        }
        //        _dbHelper.CommitTransaction(transaction);
        //    }
        //    catch (Exception ex)
        //    {
        //        _dbHelper.RollbackTransaction(transaction);
        //        YelpTrace.Write(ex);
        //    }

        //    return recordsAffected;
        //}
        //public DataTable ExecuteSelectQuery(string query)
        //{
        //    DataTable table = new DataTable();
        //    string sqlCommand = query;

        //    try
        //    {
        //        table = _dbHelper.ExecuteDataTable(sqlCommand);
        //    }
        //    catch (Exception ex)
        //    {
        //        YelpTrace.Write(ex);
        //    }

        //    return table;
        //}

        //public bool StoreUserDetails(DBParameterCollection userParamCollection, DBParameterCollection personalParamCollection, DBParameterCollection securityParamCollection, out string message)
        //{
        //    bool bReturn = false;
        //    int recordsAffected = 0;

        //    //check in user name is available
        //    string selectUserName = "select * from UserDetails where username = @UserName;";
        //    int userNameExists = ExecuteScalarQuery(selectUserName, userParamCollection);
        //    if (userNameExists < 1)
        //    {
        //        message = " User {0} is successfully added";
        //        IDbTransaction transaction = _dbHelper.BeginTransaction();
        //        string insertSecurityDetailsQuery = "Insert into UserSecurityDetails (SecurityQ1, SecurityA1) values (@SecurityQ1, @SecurityA1);";
        //        try
        //        {
        //            recordsAffected = _dbHelper.ExecuteNonQuery(insertSecurityDetailsQuery, securityParamCollection, transaction);
        //            if (recordsAffected > 0)
        //            {
        //                string selectSecurityDetailId = "select max(UserSecurityDetailId) as UserSecurityDetailId from UserSecurityDetails;";
        //                int maxSecurityId = ExecuteScalarQuery(selectSecurityDetailId);
        //                maxSecurityId = maxSecurityId + 1;

        //                string insertPresonalDetailsQuery = "Insert into PersonalDetail(FirstName, LastName, EmailId, PhoneNumber, userSecurityDetailId) values " +
        //                    "(@FirstName, @LastName, @EmailId, @PhoneNumber, " + maxSecurityId.ToString() + ");";
        //                recordsAffected = _dbHelper.ExecuteNonQuery(insertPresonalDetailsQuery, personalParamCollection, transaction);

        //                if (recordsAffected > 0)
        //                {
        //                    string selectPresonalDetailId = "select max(personalDetailId) from PersonalDetail";
        //                    int maxPersonalDetailId = ExecuteScalarQuery(selectPresonalDetailId);
        //                    maxPersonalDetailId = maxPersonalDetailId + 1;

        //                    string insertuserDetail = "Insert into UserDetails (UserName, [password], personalDetailId) values (@UserName, @Password, " + maxPersonalDetailId.ToString() + ");";
        //                    recordsAffected = _dbHelper.ExecuteNonQuery(insertuserDetail, userParamCollection, transaction);
        //                    if (recordsAffected > 0)
        //                    {
        //                        bReturn = true;
        //                        _dbHelper.CommitTransaction(transaction);
        //                    }
        //                }
        //            }
        //        }

        //        catch (Exception err)
        //        {
        //            _dbHelper.RollbackTransaction(transaction);
        //            message = "Error in User registration, please try again.";
        //            YelpTrace.Write(err);
        //        }
        //    }
        //    else
        //    {
        //        message = "username is not available, please try another username";
        //    }
        //    return bReturn;
            //string insertSecurityDetailsQuery = "Insert into UserSecurityDetails (SecurityQ1, SecurityA1) values ('" + dbParamCollection[] + "', '" + txtSecurityAnswer.Text + "');";
            //IDataReader objScalar = _dbHelper.ExecuteDataReader(sqlCommand, dbParamCollection, transaction, CommandType.Text);

            //if (objScalar != null)
            //{
            //    recordsAffected = objScalar.RecordsAffected; ;
            //    objScalar.Close();
            //    objScalar.Dispose();
            //}
            
            //int result = ExecuteInsertQuery(insertSecurityDetailsQuery);
            //if (result > 0)
            //{
            //    string selectSecurityDetailId = "select max(UserSecurityDetailId) as UserSecurityDetailId from UserSecurityDetails;";
            //    int maxSecurityId = dbHandler.ExecuteScalarQuery(selectSecurityDetailId);
            //    string insertPresonalDetailsQuery = "Insert into PersonalDetail(FirstName, LastName, EmailId, PhoneNumber, userSecurityDetailId) values ('" +
            //        txtFirstName.Text + "', '" + txtLastName.Text + "', '" + txtEmail.Text + "', '" + txtPhoneNumber.Text + "', " + maxSecurityId.ToString() + ");";
            //    result = dbHandler.ExecuteInsertQuery(insertPresonalDetailsQuery);
            //    if (result > 0)
            //    {
            //        string selectPresonalDetailId = "select max(personalDetailId) from PersonalDetail";
            //        int maxPersonalDetailId = dbHandler.ExecuteScalarQuery(selectPresonalDetailId);
            //        string insertuserDetail = "Insert into UserDetails(UserName, password, personalDetailId) values ('" + txtUserName.Text + "', '" + txtPassword.Text + "', " + maxPersonalDetailId.ToString() + ");";
            //        result = dbHandler.ExecuteInsertQuery(insertPresonalDetailsQuery);
            //        if (result > 0)
            //        {
            //            FailureText.Text = " User is successfully added";
            //        }
            //        else
            //        {
            //            FailureText.Text = " Error in user registration, please try again.";
            //        }
            //    }
            //    else
            //    {
            //        FailureText.Text = " Error in user registration, please try again.";
            //    }
            //}
            //else
            //{
            //    FailureText.Text = " Error in user registration, please try again.";
            //}
        //}

        //public void Close()
        //{
        //    _dbHelper.Close();
        //}
        #endregion MSAccess

        #region SQL Express
        public DBHandler(int i)
        {
        
        }

        private SqlConnection  GetConnection()
        {
            //string strConnectionString = ConfigurationSettings.AppSettings[ConfigurationSettings.AppSettings["defaultConnection"]].ToString(); 
            string strConnectionString = ConfigurationManager.ConnectionStrings["ConverterConnectionString"].ConnectionString.ToString(); 
            
            if (_sqlConnection == null)
            {
                _sqlConnection = new SqlConnection(strConnectionString);
                _sqlConnection.Open();
            }
            return _sqlConnection;
        }

        private void CloseConnection()
        {
            if (_sqlConnection != null)
            {
                if (_sqlConnection.State != ConnectionState.Closed)
                {
                    _sqlConnection.Close();
                }
                _sqlConnection.Dispose();
            }
        }

        public int ExecuteNonQuery(string storedProcName, List<SqlParameter> sqlParameters)
        {
            int nReturnValue = -1;
            SqlConnection sqlConnection = null;
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.CommandText = storedProcName;
                command.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter sqlParameter in sqlParameters)
                {
                    command.Parameters.Add(sqlParameter);
                }
                sqlConnection = GetConnection();
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    command.Connection = sqlConnection;
                    nReturnValue = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write("Failed in execution SP name " + storedProcName + " Exception Details " +  ex);
            }
            finally
            {
                if (command != null)
                {
                    command.Parameters.Clear();
                    command.Dispose();
                }
                CloseConnection();
            }
            return nReturnValue;
        }


        public int ExecuteNonQuery(string storedProcName, List<SqlParameter> sqlParameters, string outparam, out string outParamValue)
        {
            int nReturnValue = -1;
            SqlConnection sqlConnection = null;
            SqlCommand command = null;
            outParamValue = "";
            try
            {
                command = new SqlCommand();
                command.CommandText = storedProcName;
                command.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter sqlParameter in sqlParameters)
                {
                    command.Parameters.Add(sqlParameter);
                }
                sqlConnection = GetConnection();
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    command.Connection = sqlConnection;
                    nReturnValue = command.ExecuteNonQuery();
                    outParamValue = command.Parameters[outparam].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write("Failed in execution SP name " + storedProcName + " Exception Details " + ex);
            }
            finally
            {
                if (command != null)
                {
                    command.Parameters.Clear();
                    command.Dispose();
                }
                CloseConnection();
            }
            return nReturnValue;
        }

        public object ExecuteScalar(string storedProcName, List<SqlParameter> sqlParameteres)
        {
            Object returnValue = null;
            SqlConnection sqlConnection = null;
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.CommandText = storedProcName;
                command.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter sqlParameter in sqlParameteres)
                {
                    command.Parameters.Add(sqlParameter);
                }

                sqlConnection = GetConnection();
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    command.Connection = sqlConnection;
                    returnValue = command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write("Failed in ExecuteScalar SP name " + storedProcName + "Exception Details " + ex);
               
            }
            finally
            {
                if (command != null)
                {
                    command.Parameters.Clear();
                    command.Dispose();
                }
                CloseConnection();
            }
            return returnValue;
        }

        public DataSet ExecuteReader(string storedProcName, SqlParameterCollection sqlparameteres)
        {
            SqlConnection sqlConnection = null;
            DataSet resultSet = new DataSet();
            SqlCommand command = null;
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                command = new SqlCommand();
                command.CommandText = storedProcName;
                command.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter sqlParameter in sqlparameteres)
                {
                    command.Parameters.Add(sqlParameter);
                }
                
                sqlConnection = GetConnection();
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    command.Connection = sqlConnection;
                    sqlDataAdapter.SelectCommand = command;
                    sqlDataAdapter.Fill(resultSet);
                }
            }
            catch (Exception ex)
            {
                //YelpTrace.Write(ex);

                YelpTrace.Write("Failed in ExecuteReader SP name " + storedProcName + "Exception Details " + ex);
            }
            finally
            {
                if (command != null)
                {
                    command.Parameters.Clear();
                    command.Dispose();
                }
                CloseConnection();
            }
            return resultSet;
        }


        public DataSet ExecuteReader(string storedProcName, List<SqlParameter> sqlparameteres)
        {
            SqlConnection sqlConnection = null;
            DataSet resultSet = new DataSet();
            SqlCommand command = null;
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                command = new SqlCommand();
                command.CommandText = storedProcName;
                command.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter sqlParameter in sqlparameteres)
                {
                    command.Parameters.Add(sqlParameter);
                }

                sqlConnection = GetConnection();
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    command.Connection = sqlConnection;
                    sqlDataAdapter.SelectCommand = command;
                    sqlDataAdapter.Fill(resultSet);
                }
            }
            catch (Exception ex)
            {
                //YelpTrace.Write(ex);

                YelpTrace.Write("Failed in ExecuteReader SP name " + storedProcName + "Exception Details " + ex);
            }
            finally
            {
                if (command != null)
                {
                    command.Parameters.Clear();
                    command.Dispose();
                }
                CloseConnection();
            }
            return resultSet;
        }

        public DataTable ExecuteReaderinTable(string storedProcName, List<SqlParameter> sqlparameteres)
        {
            SqlConnection sqlConnection = null;
            DataSet resultSet = new DataSet();
            SqlCommand command = null;
            DataTable datatable = null;
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                command = new SqlCommand();
                command.CommandText = storedProcName;
                command.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter sqlParameter in sqlparameteres)
                {
                    command.Parameters.Add(sqlParameter);
                }

                sqlConnection = GetConnection();
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    command.Connection = sqlConnection;
                    sqlDataAdapter.SelectCommand = command;
                    sqlDataAdapter.Fill(resultSet);
                    datatable = resultSet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write("Failed in ExecuteReaderinTable SP name " + storedProcName + "Exception Details " + ex);
            }
            finally
            {
                if (command != null)
                {
                    command.Parameters.Clear();
                    command.Dispose();
                }
                CloseConnection();
            }
            return datatable;
        }

        public DataTable ExecuteReaderinTable(string query)
        {
            SqlConnection sqlConnection = null;
            DataSet resultSet = new DataSet();
            SqlCommand command = null;
            DataTable datatable = null;
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                command = new SqlCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                //foreach (SqlParameter sqlParameter in sqlparameteres)
                //{
                //    command.Parameters.Add(sqlParameter);
                //}

                sqlConnection = GetConnection();
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    command.Connection = sqlConnection;
                    sqlDataAdapter.SelectCommand = command;
                    sqlDataAdapter.Fill(resultSet);
                    datatable = resultSet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write("Failed in ExecuteReaderinTable Query " + query + "Exception Details " + ex);
            }
            finally
            {
                if (command != null)
                {
                    command.Parameters.Clear();
                    command.Dispose();
                }
                CloseConnection();
            }
            return datatable;
        }


        public int ExecuteNonQuery(string sqlQuery)
        {
            int nReturnValue = -1;
            SqlConnection sqlConnection = null;
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.CommandText = sqlQuery;
                command.CommandType = CommandType.Text;
                
                sqlConnection = GetConnection();
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                {
                    command.Connection = sqlConnection;
                    nReturnValue = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write("Failed in execution querye " + sqlQuery + " Exception Details " + ex);
            }
            finally
            {
                if (command != null)
                {
                    command.Parameters.Clear();
                    command.Dispose();
                }
                CloseConnection();
            }
            return nReturnValue;
        }
        #endregion SQL Express
    }
}
