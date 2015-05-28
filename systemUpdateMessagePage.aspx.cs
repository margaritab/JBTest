using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class systemUpdateMessagePage : System.Web.UI.Page
{
    static string userName;
    static string date;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
            Response.Redirect("systemEntrencePage.aspx");

        if (!IsPostBack)
        {
            JBTestBL bl = new JBTestBL();
            CalenderClass cal = new CalenderClass();
            LinkedList<String> messageList = new LinkedList<string>();
            if (Session["userName"] != null && Request.QueryString["date"] != null && Request.QueryString["message"] != null)
            {
                string name = bl.getSystemFullNameById(Session["userName"].ToString());
                connectedUserLable.Text = "מחובר " + name;
                dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                userName = Session["userName"].ToString();
                date = cal.changeDateFormat(Request.QueryString["date"].ToString());
                messageList = bl.getMessagesByDateAndCollageId(userName, date);
                foreach(string str in messageList)
                    if (str.Contains(Request.QueryString["message"].ToString()))
                        messageTA.Text = str;              
            }
            else
            {
                Response.Redirect("systemEntrencePage.aspx");
            }
        }
    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("systemEntrencePage.aspx");

    }
    protected void Update_Message_On_Click(object sender, EventArgs e)
    {
        string message = "";
        Boolean retVal = false;
        JBTestBL bl = new JBTestBL();

        if (messageTA.Text.Equals(""))
        {
            message = "The message field is empty";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
        else
        {
            if (!userName.Equals(""))
                retVal = bl.setUpdateMessage(date,messageTA.Text,userName);
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
