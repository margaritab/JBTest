using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class systemEntrencePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Entrence_Button_On_Click(object sender, EventArgs e)
    {
        string message;
        string systemPass = "";
        JBTestSecurity security = new JBTestSecurity();
        JBTestBL bl = new JBTestBL();
        if (usernameTB.Text.Equals("") || passwordTB.Text.Equals(""))
        {
            message = "Please fill all fields.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
        if (!usernameTB.Text.Equals("") && bl.doesSystemExist(usernameTB.Text))
        {
            systemPass = bl.checkSystemLogin(usernameTB.Text);
            if (security.MatchHash(systemPass, passwordTB.Text))
            {
                Session["userName"] = usernameTB.Text;
                Response.Redirect("systemMainPage.aspx");
            }
            else
            {
                message = "Incorect password.";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            }
        }

    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("systemEntrencePage.aspx");

    }
}