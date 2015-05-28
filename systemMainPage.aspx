<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="systemMainPage.aspx.cs" Inherits="systemMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" runat="Server">
    <ul>
        <li><a href="systemMainPage.aspx">דף הבית</a></li>
        <li><a href="systemSchedulePage.aspx">צפייה בלו"ז</a></li>
        <li><a href="systemReportPage.aspx">דו"ח תקופתי</a></li>
        <li><a href="systemCoursesPage.aspx">קורסים</a></li>
        <li><a href="systemTestsPage.aspx">מבחנים</a></li>
        <li><a href="systemMessages.aspx">הודעות</a></li>
        <li><asp:LinkButton ID="LogoutBtn" runat="server" OnClick="LogoutBtn_Click">התנתק</asp:LinkButton></li>
        <li id="search">
            <asp:Label ID="searchLB" runat="server" Text="חיפוש:"></asp:Label>
            <asp:TextBox ID="searchTB" runat="server"></asp:TextBox>
            <asp:Button ID="searchButton" OnClick="Search_Button_On_Click" runat="server" Text="חפש" BackColor="#F00000" ForeColor="White" Font-Bold="True" Width="50" Height="25" />
        </li>
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
    <h1>ברוך הבא
        <asp:Label ID="usernameLB" runat="server" Text="Label"></asp:Label>
    </h1>

    <div class="tableHeadline">
        <h3>נבחנים היום</h3>

        <asp:Table ID="todayTestTable" CssClass="tables" runat="server" Width="70%">
            <asp:TableHeaderRow CssClass="tableHeader" HorizontalAlign="Center">
                <asp:TableHeaderCell>ת.ז</asp:TableHeaderCell>
                <asp:TableHeaderCell>שם משפחה</asp:TableHeaderCell>
                <asp:TableHeaderCell>שם פרטי</asp:TableHeaderCell>
                <asp:TableHeaderCell>שעה</asp:TableHeaderCell>
                <asp:TableHeaderCell>עבר</asp:TableHeaderCell>
                <asp:TableHeaderCell>נכשל</asp:TableHeaderCell>
                <asp:TableHeaderCell>שולם</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
    <br />
    <div class="tableHeadline">
        <h3>נרשמים שמחכים לאישור רישום</h3>
        <asp:Table ID="waitForApproveSysTable" CssClass="tables" runat="server" Width="70%">
            <asp:TableHeaderRow CssClass="tableHeader" HorizontalAlign="Center">
                <asp:TableHeaderCell>ת.ז</asp:TableHeaderCell>
                <asp:TableHeaderCell>שם משפחה</asp:TableHeaderCell>
                <asp:TableHeaderCell>שם פרטי</asp:TableHeaderCell>
                <asp:TableHeaderCell>תאריך</asp:TableHeaderCell>
                <asp:TableHeaderCell>שעה</asp:TableHeaderCell>
                <asp:TableHeaderCell>הצג</asp:TableHeaderCell>
            </asp:TableHeaderRow>

        </asp:Table>
    </div>
    <br />
    <div class="tableHeadline">
        <h3>נרשמים שמחכים לאישור ביטול</h3>
        <asp:Table ID="waitForCancelationSysTable" CssClass="tables" runat="server" Width="70%">
            <asp:TableHeaderRow CssClass="tableHeader" HorizontalAlign="Center">
                <asp:TableHeaderCell>ת.ז</asp:TableHeaderCell>
                <asp:TableHeaderCell>שם משפחה</asp:TableHeaderCell>
                <asp:TableHeaderCell>שם פרטי</asp:TableHeaderCell>
                <asp:TableHeaderCell>תאריך</asp:TableHeaderCell>
                <asp:TableHeaderCell>שעה</asp:TableHeaderCell>
                <asp:TableHeaderCell>בטל</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
</asp:Content>

