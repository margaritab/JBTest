<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="forgetUsernamePage.aspx.cs" Inherits="forgetUserName" %>

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
        <h1>שכחת שם משתמש?</h1>
         <br/>
        <div id="studentEntrance">
            <div>
                <asp:Label ID="id_LB" runat="server" Text="ת.ז:" Width="100"></asp:Label>
                <asp:TextBox ID="id_TB" runat="server"></asp:TextBox>
            </div>
            <br/>
            <div>
                <asp:Label ID="mail_LB" runat="server" Text="דואר אלקטרוני:" width="100"></asp:Label> 
                <asp:TextBox ID="mail_TB" runat="server"></asp:TextBox>
            </div>
        </div>
        <br/><br/><br/><br/><br/>
        <h5><span style="color:#F00000">*</span>שם המשתמש יישלח לדואר האלקטרוני שהקלדת</h5>
        <div id="confirm">
              <asp:Button ID="sendButton" OnClick="Send_Button_On_Click" runat="server" Text="שלח" Width="130px" Height="30px" Font-Size="Large" BackColor="#f00000" BorderStyle="None" ForeColor="White"  />
        </div>
         
     </div>
</asp:Content>

