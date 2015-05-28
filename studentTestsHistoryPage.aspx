<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentTestsHistoryPage.aspx.cs" Inherits="testsHistoryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" Runat="Server">
      <ul>
        <li><a href="studentMainPage.aspx">דף הבית</a></li>
        <li><a href="studentRegToTesrPage.aspx">הרשמה למבחן</a></li>
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
    <div class="tableHeadline">
        <h1>הסטורית מבחנים</h1>
        <asp:table id="testsHistory" cssClass="tables" runat="server" Width="70%">
            <asp:TableHeaderRow  CssClass="tableHeader" HorizontalAlign="Center">
                <asp:TableHeaderCell>מספר קורס</asp:TableHeaderCell>
                <asp:TableHeaderCell>מספר מבחן</asp:TableHeaderCell>
                <asp:TableHeaderCell>שם מבחן</asp:TableHeaderCell>
                <asp:TableHeaderCell>תאריך</asp:TableHeaderCell>
                <asp:TableHeaderCell >ציון</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:table>
    </div>
</asp:Content>


