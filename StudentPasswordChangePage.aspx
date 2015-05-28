<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentPasswordChangePage.aspx.cs" Inherits="StudentPasswordChangePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color: rgb(240,240,240)">
    <form id="password_change" runat="server">
        <div>
            <h1 style="text-align: center; color: rgb(240,0,0)">
                <asp:Label ID="name" runat="server" Text="Label"></asp:Label>
                שנה/י סיסמה</h1>

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

        <div id="passwordButtons">
            
            <a href="StudentPasswordChangePage.aspx" target="_self"
                onclick="window.close(this.href, '','toolbar=no, resizable=no, location=no, width=400, height=300');return false;">
                <asp:Button ID="cancelButton" OnClick="Cancel_Button_On_Click" runat="server" Text="בטל" BackColor="#F00000" ForeColor="White" /></a>
            <asp:Button ID="confirmButton" OnClick="Confirm_Button_On_Click" runat="server" Text="אשר" BackColor="#F00000" ForeColor="White" />

        </div>
    </form>
</body>
</html>
