using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Globalization;

public partial class calanderPage : System.Web.UI.UserControl
{
    int currMonth;
    int currYear;
    string url;
    JBTestBL bl;

    static string userName;
    static int maxNumOfTestPerDayTLV = 4;
    static int maxNumOfTestPerDayJER = 3;
    static int maxNumOfTestPerDay;
    protected void Page_Load(object sender, EventArgs e)
    {
        //save the last url
        url = HttpContext.Current.Request.Url.AbsolutePath;
        url = url.Remove(0, 1);

        bl = new JBTestBL();
        CalenderClass calClass = new CalenderClass();

        string todayFullDate = calClass.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        int today = Int32.Parse(todayFullDate.Substring(0, 2));

        if (Session["userName"] != null)
        {
            userName = Session["userName"].ToString();
        }
        
     
        //set year
        if (Request.QueryString["year"] != null)
            currYear = Int32.Parse(Request.QueryString["year"].ToString());
        else
            currYear = calClass.getTodayYear();

        //set month
        if (Request.QueryString["month"] != null)
            currMonth = Convert.ToInt32(Request.QueryString["month"].ToString());
        else
            currMonth = calClass.getTodayMonth();       

        int dayInMonth = calClass.getdaysInMonth(currYear, currMonth);
        int startDayOfMonth = calClass.getFirstDayOfMonth(currYear, currMonth);

        //set lable
        hebMonthNameLB.Text = calClass.getHebMonthName(currMonth);

        int dayNum = 1;
        for (int i = 0; i < 6; i++) //for each week in the month
        {
            if (dayNum > dayInMonth)
                break;
            //create new row
            TableRow tRow = new TableRow();
            calenderTableID.Rows.Add(tRow);

            for (int j = 0; j < 7; j++) //for each day in the week
            {
                //create new cell
                TableCell tCell = new TableCell();
                tRow.Cells.Add(tCell);

                if (dayNum <= dayInMonth)
                {
                    if ((i == 0 && j >= startDayOfMonth) || i != 0)
                    {
                        //mark current date on calender
                        if (dayNum == today && currMonth == calClass.getTodayMonth() && currYear == calClass.getTodayYear())
                        {
                            calenderTableID.Rows[i + 1].Cells[j].BorderStyle = BorderStyle.Ridge;
                            calenderTableID.Rows[i + 1].Cells[j].BorderColor = Color.Silver;
                            calenderTableID.Rows[i + 1].Cells[j].BorderWidth = 10;
                        }

                        if (j == 0 || j == 5 || j == 6) //if sunday friday or saturday not working days
                        {
                            calenderTableID.Rows[i + 1].Cells[j].BackColor = Color.LightGray;
                        }
                        else //other days in the week - set scheduleDB
                        {

                            string strDate = currMonth + "/" + dayNum + "/" + currYear;
                            int testInDay;
                            if (url.Contains("system"))
                            {
                                testInDay = bl.getNumOfTestsInDay(strDate, userName, "staff");
                                if (bl.getStaffWorkCollageId(userName).Equals("JER"))
                                    maxNumOfTestPerDay = maxNumOfTestPerDayJER;
                                else
                                    maxNumOfTestPerDay = maxNumOfTestPerDayTLV;
                            }
                            else
                            {
                                testInDay = bl.getNumOfTestsInDay(strDate, userName, "student");
                                if(bl.getStudentCollageCode(userName).Equals("JER"))
                                    maxNumOfTestPerDay = maxNumOfTestPerDayJER;
                                else
                                    maxNumOfTestPerDay = maxNumOfTestPerDayTLV;
                            }
                            if (bl.getDateExistence(strDate).Equals("")) //if date does not exist
                            {
                                bl.setScheduleByCalender(strDate); //set new schedule date
                                if (url.Contains("system"))
                                    bl.setScheduleInCollageByCalender(strDate, maxNumOfTestPerDay, userName, "staff"); //set new schedule in collage date
                                else
                                    bl.setScheduleInCollageByCalender(strDate, maxNumOfTestPerDay, userName, "student"); //set new schedule in collage date
                            }
                            else
                            {
                                if (url.Contains("system"))
                                { 
                                    if (bl.getDateExistence(strDate, userName,"staff").Equals("")) //if date in collage does not exist
                                            bl.setScheduleInCollageByCalender(strDate, maxNumOfTestPerDay, userName, "staff"); //set new schedule in collage date
                                }
                                else
                                {
                                    if (bl.getDateExistence(strDate, userName, "student").Equals("")) //if date in collage does not exist
                                      bl.setScheduleInCollageByCalender(strDate, maxNumOfTestPerDay, userName, "student"); //set new schedule in collage date
                                }
                            }

                            if (testInDay == 0) //check if num of tests in date is full
                            {
                                calenderTableID.Rows[i + 1].Cells[j].BackColor = Color.Red;
                            }
                        }
                        string temp = dayNum.ToString();
                        tCell.Text = temp;
                        tCell.Height = 80;

                        //for system page write all students tests
                        string date = currMonth + "/" + dayNum + "/" + currYear;

                        LinkedList<String> studentTestList = bl.getAllConfirmedTests(date, userName);
                        if (studentTestList != null)
                        {
                            if (url.Contains("system"))
                            {
                                foreach (string s in studentTestList)
                                {
                                    string str = "\r\n" + s;
                                    string modifiedString = str.Replace(Environment.NewLine, "<br />").Replace("\r", "<br />").Replace("\n", "<br />");
                                    tCell.Text += modifiedString;
                                }
                            }
                        }

                        //for current student schedule page write all his confirmed tests
                       
                        LinkedList<String> studentIDTestList = bl.getAllConfirmedTestsByStudentId(userName, date);
                        if (studentIDTestList != null)
                        {
                            if (url.Contains("studentSchedulePage"))
                            {
                                foreach (string s in studentIDTestList)
                                {
                                    string str = "\r\n" + s;
                                    string modifiedString = str.Replace(Environment.NewLine, "<br />").Replace("\r", "<br />").Replace("\n", "<br />");
                                    tCell.Text += modifiedString;
                                }
                            }
                        }

                        dayNum++;
                    }
                    else
                    {
                        calenderTableID.Rows[i + 1].Cells[j].BackColor = Color.Gray; //not days in curr month
                    }
                }
                else
                {
                    calenderTableID.Rows[i + 1].Cells[j].BackColor = Color.Gray; //not days in curr month
                }
            }

        }

    }

    protected void Next_Month_Button_On_Click(object sender, EventArgs e)
    {
        if (currMonth + 1 <= 12)
        {
            currMonth++;
            Response.Redirect(url + "?month=" + currMonth + "&year=" + currYear);
        }
        else
        {
            currMonth = 1;
            currYear++;
            Response.Redirect(url + "?month=" + currMonth + "&year=" + currYear);
        }
    }

    protected void Prev_Month_Button_On_Click(object sender, EventArgs e)
    {
        if (currMonth - 1 >= 1)
        {
            currMonth--;
            Response.Redirect(url + "?month=" + currMonth + "&year=" + currYear);
        }
        else
        {
            currMonth = 12;
            currYear--;
            Response.Redirect(url + "?month=" + currMonth + "&year=" + currYear);
        }
    }
}