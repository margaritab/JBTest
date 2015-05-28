using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text.RegularExpressions;
public partial class forgetUserName : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Send_Button_On_Click(object sender, EventArgs e)
    {
        string userName = "";
        Boolean sendFlag = true;
        JBTestBL bl = new JBTestBL();
    
        if (mail_TB.Text.Equals("") || id_TB.Text.Equals(""))
        {
            string message = "Please fill all form fields.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            sendFlag = false;
        }

        if(!bl.doesSystemExistById(id_TB.Text))
        {
            string message = "User name does not exist.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            sendFlag = false;
        }

        Boolean matchMail = Regex.IsMatch(mail_TB.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
        if (!matchMail)
        {
            string message = "Email is not valid";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            sendFlag = false;
        }
        
        if(sendFlag)
        {

            userName = bl.getStaffUserNameById(id_TB.Text);
            if (!userName.Equals("") && bl.getConfirmSystemEmailByID(id_TB.Text, mail_TB.Text))
            {
                try
                {
                    //sent mail
                    MailMessage mailMsg = new MailMessage();
                    mailMsg.From = new MailAddress("jbtest.jbt@gmail.com", "jbt");
                    mailMsg.To.Add(new MailAddress(mail_TB.Text));
                    mailMsg.Subject = "שם משתמש";
                    mailMsg.Body = "שם המשתמש שלך הוא: " + userName;
                    mailMsg.Priority = MailPriority.Normal;

                    SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
                    mailClient.Credentials = new System.Net.NetworkCredential("jbtest.jbt@gmail.com", "jbtest1234");
                    mailClient.EnableSsl = true;
                    mailClient.Send(mailMsg);
                    string message = "email was sent from";
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