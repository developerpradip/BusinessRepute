using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace AppHelper.Common
{
    public class Helper
    {
        public static string GetUserId(string userName)
        {
            string userId = string.Empty;
            if (!string.IsNullOrEmpty(userName))
            {
                List<SqlParameter> dbParamCollection = new List<SqlParameter>();
                //username
                SqlParameter dbparamuserName = new SqlParameter("@USER_NAME", System.Data.SqlDbType.VarChar, 50);

                dbparamuserName.Value = userName;
                dbParamCollection.Add(dbparamuserName);

                //[USEREMAIL_SELECT]
                DBHandler dbHandler = new DBHandler(1);
                object result = dbHandler.ExecuteScalar(Constants.SP_USER_SELECT, dbParamCollection);
                if (result != null)
                {
                    userId = result.ToString();
                }
            }


            return userId;
        }

        /// <summary>
        /// to get the accessToken from database
        /// </summary>
        /// <returns></returns>
        public static string GetAccessTokenFromDB(string siteName)
        {
            string accessToken = string.Empty;
            List<SqlParameter> dbParamCollection = new List<SqlParameter>();
            //sitename
            SqlParameter dbparamSitename = new SqlParameter("@sitename", SqlDbType.VarChar, 10);
            dbparamSitename.Value = siteName;
            dbParamCollection.Add(dbparamSitename);
            try
            {
                DBHandler dbHandler = new DBHandler(1);
                Object objResult = dbHandler.ExecuteScalar(Constants.SP_ACCESSTOKEN_SELECT, dbParamCollection);
                if (objResult != null)
                {
                    accessToken = objResult.ToString();
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }
            return accessToken;
        }

        /// <summary>
        /// insert record in AccessToken table
        /// </summary>
        /// <param name="yelpData"></param>
        /// <param name="term"></param>
        /// <param name="location"></param>
        /// <param name="searchId"></param>
        /// <returns></returns>
        public static bool InsertAccessToken(string accessToken, int days, string sitename)
        {
            bool bReturn = false;
            try
            {
                List<SqlParameter> dbParamCollection = new List<SqlParameter>();
                //accessToken
                SqlParameter dbparamAccessToken = new SqlParameter("@accessToken", SqlDbType.VarChar, 200);
                dbparamAccessToken.Value = accessToken;
                dbParamCollection.Add(dbparamAccessToken);

                //searchId
                SqlParameter dbparamDays = new SqlParameter("@days", SqlDbType.Int);
                dbparamDays.Value = days;
                dbParamCollection.Add(dbparamDays);

                //sitename
                SqlParameter dbparamSitename = new SqlParameter("@sitename", SqlDbType.VarChar, 10);
                dbparamSitename.Value = sitename;
                dbParamCollection.Add(dbparamSitename);

                //[[ACCESSTOKEN_INSERT]]
                DBHandler dbHandler = new DBHandler(1);
                int rowsAffected = dbHandler.ExecuteNonQuery(Constants.SP_ACCESSTOKEN_INSERT, dbParamCollection);
                if (rowsAffected < 0)
                {
                    //errror in query execution
                    bReturn = false;
                }
                bReturn = true;
            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);

            }
            finally
            {
            }
            return bReturn;
        }


        
        /// <summary>
        /// Delete record in AccessToken table
        /// </summary>
        /// <returns></returns>
        public static bool DeleteAccessToken(string sitename)
        {
            bool bReturn = false;
            List<SqlParameter> dbParamCollection = new List<SqlParameter>();
            //sitename
            SqlParameter dbparamSitename = new SqlParameter("@sitename", SqlDbType.VarChar, 10);
            dbparamSitename.Value = sitename;
            dbParamCollection.Add(dbparamSitename);
            try
            {
                //[[ACCESSTOKEN_INSERT]]
                DBHandler dbHandler = new DBHandler(1);
                int rowsAffected = dbHandler.ExecuteNonQuery(Constants.SP_ACCESSTOKEN_DELETE, dbParamCollection);
                if (rowsAffected < 0)
                {
                    //errror in query execution
                    bReturn = false;
                }
                bReturn = true;
            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);

            }
            finally
            {
            }
            return bReturn;
        }

        /// <summary>
        /// Update search Status 
        /// </summary>
        /// <param name="searchresults"></param>
        public static int UpdateSearchStatus(int searchId, string strTerm, string strLocation, int searchStatus)
        {
            int rowsAffected = -1;
            DBHandler dbHandler = null;
            try
            {
                List<SqlParameter> dbParamCollection = new List<SqlParameter>();
                //searchId
                if (searchId > 0)
                {
                    SqlParameter dbparamSearchId = new SqlParameter("@SearchId", SqlDbType.Int);
                    dbparamSearchId.Value = searchId;
                    dbParamCollection.Add(dbparamSearchId);
                }

                //Term
                SqlParameter dbparamTerm = new SqlParameter("@Term", SqlDbType.VarChar, 50);
                dbparamTerm.Value = strTerm;
                dbParamCollection.Add(dbparamTerm);

                //Location
                SqlParameter dbparamLocation = new SqlParameter("@Location", SqlDbType.VarChar, 50);
                dbparamLocation.Value = strLocation;
                dbParamCollection.Add(dbparamLocation);

                //SearchFinished
                SqlParameter dbparamSearchFinished = new SqlParameter("@SearchFinished", SqlDbType.Int);
                dbparamSearchFinished.Value = searchStatus;
                dbParamCollection.Add(dbparamSearchFinished);

                dbHandler = new DBHandler(1);
                rowsAffected = dbHandler.ExecuteNonQuery(Constants.SP_HouzzMAINSEARCH_SEARCHFINISHED_UPDATE, dbParamCollection);
                if (rowsAffected < 0)
                {
                    //errror in query execution
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write("UpdateSearchStatus" + ex);
            }
            return rowsAffected;
        }
    }
}