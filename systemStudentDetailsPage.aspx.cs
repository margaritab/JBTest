using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Drawing.Printing;

public partial class systemStudentDetailsPage : System.Web.UI.Page
{
    static string staffUserName;
    static int tests;
    static string originalCollage;
    static string originalCourse;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
            Response.Redirect("systemEntrencePage.aspx");

        JBTestBL bl = new JBTestBL();
        if (!Page.IsPostBack)
        {           
            CalenderClass cal = new CalenderClass();

            if (Request.QueryString["id"] != null || Session["userName"] != null)
            {
                Id_TB.Text = Request.QueryString["id"].ToString();
                connectedUserLable.Text = "מחובר " + bl.getSystemFullNameById(Session["userName"].ToString());
                dateLable.Text = cal.getTodayFullDate().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                staffUserName = Session["userName"].ToString();
                tests = bl.getTestsAmount(Id_TB.Text);
                originalCollage = bl.getStudentCollageName(Id_TB.Text);
                originalCourse = bl.getCourseById(Id_TB.Text);
            }
            else
            {
                Response.Redirect("systemEntrencePage.aspx");
            }
       
            
            sr_TB.Text = bl.getStudentSr(Id_TB.Text);
            First_Name_TB.Text = bl.getStudentFullNameById(Id_TB.Text).Substring(0, bl.getStudentFullNameById(Id_TB.Text).IndexOf(" "));
            Last_Name_TB.Text = bl.getStudentFullNameById(Id_TB.Text).Substring(bl.getStudentFullNameById(Id_TB.Text).IndexOf(" ") + 1);
            Email_TB.Text = bl.getStudentEmail(Id_TB.Text);
            Prime_Phone_TB.Text = bl.getStudentPrimePhone(Id_TB.Text);
            Sec_Phone_TB.Text = bl.getStudentSecPhone(Id_TB.Text);
            First_Name_Eng_TB.Text = bl.getStudentEngFirstName(Id_TB.Text);
            Last_Name_Eng_TB.Text = bl.getStudentEngLastName(Id_TB.Text);
            Addr_TB.Text = bl.getStudentAddr(Id_TB.Text);
            City_TB.Text = bl.getStudentCity(Id_TB.Text);
            Collage_DL.Items.Add(new ListItem(bl.getStudentCollageName(Id_TB.Text)));
            Course_DL.Items.Add(new ListItem(bl.getStudentCourseCode(Id_TB.Text)));
            LinkedList<String> collageList = bl.getAllCollagesNames();
            if (collageList != null)
            {
                foreach (string str in collageList)
                {
                    if (!str.Equals(Collage_DL.Text))
                        Collage_DL.Items.Add(new ListItem(str));
                }
            }
            FreeTestNum_DL.Text = bl.getStudentFreeTestNum(Id_TB.Text, bl.getCollageCodeByCollageName(bl.getStudentCollageName(Id_TB.Text).ToString()).ToString(), bl.getStudentCourseCode(Id_TB.Text).ToString()).ToString();
        }
         
        LinkedList<String[]> appList = bl.getStudentsTestToApprove(Id_TB.Text);
        if (appList != null)
            Approve_Tests_For_Student(appList);

        LinkedList<String[]> closeList = bl.getStudentsCloseTests(Id_TB.Text);
        if (closeList != null)
            Close_Tests_For_Student(closeList);

        LinkedList<String[]> pastList = bl.getStudentsPastTests(Id_TB.Text);
        if (closeList != null)
            Past_Tests_For_Student(pastList);

    }
     protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("systemEntrencePage.aspx");

    }
    protected void Load_Courses(object sender, EventArgs e)
    {
        Course_DL.Items.Clear();

        string collageName = Collage_DL.SelectedValue.ToString();
        JBTestBL bl = new JBTestBL();
        LinkedList<String> courseList = bl.getAllCoursesNamesInCollage(collageName);
        if (courseList != null)
        {
            foreach (string str in courseList)
                Course_DL.Items.Add(new ListItem(str));
        }

    }


    protected void Update_Bottun_On_Click(object sender, EventArgs e)
    {
        string message = "";
        String password = "";
        Boolean retVal = false;
        Boolean addStudentFlag = true;
        JBTestBL bl = new JBTestBL();
        if (Session["password"] != null)
            password = Session["password"].ToString();
        //check if one of the textboxes is empty
        if (First_Name_TB.Text.Equals("") || Last_Name_TB.Text.Equals("") ||
           Prime_Phone_TB.Text.Equals("") || Email_TB.Text.Equals("") ||
           First_Name_Eng_TB.Text.Equals("") || Last_Name_Eng_TB.Text.Equals("") || Addr_TB.Text.Equals(""))
        {
            message = "One of the details you entered is incorrect";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            addStudentFlag = false;
        }

        //check if the english fields are in english letters only
        Boolean matchFirstName = Regex.IsMatch(First_Name_Eng_TB.Text, "^[a-zA-Z ]*$");
        Boolean matchLastName = Regex.IsMatch(Last_Name_Eng_TB.Text, "^[a-zA-Z ]*$");
        Boolean matchAddress = Regex.IsMatch(Addr_TB.Text, "^[a-zA-Z0-9 ]*$");
        if (!matchFirstName || !matchLastName || !matchAddress)
        {
            message = "One of the english only fields are incorrect";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            addStudentFlag = false;
        }
        Boolean matchMail = Regex.IsMatch(Email_TB.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
        if (!matchMail)
        {
            message = "Email is not valid";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            addStudentFlag = false;
        }
        //encript password/hash table
        JBTestSecurity securePass = new JBTestSecurity();
        string encryptPass = securePass.CreateHash(password);
        if (!encryptPass.Equals("") && addStudentFlag)
        {
            retVal = bl.setUpdatedStudentDetailesBySystem(Id_TB.Text, First_Name_TB.Text, Last_Name_TB.Text, encryptPass,
                Prime_Phone_TB.Text, Sec_Phone_TB.Text, Email_TB.Text, First_Name_Eng_TB.Text, Last_Name_Eng_TB.Text,
                Addr_TB.Text, City_TB.Text, Collage_DL.SelectedItem.Text, Course_DL.SelectedItem.Text,
                sr_TB.Text, originalCollage, originalCourse, tests);
        }
        if (retVal)
        {
            message = "Your details has successfuly been updated.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            Response.Redirect("systemStudentDetailsPage.aspx?id=" + Id_TB.Text);
        }
        else
        {
            message = "There was a problem updated your details. Please try again later.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);

        }
    }


    private void Approve_Tests_For_Student(LinkedList<String[]> approveList)
    {
        int i = 1;

        foreach (String[] arr in approveList)
        {
            TableRow tRow = new TableRow();
            approveTestTabel.Rows.Add(tRow);

            foreach (string str in arr)
            {
                TableCell tCell = new TableCell();
                tRow.Cells.Add(tCell);

                tCell.Text = str;
            }


            //approve button

            TableCell tCellBP = new TableCell();
            Button appButton = new Button();
            appButton.ID = "appButton_" + i;
            appButton.Text = "אשר";
            appButton.Click += new EventHandler(Approve_Button_On_Click);
            tCellBP.Controls.Add(appButton);
            tRow.Cells.Add(tCellBP);


            //use free test database
            TableCell tCellFCB = new TableCell();
            CheckBox freeCB = new CheckBox();
            freeCB.ID = "freeCB_" + i;
            tCellFCB.Controls.Add(freeCB);
            tRow.Cells.Add(tCellFCB);


            //use second shot
            TableCell tCellSCB = new TableCell();
            CheckBox secCB = new CheckBox();
            secCB.ID = "secCB_" + i;
            tCellSCB.Controls.Add(secCB);
            tRow.Cells.Add(tCellSCB);

            i++;
        }



    }


    protected void Approve_Button_On_Click(object sender, EventArgs e)
    {
        Boolean retVal = false;

        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));

       
        string testCode = approveTestTabel.Rows[lineNum].Cells[0].Text;
        CalenderClass cal = new CalenderClass();
        string tmp = approveTestTabel.Rows[lineNum].Cells[1].Text;
       
        string date = cal.changeDateFormat(tmp);
       
        string secShot = approveTestTabel.Rows[lineNum].Cells[3].Text;
        JBTestBL bl = new JBTestBL();

        Boolean isSSChecked = ((CheckBox)approveTestTabel.Rows[lineNum].FindControl("secCB_" + lineNum)).Checked;
        Boolean isFTChecked = ((CheckBox)approveTestTabel.Rows[lineNum].FindControl("freeCB_" + lineNum)).Checked;
        
        //second shot checkbox is checked
        if (isSSChecked)
        {
            if (secShot.Equals("-")) //no sec shot
            {
                string message = "This second shot number is not valid";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            }
            else if (!secShot.Equals("-") && bl.getSecondShotWasUsed(secShot)) //sec shot was already in use
            {
                string message = "This second shot number was already used";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            }
            else //set sec shot to yes value - use sec shot
            {
                retVal = bl.setConfirmStudentsTest(Id_TB.Text, testCode, date, "no", "yes", staffUserName);
                if (retVal)
                {
                    string message = "The student test was confirmed succesfully";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                }
            }
        }
        else if (isFTChecked) //second shot checkbox is checked
        {
            if (bl.setUpdateStudentFreeTest(-1, Id_TB.Text, Course_DL.SelectedItem.Text))
            {
                retVal = bl.setConfirmStudentsTest(Id_TB.Text, testCode, date, "yes", "no", staffUserName);
                if(retVal)
                {
                    string message = "The student test was confirmed succesfully";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                }
            }
            else
            {
                string message = "The student doesn't have any free tests";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            }
        }
        else //no checkbox were checked - dont use free tests and dont use sec shot
        {
            retVal = bl.setConfirmStudentsTest(Id_TB.Text, testCode, date, "no", "no", staffUserName);
        }

        Response.Redirect(Request.RawUrl);
     }


    private void Close_Tests_For_Student(LinkedList<String[]> closeList)
    {
        foreach (String[] arr in closeList)
        {
            TableRow tRow = new TableRow();
            upcomingTestTable.Rows.Add(tRow);

            foreach (string str in arr)
            {
                TableCell tCell = new TableCell();
                tRow.Cells.Add(tCell);

                tCell.Text = str;
            }

        }
    }

    private void Past_Tests_For_Student(LinkedList<String[]> pastList)
    {
        int i = 1;

        foreach (String[] arr in pastList)
        {
            TableRow tRow = new TableRow();
            pastTestTable.Rows.Add(tRow);

            foreach (string str in arr)
            {
                TableCell tCell = new TableCell();
                tRow.Cells.Add(tCell);

                tCell.Text = str;
            }

            //print button

            TableCell tCellBP = new TableCell();
            Button printButton = new Button();
            printButton.ID = "showButton_" + i;
            printButton.Text = "הצג";
            printButton.Click += new EventHandler(Show_Button_On_Click);
            tCellBP.Controls.Add(printButton);
            tRow.Cells.Add(tCellBP);

            i++;
        }

    }
    protected void Show_Button_On_Click(object sender, EventArgs e)
    {
        Button clickedButton = (Button)sender;
        int lineNum = Int32.Parse(clickedButton.ID.Substring(clickedButton.ID.IndexOf("_") + 1));
        string Tc = pastTestTable.Rows[lineNum].Cells[0].Text;
        string D = pastTestTable.Rows[lineNum].Cells[1].Text;
        string H = pastTestTable.Rows[lineNum].Cells[2].Text;
        string P = pastTestTable.Rows[lineNum].Cells[3].Text;
        string Ss = pastTestTable.Rows[lineNum].Cells[4].Text;          
        Response.Redirect("printStudentDetailsAndTestInfo.aspx?id=" + Id_TB.Text + "&Tc=" + Tc + "&D=" + D + "&H=" + H + "&P=" + P + "&Ss=" + Ss);
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