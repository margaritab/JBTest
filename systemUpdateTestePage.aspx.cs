using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;
public partial class systemUpdateTestePage : System.Web.UI.Page
{
    static string oldCode = "";
    static string oldName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
            Response.Redirect("systemEntrencePage.aspx");

        if (!IsPostBack)
        {
            JBTestBL bl = new JBTestBL();
            CalenderClass cal = new CalenderClass();
            if (Session["userName"] != null)
            {
                string name = bl.getSystemFullNameById(Session["userName"].ToString());
                connectedUserLable.Text = "מחובר " + name;
                dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                oldCode = Request.QueryString["testCode"].ToString();
                testCode_TB.Text = oldCode;
                oldName = bl.getTestNameByCode(oldCode);
                testName_TB.Text = oldName;
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

    protected void Update_Test_On_Click(object sender, EventArgs e)
    {
        
        string newCode = testCode_TB.Text;
        string newName = testName_TB.Text;
        JBTestBL bl = new JBTestBL();
        bl.setUpdateTestInfo(newCode, newName, oldCode, oldName);
       Response.Redirect("systemTestsPage.aspx");
    }
    protected void Cancel_Test_On_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("systemTestsPage.aspx");
    }
}