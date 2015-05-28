using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class systemUpdateCoursePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        CalenderClass cal = new CalenderClass();
        if (Session["userName"] != null)
        {
            courseNameTB.Text = Request.QueryString["courseCode"].ToString();
            string name = bl.getSystemFullNameById(Session["userName"].ToString());
            connectedUserLable.Text = "מחובר " + name;
            dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        else
        {
            Response.Redirect("systemEntrencePage.aspx");
        }

    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("systemEntrencePage.aspx");

    }
    protected void Update_Course_On_Click(object sender, EventArgs e)
    {
        CalenderClass cal = new CalenderClass();
        string sDate = cal.changeDateFormat(start_DateTB.Text);
        string eDate = cal.changeDateFormat(end_DateTB.Text);
        JBTestBL bl = new JBTestBL();
        Boolean retVal = bl.setUpdateCourseInfo(sDate,eDate,courseNameTB.Text);
        if (!retVal)
        {
            string message = "There was a problem updated your details. Please try again later.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
        else
            Response.Redirect("systemCoursesPage.aspx");

    }
    protected void Cancel_On_Click(object sender, EventArgs e)
    {
        Response.Redirect("systemCoursesPage.aspx");

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