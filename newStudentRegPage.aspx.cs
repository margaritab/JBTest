using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;


public partial class newStudentRegPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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


    //do changes after the page is loaded
    protected void Load_Courses(object sender, EventArgs e)
    {
        courseDropDownList.Items.Clear();
        courseDropDownList.Items.Add(new ListItem("בחר"));
        string collageName = collageDropDownList.SelectedValue.ToString();
        JBTestBL bl = new JBTestBL();
        LinkedList<String> courseList = bl.getAllCoursesNamesInCollage(collageName);
        if (courseList != null)
        {
            foreach (string str in courseList)
                courseDropDownList.Items.Add(new ListItem(str));
        }

    }

    protected void Confirm_Bottun_On_Click(object sender, EventArgs e)
    {
        string message = "";
        Boolean retVal = false;
        Boolean addStudentFlag = true;
        JBTestBL bl = new JBTestBL();

        //check if student already exist
        if (!idTB.Text.Equals("") && bl.doesStudentExist(idTB.Text))
        {
            message = "You are already sign up";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            addStudentFlag = false;
        }

        //check if one of the textboxes is empty
        if (idTB.Text.Equals("") || hebFirstNameTB.Text.Equals("") || hebLastNameTB.Text.Equals("") ||
            passwordTB.Text.Equals("") || passwordConfirmTB.Text.Equals("") || primePhoneTB.Text.Equals("") ||
            emailTB.Text.Equals("") || engFirstNameTB.Text.Equals("") || engLastNameTB.Text.Equals("") || addressTB.Text.Equals("") || collageDropDownList.SelectedItem.Text.Equals("בחר") ||
            courseDropDownList.SelectedItem.Text.Equals("בחר"))
        {
            message = "One of the details you entered is incorrect";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            addStudentFlag = false;
        }
 
        //check if the english fields are in english letters only
          Boolean matchFirstName = Regex.IsMatch(engFirstNameTB.Text, "^[a-zA-Z ]*$");
          Boolean matchLastName = Regex.IsMatch(engLastNameTB.Text, "^[a-zA-Z ]*$");
          Boolean matchAddress = Regex.IsMatch(addressTB.Text, "^[a-zA-Z0-9 ]*$");
          if (!matchFirstName || !matchLastName || !matchAddress)
          {
              message = "One of the english only fields are incorrect";
              ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
              addStudentFlag = false;
          }

          Boolean matchMail = Regex.IsMatch(emailTB.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
          if (!matchMail)
          {
              message = "Email is not valid";
              ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
              addStudentFlag = false;
          }


        //check if password and the confirm password are equal
        if (!passwordTB.Text.Equals(passwordConfirmTB.Text))
        {
            message = "The password does not match to the confirm password. Please retype both password";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            addStudentFlag = false;
        }

        //check if standardization check box was clicked
        if (!rulesCB.Checked)
        {
            message = "The standardization must be read and checked before confirmation.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            addStudentFlag = false;
        }


        //encript password/hash table

        JBTestSecurity securePass = new JBTestSecurity();
        string encryptPass = securePass.CreateHash(passwordTB.Text);
        if (!encryptPass.Equals("") && addStudentFlag)
        {
            retVal = bl.setNewStudentDetailes(idTB.Text, hebFirstNameTB.Text, hebLastNameTB.Text, encryptPass,
                primePhoneTB.Text, secPhoneTB.Text, emailTB.Text, engFirstNameTB.Text, engLastNameTB.Text,
                addressTB.Text, cityTB.Text, collageDropDownList.SelectedItem.Text,
                courseDropDownList.SelectedItem.Text);
        }
        if (retVal)
        {
            message = "Your details has successfuly been added. You will be transfared now to your private student page";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            Session["userName"] = idTB.Text;
            Response.Redirect("studentMainPage.aspx");
        }
        else
        {
            message = "There was a problem adding your details. Please try again later.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);

        }
    }

    protected void Clear_Bottun_On_Click(object sender, EventArgs e)
    {
        idTB.Text = "";
        hebFirstNameTB.Text = "";
        hebLastNameTB.Text = "";
        passwordTB.Text = "";
        passwordConfirmTB.Text = "";
        primePhoneTB.Text = "";
        secPhoneTB.Text = "";
        emailTB.Text = "";
        engFirstNameTB.Text = "";
        engLastNameTB.Text = "";
        addressTB.Text = "";
        cityTB.Text = "";
        collageDropDownList.SelectedIndex = 0;
        courseDropDownList.Items.Clear();
        courseDropDownList.Items.Add(new ListItem("בחר"));
    }

    protected void Cancel_Bottun_On_Click(object sender, EventArgs e)
    {
        Response.Redirect("studentEntrencePage.aspx");
    }
}


