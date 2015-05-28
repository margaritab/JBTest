<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminUpdateSystemMemberPage.aspx.cs" Inherits="AdminUpdateSystemMemberPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" runat="Server">
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
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBody" runat="Server">

    <h1>עדכן פרטי איש סגל </h1>

    <div class="rightReg">
        <div>
            <asp:Label ID="Label1" runat="server" Text="ת.ז:" Width="100" ></asp:Label>
            <asp:TextBox ID="idTB" runat="server" Enabled="false"></asp:TextBox><br />
        </div>
        <br />
        <div>
            <asp:Label ID="Label2" runat="server" Text="שם פרטי:" Width="100"></asp:Label>
            <asp:TextBox ID="FirstNameTB" runat="server"></asp:TextBox><br />

        </div>
        <br />
        <div>
            <asp:Label ID="Label3" runat="server" Text="שם משפחה:" Width="100"></asp:Label>
            <asp:TextBox ID="LastNameTB" runat="server"></asp:TextBox><br />

        </div>
        <br />
        <div>
            <asp:Label ID="Label6" runat="server" Text="שם משתמש:" Width="100"></asp:Label>
            <asp:TextBox ID="usernameTB" runat="server"></asp:TextBox><br />

        </div>
        <br />

        <div>
            <asp:Label ID="email_LB" runat="server" Text="כתובת מייל:" Width="100"></asp:Label>
            <asp:TextBox ID="email_TB" runat="server"></asp:TextBox><br />

        </div>

        <br />
        <div>
            <asp:Label ID="collages" runat="server" Text="בחר מכללה:" Width="100"></asp:Label>
            <asp:DropDownList AutoPostBack="true" ID="collageDropDownList" runat="server" Width="155">
            </asp:DropDownList>
        </div>
        <br />
    </div>
    <br />
    <div id="RegAdminButton">
        <asp:Button ID="updateButton" OnClick="Update_Button_On_Click" runat="server" Text="אישור" BackColor="#F00000" ForeColor="White" />
        <asp:Button ID="cancelButton" OnClick="Cancel_Button_On_Click" runat="server" Text="בטל" BackColor="#F00000" ForeColor="White" />

    </div>



</asp:Content>

