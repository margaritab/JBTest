<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="aboutUsPage.aspx.cs" Inherits="aboutUsPage" %>

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
    <h3 id="welcomeText">
        אנו שלושה סטודנטים להנדסת תוכנה "במכללה האקדמית להנדסה " - דדי סביליה, רוית לוי ומרגריטה בראון.
        <br/>
 במסגרת קורס "תכנות בסביבת אינטרנט" הקמנו אתר זה עבור מכללת ג'ון ברייס במטרה לייעל את מערכת ניהול רישום סטודנטים למבחנים,וניהול המבחנים.
        <br /><br />
         המערכת נועדה לשם הרשמה למבחני מייקרוסופט הן לסטודנטים של מכללת ג'ון ברייס והן לכלל הציבור אשר מעוניינים להירשם למבחנים ולהיבחן במכללת ג'ון ברייס בירושלים או במכללת ג'ון ברייס בתל אביב.
        <br /><br />
        חברת ג'ון ברייס - מכללת הי-טק מקבוצת matrix הינה חברת ההדרכה המובילה בישראל ומהמובילות באירופה בתחום הדרכת המחשוב וטכנולוגיית המידע והאלקטרוניקה ובתחום הדרכת מיומנות הניהול, השירות והמכירות. לג'ון ברייס - מכללת הי-טק 3 מרכזי הדרכה בישראל ומרכזים נוספים באירופה ובמזרח הרחוק.
    </h3>
</asp:Content>

