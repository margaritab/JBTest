<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="systemEntrencePage.aspx.cs" Inherits="systemEntrencePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" Runat="Server">
     <ul>
        <li><a href="main.aspx">דף הבית</a></li>
        <li><a href="studentEntrencePage.aspx">כניסת סטודנט</a></li>
        <li><a href="systemEntrencePage.aspx">כניסת סגל</a></li>
        <li><a href="aboutUsPage.aspx">אודות</a></li>
        <li><a href="linksPage.aspx">קישורים</a></li>
        <li><a href="contactUs.aspx">צור קשר</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBody" Runat="Server">
   <div class="entrence">
        <h1>כניסת סגל</h1>
         <br/>
        <div id="studentEntrance">
            <div>
                <asp:Label ID="username" runat="server" Text="שם משתמש:" Width="100"></asp:Label>
                <asp:TextBox ID="usernameTB" runat="server"></asp:TextBox>
            </div>
            <br/>
            <div>
                <asp:Label ID="password" runat="server" Text="סיסמה:" width="100"></asp:Label> 
                <asp:TextBox ID="passwordTB" TextMode="Password" runat ="server"></asp:TextBox>
            </div>
        </div>
        <br/><br/><br/><br/><br/><br/>
        <div id="entrenceLinks">  
            <a href="forgetUsernamePage.aspx">שכחת שם משתמש?</a>
           <br/><br/>
            <a href="forgotPasswordPage.aspx">שכחת סיסמה?</a>

            <br/>
        </div>
        <br/>
        
        <div id="confirm">
              <asp:Button ID="entrenceButton" OnClick="Entrence_Button_On_Click" runat="server" Text="אישור" Width="130px" Height="30px" Font-Size="Large" BackColor="#f00000" BorderStyle="None" ForeColor="White" />
        </div>
     </div>
</asp:Content>

