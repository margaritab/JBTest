using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class systemCoursesPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LinkedList<String[]> courseList = null;
        JBTestBL bl = new JBTestBL();
        CalenderClass cal = new CalenderClass();
        if (Session["userName"] != null)
        {
            string name = bl.getSystemFullNameById(Session["userName"].ToString());
            connectedUserLable.Text = "מחובר " + name;
            dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            courseList = bl.getActiveCoursesList(Session["userName"].ToString());

        }
        else
        {
            Response.Redirect("systemEntrencePage.aspx");
        }
        int i = 1;
        if (courseList != null)
            foreach (String[] arr in courseList)
            {
                TableRow tRow = new TableRow();
                courseListTable.Rows.Add(tRow);
                foreach (String str in arr)
                {
                    TableCell tCell = new TableCell();
                    tCell.Text = str;            
                    tRow.Cells.Add(tCell);
                }
                TableCell tCellBS = new TableCell();
                Button showButton = new Button();
                showButton.ID = "showButton_" + i;
                showButton.Text = "הצג";
                showButton.Click += new EventHandler(Show_Button_On_Click);
                tCellBS.Controls.Add(showButton);
                tRow.Cells.Add(tCellBS);


                TableCell tCellBU = new TableCell();
                Button updateButton = new Button();
                updateButton.ID = "updateButton_" + i;
                updateButton.Text = "עדכן";
                updateButton.Click += new EventHandler(Update_Button_On_Click);
                tCellBU.Controls.Add(updateButton);
                tRow.Cells.Add(tCellBU);
                i++;
            }
       

    }

    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("systemEntrencePage.aspx");

    }
    protected void Show_Button_On_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));
        string courseCode = courseListTable.Rows[lineNum].Cells[0].Text;

        Response.Redirect("systemViewCourses.aspx?courseCode="+courseCode);

    }


    protected void Update_Button_On_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));
        string courseCode = courseListTable.Rows[lineNum].Cells[0].Text;
        string startDate = courseListTable.Rows[lineNum].Cells[2].Text;
        Response.Redirect("systemUpdateCoursePage.aspx?courseCode="+courseCode);

    }


    protected void Old_Courses_Button_On_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("systemOldCoursesPage.aspx");

    }

    protected void Create_Courses_Button_On_Click(object sender, EventArgs e)
    {

        Response.Redirect("systemCreateNewCoursePage.aspx");

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