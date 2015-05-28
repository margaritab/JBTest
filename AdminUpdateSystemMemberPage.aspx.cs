using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
public partial class AdminUpdateSystemMemberPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            JBTestBL bl = new JBTestBL();

            if (Session["userName"] == null)
            {
                Response.Redirect("AdminPage.aspx");
            }

            if (Request.QueryString["id"] != null)
            {
                idTB.Text = Request.QueryString["id"].ToString();
            }

            String[] sysInfo = bl.getSystemInfo(idTB.Text);
            FirstNameTB.Text = sysInfo[0];
            LastNameTB.Text = sysInfo[1];
            usernameTB.Text = sysInfo[2];
            email_TB.Text = sysInfo[3];
            collageDropDownList.Items.Add(new ListItem(bl.getCollageNameByCollageCode(bl.getStaffWorkCollageId(usernameTB.Text))));
            
            LinkedList<String> collageList = bl.getAllCollagesNames();
            if (collageList != null)
            {
                foreach (string str in collageList)
                {
                    if (!str.Equals(collageDropDownList.Text) && !str.Equals("המכללה החרדית") && !str.Equals("אקסטרני"))
                        collageDropDownList.Items.Add(new ListItem(str));
                }
            }
        }
    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("AdminPage.aspx");

    }
    protected void Search_Button_On_Click(object sender, EventArgs e)
    {
        string searchText = searchTB.Text;
        JBTestBL bl = new JBTestBL();
        Boolean matchID = Regex.IsMatch(searchText, "^[0-9]*$");

        if (matchID && bl.doesSystemExistById(searchText))
        {
            Response.Redirect("AdminUpdateSystemMemberPage.aspx?id=" + searchText);
        }
        else//search by id
        {
            string message = "please enter a correct search text";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }


    }

    protected void Update_Button_On_Click(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        string message;
        string collageCode = bl.getCollageCodeByCollageName(collageDropDownList.SelectedItem.Text);
        Boolean retVal = bl.setUpdateStaffInfo(FirstNameTB.Text,LastNameTB.Text,usernameTB.Text,email_TB.Text,collageCode,idTB.Text);
        if (!retVal || FirstNameTB.Text.Equals("") || LastNameTB.Text.Equals("") || usernameTB.Text.Equals("") || email_TB.Text.Equals(""))
        {
            message = "One of the details you entered is incorrect";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
        else
        {
            message = "Update ended successfully";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
    }

    protected void Cancel_Button_On_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMainPage.aspx");
    }

}