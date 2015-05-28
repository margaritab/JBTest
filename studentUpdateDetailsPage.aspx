<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentUpdateDetailsPage.aspx.cs" Inherits="studentUpdateDetailsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" Runat="Server">
   <ul>
        <li><a href="studentMainPage.aspx">דף הבית</a></li>
        <li><a href="studentRegToTestPage.aspx">הרשמה למבחן</a></li>
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
   <h1>עדכון פרטים אישיים</h1>
     <div class="rightReg">
       <div>
            <asp:Label ID="Label1" runat="server" Text="ת.ז:" Width="100"></asp:Label>
            <asp:TextBox ID="Id_TB" runat="server" Enabled="False"></asp:TextBox><br/>
       </div> 
     <br/>
       <div>
           <asp:Label ID="Label2" runat="server" Text="שם פרטי:" Width="100"></asp:Label>
           <asp:TextBox ID="First_Name_TB" runat="server"></asp:TextBox><br/>

       </div>
      <br/>
       <div>
           <asp:Label ID="Label3" runat="server" Text="שם משפחה:" Width="100"></asp:Label>
           <asp:TextBox ID="Last_Name_TB" runat="server"></asp:TextBox><br/>

       </div> 
       <br/>
           <div>
                <asp:Label ID="Label8" runat="server" Text="דואר אלקטרוני:" Width="100"></asp:Label>
                <asp:TextBox ID="Email_TB" runat="server"></asp:TextBox><br/>
           </div>
     <br/>
        <div>
           <asp:Label ID="Label6" runat="server" Text="טלפון:" Width="100"></asp:Label >
           <asp:TextBox ID="Prime_Phone_TB" runat="server"></asp:TextBox><br/>

       </div> 
      <br/>
        <div>
           <asp:Label ID="Label7" runat="server" Text="טלפון משני:" Width="100"></asp:Label>
           <asp:TextBox ID="Sec_Phone_TB" runat="server"></asp:TextBox><br/>

       </div>
       
    </div>
    <div class="leftReg">
            <div>
                <asp:Label ID="Label9" runat="server" Text="שם פרטי (אנגלית):" Width="150"></asp:Label>
                <asp:TextBox ID="First_Name_Eng_TB" runat="server"></asp:TextBox><br/>
            </div>
         <br/>   
         <div>
                <asp:Label ID="Label10" runat="server" Text="שם משפחה (אנגלית):" Width="150"></asp:Label>
                <asp:TextBox ID="Last_Name_Eng_TB" runat="server"></asp:TextBox><br/>
            </div>
         <br/>    
        <div>
                <asp:Label ID="Label11" runat="server" Text="כתובת מגורים (אנגלית):" Width="150"></asp:Label>
                <asp:TextBox ID="Addr_TB" runat="server"></asp:TextBox><br/>           
            </div>
         <br/>   
         <div>
                <asp:Label ID="Label12" runat="server" Text="עיר:" Width="150"></asp:Label>
                <asp:TextBox ID="City_TB" runat="server"></asp:TextBox><br/>
            </div>
         <br/>   
         <div>
             <asp:Label ID="Label13" runat="server" Text="בחר מכללה:" Width="150"></asp:Label>
             <asp:DropDownList OnTextChanged="Load_Courses" AutoPostBack="true" ID="Collage_DL" runat="server" Width="155">
             </asp:DropDownList>
         </div>
         <br/>    
        <div>
            <asp:Label ID="Label14" runat="server" Text="בחר קורס:" Width="150"></asp:Label>
            <asp:DropDownList ID="Course_DL" runat="server" Width="155">
            </asp:DropDownList>
        </div>
       
    </div> 
     <div id="StudentUpdateButton">
        <asp:Button ID="UpdateButton" OnClick="Update_Bottun_On_Click" runat="server" Text="עדכן" BackColor="#F00000" ForeColor="White" />
        <asp:Button ID="CancelButton" OnClick="Cancel_Bottun_On_Click" runat="server" Text="ביטול" BackColor="#F00000" ForeColor="White"/>
         <a href="StudentPasswordChangePage.aspx" target="popup"
            onclick="window.open(this.href, '','toolbar=no, resizable=no, location=no, width=400, height=300');return false;" ><asp:Button ID="ChangeButton" OnClientClick="Change_Bottun_On_Click" runat="server" Text="שנה סיסמא" BackColor="#F00000" ForeColor="White"/></a>
     </div>  
</asp:Content>

