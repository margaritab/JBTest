using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class studentUpdateDetailsPage : System.Web.UI.Page
{
    static int tests;
    static string originalCollage;
    static string originalCourse;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
            Response.Redirect("studentEntrencePage.aspx");

        if (!Page.IsPostBack)
        {
            JBTestBL bl = new JBTestBL();
            CalenderClass cal = new CalenderClass();

            if (Session["userName"] != null)
            {
                Id_TB.Text = Session["userName"].ToString();
                connectedUserLable.Text = "מחובר " + bl.getStudentFullNameById(Session["userName"].ToString());
                dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                tests = bl.getTestsAmount(Session["userName"].ToString());
                originalCollage = bl.getStudentCollageName(Session["userName"].ToString());
                originalCourse = bl.getCourseById(Session["userName"].ToString()); 
            }
            else
            {
                Response.Redirect("studentEntrencePage.aspx");
            }


            First_Name_TB.Text = bl.getStudentFullNameById(Id_TB.Text).Substring(0, bl.getStudentFullNameById(Id_TB.Text).IndexOf(" "));
            Last_Name_TB.Text = bl.getStudentFullNameById(Id_TB.Text).Substring(bl.getStudentFullNameById(Id_TB.Text).IndexOf(" ") + 1);
            Email_TB.Text = bl.getStudentEmail(Id_TB.Text);
            Prime_Phone_TB.Text = bl.getStudentPrimePhone(Id_TB.Text);
            Sec_Phone_TB.Text = bl.getStudentSecPhone(Id_TB.Text);
            First_Name_Eng_TB.Text = bl.getStudentEngFirstName(Id_TB.Text);
            Last_Name_Eng_TB.Text = bl.getStudentEngLastName(Id_TB.Text);
            Addr_TB.Text = bl.getStudentAddr(Id_TB.Text);
            City_TB.Text = bl.getStudentCity(Id_TB.Text);
            Collage_DL.Items.Add(new ListItem(bl.getStudentCollageName(Id_TB.Text)));
            Course_DL.Items.Add(new ListItem(bl.getStudentCourseCode(Id_TB.Text)));
            LinkedList<String> collageList = bl.getAllCollagesNames();
            if (collageList != null)
            {
                foreach (string str in collageList)
                {
                    if(!str.Equals(Collage_DL.Text))
                        Collage_DL.Items.Add(new ListItem(str));
                }
            }
            
           
        }
    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("studentEntrencePage.aspx");

    }
    protected void Load_Courses(object sender, EventArgs e)
    {
        Course_DL.Items.Clear();
        
        string collageName = Collage_DL.SelectedValue.ToString();
        JBTestBL bl = new JBTestBL();
        LinkedList<String> courseList = bl.getAllCoursesNamesInCollage(collageName);
        if (courseList != null)
        {
            foreach (string str in courseList)
                Course_DL.Items.Add(new ListItem(str));
        }

    }

    protected void Update_Bottun_On_Click(object sender, EventArgs e)
    {
        string message = "";
        String password = "";
        Boolean retVal = false;
        Boolean addStudentFlag = true;
        JBTestBL bl = new JBTestBL();
        if (Session["password"] != null)
                password = Session["password"].ToString();
        //check if one of the textboxes is empty
        if (First_Name_TB.Text.Equals("") || Last_Name_TB.Text.Equals("") ||
           Prime_Phone_TB.Text.Equals("") || Email_TB.Text.Equals("") || 
           First_Name_Eng_TB.Text.Equals("") || Last_Name_Eng_TB.Text.Equals("") || Addr_TB.Text.Equals(""))
        {
            message = "One of the details you entered is incorrect";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            addStudentFlag = false;
        }

        //check if the english fields are in english letters only
        Boolean matchFirstName = Regex.IsMatch(First_Name_Eng_TB.Text, "^[a-zA-Z ]*$");
        Boolean matchLastName = Regex.IsMatch(Last_Name_Eng_TB.Text, "^[a-zA-Z ]*$");
        Boolean matchAddress = Regex.IsMatch(Addr_TB.Text, "^[a-zA-Z0-9 ]*$");
        if (!matchFirstName || !matchLastName || !matchAddress)
        {
            message = "One of the english only fields are incorrect";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            addStudentFlag = false;
        }
        Boolean matchMail = Regex.IsMatch(Email_TB.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
        if (!matchMail)
        {
            message = "Email is not valid";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            addStudentFlag = false;
        }


        //encript password/hash table

        JBTestSecurity securePass = new JBTestSecurity();
        string encryptPass = securePass.CreateHash(password);
        if (!encryptPass.Equals("") && addStudentFlag)
            retVal = bl.setUpdatedStudentDetailes(Id_TB.Text, First_Name_TB.Text, Last_Name_TB.Text, encryptPass,
                Prime_Phone_TB.Text, Sec_Phone_TB.Text, Email_TB.Text, First_Name_Eng_TB.Text, Last_Name_Eng_TB.Text,
                Addr_TB.Text, City_TB.Text, Collage_DL.SelectedItem.Text,
                Course_DL.SelectedItem.Text, originalCollage, originalCourse, tests);
        if (retVal)
        {
            message = "Your details has successfuly been updated. You will be transfared now to your private student page";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            Response.Redirect("studentUpdateDetailsPage.aspx");
        }
        else
        {
            message = "There was a problem updated your details. Please try again later.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);

        }
    }

    protected void Cancel_Bottun_On_Click(object sender, EventArgs e)
    {

        Session["userName"] = Id_TB.Text;
        Response.Redirect("studentMainPage.aspx");

    }

    protected void Change_Bottun_On_Click(object sender, EventArgs e)
    {
        Session["userName"] = Id_TB.Text;
        
        Response.Redirect("StudentPasswordChangePage.aspx");
        

    }
}