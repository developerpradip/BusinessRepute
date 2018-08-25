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

namespace BusinessRepute
{
    public partial class BussUserList : System.Web.UI.Page
    {
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
                    userId =  Helper.GetUserId(userName);
                }
            }
            else
            {
                userId = Session["USERID"].ToString();
            }


            int nUserID = Convert.ToInt32(userId);
            
            DataTable businessDetailTable =  Utility.GetBusinessDetails(nUserID);
            //set header
            if (businessDetailTable != null)
            {
                if (businessDetailTable.Rows.Count > 0)
                {
                    lblClientName.Text = (string)businessDetailTable.Rows[0]["businessName"];
                    lblHeaderAddress1.Text = (string)businessDetailTable.Rows[0]["businessAddress1"];
                    lblHeaderAddress2.Text = (string)businessDetailTable.Rows[0]["businessAddress2"];
                    lblHeaderConatct.Text = "Phone: " + (string)businessDetailTable.Rows[0]["businessPhone"]; // +" Fax: " + (string)businessDetailTable.Rows[0]["businessFax"];
                    lblHeaderEmail.Text = "Email: " + (string)businessDetailTable.Rows[0]["businessEmailId"];
                    hdnBusinessDetailsID.Value = (businessDetailTable.Rows[0]["BusinessDetailId"]).ToString();
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
                }
            }
            if (IsPostBack)
            {
                //LoadStatus();
                GetUsers();
                gridUserList.Visible = true;
            }
            else
            {
                //LoadStatus();
                GetUsers();
            }
        
        }

        private void GetUsers()
        {
            try
            {
                int businessDetailId;
                Int32.TryParse(hdnBusinessDetailsID.Value, out businessDetailId);
                
                DataTable searchDataTable = Utility.GetUsers(businessDetailId);
                int recordsFound = 0;
                if (searchDataTable != null)
                {
                    recordsFound = searchDataTable.Rows.Count;
                    if (recordsFound >= 3)
                    {
                        btnAddUser.Enabled = false;
                    }

                    gridUserList.DataSource = searchDataTable;
                    gridUserList.DataBind();
                    gridUserList.Visible = true;
                }
            }
            catch (Exception ex)
            {
                YelpTrace.Write(ex);
            }
        }

        protected void gridUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridUserList.PageIndex = e.NewPageIndex;
            gridUserList.DataBind();
        }

        protected void gridUserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gridUserList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            GridViewRow row = gridUserList.Rows[index];

            string searchId = row.Cells[0].Text;
            string strPath = "BussUser.aspx?ViewId=" + searchId.ToString();
            Server.Transfer(strPath, false);
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("BussUser.aspx");

        }
    }
}