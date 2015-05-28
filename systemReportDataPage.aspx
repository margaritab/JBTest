<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="systemReportDataPage.aspx.cs" Inherits="systemReportDatePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" Runat="Server">
    <ul>
        <li><a href="studentMainPage.aspx">דף הבית</a></li>
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
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div id="upperLables">
        <div id="rightLable">
            <asp:Label ID="connectedUserLable" runat="server" Text="Label"></asp:Label>
        </div>
        <div id="leftLable">
            <asp:Label ID="dateLable" runat="server" Text="Label"></asp:Label>
        </div>
    </div>
    <br/><br/>
    <div class="tableHeadline">
     
        <h1>דו"ח לתקופה:
          <asp:Label ID="sDateL" runat="server" Text="Label"></asp:Label>
          <asp:Label ID="eDateL" runat="server" Text="Label"></asp:Label>
     </h1>
        <asp:Label ID="startDateL" runat="server" Text="תאריך התחלה:"></asp:Label>
        <asp:TextBox ID="startDateTB" runat="server" ></asp:TextBox>
        <asp:Label ID="endDateL" runat="server" Text="תאריך סיום:"></asp:Label>
        <asp:TextBox ID="endDateTB" runat="server" ></asp:TextBox>
        <asp:Button ID="changeB" OnClick="Change_Button_On_Click" runat="server" Text="שנה" />
          <br/>
        <br/>
        <asp:Table ID="reportTable" CssClass="tables" runat="server" Width="70%">
            <asp:TableHeaderRow CssClass="tableHeader" HorizontalAlign="Center">
                <asp:TableHeaderCell>ת.ז</asp:TableHeaderCell>
                <asp:TableHeaderCell>שם מלא</asp:TableHeaderCell>
                <asp:TableHeaderCell>קוד קורס</asp:TableHeaderCell>
                <asp:TableHeaderCell>קוד מבחן</asp:TableHeaderCell>
                <asp:TableHeaderCell>תאריך</asp:TableHeaderCell>
                <asp:TableHeaderCell>עבר/לא</asp:TableHeaderCell>
                <asp:TableHeaderCell>הצג טופס בחינה </asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
      <br/><br/><br/><br/>
        <div id="showButton">
            <asp:Button ID="printTable" OnClick="Show_Table_Button_On_Click" runat="server" Text="הצג טופס להדפסה" BackColor="#F00000" ForeColor="White" Height="30"/>
            </div>
   </div>

</asp:Content>

