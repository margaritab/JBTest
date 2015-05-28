using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class studentMainPage : System.Web.UI.Page
{
    static string status="בטל";
    string studentId;
    protected void Page_Load(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        CalenderClass cal = new CalenderClass();
        LinkedList<String[]> infoListApprove = new LinkedList<string[]>();
        LinkedList<String[]> infoListNotApprove = new LinkedList<string[]>();
        LinkedList<String[]> messagesList = new LinkedList<string[]>();

        if (Session["userName"] != null)
        {
            infoListApprove = bl.getApprovedTests(Session["userName"].ToString());
            infoListNotApprove = bl.getNotApprovedTests(Session["userName"].ToString());
            messagesList = bl.getCurrMessages(Session["userName"].ToString(),"student");

            usernameLB.Text = bl.getStudentFullNameById(Session["userName"].ToString());
            studentId = Session["userName"].ToString();
            connectedUserLable.Text = "מחובר " + bl.getStudentFullNameById(Session["userName"].ToString()); ;
            dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        else
        {
            Response.Redirect("studentEntrencePage.aspx");
        }
        setMessageBox(messagesList);
        approvedTable(infoListApprove);
        notApprovedTable(infoListNotApprove);
    }

    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("studentEntrencePage.aspx");

    }
    private void setMessageBox(LinkedList<String[]> msgList)
    {
        Boolean dateflag = false;
        string htmlStr = "";
        foreach (String[] arr in msgList)
        {
            htmlStr = "<p>";
            dateflag = true;
            foreach (String str in arr)
            {
                if (dateflag)
                {
                    dateflag = false;
                    htmlStr += str + ":";
                }
                else
                    htmlStr += str;
                htmlStr += "<br />";
            }
            htmlStr += "</p>";
            staffMessages.InnerHtml += htmlStr;//enter the string to the screen
        }
    }

    private void approvedTable(LinkedList<String[]> infoList)
    {
        int c = 1;
        foreach (String[] arr in infoList)
        {
            TableRow tRow = new TableRow();
            testApproved.Rows.Add(tRow);
            foreach (String str in arr)
            {

                TableCell tCell = new TableCell();
                tCell.Text = str;
                tRow.Cells.Add(tCell);
            }

            TableCell tCellBtn = new TableCell();
            Button cancelButton = new Button();
            cancelButton.ID = "cancelButtonApprove_" + c;
            cancelButton.Width = 80;

            cancelButton.Text = status;
            cancelButton.Click += new EventHandler(Cancel_Approve_On_Click);
            tCellBtn.Controls.Add(cancelButton);
            tRow.Cells.Add(tCellBtn);
            c++;
        }
    }
    private void notApprovedTable(LinkedList<String[]> infoList)
    {
        int c = 1;
        
        foreach (String[] arr in infoList)
        {
            TableRow tRow = new TableRow();
            waitForApprove.Rows.Add(tRow);
            foreach (String str in arr)
            {

                TableCell tCell = new TableCell();
                tCell.Text = str;
                tRow.Cells.Add(tCell);
            }

            TableCell tCellBtn1 = new TableCell();
            Button updateButton = new Button();
            updateButton.ID = "updaeButton_" + c;
            updateButton.Width = 60;
            updateButton.Text = "עדכן";
            updateButton.Click += new EventHandler(Update_On_Click);
            tCellBtn1.Controls.Add(updateButton);
            tRow.Cells.Add(tCellBtn1);

            TableCell tCellBtn2 = new TableCell();
            Button cancelButton2 = new Button();
            cancelButton2.ID = "cancelButtonNotApprove_" + c;
            cancelButton2.Width = 60;
            cancelButton2.Text = "בטל";
            cancelButton2.Click += new EventHandler(Cancel_Wait_For_Approve_On_Click);
            tCellBtn2.Controls.Add(cancelButton2);
            tRow.Cells.Add(tCellBtn2);
            c++;
        }
    }
    protected void Cancel_Approve_On_Click(object sender, EventArgs e)
    {
        CalenderClass cal = new CalenderClass();
        Button button1 = (Button)sender;
        int lineNum = Int32.Parse(button1.ID.Substring(button1.ID.IndexOf("_")+1));
        string testCode = testApproved.Rows[lineNum].Cells[1].Text;
        string date = testApproved.Rows[lineNum].Cells[3].Text;
        
        date = cal.changeDateFormat(date);
        JBTestBL bl = new JBTestBL();
        //dateLable.Text = date;
        if (button1 != null)
        {
            if (bl.getCancelButtonWaitStatus(studentId, date, testCode))
            {
                status = "בטל";
                button1.Text = status;
                 bl.setChangeApproveTestStudentTable(studentId, testCode); //cancelled - Nyet
            }
            else
            {
                status = "ממתין לביטול";
                button1.Text = status;
                bl.setApproveTestStudentTable(studentId, testCode); //cancelled - wait
            }
        }
    }

    protected void Cancel_Wait_For_Approve_On_Click(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        CalenderClass cal = new CalenderClass();
        int lineNum = Int32.Parse(button.ID.Substring(button.ID.IndexOf("_") + 1));
        string testCode = waitForApprove.Rows[lineNum].Cells[1].Text;
        string date = waitForApprove.Rows[lineNum].Cells[3].Text;
        date = cal.changeDateFormat(date);
        dateLable.Text = date;

        JBTestBL bl = new JBTestBL();

        if (button != null)
        {
            bl.cancelApproveTestStudentTable(studentId, date, testCode);
            Response.Redirect("studentMainPage.aspx");
        }

    }

    
    protected void Update_On_Click(object sender, EventArgs e)
    {
        Button updatebutton = (Button)sender;
        JBTestBL bl = new JBTestBL();
        int lineNum = Int32.Parse(updatebutton.ID.Substring(updatebutton.ID.IndexOf("_")+1));
        
        String courseCode = waitForApprove.Rows[lineNum].Cells[0].Text;
        String testCode = waitForApprove.Rows[lineNum].Cells[1].Text;
        String date = waitForApprove.Rows[lineNum].Cells[3].Text;
        String hour = waitForApprove.Rows[lineNum].Cells[4].Text;

        if (updatebutton != null)
        {
            updatebutton.Text = "מעדכן";
        }
        Response.Redirect("studentUpdateRegToTest.aspx?cc =" + courseCode + " &tc="+testCode+"&h="+hour+"&d="+date);

    }

}

