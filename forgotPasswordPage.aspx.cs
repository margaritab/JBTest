using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net.Mail;

public partial class forgotPasswordPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Send_Button_On_Click(object sender, EventArgs e)
    {
        JBTestBL bl = new JBTestBL();
        Boolean resetFlag = true;

        if (mail_TB.Text.Equals("") ||username_TB.Text.Equals(""))
        {
            string message = "Please fill all form fields.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            resetFlag = false;
        }
        Boolean matchMail = Regex.IsMatch(mail_TB.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");// mail format
        if (!matchMail)
        {
            string message = "Email is not valid";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            resetFlag = false;
        }
         Boolean matchUserName = Regex.IsMatch(username_TB.Text, "^[0-9]*$"); //student user name is id
         if ((!matchUserName && !bl.doesSystemExist(username_TB.Text)) || (matchUserName && !bl.doesStudentExist(username_TB.Text)))
         {
             string message = "The user name you entered does not exist";
             ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
             resetFlag = false;
         }
        

         if(resetFlag)
         {
             //check if the enterd emails are the same emails in database
             if ((matchUserName && bl.getConfirmStudentEmailByID(username_TB.Text, mail_TB.Text)) ||
                 (!matchUserName && bl.getConfirmSystemEmailByUser(username_TB.Text, mail_TB.Text)))
             {
                 try
                 {
                     //sent mail
                     MailMessage mailMsg = new MailMessage();
                     mailMsg.From = new MailAddress("jbtest.jbt@gmail.com", "jbt");
                     mailMsg.To.Add(new MailAddress(mail_TB.Text));
                     mailMsg.Subject = "איפוס סיסמה";
                     mailMsg.Body = "לאיפוס הסיסמא לחצו על הלינק: http://localhost:59338/resetPasswordPage.aspx";
                     mailMsg.Priority = MailPriority.Normal;

                     SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
                     mailClient.Credentials = new System.Net.NetworkCredential("jbtest.jbt@gmail.com", "jbtest1234");
                     mailClient.EnableSsl = true;
                     mailClient.Send(mailMsg);
                     string message = "email was sent";
                     ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                 }
                 catch
                 {
                     string message = "there was a problem sending mail";
                     ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                 }
             }
         }
    }
}