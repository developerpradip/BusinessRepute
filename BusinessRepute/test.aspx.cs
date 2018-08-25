using AppHelper;
using AppHelper.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace BusinessRepute
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlSQlBox.Visible = false;
            if (Page.IsPostBack)
            {
                if (hdnAuthenticated.Value == "1")
                {
                    pnlLoginBox.Visible = false;
                    pnlSQlBox.Visible = true;
                    hdnAuthenticated.Value = "1";
                }
                else
                {
                    pnlLoginBox.Visible = true;
                    pnlSQlBox.Visible = false;
                    hdnAuthenticated.Value = "0";
                }
            }

        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            ClearMessage();
            //send user creation Email
            SendUserRegistrationEmail("Pradip", "Pradip First", "developerpradip@gmail.com", "abcd");

        }
        private void GetPatients()
        {
            gridPatientList.Visible = false;
            #region SQLExpress
            try
            {
                DBHandler dbHandler = new DBHandler(1);
                DataTable dataTable = dbHandler.ExecuteReaderinTable(txtSQlQuery.Text.Trim());

                int recordsFound = 0;
                if (dataTable != null)
                {
                    recordsFound = dataTable.Rows.Count;
                    if (recordsFound > 0)
                    {
                        gridPatientList.DataSource = dataTable;

                        gridPatientList.DataBind();
                        gridPatientList.Visible = true;
                    }
                    else
                    {
                        DisplayMessage(MessageType.Success, "Zero (0) records found");
                    }
                }
                else
                {
                    DisplayMessage(MessageType.Error, "Error in SQL Query, please check");
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(MessageType.Exception, "Error in query execution" + ex);
                YelpTrace.Write(ex);
            }
            finally
            {

            }
            #endregion SQLExpress
        }
        private void SendUserRegistrationEmail(string strUserName, string strFirstName, string strEmailId, string strActivationCode)
        {
           
            //read the eamil template.xml file
            XMLHandler xmlHandler = new XMLHandler("EmailTemplates.xml");
            XmlNode xmlNode = xmlHandler.GetXMLNode("UserCreationEmail");
            string subject = xmlHandler.GetSubject(xmlNode);
            string messageBody = xmlHandler.GetMessageBody(xmlNode);
            string userActivationRawLink = string.Empty;
            try
            {
                userActivationRawLink = ConfigurationManager.AppSettings["BR_USER_ACCOUNT"];
            }
            catch (Exception ex)
            {
                userActivationRawLink = "https://www.BusinessRepute.xyz//UsersAccount.aspx&amp;type={0}&amp;code={1}";
                YelpTrace.Write(ex);
            }
            string userActivationLink = string.Format(userActivationRawLink, "activation", strActivationCode);

            messageBody = messageBody.Replace("USERNAME", strUserName);
            messageBody = messageBody.Replace("USERACTIVATIONLINK", userActivationLink);

            messageBody = "Hello " + strFirstName + "," + Environment.NewLine + messageBody;

            //get to Email Address
            try
            {

                Exception ex = null;
                if (Communication.SendMail(strEmailId, string.Empty, subject, messageBody, "Pradip", out ex))
                {
                    DisplayMessage(MessageType.Success, "Mail is sent Successfully");
                    YelpTrace.Write("User registration Email is sent successfully" + ex);
                }
                else
                {
                    DisplayMessage(MessageType.Success, "Issue in sending Email ");
                    YelpTrace.Write("sending user registration Email is failed " + ex);
                }

            }
            catch (Exception ex)
            {
                YelpTrace.Write("Exception in  user registration Email activation Email sending" + ex);
            }
            
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            ClearMessage();
            GetPatients();
        }

        protected void gridPatientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPatientList.PageIndex = e.NewPageIndex;
            gridPatientList.DataBind();
            GetPatients();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ClearMessage();
            if (txtUserName.Text.Trim().ToUpper() == "PRADIP" && txtPassword.Text.Trim() == "BMyLove")
            {
                pnlLoginBox.Visible = false;
                pnlSQlBox.Visible = true;
                hdnAuthenticated.Value = "1";
                DisplayMessage(MessageType.Success, "User is Authenticated successfully");
            }
            else
            {
                pnlLoginBox.Visible = true;
                pnlSQlBox.Visible = false;
                hdnAuthenticated.Value = "0";
                DisplayMessage(MessageType.Error, "User Id and password are not matching");
            }
        }
        private void DisplayMessage(MessageType messageType, string message)
        {
            switch (messageType)
            
            {
                case MessageType.Success:
                    divMessageDisplay.CssClass = "alert alert-success";
                    FailureText.Text = message;
                    break;
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