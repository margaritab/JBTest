<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="systemStudentDetailsPage.aspx.cs" Inherits="systemStudentDetailsPage" %>

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
    <br />
    <div class="rightReg">
        <div>
            <asp:Label ID="Label2" runat="server" Text="ת.ז:" Width="100"></asp:Label>
            <asp:TextBox ID="Id_TB" runat="server" Enabled="False"></asp:TextBox><br />
        </div>
        <br />
        <div>
            <asp:Label ID="Label5" runat="server" Text="sr:" Width="100"></asp:Label>
            <asp:TextBox ID="sr_TB" runat="server"></asp:TextBox><br />
        </div>
        <br />
        <div>
            <asp:Label ID="Label3" runat="server" Text="שם פרטי:" Width="100"></asp:Label>
            <asp:TextBox ID="First_Name_TB" runat="server"></asp:TextBox><br />

        </div>
        <br />
        <div>
            <asp:Label ID="Label4" runat="server" Text="שם משפחה:" Width="100"></asp:Label>
            <asp:TextBox ID="Last_Name_TB" runat="server"></asp:TextBox><br />

        </div>
        <br />
        <div>
            <asp:Label ID="Label8" runat="server" Text="דואר אלקטרוני:" Width="100"></asp:Label>
            <asp:TextBox ID="Email_TB" runat="server"></asp:TextBox><br />
        </div>
        <br />
        <div>
            <asp:Label ID="Label6" runat="server" Text="טלפון:" Width="100"></asp:Label>
            <asp:TextBox ID="Prime_Phone_TB" runat="server"></asp:TextBox><br />

        </div>
        <br />
        <div>
            <asp:Label ID="Label7" runat="server" Text="טלפון משני:" Width="100"></asp:Label>
            <asp:TextBox ID="Sec_Phone_TB" runat="server"></asp:TextBox><br />

        </div>

    </div>
    <div class="leftReg">
        <div>
            <asp:Label ID="Label9" runat="server" Text="שם פרטי (אנגלית):" Width="150"></asp:Label>
            <asp:TextBox ID="First_Name_Eng_TB" runat="server"></asp:TextBox><br />
        </div>
        <br />
        <div>
            <asp:Label ID="Label10" runat="server" Text="שם משפחה (אנגלית):" Width="150"></asp:Label>
            <asp:TextBox ID="Last_Name_Eng_TB" runat="server"></asp:TextBox><br />
        </div>
        <br />
        <div>
            <asp:Label ID="Label11" runat="server" Text="כתובת מגורים (אנגלית):" Width="150"></asp:Label>
            <asp:TextBox ID="Addr_TB" runat="server"></asp:TextBox><br />
        </div>
        <br />
        <div>
            <asp:Label ID="Label12" runat="server" Text="עיר:" Width="150"></asp:Label>
            <asp:TextBox ID="City_TB" runat="server"></asp:TextBox><br />
        </div>
        <br />
        <div>
            <asp:Label ID="Label13" runat="server" Text="בחר מכללה:" Width="150"></asp:Label>
            <asp:DropDownList OnTextChanged="Load_Courses" AutoPostBack="true" ID="Collage_DL" runat="server" Width="155">
            </asp:DropDownList>
        </div>
        <br />
        <div>
            <asp:Label ID="Label14" runat="server" Text="בחר קורס:" Width="150"></asp:Label>
            <asp:DropDownList ID="Course_DL" runat="server" Width="155">
            </asp:DropDownList>
        </div>
        <br />
        <div>
            <asp:Label ID="Label15" runat="server" Text="מאגר בחינות חינם:" Width="150"></asp:Label>
            <asp:TextBox ID="FreeTestNum_DL" runat="server" Enabled="false"></asp:TextBox><br />
        </div>
    </div>

    <br />
    <div id="StudentUpdateButton">
        <asp:Button ID="UpdateButton" OnClick="Update_Bottun_On_Click" runat="server" Text="עדכן" BackColor="#F00000" ForeColor="White" />
    </div>
    <br />
    <div id="studentTestTableDetails">
        <div class="tableHeadline">
            <h3>מבחנים לאישור</h3>
            <asp:Table ID="approveTestTabel" CssClass="tables" runat="server" Width="70%">
                <asp:TableHeaderRow CssClass="tableHeader" HorizontalAlign="Center">
                    <asp:TableHeaderCell Width="10%">מבחן</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="10%">תאריך</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="10%">שעה</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="10%">second shot</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="10%">אישור בחינה</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="10%">מאגר בחינות חינם</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="10%">שימוש ב-second shot</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
        <br />
       
        <div class="tableHeadline">
            <h3>מבחנים קרובים</h3>
            <asp:Table ID="upcomingTestTable" CssClass="tables" runat="server" Width="70%">
                <asp:TableHeaderRow CssClass="tableHeader" HorizontalAlign="Center">
                    <asp:TableHeaderCell Width="17%">מבחן</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="17%">תאריך</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="17%">שעה</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="17%">second shot</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
        <br />
        <div class="tableHeadline">
            <h3>מבחנים קודמים</h3>
            <asp:Table ID="pastTestTable" CssClass="tables" runat="server" Width="70%">
                <asp:TableHeaderRow CssClass="tableHeader" HorizontalAlign="Center">
                    <asp:TableHeaderCell Width="11%">מבחן</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="11%">תאריך</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="11%">שעה</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="11%">עבר</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="11%">second shot</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="11%">הצג טופס בחינה</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
    </div>

</asp:Content>

