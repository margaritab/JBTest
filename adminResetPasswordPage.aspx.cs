using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
public partial class adminResetPasswordPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        if (Session["username"] == null)
        {
            Response.Redirect("AdminPage.aspx");
        }
    

    }

    protected void Confirm_Button_On_Click(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        JBTestSecurity securePass = new JBTestSecurity();
        string encryptPass = securePass.CreateHash(passwordTB.Text);
        string encryptOldPass = securePass.CreateHash(oldPasswordTB.Text);

        if (passwordConfirmTB.Text.Equals(passwordTB.Text) && (!passwordTB.Text.Equals("") && !oldPasswordTB.Text.Equals("") && !passwordConfirmTB.Text.Equals("")))
        {
            bl.setResetAdminPassword(encryptOldPass,encryptPass);
            string message = "Password has successfuly changed!.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
        else
        {
            string message = "please type the same password!.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            Response.Redirect("adminResetPasswordPage.aspx");
         
        }
    }
    protected void Cancel_Button_On_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMainPage.aspx");

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


    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("AdminPage.aspx");

    }
}