<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="contactUs.aspx.cs" Inherits="contactUs" %>

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
   <h1>טופס צור קשר</h1>
    <h3><span style ="color:#F00000">* </span>נא למלא את הטופס רק עבור בעיות כניסה למערכת</h3>
     <div id="contactUsLables"> 
        <div>
             <asp:Label ID="firstName_LB" runat="server" Text="שם פרטי:" Width="100"></asp:Label>
             <asp:TextBox ID="firstName_TB" runat="server"></asp:TextBox><br/>

         </div>
         <br/>
         <div>
            <asp:Label ID="lastName_LB" runat="server" Text="שם משפחה:" Width="100"></asp:Label>
            <asp:TextBox ID="lastName_TB" runat="server"></asp:TextBox><br/>

         </div> 
         <br/>
         <div>
             <asp:Label ID="email_LB" runat="server" Text="דואר אלקטרוני:" Width="100"></asp:Label>
             <asp:TextBox ID="email_TB" runat="server"></asp:TextBox><br/>
         </div>
        <br/>
        <div>
            <asp:Label ID="subject_LB" runat="server" Text="נושא הפנייה:" Width="100"></asp:Label>
            <asp:TextBox ID="subject_TB" runat="server"></asp:TextBox>
        </div>
        <br/>
        <div id="contacting">
            <asp:Label ID="content_LB" runat="server" Text="תוכן הפנייה:" Width="100"></asp:Label>   
            <asp:TextBox id="content_TA" TextMode="multiline" Columns="50" Rows="5" runat="server" />
        </div>
         <br/>
         <div id="sendButton">
            <asp:Button ID="send" OnClick="Send_Button_On_Click" runat="server" Text="שליחה" BackColor="#F00000 " ForeColor="White" Width="100" Height="30" Font-Bold="True" />
         </div> 
         <br/> 
    </div>
</asp:Content>

