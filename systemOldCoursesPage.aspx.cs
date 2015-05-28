using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class systemOldCoursesPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        JBTestBL bl = new JBTestBL();
        CalenderClass cal = new CalenderClass();
        LinkedList<String[]> courseList = null;
        if (Session["userName"] != null)
        {
            string name = bl.getSystemFullNameById(Session["userName"].ToString());
            connectedUserLable.Text = "מחובר " + name;
            dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            courseList = bl.getOldCoursesList(Session["userName"].ToString());
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
                oldCourseListTable.Rows.Add(tRow);
                foreach (String str in arr)
                {
                    TableCell tCell = new TableCell();
                    tCell.Text = str;
                    tRow.Cells.Add(tCell);
                }
                TableCell tCellBS = new TableCell();
                Button showButton = new Button();
                showButton.ID = "showButton_" + i;
                showButton.Text = "צפה";
                showButton.Click += new EventHandler(Show_Old_Button_On_Click);
                tCellBS.Controls.Add(showButton);
                tRow.Cells.Add(tCellBS);

/*
                TableCell tCellBU = new TableCell();
                Button updateButton = new Button();
                updateButton.ID = "updateButton" + i;
                updateButton.Text = "עדכן";
                updateButton.Click += new EventHandler(Update_Old_Button_On_Click);
                tCellBU.Controls.Add(updateButton);
                tRow.Cells.Add(tCellBU);*/
            }

    }

    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("systemEntrencePage.aspx");

    }

    protected void Show_Old_Button_On_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));
        string courseCode = oldCourseListTable.Rows[lineNum].Cells[0].Text;

        Response.Redirect("#");

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