using AppHelper;
using AppHelper.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace BusinessRepute
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lstPurpose.Items.Add(new ListItem("Select Purpose", "0"));
                lstPurpose.Items.Add(new ListItem("Request for Demo", "1"));
                lstPurpose.Items.Add(new ListItem("Raise a complaint", "2"));
                lstPurpose.Items.Add(new ListItem("Ask for a Feature", "3"));
                lstPurpose.Items.Add(new ListItem("Just to Say Hi", "4"));
                lstPurpose.Items.Add(new ListItem("Contact", "5"));
                lstPurpose.Items.Add(new ListItem("Advertisement Request", "6"));
                lstPurpose.Items.Add(new ListItem("Feedback", "7"));
                lstPurpose.Items.Add(new ListItem("Other", "8"));
            }
        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            //store the details in database and send a mail to client as well as to Admin
            string name = txtProspectName.Text.Trim();
            string email = txtProspectEmail.Text.Trim();
            string subject = txtMessageSubject.Text.Trim();
            string message = txtMessage.Text.Trim();
            string selectedPupose = lstPurpose.SelectedItem.Text;

            DBHandler dbHandler = null;
            #region SQLExpress
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                //Name
                SqlParameter dbparamName = new SqlParameter("@Name", SqlDbType.VarChar, 50);
                dbparamName.Value = name;
                sqlParameters.Add(dbparamName);

                //Email
                SqlParameter dbparamEmail = new SqlParameter("@Email", SqlDbType.VarChar, 50);
                dbparamEmail.Value = email;
                sqlParameters.Add(dbparamEmail);

                //subject
                SqlParameter dbparamSubject = new SqlParameter("@subject", SqlDbType.VarChar, 50);
                dbparamSubject.Value = subject;
                sqlParameters.Add(dbparamSubject);

                //Message
                SqlParameter dbparamMessage = new SqlParameter("@message", SqlDbType.VarChar, 2000);
                dbparamMessage.Value = message;
                sqlParameters.Add(dbparamMessage);

                //Purpose
                SqlParameter dbparamPurpose = new SqlParameter("@purpose", SqlDbType.VarChar, 100);
                dbparamPurpose.Value = selectedPupose;
                sqlParameters.Add(dbparamPurpose);

                string response = string.Empty;
                dbHandler = new DBHandler(1);
                int result = dbHandler.ExecuteNonQuery(Constants.SP_FeedbackDetails_I, sqlParameters);

                DisplayMessage(MessageType.Information, string.Format(" Thank you for contacting us, we will get back to you immediately."));

                //send Email to Admin
                try
                {

                    Exception ex = null;
                    if (Communication.SendMail("Admin@BusinessRepute.xyz", string.Empty, subject, message, "Business Reputation", out ex) == false)
                    {
                        YelpTrace.Write("sending Email to Admin is failed " + ex);
                    }
                }
                catch (Exception ex)
                {
                    YelpTrace.Write("Exception in sending Email to Admin for feedback " + ex);
                }

                //send thank you Email to user
                try
                {
                    //read the eamil template.xml file
                    XMLHandler xmlHandler = new XMLHandler("EmailTemplates.xml");
                    XmlNode xmlNode = xmlHandler.GetXMLNode("ThankForFeedback");
                    string msgSubject = xmlHandler.GetSubject(xmlNode);
                    string messageBody = xmlHandler.GetMessageBody(xmlNode);
                    

                    messageBody = "Hello " + name+ "," + Environment.NewLine + messageBody;

                    Exception ex = null;
                    if (Communication.SendMail(email, string.Empty, msgSubject, messageBody, "Business Reputation", out ex) == false)
                    {
                        YelpTrace.Write("sending Email to user for feedback is failed " + ex);
                    }
                }
                catch (Exception ex)
                {
                    YelpTrace.Write("Exception in sending Email to user for feedback " + ex);
                }

                

                //Send Email to Client.
                ClearFields();
            }
            catch (Exception ex)
            {
                DisplayMessage(MessageType.Exception, ex.Message);
                YelpTrace.Write(ex);
            }
            finally
            {
                //dbHandler.Close();
            }
            #endregion MSAccess
        }
            
        
        private void ClearFields()
        {
            txtProspectName.Text = string.Empty;
            txtProspectEmail.Text = string.Empty;
            txtMessageSubject.Text = string.Empty;
            txtMessage.Text = string.Empty;
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
