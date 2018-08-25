using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;
using System.Data.SqlClient;
using AppHelper;
using AppHelper.Common;


namespace BusinessRepute
{
    public partial class ChangePassword : Page
    {
        private string userId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERID"] != null)
            {
                userId = (string)Session["USERID"];
            }
            if (string.IsNullOrEmpty(userId))
            {
                string userName = Context.User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    Server.Transfer("~/login.aspx", false);
                }
                else
                {
                    userId = Helper.GetUserId(userName);
                }
            }

            if (Request.QueryString["errorMessage"] != null)
            {
                DisplayMessage(MessageType.Information, Request.QueryString["errorMessage"]);
                FailureText.Visible = true;
            }
            else
            {
                FailureText.Visible = false;
            }
        }
        private bool ValidateUser()
        {
            if (string.IsNullOrEmpty(txtOldPassword.Text.Trim()))
                return false;
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
                return false;
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
                return false;
            return true;
        }
        
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (ValidateUser())
            {

                #region SQL Express
                DBHandler dbHandler = null;
                try
                {
                    List<SqlParameter> dbParamCollection = new List<SqlParameter>();

                    //username
                    SqlParameter dbparamuserId = new SqlParameter("@userId", System.Data.SqlDbType.Int);
                    dbparamuserId.Value = userId;
                    dbParamCollection.Add(dbparamuserId);

                    //Oldpassword
                    SqlParameter dbparamOldPassword = new SqlParameter("@OldPassword", System.Data.SqlDbType.VarChar, 25);
                    dbparamOldPassword.Value = txtOldPassword.Text.Trim();
                    dbParamCollection.Add(dbparamOldPassword);

                    //Newpassword
                    SqlParameter dbparamNewPassword = new SqlParameter("@NewPassword", System.Data.SqlDbType.VarChar, 25);
                    dbparamNewPassword.Value = txtNewPassword.Text.Trim();
                    dbParamCollection.Add(dbparamNewPassword);
                    
                    //USERDETAILS_SELECT
                    dbHandler = new DBHandler(1);
                    int nResult = dbHandler.ExecuteNonQuery(Constants.SP_USERDETAILS_CHANGE_PASSWORD, dbParamCollection);

                    if (nResult > 0)
                    {
                        Response.Redirect("/ChangePassword.aspx?errorMessage=Password is changed successfully..");
                    }
                    else 
                    {
                        Response.Redirect("/ChangePassword.aspx?errorMessage=Old password is not correct, password is not changed.");
                    }
                }
                catch (Exception ex)
                {
                    // Add error handling here for debugging.
                    // This error message should not be sent back to the caller.
                    YelpTrace.Write("[ValidateUser] Exception " + ex.Message);
                    YelpTrace.Write(ex);
                    DisplayMessage(MessageType.Success, "Error in change password");
                }
                finally
                {
                    //if (dbHelper != null)
                    //    dbHelper.Close();
                }
                #endregion SQL Express
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtOldPassword.Text = String.Empty;
            txtNewPassword.Text = String.Empty;
            txtConfirmPassword.Text = String.Empty;

        }

        private void DisplayMessage(MessageType messageType, string message)
        {
            switch (messageType)
            {
                case MessageType.Information:
                    divMessageDisplay.CssClass = "alert alert-success";
                    FailureText.Text = message;
                    break;
                case MessageType.Warning:
                    divMessageDisplay.CssClass = "alert alert-warning";

                    FailureText.Text = message;
                    break;
                case MessageType.Error:
                    FailureText.Text = message;
                    divMessageDisplay.CssClass = "alert alert-danger";

                    break;
                case MessageType.Exception:
                    FailureText.Text = message;
                    divMessageDisplay.CssClass = "alert alert-danger";

                    break;
                default:
                    divMessageDisplay.CssClass = "alert alert-light";
                    FailureText.Text = message;
                    break;

            }

        }

        private void ClearMessage()
        {
            FailureText.Text = string.Empty;
        }
    }
}