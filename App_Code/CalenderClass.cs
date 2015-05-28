using System;
//using System.DateTime;

public class CalenderClass
{

    public int getdaysInMonth(int year, int month)
    {
        return DateTime.DaysInMonth(year, month);
    } 

    public int getTodayYear()
    {
        DateTime now = DateTime.Now;   // This gets the current date and time.
        return now.Year;
    }

    public int getTodayMonth()
    {
        DateTime now = DateTime.Now;   // This gets the current date and time.
        return now.Month;
    }

    public int getTodayDay()
    {
        DateTime now = DateTime.Now;   // This gets the current date and time.
        return now.Day;
    }

    public DateTime getTodayFullDate()
    {
        DateTime now = DateTime.Now;   // This gets the current date and time.
        return now.Date;
    }
    public String getHebMonthName(int month)
    {
        switch (month)
        {
            case 1:
                return "ינואר";
            case 2:
                return "פברואר";
            case 3:
                return "מרץ";
            case 4:
                return "אפריל";
            case 5:
                return "מאי";
            case 6:
                return "יוני";
            case 7:
                return "יולי";
            case 8:
                return "אוגוסט";
            case 9:
                return "ספטמבר";
            case 10:
                return "אוקטובר";
            case 11:
                return "נובמבר";
            case 12:
                return "דצמבר";
            default:
                return "";

        }
    }
    public int getFirstDayOfMonth(int year, int month)
    {
        DateTime startOfMonth=  new DateTime(year, month, 1);
        return (int)startOfMonth.DayOfWeek;
    }


    public string changeDateFormat(string oldDate)//change from dd/mm/yyyy to mm/dd/yyyy
    {
        string month, day, year;
        string sign = "/";
        string newDate =oldDate;

        if (oldDate.Contains("-"))
            sign = "-";

        if (!oldDate.Equals(""))
        {
            day = oldDate.Substring(0, oldDate.IndexOf(sign));
            oldDate = oldDate.Remove(0, oldDate.IndexOf(sign) + 1);
            month = oldDate.Substring(0, oldDate.IndexOf(sign));
            year = oldDate.Substring(oldDate.IndexOf(sign) + 1);
            newDate = month + "/" + day + "/" + year;
        }
        return newDate;
    }
}
