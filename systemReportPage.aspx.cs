using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class systemReportPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
            Response.Redirect("systemEntrencePage.aspx");
    }

    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("systemEntrencePage.aspx");

    }
    protected void Show_Button_On_Click(object sender, EventArgs e)
    {

        string sDate = StartDateTB.Text;
        string eDate = EndDateTB.Text;
        Response.Redirect("systemReportDataPage.aspx?startDate=" + sDate + "&endDate=" + eDate);

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