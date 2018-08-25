using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using AppHelper;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessRepute
{
    public class Utility
    {
        public static string GetImageFolder()
        {
            string serverFolderPath = string.Empty;
            string imageFolderName = string.Empty;
            
            try
            {
                serverFolderPath = ConfigurationManager.AppSettings["ServerPath"];

            }
            catch (Exception ex)
            {
                serverFolderPath = "";
                YelpTrace.Write(ex);
            }

            try
            {
                imageFolderName = ConfigurationManager.AppSettings["ImageFolder"];

            }
            catch (Exception ex)
            {
                serverFolderPath = "ClientImages";
                YelpTrace.Write(ex);
            }
            return serverFolderPath + imageFolderName;
        }


        public static string GetXMLEmailTemplate()
        {
            string xmlDataFolder = string.Empty;
            string emailTeampletFileName = string.Empty;

            try
            {
                xmlDataFolder = ConfigurationManager.AppSettings["XMLDATAFOLDERNAME"];

            }
            catch (Exception ex)
            {
                xmlDataFolder = "";
                YelpTrace.Write(ex);
            }

            try
            {
                emailTeampletFileName = ConfigurationManager.AppSettings["XML_EMAIL_TEMPLATE_FILE"];

            }
            catch (Exception ex)
            {
                emailTeampletFileName = "EmailTemplates.xml";
                YelpTrace.Write(ex);
            }
            return xmlDataFolder + emailTeampletFileName;
        }

        public static DataTable GetPatientDetails(int npatientId)
        {
            List<SqlParameter> dbParamCollection = new List<SqlParameter>();
            DataTable searchDataTable = null;
            DBHandler dbHandler = null;
            try
            {
                //Userid
                SqlParameter dbparamPatientID = new SqlParameter("@PatientId", System.Data.SqlDbType.Int);
                dbparamPatientID.Value = npatientId;
                dbParamCollection.Add(dbparamPatientID);

                //USERDETAILS_SELECT
                dbHandler = new DBHandler(1);
                searchDataTable = dbHandler.ExecuteReaderinTable(Constants.SP_PATIENT_TREATMENT_S, dbParamCollection);

            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }

            return searchDataTable;
        }
        public static DataTable GetBusinessDetails(int userId)
        {
            List<SqlParameter> dbParamCollection = new List<SqlParameter>();
            DataTable searchDataTable = null;
            DBHandler dbHandler = null;
            try
            {
                //Userid
                SqlParameter dbparamUserID = new SqlParameter("@UserId", System.Data.SqlDbType.Int);
                dbparamUserID.Value = userId;
                dbParamCollection.Add(dbparamUserID);

                //USERDETAILS_SELECT
                dbHandler = new DBHandler(1);
                searchDataTable = dbHandler.ExecuteReaderinTable(Constants.SP_BUSINESS_DETAILS_S, dbParamCollection);

            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }

            return searchDataTable;
        }

        public static DataTable GetBusinessSettings(int businessId)
        {
            List<SqlParameter> dbParamCollection = new List<SqlParameter>();
            DataTable searchDataTable = null;
            DBHandler dbHandler = null;
            try
            {
                //Userid
                SqlParameter dbparambusinessId = new SqlParameter("@businessDetailId", System.Data.SqlDbType.Int);
                dbparambusinessId.Value = businessId;
                dbParamCollection.Add(dbparambusinessId);

                //USERDETAILS_SELECT
                dbHandler = new DBHandler(1);
                searchDataTable = dbHandler.ExecuteReaderinTable(Constants.SP_BusinessSettings_S, dbParamCollection);

            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }

            return searchDataTable;
        }


        public static Int32 GetBusinessIdForUser(int userId, int businessRoleId)
        {
            List<SqlParameter> dbParamCollection = new List<SqlParameter>();
            string businessDetailId;
            int nbusinessDetailId =0;

            DBHandler dbHandler = null;
            try
            {
                //Userid
                SqlParameter dbparamUserID = new SqlParameter("@UserId", System.Data.SqlDbType.Int);
                dbparamUserID.Value = userId;
                dbParamCollection.Add(dbparamUserID);

                SqlParameter dbparamBusinessRoleId = new SqlParameter("@businessRoleId", System.Data.SqlDbType.Int);
                dbparamBusinessRoleId.Value = businessRoleId == 0 ? 1 : businessRoleId;  //pass admin role id if zero 
                dbParamCollection.Add(dbparamBusinessRoleId);

                //USERDETAILS_SELECT
                dbHandler = new DBHandler(1);
                Object objResult = dbHandler.ExecuteScalar(Constants.SP_BUSINESSID_FROM_USERID_S, dbParamCollection);
                if (objResult != null)
                {
                    businessDetailId = objResult.ToString();
                    Int32.TryParse(businessDetailId, out nbusinessDetailId);
                }

            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }

            return nbusinessDetailId;
        }

        public static int GetTTTypeId(string ttType)
        {
            List<SqlParameter> dbParamCollection = new List<SqlParameter>();
            int nttTypeId =-1;
            string strttTypeId = string.Empty;
            DBHandler dbHandler = null;
            try
            {
                //Userid
                SqlParameter dbparamttType = new SqlParameter("@ttType", System.Data.SqlDbType.VarChar,25);
                dbparamttType.Value = ttType;
                dbParamCollection.Add(dbparamttType);

               
                dbHandler = new DBHandler(1);
                Object objResult = dbHandler.ExecuteScalar(Constants.SP_GET_TREATMENTTYPEID, dbParamCollection);
                if (objResult != null)
                {
                    strttTypeId = objResult.ToString();
                    Int32.TryParse(strttTypeId, out nttTypeId);
                }

            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }
            //switch (ttType)
            //{
            //    case "Invisalign" :
            //        {
            //        TtTypeId=1;
            //        break;
            //        }
            //    case "Braces":
            //        {
            //            TtTypeId = 2;
            //            break;
            //        }
            //    case "Both":
            //        {
            //            TtTypeId = 3;
            //            break;
            //        }
            //    default:
            //        {
            //            TtTypeId = -1;
            //            break;
            //        }
            //}
            return nttTypeId;
        }


        public static string GetTTTypeName(short ttTypeid)
        {
            List<SqlParameter> dbParamCollection = new List<SqlParameter>();
            string treatementName= string.Empty;
            DBHandler dbHandler = null;
            try
            {
                //Userid
                SqlParameter dbparamttType = new SqlParameter("@ttType", System.Data.SqlDbType.SmallInt);
                dbparamttType.Value = ttTypeid;
                dbParamCollection.Add(dbparamttType);


                dbHandler = new DBHandler(1);
                Object objResult = dbHandler.ExecuteScalar(Constants.SP_GET_TREATMENTTYPE_NAME, dbParamCollection);
                if (objResult != null)
                {
                    treatementName = objResult.ToString();
                }

            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }

            return treatementName;
        }


        
        public static DataSet GetRoles()
        {
            DataSet resultSet = null;
            DBHandler dbHandler = null;
            try
            {
                List<SqlParameter> dbParamCollection = new List<SqlParameter>();

                
                //USERDETAILS_SELECT
                dbHandler = new DBHandler(1);
                resultSet = dbHandler.ExecuteReader(Constants.SP_BusinessRole_S, dbParamCollection);
                
            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }
            finally
            {
                //if (dbHelper != null)
                //    dbHelper.Close();
            }
            return resultSet;
        }

        public static DataTable GetUsers(int businessDetailId)
        {
            DataTable searchDataTable = null;
            try
            {
                List<SqlParameter> dbParamCollection = new List<SqlParameter>();
                
                DBHandler dbHandler = new DBHandler(1);
                SqlParameter dbparamBusinessDetailId = new SqlParameter("@BusinessDetailId", System.Data.SqlDbType.Int);
                dbparamBusinessDetailId.Value = businessDetailId;
                dbParamCollection.Add(dbparamBusinessDetailId);

                //USERDETAILS_SELECT
                searchDataTable = dbHandler.ExecuteReaderinTable(Constants.SP_BusinessUsers_S, dbParamCollection);
            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }
            return searchDataTable;
        }


        public static string GetPatientId(int npassedPatientId)
        {
            string userid = string.Empty;
            try
            {
                List<SqlParameter> dbParamCollection = new List<SqlParameter>();

                DBHandler dbHandler = new DBHandler(1);
                SqlParameter dbparamPatientId = new SqlParameter("@PatientId", System.Data.SqlDbType.Int);
                dbparamPatientId.Value = npassedPatientId;
                dbParamCollection.Add(dbparamPatientId);

                //USERDETAILS_SELECT
                Object objResult = dbHandler.ExecuteScalar(Constants.SP_GET_PATIENT_USERID, dbParamCollection);
                if (objResult != null)
                {
                    userid = objResult.ToString();
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }

            return userid;
        }

        public static bool IsDuplicateUserName(string userName)
        {
            bool bDuplicate = false;
            #region SQL Express
            DBHandler dbHandler = null;
            try
            {
                List<SqlParameter> dbParamCollection = new List<SqlParameter>();

                //username
                SqlParameter dbparamuserName = new SqlParameter("@User_UserName", System.Data.SqlDbType.VarChar, 25);
                dbparamuserName.Value = userName.Trim();
                dbParamCollection.Add(dbparamuserName);

                //USERDETAILS_SELECT
                dbHandler = new DBHandler(1);
                object result = dbHandler.ExecuteScalar(Constants.SP_CHECK_DUPLICATE_USERNAME, dbParamCollection);
                int userID = 0;
                if (result != null)
                {
                    Int32.TryParse(result.ToString(), out userID);
                    bDuplicate = true;
                }
            }
            catch (Exception ex)
            {
                bDuplicate = false;
                YelpTrace.Write(ex);
            }
            finally
            {
                //if (dbHelper != null)
                //    dbHelper.Close();
            }
            #endregion SQL Express
            return bDuplicate;
        }
    }
}