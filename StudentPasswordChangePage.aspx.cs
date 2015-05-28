using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
public partial class StudentPasswordChangePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
            Response.Redirect("studentEntrencePage.aspx");
            
        if (!IsPostBack)
        {
            if (Session["userName"] != null)
            {
                JBTestBL bl = new JBTestBL();
                name.Text = bl.getStudentFullNameById(Session["userName"].ToString());
            }
            else
            {
                Response.Redirect("studentEntrencePage.aspx");
            }
        }

    }
    protected void Confirm_Button_On_Click(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        Boolean retVal = false;
        string message = "";
        //check if one of the textboxes is empty
        if (passwordTB.Text.Equals("") || passwordConfirmTB.Text.Equals(""))
        {
            message = "Fill all fields";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }


        if (!passwordTB.Text.Equals(passwordConfirmTB.Text))
        {
            message = "The password does not match to the confirm password. Please retype both password";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }

        //encript password/hash table

        JBTestSecurity securePass = new JBTestSecurity();
        string encryptPass = securePass.CreateHash(passwordTB.Text);
        if (!encryptPass.Equals(""))
        {
            retVal = bl.setUpdatedStudentPassword(Session["userName"].ToString(), encryptPass);
        }
        if (retVal)
        {
            message = "Your details has successfuly been updated.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            
        }
        else
        {
            message = "There was a problem updating your details. Please try again later.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);

        }
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);

    }
    protected void Cancel_Button_On_Click(object sender, EventArgs e)
    {
        
    }
}