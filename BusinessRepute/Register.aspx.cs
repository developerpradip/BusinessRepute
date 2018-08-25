using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Microsoft.AspNet.Membership.OpenAuth;
//using YelpSearchAPI.Common;
//using YelpSearchAPI.Handlers;
//using DALC4NET;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Configuration;
using AppHelper.Common;
using AppHelper;
using System.IO;

namespace BusinessRepute
{
    public partial class Register : Page
    {
        private int nPageMode = 0;  //0=> add, 1 for Edit
        private string userId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if ((Session["USERID"] == null) || (Session["USERID"] == ""))
            {
                //FailureText.Text = " Context.User.Identity.Name " + Context.User.Identity.Name + " Session[USERID] " + Session["USERID"];
                string userName = Context.User.Identity.Name;
                if (!string.IsNullOrEmpty(userName))
                {
                    userId = AppHelper.Common.Helper.GetUserId(userName);
                    Session["USERID"] = userId;
                }
            }
            else
            {
                userId = Session["USERID"].ToString();
            }
            if (!string.IsNullOrEmpty(userId))
            {
                //get business details from database and populate fields
                nPageMode = 1;
                
            }
            

            if (!Page.IsPostBack)
            {
                PopulateBusinessTypes();
                if (nPageMode == 1)
                {
                    GetBusinessInfo(userId);
                    HideShowControl();
                }
            }
            
            //RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        private void PopulateBusinessTypes()
        {
            cmbBusinessType.Items.Clear();
            DBHandler dbHandler = null;
            try
            {
                List<SqlParameter> dbParamCollection = new List<SqlParameter>();

                //USERDETAILS_SELECT
                dbHandler = new DBHandler(1);
                DataTable dataTable = dbHandler.ExecuteReaderinTable(Constants.SP_BusinessType_S, dbParamCollection);
                if (dataTable != null)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        cmbBusinessType.Items.Add(new ListItem(dataRow["BusinessType"].ToString(), dataRow["BusinessTypeId"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write("Exception in PopulateBusinessTypes " + ex);
            }
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            //FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);

            ////string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            //if (!OpenAuth.IsLocalUrl(continueUrl))
            //{
            //    continueUrl = "~/";
            //}
            //Response.Redirect(continueUrl);
        }

        protected void RegisterUser_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            //string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            //Response.Redirect(continueUrl);
        }

        protected void RegisterUser_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            //string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            //Response.Redirect(continueUrl);
        }

        protected void RegisterUser_CreatingUser(object sender, LoginCancelEventArgs e)
        {
            //string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            //Response.Redirect(continueUrl);
        }

       
        private bool ValidatePage()
        {
            bool bReturn = true;
            
            //validation for required fields
            if (string.IsNullOrEmpty(txtBusinessName.Text))
            {
                DisplayMessage(MessageType.Error, Constants.BLANK_BUSINESS_NAME);
                bReturn = false;
                return bReturn;
            }

            if (nPageMode == 0)
            {
                if ((txtUserName.Visible == true) && string.IsNullOrEmpty(txtUserName.Text))
                {
                    DisplayMessage(MessageType.Error, Constants.BLANK_USER_NAME);
                    bReturn = false;
                    return bReturn;
                }
                if ((txtPassword.Visible == true) && string.IsNullOrEmpty(txtPassword.Text))
                {
                    DisplayMessage(MessageType.Error, Constants.BLANK_PASSWORD);
                    bReturn = false;
                    return bReturn;
                }
                if ((txtConfirmPassword.Visible == true) && string.IsNullOrEmpty(txtConfirmPassword.Text))
                {
                    DisplayMessage(MessageType.Error, Constants.BLANK_CONFIRM_PASSWORD);
                    bReturn = false;
                    return bReturn;
                }
                if ((txtPassword.Visible == true) && !txtConfirmPassword.Text.Equals(txtPassword.Text))
                {
                    DisplayMessage(MessageType.Error, Constants.PASSWORDS_NOT_SAME);
                    bReturn = false;
                    return bReturn;
                }
                if ((txtUserName.Visible == true) && IsDuplicateUserName(txtUserName.Text.Trim()))
                {
                    DisplayMessage(MessageType.Error, Constants.DUPLICATE_USERNAME);
                    bReturn = false;
                }
                if (string.IsNullOrEmpty(txtSecurityQue1.Text))
                {
                    DisplayMessage(MessageType.Error, Constants.BLANK_SECURITY_QUE_1);
                    bReturn = false;
                    return bReturn;
                }
                if (string.IsNullOrEmpty(txtSecurityAns1.Text))
                {
                    DisplayMessage(MessageType.Error, Constants.BLANK_SECURITY_ANS_1);
                    bReturn = false;
                    return bReturn;
                }
                if (string.IsNullOrEmpty(txtSecurityQue2.Text))
                {
                    DisplayMessage(MessageType.Error, Constants.BLANK_SECURITY_QUE_2);
                    bReturn = false;
                    return bReturn;
                }
                if (string.IsNullOrEmpty(txtSecurityAns2.Text))
                {
                    DisplayMessage(MessageType.Error, Constants.BLANK_SECURITY_ANS_2);
                    bReturn = false;
                    return bReturn;
                }
            }
            if (string.IsNullOrEmpty(txtBusinessEmail.Text))
            {
                DisplayMessage(MessageType.Error, Constants.BLANK_EMAIL);
                bReturn = false;
                return bReturn;
            }

            //if (string.IsNullOrEmpty(txtBussOwnerFirstname.Text))
            //{
            //    DisplayMessage(MessageType.Error, Constants.BLANK_BUSS_FIRST_NAME);
            //    bReturn = false;
            //    return bReturn;
            //}

            


            //if (cmbRoleType.SelectedIndex == 0)
            //{
            //    DisplayMessage(MessageType.Error, "Please select Role for the user");
            //    bReturn = false;
            //    return bReturn;
            //}
            //validation for duplicate check

            
            //validation for 
            return bReturn;
        }

        private bool IsDuplicateUserName(string userName)
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
                    //Session["USERID"] = userID.ToString();
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

        private void SaveDetails()
        {
            DBHandler dbHandler = null;
            string message = string.Empty;
            int result = -1;
            #region SQLExpress
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                //Business Details
                //Business Name
                SqlParameter dbparamBussName = new SqlParameter("@BussName", SqlDbType.VarChar, 50);
                dbparamBussName.Value = txtBusinessName.Text.Trim();
                sqlParameters.Add(dbparamBussName);

                //Business Address1
                SqlParameter dbparamBussAddr1 = new SqlParameter("@BussAddress1", SqlDbType.VarChar, 100);
                dbparamBussAddr1.Value = txtBusinessAddress1.Text.Trim();
                sqlParameters.Add(dbparamBussAddr1);

                //Business Address2
                SqlParameter dbparamBussAddr2 = new SqlParameter("@BussAddress2", SqlDbType.VarChar, 100);
                dbparamBussAddr2.Value = txtBusinessAddress2.Text.Trim();
                sqlParameters.Add(dbparamBussAddr2);

                //Email
                SqlParameter dbparamBussEmail = new SqlParameter("@BussEmailId", SqlDbType.VarChar, 50);
                dbparamBussEmail.Value = txtBusinessEmail.Text.Trim().ToLower();
                sqlParameters.Add(dbparamBussEmail);

                //Phone number
                SqlParameter dbparamBussPhoneNumber = new SqlParameter("@BussPhoneNumber", SqlDbType.VarChar, 25);
                dbparamBussPhoneNumber.Value = txtBusinessPhone.Text.Trim();
                sqlParameters.Add(dbparamBussPhoneNumber);

                //Fax number
                SqlParameter dbparamBussFaxNumber = new SqlParameter("@BussFaxNumber", SqlDbType.VarChar, 25);
                dbparamBussFaxNumber.Value = string.Empty;
                sqlParameters.Add(dbparamBussFaxNumber);

                //City
                SqlParameter dbparamBussCity = new SqlParameter("@BussCity", SqlDbType.VarChar, 25);
                dbparamBussCity.Value = txtBussCity.Text.Trim();
                sqlParameters.Add(dbparamBussCity);

                //State
                SqlParameter dbparamBussState = new SqlParameter("@BussState", SqlDbType.VarChar, 25);
                dbparamBussState.Value = txtBussState.Text.Trim();
                sqlParameters.Add(dbparamBussState);

                //ZipCode
                SqlParameter dbparamBussZipCode = new SqlParameter("@BussZipCode", SqlDbType.VarChar, 10);
                dbparamBussZipCode.Value = txtBusinessZip.Text.Trim();
                sqlParameters.Add(dbparamBussZipCode);

                //BusinessTypeId
                SqlParameter dbparamBussTypeId = new SqlParameter("@BusinessTypeId", SqlDbType.SmallInt);
                dbparamBussTypeId.Value = cmbBusinessType.SelectedItem.Value;
                sqlParameters.Add(dbparamBussTypeId);

                //mode
                SqlParameter dbparamMode = new SqlParameter("@Mode", SqlDbType.Int);
                dbparamMode.Value = nPageMode;
                sqlParameters.Add(dbparamMode);


                if (nPageMode == 0)
                {
                    //username
                    SqlParameter dbparamUserName = new SqlParameter("@User_userName", SqlDbType.VarChar, 25);
                    dbparamUserName.Value = (txtUserName.Visible == true) ? txtUserName.Text.Trim() :null ;
                    sqlParameters.Add(dbparamUserName);

                    //password
                    SqlParameter dbparamPassword = new SqlParameter("@password", SqlDbType.VarChar, 25);
                    dbparamPassword.Value = (txtPassword.Visible == false) ? null : txtPassword.Text.Trim();
                    sqlParameters.Add(dbparamPassword);

                    //First Name
                    SqlParameter dbparamFirstName = new SqlParameter("@FirstName", SqlDbType.VarChar, 25);
                    //dbparamFirstName.Value = txtBussOwnerFirstname.Text.Trim();
                    dbparamFirstName.Value = (txtUserName.Visible == false) ? null : txtUserName.Text.Trim();
                    sqlParameters.Add(dbparamFirstName);

                    //last Name
                    SqlParameter dbparamLastName = new SqlParameter("@LastName", SqlDbType.VarChar, 25);
                    //dbparamLastName.Value = txtBussOwnerLastname.Text.Trim();
                    dbparamLastName.Value = (txtUserName.Visible == false) ? null : txtUserName.Text.Trim();
                    sqlParameters.Add(dbparamLastName);

                    //Email
                    SqlParameter dbparamEmail = new SqlParameter("@EmailId", SqlDbType.VarChar, 50);
                    dbparamEmail.Value = txtBusinessEmail.Text.Trim();
                    sqlParameters.Add(dbparamEmail);

                    //Phone number
                    SqlParameter dbparamPhoneNumber = new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 25);
                    dbparamPhoneNumber.Value = txtBusinessPhone.Text.Trim();
                    sqlParameters.Add(dbparamPhoneNumber);


                    //Business Logo Name
                    SqlParameter dbparamBussLogoName = new SqlParameter("@BussLogoName", SqlDbType.VarChar, 25);
                    dbparamBussLogoName.Value = (flupldClientLogo.Visible == false) ? null : flupldClientLogo.FileName.Trim();
                    sqlParameters.Add(dbparamBussLogoName);

                    //Business Logo file Name
                    SqlParameter dbparamBussLogoFileName = new SqlParameter("@BussLogoFileName", SqlDbType.VarChar, 55);
                    dbparamBussLogoFileName.Value = (flupldClientLogo.Visible == false) ? null : flupldClientLogo.FileName.Trim();
                    sqlParameters.Add(dbparamBussLogoFileName);

                    //Security Q1
                    SqlParameter dbparamSecurityQ = new SqlParameter("@SecurityQ1", SqlDbType.VarChar, 50);
                    dbparamSecurityQ.Value = txtSecurityQue1.Text.Trim();
                    sqlParameters.Add(dbparamSecurityQ);

                    ////Security Ans 1
                    SqlParameter dbparamSecurityA = new SqlParameter("@SecurityA1", SqlDbType.VarChar, 50);
                    dbparamSecurityA.Value = txtSecurityAns1.Text.Trim();
                    sqlParameters.Add(dbparamSecurityA);

                    //Security Q2
                    SqlParameter dbparamSecurityQ2 = new SqlParameter("@SecurityQ2", SqlDbType.VarChar, 50);
                    dbparamSecurityQ2.Value = txtSecurityQue2.Text.Trim();
                    sqlParameters.Add(dbparamSecurityQ2);

                    ////Security Ans 2
                    SqlParameter dbparamSecurityA2 = new SqlParameter("@SecurityA2", SqlDbType.VarChar, 50);
                    dbparamSecurityA2.Value = txtSecurityAns2.Text.Trim();
                    sqlParameters.Add(dbparamSecurityA2);

                    //Role
                    SqlParameter dbparamBussRoleId = new SqlParameter("@BussRoleID", SqlDbType.Int);
                    //dbparamBussRoleId.Value = cmbRoleType.SelectedIndex;
                    dbparamBussRoleId.Value = 1; //admin role
                    sqlParameters.Add(dbparamBussRoleId);

                    //ActivationCode
                    string strActivationCode = Guid.NewGuid().ToString();
                    SqlParameter dbparamActivationCode = new SqlParameter("@ActivationCode", SqlDbType.VarChar, 50);
                    dbparamActivationCode.Value = strActivationCode;
                    sqlParameters.Add(dbparamActivationCode);

                    dbHandler = new DBHandler(1);
                    result = dbHandler.ExecuteNonQuery(Constants.SP_USER_INSERT_UPDATE, sqlParameters);

                    if (result == 1)
                    {
                        //uplaod the logo file
                        if (flupldClientLogo.HasFile)
                        {
                            try
                            {
                                string filename = Path.GetFileName(flupldClientLogo.FileName);
                                flupldClientLogo.SaveAs(Utility.GetImageFolder() + filename);
                            }
                            catch (Exception ex)
                            {
                                YelpTrace.Write(ex);
                            }
                        }

                        DisplayMessage(MessageType.Information, string.Format(" Business Information {0} is successfully added. Please activate the account by clicking the link sent to your Email address.", txtUserName.Text));

                        //send user creation Email
                        SendUserRegistrationEmail(txtUserName.Text, txtUserName.Text, txtBusinessEmail.Text.Trim(), strActivationCode);

                        ClearFields();
                    }
                    else
                    {
                        DisplayMessage(MessageType.Error, message);
                    }
                }
                else
                {
                    //userID
                    SqlParameter dbparamUserId = new SqlParameter("@userId", SqlDbType.Int);
                    int nUserID =-1;
                    dbparamUserId.Value = Int32.TryParse(userId, out nUserID) ? nUserID : 0;
                    sqlParameters.Add(dbparamUserId);
                    
                    //userID
                    SqlParameter dbparambusinessDetailsID = new SqlParameter("@BusinessDetailId", SqlDbType.Int);
                    int businessDetailsID=-1;
                    dbparambusinessDetailsID.Value = Int32.TryParse(hdnBusinessDetailsID.Value, out businessDetailsID) ? businessDetailsID : 0;
                    sqlParameters.Add(dbparambusinessDetailsID);
                    
                    //ReturnBusinessDetailID
                    SqlParameter dbparamReturnBusinessDetailId = new SqlParameter("@ReturnBusinessDetailId", SqlDbType.Int);
                    dbparamReturnBusinessDetailId.Value = 0;
                    sqlParameters.Add(dbparamReturnBusinessDetailId);

                    dbHandler = new DBHandler(1);
                    result = dbHandler.ExecuteNonQuery(Constants.SP_BUSINESS_INSERT_UPDATE, sqlParameters);
                    if (result == 1)
                    {
                        DisplayMessage(MessageType.Information, " Business information is successfully updated.");
                        //ClearFields();
                    }
                }
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
                userActivationRawLink = "https://BusinessRepute.xyz/UsersAccount.aspx?type={0}&amp;code={1}";
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
                if (Communication.SendMail(strEmailId, string.Empty, subject, messageBody, "Business Reputation", out ex))
                {
                    YelpTrace.Write("sending user registration Email is failed " + ex);
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write("Exception in  user registration Email activation Email senfing" + ex);
            }
        }
        protected void txtReset_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            if (nPageMode == 0)
            {
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtConfirmPassword.Text = string.Empty;
            }
                //txtBussOwnerFirstname.Text = string.Empty;
                //txtBussOwnerLastname.Text = string.Empty;
                txtBusinessPhone.Text = string.Empty;
                txtBusinessEmail.Text = string.Empty;
                txtBusinessAddress1.Text = string.Empty;
                txtBusinessAddress2.Text = string.Empty;
            //txtBusinessFax.Text = string.Empty;
            txtBusinessName.Text = string.Empty;
            txtSecurityAns1.Text = string.Empty;
            txtSecurityAns2.Text = string.Empty;
            txtSecurityQue1.Text = string.Empty;
            txtSecurityQue2.Text = string.Empty;

            txtBussCity.Text = string.Empty;
            txtBusinessZip.Text = string.Empty;
            txtBussState.Text = string.Empty;
            //cmbRoleType.SelectedIndex = 0;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Validate the form for 
            if (ValidatePage())
            {
                //submit the request and store the details.. show confirmation message to the user. 
                SaveDetails();
            }
        }

        private void GetBusinessInfo(string userId)
        {
            //if (!string.IsNullOrEmpty(userId))
            //{
            //    int nUserID = Convert.ToInt32(userId);
            //    DataTable businessDetailTable = Utility.GetBusinessDetails(nUserID);
            //    //set header
            //    if (businessDetailTable != null)
            //    {
            //        if (businessDetailTable.Rows.Count > 0)
            //        {
            //            lblClientName.Text = (string)businessDetailTable.Rows[0]["businessName"];
            //            lblHeaderAddress1.Text = (string)businessDetailTable.Rows[0]["businessAddress1"];
            //            lblHeaderAddress2.Text = (string)businessDetailTable.Rows[0]["businessAddress2"];
            //            lblHeaderConatct.Text = "Phone: " + (string)businessDetailTable.Rows[0]["businessPhone"] + " Fax: " + (string)businessDetailTable.Rows[0]["businessFax"];
            //            lblHeaderEmail.Text = "Email: " + (string)businessDetailTable.Rows[0]["businessEmailId"];
            //            Session["BUSINESSID"] = (businessDetailTable.Rows[0]["BusinessDetailId"]).ToString();
            //            string logoFileName = (string)businessDetailTable.Rows[0]["logoFileName"];
            //            try
            //            {
            //                //imgClientLogo.ImageUrl =(Utility.GetImageFolder() + logoFileName);
            //                imgClientLogo.ImageUrl = ("ClientImages\\" + logoFileName);
            //                imgClientLogo1.ImageUrl = ("ClientImages\\" + logoFileName);

            //            }
            //            catch (Exception ex)
            //            {
            //                YelpTrace.Write(ex);
            //            }
            //        }
            //    }
            //}


            int nUserId = -1;
            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    Int32.TryParse(userId, out nUserId);
                    DataTable businessDetailTable = Utility.GetBusinessDetails(nUserId);
                    //set header
                    if (businessDetailTable != null)
                    {
                        if (businessDetailTable.Rows.Count > 0)
                        {
                            lblClientName.Text = (string)businessDetailTable.Rows[0]["businessName"];
                            lblHeaderAddress1.Text = (string)businessDetailTable.Rows[0]["businessAddress1"];
                            lblHeaderAddress2.Text = (string)businessDetailTable.Rows[0]["businessAddress2"];
                            lblHeaderConatct.Text = "Phone: " + (string)businessDetailTable.Rows[0]["businessPhone"];
                            lblHeaderEmail.Text = "Email: " + (string)businessDetailTable.Rows[0]["businessEmailId"];
                            Session["BUSINESSID"] = (businessDetailTable.Rows[0]["BusinessDetailId"]).ToString();
                            string logoFileName = (string)businessDetailTable.Rows[0]["logoFileName"];
                            try
                            {
                                //imgClientLogo.ImageUrl =(Utility.GetImageFolder() + logoFileName);
                                imgClientLogo.ImageUrl = ("ClientImages\\" + logoFileName);
                                imgClientLogo1.ImageUrl = ("ClientImages\\" + logoFileName);

                            }
                            catch (Exception ex)
                            {
                                YelpTrace.Write(ex);
                            }
                            txtBusinessName.Text = (string)businessDetailTable.Rows[0]["businessName"];
                            txtBusinessAddress1.Text = (string)businessDetailTable.Rows[0]["businessAddress1"];
                            txtBusinessAddress2.Text = (string)businessDetailTable.Rows[0]["businessAddress2"];
                            txtBusinessPhone.Text = (string)businessDetailTable.Rows[0]["businessPhone"];
                            txtBusinessEmail.Text = (string)businessDetailTable.Rows[0]["businessEmailId"];
                            txtBussCity.Text = (string)businessDetailTable.Rows[0]["businessCity"];
                            txtBussState.Text = (string)businessDetailTable.Rows[0]["businessState"];
                            txtBusinessZip.Text = (string)businessDetailTable.Rows[0]["businesszip"];
                            hdnBusinessDetailsID.Value = (businessDetailTable.Rows[0]["BusinessDetailId"]).ToString();
                            cmbBusinessType.SelectedValue = (businessDetailTable.Rows[0]["BusinessTypeId"]).ToString();
                            //txtSecurityQue1.Text = (string)businessDetailTable.Rows[0]["SecurityQ1"];
                            //txtSecurityAns1.Text = (string)businessDetailTable.Rows[0]["SecurityA1"];
                            //txtSecurityQue2.Text = (string)businessDetailTable.Rows[0]["SecurityQ2"];
                            //txtSecurityAns2.Text = (string)businessDetailTable.Rows[0]["SecurityA2"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    YelpTrace.Write(ex);
                }
            }
        }
        private void HideShowControl()
        {
            txtUserName.Visible=false;
            txtPassword.Visible=false;
            txtConfirmPassword.Visible=false;
            pnlLoginInfo.Visible = false;
            pnlLogoInfo.Visible = false;
            pnlUserDetails.Visible = false;
        }
    }
}