<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="systemUpdateCoursePage.aspx.cs" Inherits="systemUpdateCoursePage" %>

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
    <div id="updateCourse">
        <h1>קורס 
        <asp:Label ID="courseNameTB" runat="server" Text="Label"></asp:Label>
        </h1>
        <br /><br />
        <asp:Label ID="start_DateL" runat="server" Text="תאריך התחלה:"></asp:Label>
        <asp:TextBox ID="start_DateTB" runat="server"></asp:TextBox>&nbsp &nbsp
        <asp:Label ID="end_DateL" runat="server" Text="תאריך סיום:"></asp:Label>
        <asp:TextBox ID="end_DateTB" runat="server"></asp:TextBox>
       
    </div>
    <div id="courseUpdateButtons">
    <asp:Button ID="updateCourseB" OnClick="Update_Course_On_Click" runat="server" Text="עדכן" BackColor="#F00000" ForeColor="White" />
    <asp:Button ID="cancel" OnClick="Cancel_On_Click" runat="server" Text="בטל" BackColor="#F00000" ForeColor="White"/>
        </div>
</asp:Content>

