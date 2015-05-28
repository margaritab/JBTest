<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentMainPage.aspx.cs" Inherits="studentMainPage" %>

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
        <li><a href="studentTestsHistoryPage.aspx">היסטורית מבחנים</a></li>
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

    <h1>ברוך הבא
        <asp:Label ID="usernameLB" runat="server" Text="Label"></asp:Label>
    </h1>
    <div id="studentMain">
        <div id="tablesStudentMain">
            <div class="tableHeadline">
                <h3>מבחנים שאושרו</h3>
                <asp:Table ID="testApproved" CssClass="MainTables" runat="server" Width="100%">
                    <asp:TableHeaderRow CssClass="tableHeader" HorizontalAlign="Center">
                        <asp:TableHeaderCell>מספר קורס</asp:TableHeaderCell>
                        <asp:TableHeaderCell>מספר מבחן</asp:TableHeaderCell>
                        <asp:TableHeaderCell>שם מבחן</asp:TableHeaderCell>
                        <asp:TableHeaderCell>תאריך</asp:TableHeaderCell>
                        <asp:TableHeaderCell>שעה</asp:TableHeaderCell>
                        <asp:TableHeaderCell>בטל מבחן</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </div>
            <br />
            <div class="tableHeadline">
                <h3>מבחנים המחכים לאישור</h3>
                <asp:Table ID="waitForApprove" CssClass="MainTables" runat="server" Width="100%">
                    <asp:TableHeaderRow CssClass="tableHeader" HorizontalAlign="Center">
                        <asp:TableHeaderCell>מספר קורס</asp:TableHeaderCell>
                        <asp:TableHeaderCell>מספר מבחן</asp:TableHeaderCell>
                        <asp:TableHeaderCell>שם מבחן</asp:TableHeaderCell>
                        <asp:TableHeaderCell>תאריך</asp:TableHeaderCell>
                        <asp:TableHeaderCell>שעה</asp:TableHeaderCell>
                        <asp:TableHeaderCell>עדכון</asp:TableHeaderCell>
                        <asp:TableHeaderCell>ביטול</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </div>
        </div>
        <div id="messageBox">
            <h3>הודעות חשובות:</h3>
            <marquee direction="up" scrollamount="3" behavior="scroll" onmouseover="this.stop();" onmouseout="this.start();">
            <div runat="server" id="staffMessages"></div>
            </marquee>
        </div>
    </div>
</asp:Content>

