<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="printStudentDetailsAndTestInfo.aspx.cs" Inherits="printStudentDetailsAndTestInfo" %>

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
        <h1>טופס בחינה</h1>
        <div class="rightReg">
            <div>
                <asp:Label ID="Id_LB" runat="server" Text="ת.ז:" Width="150"></asp:Label>
                <asp:Label ID="Id_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="Sr_LB" runat="server" Text="sr:" Width="150"></asp:Label>
                <asp:Label ID="Sr_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="First_Name_LB" runat="server" Text="שם פרטי:" Width="150"></asp:Label>
                <asp:Label ID="First_Name_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="Last_Name_LB" runat="server" Text="שם משפחה:" Width="150"></asp:Label>
                <asp:Label ID="Last_Name_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="First_Name_Eng_LB" runat="server" Text="שם פרטי (אנגלית):" Width="150"></asp:Label>
                <asp:Label ID="First_Name_Eng_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="Last_Name_Eng_LB" runat="server" Text="שם משפחה (אנגלית):" Width="150"></asp:Label>
                <asp:Label ID="Last_Name_Eng_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="Addr_LB" runat="server" Text="כתובת מגורים (אנגלית):" Width="150"></asp:Label>
                <asp:Label ID="Addr_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="City_LB" runat="server" Text="עיר:" Width="150"></asp:Label>
                <asp:Label ID="City_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="Email_LB" runat="server" Text="דואר אלקטרוני:" Width="150"></asp:Label>
                <asp:Label ID="Email_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
        </div>

        <div class="leftReg">

            <div>
                <asp:Label ID="Prime_Phone_LB" runat="server" Text="טלפון:" Width="150"></asp:Label>
                <asp:Label ID="Prime_Phone_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="Collage_LB" runat="server" Text="מכללה:" Width="150"></asp:Label>
                <asp:Label ID="Collage_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="Course_LB" runat="server" Text="קורס:" Width="150"></asp:Label>
                <asp:Label ID="Course_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="Test_Code_LB" runat="server" Text="מספר בחינה:" Width="150"></asp:Label>
                <asp:Label ID="Test_Code_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="Date_LB" runat="server" Text="תאריך הבחינה:" Width="150"></asp:Label>
                <asp:Label ID="Date_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="Hour_LB" runat="server" Text="שעת בחינה:" Width="150"></asp:Label>
                <asp:Label ID="Hour_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="Second_Shot_LB" runat="server" Text="second shot:" Width="150"></asp:Label>
                <asp:Label ID="Second_Shot_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />

            <div>
                <asp:Label ID="Cost_LB" runat="server" Text="עלות בחינה:" Width="150"></asp:Label>
                <asp:Label ID="Cost_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
            <div>
                <asp:Label ID="Pass_LB" runat="server" Text="תוצאות הבחינה:" Width="150"></asp:Label>
                <asp:Label ID="Pass_Info_LB" runat="server" Text=""></asp:Label><br />
            </div>
            <br />
        </div>
        <br />
    
    <div id="printButton">
        <asp:Button ID="PrintButton" OnClick="Print_Button_On_Click" runat="server" Text="הדפס" />
    </div>

</asp:Content>

