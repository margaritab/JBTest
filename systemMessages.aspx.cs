using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class systemMessages : System.Web.UI.Page
{
    static string userName;
    protected void Page_Load(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        CalenderClass cal = new CalenderClass();

        if (Session["userName"] != null)
        {
            userName = Session["userName"].ToString();
            connectedUserLable.Text = "מחובר " + bl.getSystemFullNameById(Session["userName"].ToString());
            dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            LinkedList<String[]> messagesList = bl.getCurrMessages(userName, "staff");
            setCurrMessageTable(messagesList);
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
    /*todays tests table*/
    private void setCurrMessageTable(LinkedList<String[]> messagesList)
    {
        int i = 1;
        foreach (String[] arr in messagesList)
        {
            TableRow tRow = new TableRow();
            currMessageTable.Rows.Add(tRow);
             
            //addRowNum
            TableCell tCellNum = new TableCell();
            tCellNum.Text = i.ToString();
            tRow.Cells.Add(tCellNum);

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
            Button delButton = new Button();
            delButton.ID = "delButton_" + i;
            delButton.Text = "מחק";
            delButton.Click += new EventHandler(Delete_Button_On_Click);
            tCellBD.Controls.Add(delButton);
            tRow.Cells.Add(tCellBD);

            i++;

        }
    }

    protected void Update_Button_On_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));
        string date = currMessageTable.Rows[lineNum].Cells[1].Text;
        string message = currMessageTable.Rows[lineNum].Cells[2].Text;
        message = message.Substring(0, message.IndexOf("\r\n"));
        JBTestBL bl = new JBTestBL();
        Response.Redirect("systemUpdateMessagePage.aspx?date=" + date + "&message=" + message);
    }

    protected void Delete_Button_On_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));
        CalenderClass cal = new CalenderClass();
        string date = cal.changeDateFormat(currMessageTable.Rows[lineNum].Cells[1].Text);
        string msg = currMessageTable.Rows[lineNum].Cells[2].Text;
        JBTestBL bl = new JBTestBL();
        
        Boolean retVal = bl.setDeleteMessage(date, msg, userName);
        if (!retVal)
        {
            string message = "There was a problem updated your details. Please try again later.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }

        Response.Redirect(Request.RawUrl);

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

    protected void Create_Message_Button_On_Click(object sender, EventArgs e)
    {
        Response.Redirect("systemCreateNewMessagePage.aspx");
    }
}