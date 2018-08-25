using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
//using DALC4NET;
//using YelpSearchAPI.Handlers;
using System.Drawing;
using System.Data.SqlClient;
//using YelpSearchAPI.Common;
using System.Data;
using System.Xml;
using System.Configuration;
//using HouzzHelper;
//using HouzzHelper.Common;
using System.Configuration;
using AppHelper.Common;
using AppHelper;


namespace BusinessRepute
{
    public partial class ResetPassword : Page
    {
        private int pageMode =-1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["errorMessage"] != null)
            {
                string msg = Request.QueryString["errorMessage"];
                FailureText.Visible = true;
                DisplayMessage(MessageType.Information, msg);
            }
            else
            {
                FailureText.Visible = false;
            }

            if (Request.QueryString["pageMode"] != null)
            {
                string temp = Request.QueryString["pageMode"];
                if (temp == "0")
                {
                    pnlEmail.Visible = true;
                    pnlResetPassword.Visible = false;
                }
                else if (temp == "1")
                {
                    pnlEmail.Visible = false;
                    pnlResetPassword.Visible = true;
                }
                else
                {
                    pnlEmail.Visible = true;
                    pnlResetPassword.Visible = false;
                }
            }
            else
            {
                pnlEmail.Visible = true;
                pnlResetPassword.Visible = false;
            }
            
        }
        private bool ValidatePassword()
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
                return false;
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
                return false;
            return true;
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            #region SQL Express

            DBHandler dbHandler = null;
            try
            {
                List<SqlParameter> dbParamCollection = new List<SqlParameter>();

                //username
                SqlParameter dbparamEmailId = new SqlParameter("@EmailId", SqlDbType.VarChar, 50);
                dbparamEmailId.Value = txtEmail.Text.Trim().ToLower();
                dbParamCollection.Add(dbparamEmailId);

                //ActivationCode
                string strActivationCode = Guid.NewGuid().ToString();
                SqlParameter dbparamActivationCode = new SqlParameter("@ActivationCode", SqlDbType.VarChar, 50);
                dbparamActivationCode.Value = strActivationCode;
                dbParamCollection.Add(dbparamActivationCode);

                //USERDETAILS_SELECT
                dbHandler = new DBHandler(1);
                int nResult = dbHandler.ExecuteNonQuery(Constants.SP_USERDETAILS_ACTIVATION_INSERT_RESET, dbParamCollection);

                if (nResult > 0)
                {
                    SendUserResetPasswordEmail(strActivationCode, txtEmail.Text.Trim());
                    Response.Redirect("/ResetPassword.aspx?pageMode=0&errorMessage=Email with instructions to Reset password is sent.");
                }
                else
                {
                    Response.Redirect("/ResetPassword.aspx??pageMode=0&errorMessage=Email Id is not registered with us.");
                }
            }
            catch (Exception ex)
            {
                // Add error handling here for debugging.
                // This error message should not be sent back to the caller.
                YelpTrace.Write("[btnChangePassword_Click] Exception " + ex.Message);
                YelpTrace.Write(ex);
                FailureText.Text = "Error in Reset password";
            }
            finally
            {
                //if (dbHelper != null)
                //    dbHelper.Close();
            }
            
            #endregion SQL Express
        }

        private void SendUserResetPasswordEmail(string strActivationCode, string strEmailID)
        {
            string XMLTemplatepath = ConfigurationManager.AppSettings["XML_EMAIL_TEMPLATE_FILE"];
            //read the eamil template.xml file
            XMLHandler xmlHandler = new XMLHandler(XMLTemplatepath);
            XmlNode xmlNode = xmlHandler.GetXMLNode("ResetPasswordEmail");
            string subject = xmlHandler.GetSubject(xmlNode);
            string messageBody = xmlHandler.GetMessageBody(xmlNode);
            string passwordResetRawLink = string.Empty;
            try
            {
                passwordResetRawLink = ConfigurationManager.AppSettings["BR_RESET_PASSWORD"];
            }
            catch (Exception ex)
            {
                passwordResetRawLink = "https://www.BusinessRepute.xyz/ResetPassword.aspx&amp;pageMode={0}&amp;code={1}";
                YelpTrace.Write(ex);
            }
            string passwordResetLink = string.Format(passwordResetRawLink, "1", strActivationCode);
            messageBody = messageBody.Replace("RESETPASSWORDLINK", passwordResetLink);

            //get to Email Address
            try
            {
                Exception ex = null;
                if (Communication.SendMail(strEmailID, string.Empty, subject, messageBody, lblClientName.Text,  out ex) == true)
                {
                    DisplayMessage(MessageType.Success, "Instructions to Reset password is sent in registered Email.");
                    YelpTrace.Write("Instructions to Reset password is sent in registered Email.");
                }
                else
                {
                    DisplayMessage(MessageType.Success, "Error in sending Email Reset password instruction, please retry.");
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write("Exception in sending reset password Email" + ex);
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtNewPassword.Text = String.Empty;
            txtConfirmPassword.Text = String.Empty;
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string tempCode = string.Empty;
            if (Request.QueryString["code"] != null)
            {
                tempCode = Request.QueryString["code"];
            }

            if (!string.IsNullOrEmpty(tempCode))
            {
                if (ValidatePassword())
                {
                    #region SQL Express
                    DBHandler dbHandler = null;
                    try
                    {
                        List<SqlParameter> dbParamCollection = new List<SqlParameter>();

                        //code
                        SqlParameter dbparamActivationCode = new SqlParameter("@ActivationCode", System.Data.SqlDbType.VarChar, 50);
                        dbparamActivationCode.Value = tempCode;
                        dbParamCollection.Add(dbparamActivationCode);

                        //code
                        SqlParameter dbparamUserID = new SqlParameter("@userId", System.Data.SqlDbType.Int);
                        dbparamUserID.Value = 0;
                        dbParamCollection.Add(dbparamUserID);

                        //Newpassword
                        SqlParameter dbparamNewPassword = new SqlParameter("@NewPassword", System.Data.SqlDbType.VarChar, 25);
                        dbparamNewPassword.Value = txtNewPassword.Text.Trim();
                        dbParamCollection.Add(dbparamNewPassword);

                        //USERDETAILS_SELECT
                        dbHandler = new DBHandler(1);
                        int nResult = dbHandler.ExecuteNonQuery(Constants.SP_USERDETAILS_CHANGE_PASSWORD, dbParamCollection);

                        if (nResult > 0)
                        {
                            Response.Redirect("/ResetPassword.aspx?pageMode=1&errorMessage=Password is changed successfully..");
                        }
                        else
                        {
                            Response.Redirect("/ResetPassword.aspx??pageMode=1&errorMessage=Error in resetting the password, password is not changed.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Add error handling here for debugging.
                        // This error message should not be sent back to the caller.
                        YelpTrace.Write("[ValidateUser] Exception " + ex.Message);
                        YelpTrace.Write(ex);
                        DisplayMessage(MessageType.Exception,"Error in change password");
                    }
                    finally
                    {
                        //if (dbHelper != null)
                        //    dbHelper.Close();
                    }
                }
                #endregion SQL Express
            }
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