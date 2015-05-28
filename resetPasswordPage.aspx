<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="resetPasswordPage.aspx.cs" Inherits="resetPasswordPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" runat="Server">
    <ul>
        <li><a href="main.aspx">דף הבית</a></li>
        <li><a href="studentEntrencePage.aspx">כניסת סטודנט</a></li>
        <li><a href="systemEntrencePage.aspx">כניסת סגל</a></li>
        <li><a href="aboutUsPage.aspx">אודות</a></li>
        <li><a href="linksPage.aspx">קישורים</a></li>
        <li><a href="contactUs.aspx">צור קשר</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBody" runat="Server">
    <h1>איפוס סיסמא</h1>
    <div id="resetPassword">
        <asp:Label ID="user_LB" runat="server" Text="שם משתמש:" Width="150"></asp:Label>
        <asp:TextBox ID="user_TB" runat="server"></asp:TextBox>
        <br /><br />
        <asp:Label ID="newPassword_LB" runat="server" Text="סיסמא חדשה:" Width="150"></asp:Label>
        <asp:TextBox ID="newPassword_TB" TextMode="password" runat="server"></asp:TextBox>
        <br /><br />
        <asp:Label ID="confNewPassword_LB" runat="server" Text="אישור סיסמא חדשה:" Width="150"></asp:Label>
        <asp:TextBox ID="confNewPassword_TB" TextMode="password" runat="server"></asp:TextBox>
        <br /><br />
    </div>
     <div id="resetPassButton">
        <asp:Button ID="resetPasswordButton" OnClick="Reset_Password_Button_On_Click" runat="server" Text="אפס" BackColor="#F00000 " ForeColor="White" Width="100" Height="30" Font-Bold="True"/>
    </div>
</asp:Content>

