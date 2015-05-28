<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminCreateNewSystemMemberPage.aspx.cs" Inherits="AdminCreateNewSystemMemberPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" Runat="Server">
      <ul>
        <li><a href="AdminCreateNewSystemMemberPage.aspx">צור איש סגל חדש</a></li>
        <li><a href="adminResetPasswordPage.aspx">שינוי סיסמה</a></li>
        <li><asp:LinkButton ID="LogoutBtn" runat="server" OnClick="LogoutBtn_Click">התנתק</asp:LinkButton></li>

        <li id="adminSearch">
             <asp:Label ID="searchLB" runat="server" Text="חיפוש:"></asp:Label>
            <asp:TextBox ID="searchTB" runat="server"></asp:TextBox>
            <asp:Button ID="searchButton" OnClick="Search_Button_On_Click" runat="server" Text="חפש" BackColor="#F00000" ForeColor="White" Font-Bold="True" Width="50" Height="25" />
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBody" Runat="Server">

     <h1>יצירת איש סגל חדש </h1>

 <div class="rightReg">
       <div>
            <asp:Label ID="Label1" runat="server" Text="ת.ז:" Width="100"></asp:Label>
            <asp:TextBox ID="idTB" runat="server"></asp:TextBox><br/>
       </div> 
     <br/>
       <div>
           <asp:Label ID="Label2" runat="server" Text="שם פרטי:" Width="100"></asp:Label>
           <asp:TextBox ID="FirstNameTB" runat="server"></asp:TextBox><br/>

       </div>
      <br/>
       <div>
           <asp:Label ID="Label3" runat="server" Text="שם משפחה:" Width="100"></asp:Label>
           <asp:TextBox ID="LastNameTB" runat="server"></asp:TextBox><br/>

       </div> 
      <br/>
        <div>
           <asp:Label ID="Label6" runat="server" Text="שם משתמש:" Width="100"></asp:Label>
           <asp:TextBox ID="usernameTB"  runat="server"></asp:TextBox><br/>

       </div>
       <br/>
        <div>
           <asp:Label ID="Label4" runat="server" Text="סיסמה:" Width="100"></asp:Label>
           <asp:TextBox ID="passwordTB" TextMode="password" runat="server" ></asp:TextBox><br/>
       </div>
      <br/>
        <div>
           <asp:Label ID="Label5" runat="server" Text="אישור סיסמה:" Width="100"></asp:Label>
           <asp:TextBox ID="passwordConfirmTB" TextMode="password" runat="server"></asp:TextBox><br/>

       </div> 
      
   <br/>
          <div>
           <asp:Label ID="email_LB" runat="server" Text="כתובת מייל:" Width="100"></asp:Label>
           <asp:TextBox ID="email_TB" runat="server"></asp:TextBox><br/>

       </div> 
      
   <br/>
           <div>
             <asp:Label ID="collages" runat="server" Text="בחר מכללה:" Width="100"></asp:Label>
             <asp:DropDownList  AutoPostBack="true" ID ="collageDropDownList" runat="server" Width="155">
                <asp:ListItem Text="בחר" Value="0"/>
             </asp:DropDownList>
         </div>
         <br/>
     </div>
    <br/>
     <div id="RegAdminButton" >
        <asp:Button ID="confirmButton" OnClick="Confirm_Button_On_Click" runat="server" Text="אישור" BackColor="#F00000" ForeColor="White"/>
    </div>

</asp:Content>

