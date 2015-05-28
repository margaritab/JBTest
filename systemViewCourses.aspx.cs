using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class systemViewCourses : System.Web.UI.Page
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

        String[] SEdates = bl.getCourseStartAndEndDates(courseNameTB.Text);
        startDateTB.Text = SEdates[0];
        endDateTB.Text = SEdates[1];
        
        LinkedList<String[]> studentList = bl.getstudentThatStudyAtCourseList(courseNameTB.Text);

        if (studentList != null)
            foreach (String[] arr in studentList)
            {
                TableRow tRow = new TableRow();
                courseViewTable.Rows.Add(tRow);
                foreach (String str in arr)
                {
                    TableCell tCell = new TableCell();
                    tCell.Text = str;
                    if (str.Equals(arr.First()))
                    {

                        LinkButton bId = new LinkButton();
                        bId.Text = str;
                        tCell.Controls.Add(bId);
                        bId.PostBackUrl = "systemStudentDetailsPage.aspx?id=" + str;
                    }
                    tRow.Cells.Add(tCell);
                }
            }
    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("systemEntrencePage.aspx");

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