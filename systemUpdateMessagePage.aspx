<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="systemUpdateMessagePage.aspx.cs" Inherits="systemUpdateMessagePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" Runat="Server">
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
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div id="upperLables">
        <div id="rightLable">
            <asp:Label ID="connectedUserLable" runat="server" Text="Label"></asp:Label>
        </div>
        <div id="leftLable">
            <asp:Label ID="dateLable" runat="server" Text="Label"></asp:Label>
        </div> 
    </div>
    <br />
        <h1>עדכן הודעה</h1>
        <div id="messageArea">
            <div>
                <asp:Label ID="messageContentLB" runat="server" Text="תוכן ההודעה:"  Width="93"></asp:Label>
                <br />
                <asp:TextBox id="messageTA" TextMode="multiline" Columns="50" Rows="5" runat="server" />
            </div>
            <br/>
        </div>
    <br />
    <div id="messageButtons">
        <asp:Button ID="updateMessageB" OnClick="Update_Message_On_Click" runat="server" Text="עדכן" BackColor="#F00000" ForeColor="White" />
        <asp:Button ID="cancelMessage" OnClick="Cancel_Message_On_Click" runat="server" Text="בטל" BackColor="#F00000" ForeColor="White" />
    </div>

</asp:Content>

