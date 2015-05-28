<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminPage.aspx.cs" Inherits="AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="entrence">
        <h1>כניסת מנהל</h1>
         <br/>
        <div id="studentEntrance">
            <div>
                <asp:Label ID="username" runat="server" Text="שם משתמש:" Width="100"></asp:Label>
                <asp:TextBox ID="usernameTB" runat="server"></asp:TextBox>
            </div>
            <br/>
            <div>
                <asp:Label ID="password" runat="server" Text="סיסמה:" width="100"></asp:Label> 
                <asp:TextBox ID="passwordTB" TextMode="password" runat="server"></asp:TextBox>
            </div>
        </div>
        <br/><br/><br/><br/><br/><br/>
        <br/>
        <div id="confirm">
              <asp:Button ID="entrenceButton" onClick="Entrence_Button_On_Click" runat="server" Text="אישור" Width="130px" Height="30px" Font-Size="Large" BackColor="#f00000" BorderStyle="None" ForeColor="White" />
        </div>
         
     </div>
</asp:Content>

