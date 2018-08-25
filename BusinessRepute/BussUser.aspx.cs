using AppHelper;
using AppHelper.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace BusinessRepute
{
    public partial class BussUser : System.Web.UI.Page
    {
        private int nPageMode = 0;
        private int nEditUserID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = string.Empty;
            if ((Session["USERID"] == null) || (Session["USERID"] == ""))
            {
                //FailureText.Text = " Context.User.Identity.Name " + Context.User.Identity.Name + " Session[USERID] " + Session["USERID"];
                string userName = Context.User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    Server.Transfer("~/login.aspx", false);
                }
                else
                {
                    userId =Helper.GetUserId(userName);
                    Session["USERID"]= userId;
                }
            }
            else
            {
                userId = Session["USERID"].ToString();
            }
            
            //populate drop down with roles
            if (!Page.IsPostBack)
            {
                int nuserID = Convert.ToInt32(userId);
                DataTable businessDetailTable = Utility.GetBusinessDetails(nuserID);
                //set header
                if (businessDetailTable != null)
                {
                    if (businessDetailTable.Rows.Count > 0)
                    {
                        try
                        {
                            lblClientName.Text = (string)businessDetailTable.Rows[0]["businessName"];
                            lblHeaderAddress1.Text = (string)businessDetailTable.Rows[0]["businessAddress1"];
                            lblHeaderAddress2.Text = (string)businessDetailTable.Rows[0]["businessAddress2"];
                            lblHeaderConatct.Text = "Phone: " + (string)businessDetailTable.Rows[0]["businessPhone"];// +" Fax: " + (string)businessDetailTable.Rows[0]["businessFax"];
                            lblHeaderEmail.Text = "Email: " + (string)businessDetailTable.Rows[0]["businessEmailId"];
                            hdnBusinessDetailsID.Value = (businessDetailTable.Rows[0]["BusinessDetailId"]).ToString();
                            string logoFileName = (string)businessDetailTable.Rows[0]["logoFileName"];

                            //imgClientLogo.ImageUrl =(Utility.GetImageFolder() + logoFileName);
                            imgClientLogo.ImageUrl = ("ClientImages\\" + logoFileName);
                            imgClientLogo1.ImageUrl = ("ClientImages\\" + logoFileName);

                        }
                        catch (Exception ex)
                        {
                            YelpTrace.Write(ex);
                        }
                    }
                }
                cmbRoleType.DataSource = null;
                cmbRoleType.DataSource = Utility.GetRoles();
                cmbRoleType.DataTextField = "BusinessRoleName";
                cmbRoleType.DataValueField = "BusinessRoleId";

                cmbRoleType.DataBind();
                cmbRoleType.Items.Insert(0, new ListItem("Select Role", "0"));

                if (Request.QueryString["ViewId"] != null)
                {
                    string editableuserID = Request.QueryString["ViewId"].ToString();
                    hdnPageMode.Value = "1";
                    nPageMode = 1;
                    Int32.TryParse(editableuserID, out nEditUserID);
                    hdnEditUserID.Value = nEditUserID.ToString(); 
                    GetUserData(editableuserID);
                }
            }
            else
            {
                nPageMode = string.IsNullOrEmpty(hdnPageMode.Value) ? 0 : Int16.Parse(hdnPageMode.Value);
            }

        }

        private void GetUserData(string userId)
        {
            //int nUserId = -1;
            Int32.TryParse(userId, out nEditUserID);

            //get user details
            try
            {
                List<SqlParameter> dbParamCollection = new List<SqlParameter>();

                //username
                SqlParameter dbparamuserid = new SqlParameter("@Userid", System.Data.SqlDbType.Int);
                dbparamuserid.Value = nEditUserID;
                dbParamCollection.Add(dbparamuserid);

                //USERDETAILS_SELECT
                DBHandler dbHandler = new DBHandler(1);
                DataTable dataTable= dbHandler.ExecuteReaderinTable(Constants.SP_USER_INFO_S, dbParamCollection);
                if (dataTable != null)
                {
                    txtUserFirstname.Text = dataTable.Rows[0]["FirstName"].ToString();
                    txtUserLastname.Text = dataTable.Rows[0]["LastName"].ToString();
                    txtUserPhone.Text = dataTable.Rows[0]["PhoneNumber"].ToString();
                    txtUserEmail.Text = dataTable.Rows[0]["EmailId"].ToString();

                    //txtSecurityQue1.Text = dataTable.Rows[0]["SecurityQ1"].ToString();
                    //txtSecurityAns1.Text = dataTable.Rows[0]["SecurityA1"].ToString();
                    //txtSecurityQue2.Text = dataTable.Rows[0]["SecurityQ2"].ToString();
                    //txtSecurityAns2.Text = dataTable.Rows[0]["SecurityA2"].ToString();
                    cmbRoleType.SelectedValue = dataTable.Rows[0]["BusinessRoleId"].ToString();

                    pnlProfilePic.Visible = false;
                    //txtUserName.Visible= false;
                    //txtPassword.Visible = false;
                    //txtConfirmPassword.Visible = false;
                    //lblConfirmPassword.Visible = false;
                    //lblPassword.Visible = false;
                    //lblUserName.Visible = false;
                    //userPasswordHelp.Visible = false;
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        protected void Reset_Click(object sender, EventArgs e)
        {

        }
        private bool ValidatePage()
        {
            bool bReturn = true;
            if (string.IsNullOrEmpty(txtUserEmail.Text))
            {
                DisplayMessage(MessageType.Error, Constants.BLANK_EMAIL);
                bReturn = false;
                return bReturn;
            }

            if (string.IsNullOrEmpty(txtUserFirstname.Text))
            {
                DisplayMessage(MessageType.Error, Constants.BLANK_BUSS_FIRST_NAME);
                bReturn = false;
                return bReturn;
            }

            //if (string.IsNullOrEmpty(txtSecurityQue1.Text))
            //{
            //    DisplayMessage(MessageType.Error, Constants.BLANK_SECURITY_QUE_1);
            //    bReturn = false;
            //    return bReturn;
            //}
            //if (string.IsNullOrEmpty(txtSecurityAns1.Text))
            //{
            //    DisplayMessage(MessageType.Error, Constants.BLANK_SECURITY_ANS_1);
            //    bReturn = false;
            //    return bReturn;
            //}
            //if (string.IsNullOrEmpty(txtSecurityQue2.Text))
            //{
            //    DisplayMessage(MessageType.Error, Constants.BLANK_SECURITY_QUE_2);
            //    bReturn = false;
            //    return bReturn;
            //}
            //if (string.IsNullOrEmpty(txtSecurityAns2.Text))
            //{
            //    DisplayMessage(MessageType.Error, Constants.BLANK_SECURITY_ANS_2);
            //    bReturn = false;
            //    return bReturn;
            //}

            if (cmbRoleType.SelectedIndex == 0)
            {
                DisplayMessage(MessageType.Error, "Please select Role for the user");
                bReturn = false;
                return bReturn;
            }
            
            //validation for duplicate check
            //if ((nPageMode==0) && Utility.IsDuplicateUserName(txtUserName.Text.Trim()))
            //{
            //    DisplayMessage(MessageType.Error, Constants.DUPLICATE_USERNAME);
            //    bReturn = false;
            //}
            return bReturn;
        }

        

        private void SaveDetails()
        {
            DBHandler dbHandler = null;
            #region SQLExpress
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();


                //First Name
                SqlParameter dbparamFirstName = new SqlParameter("@FirstName", SqlDbType.VarChar, 25);
                dbparamFirstName.Value = txtUserFirstname.Text.Trim();
                sqlParameters.Add(dbparamFirstName);

                //last Name
                SqlParameter dbparamLastName = new SqlParameter("@LastName", SqlDbType.VarChar, 25);
                dbparamLastName.Value = txtUserLastname.Text.Trim();
                sqlParameters.Add(dbparamLastName);

                //Email
                SqlParameter dbparamEmail = new SqlParameter("@EmailId", SqlDbType.VarChar, 50);
                dbparamEmail.Value = txtUserEmail.Text.Trim();
                sqlParameters.Add(dbparamEmail);

                //Phone number
                SqlParameter dbparamPhoneNumber = new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 25);
                dbparamPhoneNumber.Value = txtUserPhone.Text.Trim();
                sqlParameters.Add(dbparamPhoneNumber);

                //Security Q1
                SqlParameter dbparamSecurityQ = new SqlParameter("@SecurityQ1", SqlDbType.VarChar, 50);
                //dbparamSecurityQ.Value = txtSecurityQue1.Text.Trim();
                dbparamSecurityQ.Value = "Que 1";
                sqlParameters.Add(dbparamSecurityQ);

                //////Security Ans 1
                SqlParameter dbparamSecurityA = new SqlParameter("@SecurityA1", SqlDbType.VarChar, 50);
                //dbparamSecurityA.Value = txtSecurityAns1.Text.Trim();
                dbparamSecurityA.Value = "Ans 1";
                sqlParameters.Add(dbparamSecurityA);

                ////Security Q2
                SqlParameter dbparamSecurityQ2 = new SqlParameter("@SecurityQ2", SqlDbType.VarChar, 50);
                //dbparamSecurityQ2.Value = txtSecurityQue2.Text.Trim();
                dbparamSecurityQ2.Value = "Que 2";
                sqlParameters.Add(dbparamSecurityQ2);

                //////Security Ans 2
                SqlParameter dbparamSecurityA2 = new SqlParameter("@SecurityA2", SqlDbType.VarChar, 50);
                //dbparamSecurityA2.Value = txtSecurityAns2.Text.Trim();
                dbparamSecurityA2.Value = "Ans 2";
                sqlParameters.Add(dbparamSecurityA2);

                ////username
                SqlParameter dbparamUserName = new SqlParameter("@User_userName", SqlDbType.VarChar, 25);
                //dbparamUserName.Value = txtUserName.Text.Trim();
                dbparamUserName.Value = txtUserFirstname.Text.Trim();
                sqlParameters.Add(dbparamUserName);

                ////password
                SqlParameter dbparamPassword = new SqlParameter("@password", SqlDbType.VarChar, 25);
                //dbparamPassword.Value = txtPassword.Text.Trim();
                dbparamPassword.Value = txtUserFirstname.Text.Trim();
                sqlParameters.Add(dbparamPassword);

                //Role
                SqlParameter dbparamBussRoleId = new SqlParameter("@BussRoleId", SqlDbType.Int);
                dbparamBussRoleId.Value = cmbRoleType.SelectedValue;
                sqlParameters.Add(dbparamBussRoleId);

                //BusinessDetailsID
                int businessDetailId;
                Int32.TryParse(hdnBusinessDetailsID.Value, out businessDetailId);
                SqlParameter dbparamBusinessDetailsID = new SqlParameter("@BusinessDetailsID", SqlDbType.Int);
                dbparamBusinessDetailsID.Value = businessDetailId;
                sqlParameters.Add(dbparamBusinessDetailsID);

                //UserID
                SqlParameter dbparamUserID = new SqlParameter("@userId", SqlDbType.Int);
                dbparamUserID.Value = string.IsNullOrEmpty(hdnEditUserID.Value) ? 0 : Int32.Parse(hdnEditUserID.Value);
                sqlParameters.Add(dbparamUserID);

                SqlParameter dbparampageMode = new SqlParameter("@Mode", SqlDbType.Int);
                dbparampageMode.Value = nPageMode;
                sqlParameters.Add(dbparampageMode);


                //Profile Picturename
                SqlParameter dbparamProfilePicName = new SqlParameter("@profilePicName", SqlDbType.VarChar, 25);
                dbparamProfilePicName.Value = flupldClientPic.FileName.Trim();
                sqlParameters.Add(dbparamProfilePicName);

                //Business Logo file Name
                SqlParameter dbparamProfilePicFileName = new SqlParameter("@profilePicFileName", SqlDbType.VarChar, 255);
                dbparamProfilePicFileName.Value = flupldClientPic.FileName.Trim();
                sqlParameters.Add(dbparamProfilePicFileName);

                //ActivationCode
                string strActivationCode = Guid.NewGuid().ToString();
                SqlParameter dbparamActivationCode = new SqlParameter("@ActivationCode", SqlDbType.VarChar, 50);
                dbparamActivationCode.Value = strActivationCode;
                sqlParameters.Add(dbparamActivationCode);

                string message = string.Empty;
                dbHandler = new DBHandler(1);
                int result = dbHandler.ExecuteNonQuery(Constants.SP_USER_INFO_IU, sqlParameters);
                if (nPageMode == 0)
                {

                    if (result == 1)
                    {
                        //uplaod the logo file
                        if (flupldClientPic.HasFile)
                        {
                            try
                            {
                                string filename = Path.GetFileName(flupldClientPic.FileName);
                                flupldClientPic.SaveAs(Utility.GetImageFolder() + filename);
                            }
                            catch (Exception ex)
                            {
                                YelpTrace.Write(ex);
                            }
                        }

                        DisplayMessage(MessageType.Information, string.Format(" User is successfully added."));

                        ClearFields();
                    }
                }

                else
                {
                    DisplayMessage(MessageType.Information, string.Format(" User information is successfully updated."));

                    //ClearFields();
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

       
        protected void txtReset_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            txtUserFirstname.Text = string.Empty;
            txtUserLastname.Text = string.Empty;
            txtUserPhone.Text = string.Empty;
            txtUserEmail.Text = string.Empty;
            //txtSecurityAns1.Text = string.Empty;
            //txtSecurityAns2.Text = string.Empty;
            //txtSecurityQue1.Text = string.Empty;
            //txtSecurityQue2.Text = string.Empty;
            //txtUserName.Text = String.Empty;
            //txtPassword.Text = String.Empty;
            //txtConfirmPassword.Text = String.Empty;
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<SqlParameter> dbParamCollection = new List<SqlParameter>();
                //Userid
                SqlParameter dbparamuserid = new SqlParameter("@userId", System.Data.SqlDbType.Int);
                dbparamuserid.Value = string.IsNullOrEmpty(hdnEditUserID.Value) ? 0 : Int32.Parse(hdnEditUserID.Value);
                dbParamCollection.Add(dbparamuserid);

                //USERDETAILS_SELECT
                DBHandler dbHandler = new DBHandler(1);
                dbHandler.ExecuteNonQuery(Constants.SP_USER_INFO_D, dbParamCollection);
                DisplayMessage(MessageType.Information, "User infomration is successfully deleted");
                ClearFields();
            }
            catch (Exception ex)
            {
                DisplayMessage(MessageType.Exception, ex.Message);
                YelpTrace.Write(ex);
            }
        }
       
    }
}