using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class testsHistoryPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        CalenderClass cal = new CalenderClass();
        LinkedList<String[]> historyTests = new LinkedList<string[]>();

        if (Session["userName"] != null)
        {
            string userName = bl.getStudentFullNameById(Session["userName"].ToString());
            connectedUserLable.Text = "מחובר " + userName;
            dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            historyTests = bl.getTestsHistoryById(Session["userName"].ToString());
        }
        else
        {
            Response.Redirect("studentEntrencePage.aspx");
        }


        testsHistoryTable(historyTests);
    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("studentEntrencePage.aspx");

    }
    private void testsHistoryTable(LinkedList<String[]> historyList)
    {
        foreach (String[] arr in historyList)
        {
            TableRow tRow = new TableRow();
            testsHistory.Rows.Add(tRow);
            foreach (String str in arr)
            {

                TableCell tCell = new TableCell();

                tRow.Cells.Add(tCell);
                tCell.Text = str;
            }
        }
    }
}