<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="adminResetPasswordPage.aspx.cs" Inherits="adminResetPasswordPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" Runat="Server">
    <ul>
        <li><a href="AdminCreateNewSystemMemberPage.aspx">צור איש סגל חדש</a></li>
        <li><a href="adminResetPasswordPage.aspx">שינוי סיסמה</a></li>
        <li><asp:LinkButton ID="LogoutBtn" runat="server" OnClick="LogoutBtn_Click">התנתק</asp:LinkButton></li>

        <li id="adminSearch">
             <asp:Label ID="searchLB" runat="server" Text="חיפוש:"></asp:Label>
            <asp:TextBox ID="searchTB" runat="server"></asp:TextBox>
            <asp:Button ID="searchButton" OnClick="Search_Button_On_Click" runat="server" Text="חפש" BackColor="#F00000" ForeColor="White" Font-Bold="True" Width="50" Height="25" />
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBody" Runat="Server">
    <br/>
            <h1 style="text-align: center; color: red">
                שנה סיסמה</h1>

            <div id="resetAdminPassword">

            <div id="oldPassword" style="text-align: right; direction: rtl">
                <asp:Label ID="Label3" runat="server" Text="סיסמה ישנה:" Width="100"></asp:Label>
                <asp:TextBox ID="oldPasswordTB" TextMode="password" runat="server"></asp:TextBox><br />
            </div>
            <br />
            <div id="passwordChange" style="text-align: right; direction: rtl">
                <asp:Label ID="Label1" runat="server" Text="סיסמה:" Width="100"></asp:Label>
                <asp:TextBox ID="passwordTB" TextMode="password" runat="server"></asp:TextBox><br />
            </div>
            <br />
            <div style="text-align: right; direction: rtl">
                <asp:Label ID="Label2" runat="server" Text="אישור סיסמה:" Width="100"></asp:Label>
                <asp:TextBox ID="passwordConfirmTB" TextMode="password" runat="server"></asp:TextBox><br />
            </div>
            <br />

        </div>
    <br/><br/>

        <div id="AdminPasswordButtons">
            <asp:Button ID="confirmButton" OnClick="Confirm_Button_On_Click" runat="server" Text="אשר" BackColor="#F00000" ForeColor="White" />
           
                <asp:Button ID="cancelButton" OnClick="Cancel_Button_On_Click"  runat="server" Text="בטל" BackColor="#F00000" ForeColor="White" />

        </div>

</asp:Content>

