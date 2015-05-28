using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class studentEntrencePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Entrence_Button_On_Click(object sender, EventArgs e)
    {
        string message;
        string studentPass = "";
        JBTestSecurity security = new JBTestSecurity();
        JBTestBL bl = new JBTestBL();
        if(usernameTB.Text.Equals("") || passwordTB.Text.Equals(""))
        {
            message = "Please fill all fields.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
        if(!usernameTB.Text.Equals("") && bl.doesStudentExist(usernameTB.Text))
        {
            studentPass = bl.checkStudentLogin(usernameTB.Text);
           if(security.MatchHash(studentPass,passwordTB.Text))
           {
               Session["userName"] = usernameTB.Text;
               Response.Redirect("studentMainPage.aspx");
           }
           else
           {
               message = "Incorect password.";
               ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
           }
        }






    }
  
}