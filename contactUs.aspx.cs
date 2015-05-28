using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class contactUs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Send_Button_On_Click(object sender, EventArgs e)
    {
        if(firstName_TB.Text.Equals("") || lastName_TB.Text.Equals("") || email_TB.Text.Equals("") ||
            subject_TB.Text.Equals("") || content_TA.Text.Equals(""))
        {
            string message = "Please fill all form fields.";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
        }
        else
        {
            try
            {
                //sent mail
                MailMessage mailMsg = new MailMessage();
                mailMsg.From = new MailAddress(email_TB.Text, lastName_TB.Text + " " + firstName_TB.Text + " <" + email_TB.Text + ">");
                mailMsg.To.Add(new MailAddress("jbtest.jbt@gmail.com"));
                mailMsg.Subject = lastName_TB.Text + " " + firstName_TB.Text + " <" + email_TB.Text + ">: " + subject_TB.Text;
                mailMsg.Body = content_TA.Text;
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