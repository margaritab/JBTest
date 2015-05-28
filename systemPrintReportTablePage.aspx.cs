using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;
public partial class systemPrintReportTablePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        CalenderClass cal = new CalenderClass();
        JBTestBL bl = new JBTestBL();

        string sDate = Request.QueryString["startDate"].ToString();
        string eDate = Request.QueryString["endDate"].ToString();
        startDateL.Text = sDate + "-";
        endDateL.Text = eDate;

        LinkedList<String[]> infoList = null;
        if (Session["userName"] != null)
        {
            string name = bl.getSystemFullNameById(Session["userName"].ToString());
            connectedUserLable.Text = "מחובר " + name;
            dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            infoList = bl.getStudentTestReport(sDate, eDate, Session["userName"].ToString());
        }
        else
        {
            Response.Redirect("systemEntrencePage.aspx");
        }

        if (infoList != null)
            foreach (String[] arr in infoList)
            {
                TableRow tRow = new TableRow();
                reportTable.Rows.Add(tRow);
                foreach (String str in arr)
                {

                    TableCell tCell = new TableCell();
                    tCell.Width = 100;
                    tCell.Text = str;
                    tRow.Cells.Add(tCell);

                }
            }
    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("systemEntrencePage.aspx");

    }

    protected void Print_Button_On_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(Page.GetType(), "RunPrintReport", "window.print();", true);
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