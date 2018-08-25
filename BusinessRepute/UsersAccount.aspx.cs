
using AppHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using YelpSearchAPI.Common;
//using YelpSearchAPI.Handlers;

namespace BusinessRepute
{
    public partial class UsersAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = string.Empty;
            string userCode = string.Empty;
            int userCodeType=-1;

            if (Request.QueryString["type"] != null)
            {
                type = Request.QueryString["type"].ToString();
            }

            if (Request.QueryString["code"] != null)
            {
                userCode = Request.QueryString["code"].ToString();
            }

            DBHandler dbHandler = null;

            if (type == "activation")
            {
                userCodeType = 1;

                try
                {
                    List<SqlParameter> dbParamCollection = new List<SqlParameter>();

                    //Term
                    SqlParameter dbparamuserCodeType = new SqlParameter("@userCodeType", SqlDbType.SmallInt);
                    dbparamuserCodeType.Value = userCodeType;
                    dbParamCollection.Add(dbparamuserCodeType);

                    //location
                    SqlParameter dbparamUserCode = new SqlParameter("@code", SqlDbType.VarChar, 50);
                    dbparamUserCode.Value = userCode;
                    dbParamCollection.Add(dbparamUserCode);

                    dbHandler = new DBHandler(1);
                    int rowsAffected = dbHandler.ExecuteNonQuery(Constants.SP_USERDETAILS_USER_ACTIONS, dbParamCollection);
                    if (rowsAffected == 1)
                    {
                        Response.Redirect("/login.aspx?errorMessage=User is activated, please login with your credentials.");
                    }
                    else
                    {
                        lblResponse.Text = "We are not able to find corresponding user for the activation code";
                    }
                }
                catch (Exception ex)
                {
                    YelpTrace.Write(ex);
                }
            }
            else if (type == "reset")
            {
                userCodeType = 2;
            }
        }
    }
}