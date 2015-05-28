using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class studentSchedulePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        CalenderClass cal = new CalenderClass();
        if (Session["userName"] != null)
        {
            string userName = bl.getStudentFullNameById(Session["userName"].ToString());
            connectedUserLable.Text = "מחובר " + userName;
            dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        else
        {
            Response.Redirect("studentEntrencePage.aspx");
        }

    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("studentEntrencePage.aspx");

    }
}