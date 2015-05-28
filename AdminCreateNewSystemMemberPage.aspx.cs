using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
public partial class AdminCreateNewSystemMemberPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            if (Session["userName"] == null)
            {
                Response.Redirect("AdminPage.aspx");
            } 

        //set collage list when the page is loaded
        if (!Page.IsPostBack)
        {


            JBTestBL bl = new JBTestBL();
            LinkedList<String> collageList = bl.getAllCollagesNames();
            if (collageList != null)
            {
                foreach (string str in collageList)
                    collageDropDownList.Items.Add(new ListItem(str));

            }
        }

    }
    protected void Confirm_Button_On_Click(object sender, EventArgs e)
    {
        string message = "";
        Boolean retVal = false;
        JBTestBL bl = new JBTestBL();
        Boolean matchMail = Regex.IsMatch(email_TB.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
        if (!matchMail)
        {
            message = "Email is not valid";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
        
        //check if password and the confirm password are equal
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
            retVal = bl.setNewSystemDetails(idTB.Text, FirstNameTB.Text, LastNameTB.Text,
                encryptPass, usernameTB.Text, collageDropDownList.SelectedItem.Text, email_TB.Text);
        }
        if (retVal)
        {
            message = "Your details has successfuly been added. You will be transfared now to your private student page";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            
        }
        else
        {
            message = "There was a problem adding your details. Please try again later.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);

        }
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