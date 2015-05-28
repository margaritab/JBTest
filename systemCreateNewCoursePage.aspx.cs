using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class systemCreateNewCoursePage : System.Web.UI.Page
{
    static string userName;
    protected void Page_Load(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        CalenderClass cal = new CalenderClass();
        if (Session["userName"] != null)
        {
            string name = bl.getSystemFullNameById(Session["userName"].ToString());
            connectedUserLable.Text = "מחובר " + name;
            dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            userName = Session["userName"].ToString();
        }
        else
        {
            Response.Redirect("systemEntrencePage.aspx");
        }

        LinkedList<String> testList = bl.getAllTests();
        if (testList != null)
        {
            int i = 0;
            foreach (string str in testList)
            {
                testListCB.ID = "test_" + i;
                testListCB.Items.Add(new ListItem(str));
            }
        }
      

        
    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("systemEntrencePage.aspx");

    }
    protected void Create_Course_On_Click(object sender, EventArgs e)
    {
        string message = "";
        Boolean retVal = false;
        CalenderClass cal = new CalenderClass();
        JBTestBL bl = new JBTestBL();
       
        
        if (courseCode_TB.Text.Equals("") || courseName_TB.Text.Equals("") || freeTestTB.Text.Equals(""))
        {
            message = "One of the details you entered is incorrect";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
        else 
        {
            retVal = bl.setCreateNewCourse(courseCode_TB.Text, courseName_TB.Text,cal.changeDateFormat(startDate_TB.Text),cal.changeDateFormat(endDate_TB.Text), freeTestTB.Text,userName);
            
        }
        for(int i = 0;i < testListCB.Items.Count;i++)
        {
            if (testListCB.Items[i].Selected)
                retVal = bl.setAddTestToNewCourse(courseCode_TB.Text, testListCB.Items[i].Text);
        }
       // Label1.Text = testListCB.Items[1].Selected.ToString();
       
        Response.Redirect("systemCoursesPage.aspx");

    }
    protected void Cancel_Course_On_Click(object sender, EventArgs e)
    {
        Response.Redirect("systemMainPage.aspx");

    }

    protected void Search_Button_On_Click(object sender, EventArgs e)
    {
        string searchText = searchTB.Text;
        JBTestBL bl = new JBTestBL();
        Boolean matchID = Regex.IsMatch(searchText, "^[0-9]*$");

        if (matchID && bl.doesStudentExist(searchText))
        {
            Response.Redirect("systemStudentDetailsPage.aspx?id=" + searchText);
        }
        else//search by id
        {
            string message = "please enter a correct search text";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
    }

}