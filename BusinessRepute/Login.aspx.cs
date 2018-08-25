using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
//using YelpSearchAPI.Common;
using log4net;
using AppHelper;
using AppHelper.Common;
using System.Security.Principal;
using System.Data;


namespace BusinessRepute
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string message =string.Empty;
            if (Request.QueryString["mode"] != null)
            {
                if (string.Compare(Request.QueryString["mode"].ToString(), "LOGOUT", true) == 0)
                {
                    Session["USERID"] = string.Empty;
                    //Context.User.Identity.Name= string.Empty;

                    Session.Abandon();
                    Session.Clear();
                    Session.RemoveAll();
                    FormsAuthentication.SignOut();
                    HttpContext.Current.User =
                        new GenericPrincipal(new GenericIdentity(string.Empty), null);

                    Response.AppendHeader("Refresh", "5;url=home.aspx");
                }
            }

            if (Request.QueryString["errorMessage"] != null)
            {
                message = Request.QueryString["errorMessage"].ToString();
                DisplayMessage(MessageType.Information, message);
            }
        }

        private bool ValidateUser(string userName, string passWord)
        {
            bool bReturn = false;
            if (string.IsNullOrEmpty(userName))
                bReturn = false;
            if(string.IsNullOrEmpty(passWord))
                bReturn = false;

            #region SQL Express
            DBHandler dbHandler = null;
            try
            {
                List<SqlParameter> dbParamCollection = new List<SqlParameter>();
                
                //username
                SqlParameter dbparamuserName = new SqlParameter("@User_UserName", System.Data.SqlDbType.VarChar, 25);
                dbparamuserName.Value = userName.Trim();
                dbParamCollection.Add(dbparamuserName);

                //password
                SqlParameter dbparamPassword = new SqlParameter("@Password", System.Data.SqlDbType.VarChar, 25);
                dbparamPassword.Value =passWord.Trim();
                dbParamCollection.Add(dbparamPassword);

                //USERDETAILS_SELECT
                dbHandler = new DBHandler(1);
                DataTable dtResultTable = dbHandler.ExecuteReaderinTable(Constants.SP_USERDETAILS_SELECT, dbParamCollection);
                if (dtResultTable != null)
                {
                    if (dtResultTable.Rows.Count > 0)
                    {
                        Session["USERID"] = dtResultTable.Rows[0]["Userid"].ToString();
                        Session["BUSINESSID"] = dtResultTable.Rows[0]["BusinessDetailId"].ToString();
                        Session["ROLEID"] = dtResultTable.Rows[0]["BusinessRoleId"].ToString();
                        Session["USERNAME"] = (string)dtResultTable.Rows[0]["FirstName"];

                        //ErrorMessage.Visible = false;
                        bReturn = true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                // Add error handling here for debugging.
                // This error message should not be sent back to the caller.
                YelpTrace.Write("[ValidateUser] Exception " + ex.Message);
                bReturn = false;
            }
            finally
            {
                //if (dbHelper != null)
                //    dbHelper.Close();
            }
            #endregion SQL Express
            return bReturn;
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //Login.FailureAction = LoginFailureAction.RedirectToLoginPage;
                string userName = txtUssername.Text.Trim();
                string password = txtPassword.Text.Trim();
                bool rememberMe = chkRememberMe.Checked;
                if (ValidateUser(userName, password))
                {
                    

                    FormsAuthentication.SetAuthCookie(userName , chkRememberMe.Checked);
                    if (chkRememberMe.Checked)
                    {
                        HttpCookie cookie = new HttpCookie("BusinessRepute");
                        cookie.Values.Add("username", txtUssername.Text);
                        cookie.Expires = DateTime.Now.AddDays(15);
                        Response.Cookies.Add(cookie);
                    }
                    string webpath = HttpContext.Current.Request.Url.AbsoluteUri;
                    string webFolderPath = webpath.Substring(0, webpath.LastIndexOf("/"));
                    webFolderPath = webFolderPath.Substring(0, webFolderPath.LastIndexOf("/"));

                    //CtrlLogin.DestinationPageUrl = webFolderPath + "/Home.aspx";
                    //CtrlLogin.PasswordRecoveryUrl = "manage.aspx";
                    YelpTrace.Write(webFolderPath + "Main.aspx");
                    //Response.Redirect(webFolderPath + "QuoteCalc.aspx");
                    Response.Redirect("Main.aspx");
                    //Server.Transfer("Home.aspx");
                }
                else
                {

                    Session["USERID"] = string.Empty;
                    Session["USERNAME"] = string.Empty;
                    //Response.Redirect("/login.aspx?errorMessage=User is not authenticated, please provide correct username and password.");
                    DisplayMessage(MessageType.Error, "User is not authenticated, please provide correct username and password");
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write("exception in btnLogin_Click " +ex );
            }
        }

        protected void Login_LoginError(object sender, EventArgs e)
        {
            //CtrlLogin.FailureText = " User is not authenticated, please provide correct username and password.";
            //YelpTrace.Write(CtrlLogin.FailureText);
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

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {

        }
        
    }
}