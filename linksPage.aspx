<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="linksPage.aspx.cs" Inherits="linksPage" %>

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
    <div id="picLink">
        <div id="johnPic">
          John Bryce<br/>
            <a href="http://www.johnbryce.co.il/"> <img src="https://dl.dropboxusercontent.com/u/229188151/pic/john%20bryce_link.png" width="320" height="250" border="1" /></a>
        </div>
        <div id="microPic">
           Microsoft<br/>
            <a href="http://www.microsoft.com/he-il/default.aspx"> <img src="https://dl.dropboxusercontent.com/u/229188151/pic/microsoft_link.png" width="320" height="250"  border="1" /></a>
        </div>
        <div id="promPic">
            Prometric<br/>
            <a href="https://www.prometric.com/en-us/Pages/home.aspx"> <img src="https://dl.dropboxusercontent.com/u/229188151/pic/prometric_link.png" width="320" height="255"/></a>  
        </div>
    </div>
</asp:Content>

