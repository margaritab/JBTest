using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class systemTestsPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        CalenderClass cal = new CalenderClass();
        if (Session["userName"] != null)
        {
            string name = bl.getSystemFullNameById(Session["userName"].ToString());
            connectedUserLable.Text = "מחובר " + name;
            dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        else
        {
            Response.Redirect("systemEntrencePage.aspx");
        }

        LinkedList<String[]> testList = bl.getTestNameAndCodeList();
        int i = 1;
        if (testList != null)
            foreach (String[] arr in testList)
            {
                TableRow tRow = new TableRow();
                courseListTable.Rows.Add(tRow);
                foreach (String str in arr)
                {
                    TableCell tCell = new TableCell();
                    tCell.Text = str;
                    tRow.Cells.Add(tCell);
                }
                
                TableCell tCellBU = new TableCell();
                Button updateButton = new Button();
                updateButton.ID = "updateButton_" + i;
                updateButton.Text = "עדכן";
                updateButton.Click += new EventHandler(Update_Button_On_Click);
                tCellBU.Controls.Add(updateButton);
                tRow.Cells.Add(tCellBU);

                TableCell tCellBD = new TableCell();
                Button deleteButton = new Button();
                deleteButton.ID = "deleteButton_" + i;
                deleteButton.Text = "מחק";
                deleteButton.Click += new EventHandler(Delete_Button_On_Click);
                tCellBD.Controls.Add(deleteButton);
                tRow.Cells.Add(tCellBD);
                i++;
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

    protected void Create_New_Test_On_Click(object sender, EventArgs e)
    {
        Response.Redirect("systemCreateNewTestPage.aspx");
    }


    protected void Update_Button_On_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));
        string courseCode = courseListTable.Rows[lineNum].Cells[0].Text;
        string startDate = courseListTable.Rows[lineNum].Cells[2].Text;
        Response.Redirect("systemUpdateTestePage.aspx?testCode=" + courseCode);

    }

    protected void Delete_Button_On_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));
        string courseCode = courseListTable.Rows[lineNum].Cells[0].Text;
        JBTestBL bl = new JBTestBL();
        bl.setDeleteTests(courseCode);
        Response.Redirect("systemTestsPage.aspx");

    }


}