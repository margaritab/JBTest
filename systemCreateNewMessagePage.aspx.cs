using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class systemCreateNewMessagePage : System.Web.UI.Page
{
    static string userName;
    protected void Page_Load(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        CalenderClass cal = new CalenderClass();
        if (Session["userName"] != null)
        {
            string name = bl.getSystemFullNameById(Session["userName"].ToString());
            connectedUserLable.Text = "מחובר " + name;
            dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            userName = Session["userName"].ToString();
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
    protected void Create_Message_On_Click(object sender, EventArgs e)
    {
        string message = "";
        Boolean retVal = false;
        CalenderClass cal = new CalenderClass();
        JBTestBL bl = new JBTestBL();

        string date = cal.changeDateFormat(dateLable.Text);

        if (messageTA.Text.Equals(""))
        {
            message = "The message field is empty";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
        else
        {
            if (!userName.Equals(""))
                retVal = bl.setNewMessage(date,messageTA.Text,userName);

        }

        Response.Redirect("systemMessages.aspx");
    }
    protected void Cancel_Message_On_Click(object sender, EventArgs e)
    {
        Response.Redirect("systemMessages.aspx");

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