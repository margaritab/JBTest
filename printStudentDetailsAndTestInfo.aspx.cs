using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class printStudentDetailsAndTestInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        if (!Page.IsPostBack)
        {
            CalenderClass cal = new CalenderClass();

            if (Session["userName"] != null)
            {
                string userName = bl.getSystemFullNameById(Session["userName"].ToString());
                connectedUserLable.Text = "מחובר " + userName;
                dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
    
            if (Request.QueryString["id"] != null && Request.QueryString["Tc"] != null && Request.QueryString["H"] != null
                && Request.QueryString["D"] != null && Request.QueryString["P"] != null && Request.QueryString["Ss"] != null)
            {
                Id_Info_LB.Text = Request.QueryString["id"].ToString();
                Test_Code_Info_LB.Text = Request.QueryString["Tc"].ToString();
                Date_Info_LB.Text = Request.QueryString["D"].ToString();
                Hour_Info_LB.Text = Request.QueryString["H"].ToString();
                Pass_Info_LB.Text = Request.QueryString["P"].ToString();
                Second_Shot_Info_LB.Text = Request.QueryString["Ss"].ToString();
            }

            Sr_Info_LB.Text = bl.getStudentSr(Id_Info_LB.Text);
            First_Name_Info_LB.Text = bl.getStudentFullNameById(Id_Info_LB.Text).Substring(0, bl.getStudentFullNameById(Id_Info_LB.Text).IndexOf(" "));
            Last_Name_Info_LB.Text = bl.getStudentFullNameById(Id_Info_LB.Text).Substring(bl.getStudentFullNameById(Id_Info_LB.Text).IndexOf(" ") + 1);
            Email_Info_LB.Text = bl.getStudentEmail(Id_Info_LB.Text);
            if (!bl.getStudentSecPhone(Id_Info_LB.Text).Equals(""))
                Prime_Phone_Info_LB.Text = bl.getStudentPrimePhone(Id_Info_LB.Text) + "/" + bl.getStudentSecPhone(Id_Info_LB.Text);
            else
                Prime_Phone_Info_LB.Text = bl.getStudentPrimePhone(Id_Info_LB.Text);
            First_Name_Eng_Info_LB.Text = bl.getStudentEngFirstName(Id_Info_LB.Text);
            Last_Name_Eng_Info_LB.Text = bl.getStudentEngLastName(Id_Info_LB.Text);
            Addr_Info_LB.Text = bl.getStudentAddr(Id_Info_LB.Text);
            City_Info_LB.Text = bl.getStudentCity(Id_Info_LB.Text);
            Collage_Info_LB.Text = bl.getStudentCollageName(Id_Info_LB.Text);
            Course_Info_LB.Text = bl.getStudentCourseCode(Id_Info_LB.Text);

            string date = cal.changeDateFormat(Date_Info_LB.Text);

            if (bl.getStudentUseSecoundShot(Id_Info_LB.Text, date, Test_Code_Info_LB.Text))
                Cost_Info_LB.Text = "ללא עלות - שימוש בsecond shot";
            else if (bl.getStudentUseFreeTests(Id_Info_LB.Text, date, Test_Code_Info_LB.Text))
                Cost_Info_LB.Text = "ללא עלות";
            else if (bl.getStudentPaidForTests(Id_Info_LB.Text, date, Test_Code_Info_LB.Text))
            {
                Cost_Info_LB.Text = "שולם";
            }
            else
            {
                Cost_Info_LB.Text = "לא שולם";
            }
        }
    }

    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("studentEntrencePage.aspx");

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

    protected void Print_Button_On_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(Page.GetType(), "RunPrintReport", "window.print();", true);
    }

}