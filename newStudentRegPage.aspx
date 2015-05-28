<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="newStudentRegPage.aspx.cs" Inherits="newStudentRegPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContantHeader" Runat="Server">
    <ul>
        <li><a href="main.aspx">דף הבית</a></li>
        <li><a href="studentEntrencePage.aspx">כניסת סטודנט</a></li>
        <li><a href="systemEntrencePage.aspx">כניסת סגל</a></li>
        <li><a href="aboutUsPage.aspx">אודות</a></li>
        <li><a href="linksPage.aspx">קישורים</a></li>
        <li><a href="contactUs.aspx">צור קשר</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBody" Runat="Server">
    <h1>רישום סטודנט חדש</h1>

 <div class="rightReg">
       <div>
            <asp:Label ID="Label1" runat="server" Text="ת.ז:" Width="100"></asp:Label>
            <asp:TextBox ID="idTB" runat="server"></asp:TextBox><br/>
       </div> 
     <br/>
       <div>
           <asp:Label ID="Label2" runat="server" Text="שם פרטי:" Width="100"></asp:Label>
           <asp:TextBox ID="hebFirstNameTB" runat="server"></asp:TextBox><br/>

       </div>
      <br/>
       <div>
           <asp:Label ID="Label3" runat="server" Text="שם משפחה:" Width="100"></asp:Label>
           <asp:TextBox ID="hebLastNameTB" runat="server"></asp:TextBox><br/>

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
           <asp:Label ID="Label6" runat="server" Text="טלפון:" Width="100"></asp:Label >
           <asp:TextBox ID="primePhoneTB" runat="server"></asp:TextBox><br/>

       </div> 
      <br/>
        <div>
           <asp:Label ID="Label7" runat="server" Text="טלפון משני:" Width="100"></asp:Label>
           <asp:TextBox ID="secPhoneTB" runat="server"></asp:TextBox><br/>

       </div>
       
    </div>
    <div class="leftReg">
            <div>
                <asp:Label ID="Label8" runat="server" Text="דואר אלקטרוני:" Width="150"></asp:Label>
                <asp:TextBox ID="emailTB" runat="server"></asp:TextBox><br/>
            </div>
        <br/>
             <div>
                <asp:Label ID="Label9" runat="server" Text="שם פרטי (אנגלית):" Width="150"></asp:Label>
                <asp:TextBox ID="engFirstNameTB" runat="server"></asp:TextBox><br/>
            </div>
         <br/>   
         <div>
                <asp:Label ID="Label10" runat="server" Text="שם משפחה (אנגלית):" Width="150"></asp:Label>
                <asp:TextBox ID="engLastNameTB" runat="server"></asp:TextBox><br/>
            </div>
         <br/>    
        <div>
                <asp:Label ID="Label11" runat="server" Text="כתובת מגורים (אנגלית):" Width="150"></asp:Label>
                <asp:TextBox ID="addressTB" runat="server"></asp:TextBox><br/>           
            </div>
         <br/>   
         <div>
                <asp:Label ID="Label12" runat="server" Text="עיר:" Width="150"></asp:Label>
                <asp:TextBox ID="cityTB" runat="server"></asp:TextBox><br/>
            </div>
         <br/>
        <asp:ScriptManager ID="MainScriptManager" runat="server" />   
        <asp:UpdatePanel ID="pnlCourseInCollage" runat="server">
         <ContentTemplate>  
           <div>
             <asp:Label ID="collages" runat="server" Text="בחר מכללה:" Width="150"></asp:Label>
             <asp:DropDownList OnTextChanged="Load_Courses" AutoPostBack="true" ID ="collageDropDownList" runat="server" Width="155">
                <asp:ListItem Text="בחר" Value="0"/>
             </asp:DropDownList>
         </div>
         <br/>    
        <div>
            
            <asp:Label ID="courses" runat="server" Text="בחר קורס:" Width="150"></asp:Label>
             <asp:DropDownList ID="courseDropDownList" AutoPostBack="true" runat="server" Width="155">
                <asp:ListItem Text="בחר" Value="0"/>
            </asp:DropDownList>
        </div>
             </ContentTemplate>

       </asp:UpdatePanel>
       
    </div>   
    <div id="regulations">
        <asp:CheckBox ID="rulesCB" runat="server"/> קראתי את <a href="standardizationPage.aspx" target="popup"
            onclick="window.open(this.href, 'תקנון','toolbar=no, resizable=no, location=no, width=400, height=300');return false;" >התקנון</a>
    </div>
    <div id="RegButton" >
        <asp:Button ID="confirmButton" OnClick="Confirm_Bottun_On_Click" runat="server" Text="אישור" BackColor="#F00000" ForeColor="White" />
        <asp:Button ID="cancelButton" OnClick="Cancel_Bottun_On_Click" runat="server" Text="ביטול" BackColor="#F00000" ForeColor="White"/>
        <asp:Button ID="clearButton" OnClick="Clear_Bottun_On_Click" runat="server" Text="נקה" BackColor="#F00000" ForeColor="White"/>    
    </div>
    <br/>
</asp:Content>

