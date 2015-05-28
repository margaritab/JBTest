<%@ Control Language="C#" AutoEventWireup="true" CodeFile="calanderPage.ascx.cs" Inherits="calanderPage" %>
<link href="Styles/siteStyle.css" rel="stylesheet" />
<div id="calender">
    <div id="calenderButtons">
        <div id="nextB">
            <asp:Button ID="nextMonthButton" runat="server" Text="חודש הבא >>" OnClick="Next_Month_Button_On_Click" />
        </div>
        <div id="prevB">
            <asp:Button ID="prevMonthButton" runat="server" Text="<< חודש קודם" OnClick="Prev_Month_Button_On_Click" />
        </div>
    </div>
    <br />
    <div id="monthNameLB">
        <asp:Label ID="hebMonthNameLB" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <div id="calTableDiv">
        <asp:Table id="calenderTableID" cssClass="calenderTable" runat="server" Width="91%">
            <asp:TableHeaderRow CssClass="calenderTableHeader" HorizontalAlign="Center">
                <asp:TableHeaderCell Width ="13%">א</asp:TableHeaderCell>
                <asp:TableHeaderCell Width ="13%">ב</asp:TableHeaderCell>
                <asp:TableHeaderCell Width ="13%">ג</asp:TableHeaderCell>
                <asp:TableHeaderCell Width ="13%">ד</asp:TableHeaderCell>
                <asp:TableHeaderCell Width ="13%">ה</asp:TableHeaderCell>
                <asp:TableHeaderCell Width ="13%">ו</asp:TableHeaderCell>
                <asp:TableHeaderCell Width ="13%">ש</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
</div>
