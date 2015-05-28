using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class resetPasswordPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Reset_Password_Button_On_Click(object sender, EventArgs e)
    {
        Boolean resetFlag = true;
        Boolean retVal = false;
        JBTestBL bl = new JBTestBL();

        if(user_TB.Text.Equals("") || newPassword_TB.Text.Equals("") || confNewPassword_TB.Text.Equals(""))
        {
            string message = "Please fill all form fields.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            resetFlag = false;
        }

        Boolean matchUserName = Regex.IsMatch(user_TB.Text, "^[0-9]*$"); //student user name is id
        if ((!matchUserName && !bl.doesSystemExist(user_TB.Text)) || (matchUserName && !bl.doesStudentExist(user_TB.Text)))
        {
            string message = "The user name you entered does not exist";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            resetFlag = false;
        }

        if(!newPassword_TB.Text.Equals(confNewPassword_TB.Text))
        {
            string message = "The passwords do not match";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            resetFlag = false;
        }

        //encript password/hash table

        JBTestSecurity securePass = new JBTestSecurity();
        string encryptPass = securePass.CreateHash(newPassword_TB.Text);
        if (!encryptPass.Equals("") && resetFlag)
        {
            if(matchUserName)
                retVal = bl.setUpdatedStudentPassword(user_TB.Text,encryptPass);
            else
                retVal = bl.setUpdatedSystemPassword(user_TB.Text, encryptPass);  
        }
        if(retVal)
        {
            string message = "The password was changed";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            if (matchUserName)
                Response.Redirect("studentEntrencePage.aspx");
            else
                Response.Redirect("systemEntrencePage.aspx");
        }
        else
        {
            string message = "There was a problem changing the password, please try again later";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
    }
}