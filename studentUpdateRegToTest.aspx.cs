using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class studentUpdateRegToTest : System.Web.UI.Page
{
    static string currSecondShot = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
            Response.Redirect("studentEntrencePage.aspx");

        if(!Page.IsPostBack)
        {
            CalenderClass calender = new CalenderClass();
            JBTestBL bl = new JBTestBL();
            LinkedList<String> testList = new LinkedList<String>();
            
            
            string todayDate = calender.getTodayMonth().ToString() + "/" + calender.getTodayDay().ToString() + "/" + calender.getTodayYear().ToString();
            string lastDate = calender.getTodayMonth().ToString() + "/" + calender.getdaysInMonth(calender.getTodayYear(), calender.getTodayMonth()).ToString() + "/" + calender.getTodayYear().ToString();

            String studentId = "";
            
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


            if(Request.QueryString["tc"] != null && Request.QueryString["d"] != null && Request.QueryString["h"] != null)
            {
                testCodeDropDownListDL.Items.Add(new ListItem(bl.getFullStudentTestCode(Request.QueryString["tc"].ToString())));
                dateDropDownListDL.Items.Add(new ListItem(Request.QueryString["d"].ToString()));
                hourDropDownListDL.Items.Add(new ListItem(Request.QueryString["h"].ToString()));

                string day = dateDropDownListDL.SelectedItem.Text.Substring(0, dateDropDownListDL.SelectedItem.Text.IndexOf("-"));
                string month = dateDropDownListDL.SelectedItem.Text.Substring(3, dateDropDownListDL.SelectedItem.Text.IndexOf("-"));
                string year = dateDropDownListDL.SelectedItem.Text.Substring(6);
                day = month + "/" + day + "/" + year;
                
                secondShotTB.Text = bl.getSecondshot(Request.QueryString["tc"], day ,studentId);
               
            }
            currSecondShot = secondShotTB.Text;
           
            LinkedList<String> codeList = bl.getTestsByCourseCode(studentId);
            if (codeList != null)
            {
                foreach (string str in codeList)
                {
                    if (!str.Equals(testCodeDropDownListDL.Text))
                        testCodeDropDownListDL.Items.Add(new ListItem(str));
                }
            }

            LinkedList<String> dateList = bl.getAvailableDates(todayDate, lastDate,studentId);
            if (dateList != null)
            {
                foreach (string str in dateList)
                {
                    dateDropDownListDL.Items.Add(new ListItem(str));
                }
            }

            hourDropDownListDL.Items.Add(new ListItem("10:00"));
            hourDropDownListDL.Items.Add(new ListItem("10:30"));
            hourDropDownListDL.Items.Add(new ListItem("11:00"));
            hourDropDownListDL.Items.Add(new ListItem("11:30"));
            hourDropDownListDL.Items.Add(new ListItem("12:00"));
            hourDropDownListDL.Items.Add(new ListItem("12:30"));
            hourDropDownListDL.Items.Add(new ListItem("13:00"));
            
            
        }

    }
    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("studentEntrencePage.aspx");

    }
    protected void Reg_Update_Button_On_Click(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        Boolean retVal = false;

        string studentId = "";
        string changedTc = testCodeDropDownListDL.SelectedItem.Text;
        string currTc = bl.getFullStudentTestCode(Request.QueryString["tc"].ToString());
        string changedDate = dateDropDownListDL.SelectedItem.Text;
        string currDate = Request.QueryString["d"].ToString();
        string changedHour = hourDropDownListDL.SelectedItem.Text;
        string currHour = Request.QueryString["h"].ToString();

        string day = currDate.Substring(0, currDate.IndexOf("-"));
        string month = currDate.Substring(3, currDate.IndexOf("-"));
        string year = currDate.Substring(6);
        currDate = month + "/" + day + "/" + year;

        string changedSs = secondShotTB.Text;
                
        currTc = currTc.Substring(0,currTc.IndexOf(" "));
        changedTc = changedTc.Substring(0, changedTc.IndexOf(" "));


        if (Session["userName"] != null)
        {
            studentId = Session["userName"].ToString();
        }
        if (!changedDate.Equals(currDate))
        {
            day = dateDropDownListDL.SelectedItem.Text.Substring(0, dateDropDownListDL.SelectedItem.Text.IndexOf("-"));
            month = dateDropDownListDL.SelectedItem.Text.Substring(3, dateDropDownListDL.SelectedItem.Text.IndexOf("-"));
            year = dateDropDownListDL.SelectedItem.Text.Substring(6);
            changedDate = month + "/" + day + "/" + year;
        }

        
        if (!studentId.Equals(""))
            retVal = bl.updateNewTestRegInfo(studentId, changedTc, changedDate, changedHour, changedSs, currTc, currDate, currHour, currSecondShot);

        if (retVal)
        {
            string message = "Your request has been transferred to system manager";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            
        }
        else
        {
            string message = "There was a problem with your registration, please try again later.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }

        Response.Redirect("studentMainPage.aspx");
    }
}