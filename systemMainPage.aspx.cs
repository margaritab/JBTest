using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class systemMain : System.Web.UI.Page
{
    string studentIdConf;
    string studentIdToday;
    int TodaysTestinfoSize;

    static string staffUserName;

    protected void Page_Load(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        CalenderClass cal = new CalenderClass();

        if (Session["userName"] != null)
        {
            usernameLB.Text = bl.getSystemFullNameById(Session["userName"].ToString());
            connectedUserLable.Text = "מחובר " + usernameLB.Text;
            dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            staffUserName = Session["userName"].ToString();
        }
        else
        {
            Response.Redirect("systemEntrencePage.aspx");
        }

        LinkedList<String[]> TodaysTestinfo = bl.getStudentTodayTests(cal.changeDateFormat(dateLable.Text), staffUserName);
        LinkedList<String[]> TestConfirmedinfo = bl.getStudentWaitForConfiremdTests(staffUserName);
        LinkedList<String[]> TestCancelinfo = bl.getStudentWaitForCancelTests(staffUserName);

        TodaysTestinfoSize = TodaysTestinfo.Count;
        setTodayTestTable(TodaysTestinfo);
        setTestWaitForConfirmedTable(TestConfirmedinfo);
        setTestWaitForCancelTable(TestCancelinfo);

    }

    /*todays tests table*/
    private void setTodayTestTable(LinkedList<String[]> TodaysTestinfo)
    {
        JBTestBL bl = new JBTestBL();
        CalenderClass cal = new CalenderClass();

        string id = "";
        string date = cal.changeDateFormat(dateLable.Text);
        string testCode = bl.getStudentTestCodeByDateAndId(id, date);

        int i = 1;
        foreach (String[] arr in TodaysTestinfo)
        {
            TableRow tRow = new TableRow();
            todayTestTable.Rows.Add(tRow);
            foreach (String str in arr)
            {
                TableCell tCell = new TableCell();
                tCell.Text = str;
                if (str.Equals(arr.First()))
                {
                    studentIdToday = str;

                    LinkButton bId = new LinkButton();
                    bId.Text = str;
                    tCell.Controls.Add(bId);
                    bId.PostBackUrl = "systemStudentDetailsPage.aspx?id=" + str;
                    id = str;
                }

                tRow.Cells.Add(tCell);

            }
            TableCell tCellBP = new TableCell();
            Button passButton = new Button();
            passButton.ID = "passButton_" + i;
            passButton.Text = "עבר";
            passButton.Click += new EventHandler(Pass_Button_On_Click);
            tCellBP.Controls.Add(passButton);
            tRow.Cells.Add(tCellBP);


            TableCell tCellBF = new TableCell();
            Button failButton = new Button();
            failButton.ID = "failButton_" + i;
            failButton.Text = "נכשל";
            failButton.Click += new EventHandler(Fail_Button_On_Click);
            tCellBF.Controls.Add(failButton);
            tRow.Cells.Add(tCellBF);


            


            TableCell tCellBPA = new TableCell();
            Button paidButton = new Button();
            paidButton.ID = "paidButton_" + i;
            paidButton.Text = "שילם";
            paidButton.Click += new EventHandler(Paid_Button_On_Click);
            tCellBPA.Controls.Add(paidButton);
            tRow.Cells.Add(tCellBPA);

            if (!bl.getStudentUseSecoundShot(id, date, testCode) || !bl.getStudentUseFreeTests(id, date, testCode))
            {
                paidButton.Visible = false;
            }

            i++;

        }


    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("systemEntrencePage.aspx");

    }

    protected void Fail_Button_On_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));
        string id = todayTestTable.Rows[lineNum].Cells[0].Text;
        JBTestBL bl = new JBTestBL();

        Boolean retVal = bl.setStudentPassFailTests(id, "no");
        if (!retVal)
        {
            string message = "There was a problem updated your details. Please try again later.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);

        }

        Response.Redirect(Request.RawUrl);

    }

    protected void Pass_Button_On_Click(object sender, System.EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));
        string id = todayTestTable.Rows[lineNum].Cells[0].Text;
        JBTestBL bl = new JBTestBL();

        Boolean retVal = bl.setStudentPassFailTests(id, "yes");
        if (!retVal)
        {
            string message = "There was a problem updated your details. Please try again later.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);

        }
        Response.Redirect(Request.RawUrl);


    }

    protected void Paid_Button_On_Click(object sender, System.EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));
        string id = todayTestTable.Rows[lineNum].Cells[0].Text;
        JBTestBL bl = new JBTestBL();

        Boolean retVal = bl.setStudentPaidTests(id);
        if (!retVal)
        {
            string message = "There was a problem updated your details. Please try again later.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);

        }
        Response.Redirect(Request.RawUrl);


    }

    /*wait for confiremd table*/
    private void setTestWaitForConfirmedTable(LinkedList<String[]> TestConfirmedinfo)
    {
        int i = 1;
        foreach (String[] arr in TestConfirmedinfo)
        {

            TableRow tRow = new TableRow();
            waitForApproveSysTable.Rows.Add(tRow);
            foreach (String str in arr)
            {

                TableCell tCell = new TableCell();
                tCell.Text = str;
                tRow.Cells.Add(tCell);
            }
            TableCell tCellBC = new TableCell();
            Button confButton = new Button();
            confButton.ID = "confButton_" + i;
            confButton.Text = "הצג";
            confButton.Click += new EventHandler(Conf_Button_On_Click);
            tCellBC.Controls.Add(confButton);
            tRow.Cells.Add(tCellBC);


            i++;
        }

    }

    protected void Conf_Button_On_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));
        string id = waitForApproveSysTable.Rows[lineNum].Cells[0].Text;



        Response.Redirect("systemStudentDetailsPage.aspx?id=" + id);


    }


    /*wait for cancelation table*/

    private void setTestWaitForCancelTable(LinkedList<String[]> TestCancelinfo)
    {
        int i = 1;
        foreach (String[] arr in TestCancelinfo)
        {

            TableRow tRow = new TableRow();
            waitForCancelationSysTable.Rows.Add(tRow);
            foreach (String str in arr)
            {

                TableCell tCell = new TableCell();
                tCell.Text = str;
                if (str.Equals(arr.First()))
                {
                    studentIdConf = str;
                    LinkButton bId = new LinkButton();
                    bId.Text = str;
                    tCell.Controls.Add(bId);
                    bId.PostBackUrl = "systemStudentDetailsPage.aspx?id=" + str;
                }

                tRow.Cells.Add(tCell);
            }
            TableCell tCellBC = new TableCell();
            Button cancelButton = new Button();
            cancelButton.ID = "cancelButton_" + i;
            cancelButton.Text = "בטל";
            cancelButton.Click += new EventHandler(Cancel_Button_On_Click);
            tCellBC.Controls.Add(cancelButton);
            tRow.Cells.Add(tCellBC);


            i++;
        }

    }

    protected void Cancel_Button_On_Click(object sender, EventArgs e)
    {
        CalenderClass cal = new CalenderClass();
        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));


        string id = waitForCancelationSysTable.Rows[lineNum].Cells[0].Text;

        string tmp = waitForCancelationSysTable.Rows[lineNum].Cells[3].Text;
        string date = cal.changeDateFormat(tmp);

        JBTestBL bl = new JBTestBL();
        string testCode = bl.getStudentTestCodeByDateAndId(id, date);
        string courseCode = bl.getStudentCourseCode(id);
        Boolean freeFlag = bl.getStudentUseFreeTests(id, date, testCode);
        if (freeFlag)
            bl.setUpdateStudentFreeTest(1, id, courseCode);
        bl.setUpdateTestCounter(1, date, bl.getStudentCollageCode(id));
        Boolean cancelTest = bl.cancelApproveTestStudentTable(id, date, testCode);

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

}