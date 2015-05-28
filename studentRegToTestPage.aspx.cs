using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class studentSchedulePage : System.Web.UI.Page
{
    string lastDate;
    static string studentId;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["userName"] == null)
            Response.Redirect("studentEntrencePage.aspx");

        CalenderClass calender = new CalenderClass();

        string todayDate = calender.getTodayMonth().ToString()+"/"+calender.getTodayDay().ToString()+"/"+calender.getTodayYear().ToString();
        lastDate = calender.getTodayMonth().ToString() + "/" + calender.getdaysInMonth(calender.getTodayYear(), calender.getTodayMonth()).ToString() + "/" + calender.getTodayYear().ToString();

        if (Request.QueryString["month"] != null)
        {
            if (Request.QueryString["year"] != null)
                lastDate = Convert.ToInt32(Request.QueryString["month"].ToString()) + "/" + calender.getdaysInMonth(Int32.Parse(Request.QueryString["year"].ToString()),Convert.ToInt32(Request.QueryString["month"].ToString())).ToString() + "/" + Int32.Parse(Request.QueryString["year"].ToString());
            else
                lastDate = Convert.ToInt32(Request.QueryString["month"].ToString()) + "/" + calender.getdaysInMonth(calender.getTodayYear(), Convert.ToInt32(Request.QueryString["month"].ToString())).ToString() + "/" + calender.getTodayYear().ToString();
        }
        

        if (!Page.IsPostBack)
        {
            JBTestBL bl = new JBTestBL();
            if (Session["userName"] != null)
            {
                studentId = Session["userName"].ToString();
                connectedUserLable.Text = "מחובר " + bl.getStudentFullNameById(Session["userName"].ToString());
                dateLable.Text = calender.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                Response.Redirect("studentEntrencePage.aspx");
            }

          
            LinkedList<String> testList = bl.getTestsByCourseCode(studentId);
            if (testList != null)
            {
                foreach (string str in testList)
                    testCodeDropDownList.Items.Add(new ListItem(str)); 
            }

            LinkedList<String> dateList = bl.getAvailableDates(todayDate,lastDate,studentId);
            if (dateList != null)
            {
                foreach (string str in dateList)
                {
                    dateDropDownList.Items.Add(new ListItem(str));
                }
            }

           


        }
    }
    protected void Reg_Button_On_Click(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        Boolean retVal = false;
        string day = dateDropDownList.SelectedItem.Text.Substring(0, dateDropDownList.SelectedItem.Text.IndexOf("-"));
        string month = dateDropDownList.SelectedItem.Text.Substring(3,dateDropDownList.SelectedItem.Text.IndexOf("-"));
        string year = dateDropDownList.SelectedItem.Text.Substring(6);
        string date = month + "/" + day + "/" + year;
       
        if (Session["userName"] != null)
        {
            studentId = Session["userName"].ToString();
        }
        
        if (dateDropDownList.SelectedItem.Text.Equals("בחר") || testCodeDropDownList.SelectedItem.Text.Equals("בחר") || hourDropDownList.SelectedItem.Text.Equals("בחר"))
        {
            string message = "One of the details you entered is incorrect";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }

        else
        {
            string testCode = testCodeDropDownList.SelectedItem.Text.Substring(0, testCodeDropDownList.SelectedItem.Text.IndexOf(" ") + 1);
            Label2.Text = date;
            if (secondShotTB.Text.Equals(""))
                secondShotTB.Text = "-";
            if(!studentId.Equals(""))
               retVal = bl.setNewTestRegInfo(studentId, testCode, date, hourDropDownList.SelectedItem.Text,secondShotTB.Text);
            
            if (retVal)
            {
                string message = "Your request has been transferred to system manager";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                Response.Redirect("studentMainPage.aspx");
            }
            else
            {
                string message = "There was a problem with your registration, please try again later.";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            }
        }
    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("studentEntrencePage.aspx");

    }
}