<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentUpdateRegToTest.aspx.cs" Inherits="studentUpdateRegToTest" %>

<%@ Register Src="~/calanderPage.ascx" TagPrefix="uc1" TagName="calanderPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" Runat="Server">
    <ul>
        <li><a href="studentMainPage.aspx">דף הבית</a></li>
        <li><a href="studentRegToTestPage.aspx">הרשמה למבחן</a></li>
        <li><a href="studentSchedulePage.aspx">צפייה בלו"ז</a></li>
        <li><a href="studentUpdateDetailsPage.aspx">עדכון פרטים אישיים</a></li>
        <li><a href="studentTestsHistoryPage.aspx">היסטורית מבחנים</a></li>
        <li><asp:LinkButton ID="LogoutBtn" runat="server" OnClick="LogoutBtn_Click">התנתק</asp:LinkButton></li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBody" Runat="Server">
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
        <asp:DropDownList ID="testCodeDropDownListDL" runat="server">
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label3" runat="server" Text="תאריך המבחן:"></asp:Label>
        <asp:DropDownList ID="dateDropDownListDL" runat="server">
        </asp:DropDownList>
        <asp:Label ID="Label4" runat="server" Text="שעה:"></asp:Label>
        <asp:DropDownList ID="hourDropDownListDL" runat="server">
        
        </asp:DropDownList>
        <asp:Label ID="Label5" runat="server" Text="Second Shot:"></asp:Label>
        <asp:TextBox ID="secondShotTB" runat="server"></asp:TextBox>
        <asp:Button ID="regButton" OnClick="Reg_Update_Button_On_Click" runat="server" Text="מעדכן" />
    </div>
</asp:Content>

