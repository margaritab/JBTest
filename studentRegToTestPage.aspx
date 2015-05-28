<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentRegToTestPage.aspx.cs" Inherits="studentSchedulePage" %>

<%@ Register Src="~/calanderPage.ascx" TagPrefix="uc1" TagName="calanderPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" runat="Server">
    <ul>
        <li><a href="studentMainPage.aspx">דף הבית</a></li>
        <li><a href="studentRegToTestPage.aspx">הרשמה למבחן</a></li>
        <li><a href="studentSchedulePage.aspx">צפייה בלו"ז</a></li>
        <li><a href="studentUpdateDetailsPage.aspx">עדכון פרטים אישיים</a></li>
        <li><a href="studentTestHistoryPage.aspx">היסטורית מבחנים</a></li>
        <li><asp:LinkButton ID="LogoutBtn" runat="server" OnClick="LogoutBtn_Click">התנתק</asp:LinkButton></li>
    </ul>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBody" runat="Server">
      <div id="upperLables">
        <div id="rightLable">
            <asp:Label ID="connectedUserLable" runat="server" Text="Label"></asp:Label>
        </div>
        <div id="leftLable">
            <asp:Label ID="dateLable" runat="server" Text="Label"></asp:Label>
        </div>
    </div>
    <uc1:calanderPage runat="server" ID="calanderPage" />
    <div id="regToTest">
        <asp:Label ID="Label2" runat="server" Text="קוד המבחן:"></asp:Label>
        <asp:DropDownList ID="testCodeDropDownList" runat="server">
            <asp:ListItem Text="בחר" Value="0" />
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label3" runat="server" Text="תאריך המבחן:"></asp:Label>
        <asp:DropDownList ID="dateDropDownList" runat="server">
            <asp:ListItem Text="בחר עבור החודש המוצג" Value="0" />
        </asp:DropDownList>
        <asp:Label ID="Label4" runat="server" Text="שעה:"></asp:Label>
        <asp:DropDownList ID="hourDropDownList" runat="server">
            <asp:ListItem Text="בחר" Value="0" />
            <asp:ListItem Text="10:00" Value="1" />
            <asp:ListItem Text="10:30" Value="2" />
            <asp:ListItem Text="11:00" Value="3" />
            <asp:ListItem Text="11:30" Value="4" />
            <asp:ListItem Text="12:00" Value="5" />
            <asp:ListItem Text="12:30" Value="6" />
            <asp:ListItem Text="13:00" Value="7" />
        </asp:DropDownList>
        <asp:Label ID="Label5" runat="server" Text="Second Shot:"></asp:Label>
        <asp:TextBox ID="secondShotTB" runat="server"></asp:TextBox>
        <asp:Button ID="regButton" OnClick="Reg_Button_On_Click" runat="server" Text="הירשם למבחן" />
    </div>
</asp:Content>

