using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;
public partial class systemReportDatePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        CalenderClass cal = new CalenderClass();
        string sDate = Request.QueryString["startDate"].ToString();
        string eDate = Request.QueryString["endDate"].ToString();
        //startDateTB.Text = sDate;
        //endDateTB.Text = eDate;
        sDateL.Text = sDate + "-";
        eDateL.Text = eDate;
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
        int i = 1;
        if (infoList != null)
            foreach (String[] arr in infoList)
            {
                TableRow tRow = new TableRow();
                reportTable.Rows.Add(tRow);
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
                TableCell tCellBP = new TableCell();
                Button showButton = new Button();
                showButton.ID = "showButton_" + i;
                showButton.Text = "הצג";
                showButton.Click += new EventHandler(Show_Button_On_Click);
                tCellBP.Controls.Add(showButton);
                tRow.Cells.Add(tCellBP);
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
        JBTestBL bl = new JBTestBL();
        Button clickButton = (Button)sender;
        int lineNum = Int32.Parse(clickButton.ID.Substring(clickButton.ID.IndexOf("_") + 1));
        String[] arr = bl.getSecoundShotAndHourById(reportTable.Rows[lineNum].Cells[0].Text);
        string Id = reportTable.Rows[lineNum].Cells[0].Text;
        string Tc = reportTable.Rows[lineNum].Cells[3].Text;
        string D = reportTable.Rows[lineNum].Cells[4].Text;
        string P = reportTable.Rows[lineNum].Cells[5].Text;
        string Ss = arr[0];
        string H = arr[1];
        Response.Redirect("printStudentDetailsAndTestInfo.aspx?id=" + Id + "&Tc=" + Tc + "&D=" + D + "&H=" + H + "&P=" + P + "&Ss=" + Ss);


    }
    protected void Change_Button_On_Click(object sender, EventArgs e)
    {

        string start = startDateTB.Text;
        string end = endDateTB.Text;
        Response.Redirect("systemReportDataPage.aspx?startDate=" + start + "&endDate=" + end);

    }

    protected void Show_Table_Button_On_Click(object sender, EventArgs e)
    {
        string start = sDateL.Text.Substring(0, sDateL.Text.IndexOf("-"));
        string end = eDateL.Text;
        Response.Redirect("systemPrintReportTablePage.aspx?startDate=" + start + "&endDate=" + end);

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