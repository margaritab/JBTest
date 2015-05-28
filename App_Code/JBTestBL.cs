using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

public class JBTestBL
{
    JBTestDAL dal;
    public JBTestBL()
    {
        dal = new JBTestDAL();
    }

    public LinkedList<String> getAllCollagesNames()
    {
        LinkedList<String> collageList = dal.allCollagesNames();
        return collageList;
    }

    public LinkedList<String> getAllCoursesNamesInCollage(string collageName)
    {
        LinkedList<String> courseList = dal.allCoursesNamesInCollage(collageName);
        return courseList;
    }

    /*add new student*/
    public Boolean setNewStudentDetailes(string id, string hebFirstName, string hebLastName,
            string password, string primePhone, string secPhone, string email, string engFirstName,
            string engLastName, string addr, string city, string collageName, string courseCode)
    {
        Boolean retVal = false;

        //find collageCode
        string collageCode = dal.collageCodeByCollageName(collageName);

        if (!collageCode.Equals(""))
        {
            if (collageCode.Equals("HA"))
            {
                collageCode = "JER";
            }
            else if (collageCode.Equals("EX"))
            {
                if (courseCode.Contains("JER"))
                    collageCode = "JER";
                else
                    collageCode = "TLV";
            }
            //setNewStudent
            retVal = dal.addNewStudentDetails(id, hebFirstName, hebLastName, password, primePhone,
                secPhone, email, engFirstName, engLastName, addr, city, collageCode, courseCode);
        }

        return retVal;
    }

    /*add new system member*/
    public Boolean setNewSystemDetails(string id, string firstName, string lastName,
             string password, string username, string collageName, string email)
    {
        Boolean retVal = false;

        //find collageCode
        string collageCode = dal.collageCodeByCollageName(collageName);

        if (!collageCode.Equals(""))
        {
            //setNewSystem
            retVal = dal.addNewSystemDetails(id, firstName, lastName, password, username, collageCode, email);
        }

        return retVal;
    }

    public Boolean doesStudentExist(string studentId)/*return true if student exist*/
    {
        return dal.studentExist(studentId);
    }

    public Boolean doesSystemExist(string username)/*return true if system exist*/
    {
        return dal.systemExist(username);
    }

    public Boolean doesSystemExistById(string id)/*return true if system exist*/
    {
        return dal.systemExistById(id);
    }

    public String checkStudentLogin(string userName)/*return student password*/
    {
        return dal.checkStudentLogin(userName);
    }

    public String checkSystemLogin(string userName)/*return system password*/
    {
        return dal.checkSystemLogin(userName);
    }

    public String getStudentFullNameById(String studentId)
    {
        return dal.studentFullNameById(studentId);
    }

    public String getSystemFullNameById(String username)
    {
        return dal.systemFullNameById(username);
    }

    /*student details functions*/

    public String getStudentEmail(String studentId)
    {
        return dal.studentEmail(studentId);
    }

    public String getStudentPrimePhone(String studentId)
    {
        return dal.studentPrimePhone(studentId);
    }

    public String getStudentSecPhone(String studentId)
    {
        return dal.studentSecPhone(studentId);
    }

    public String getStudentEngFirstName(String studentId)
    {
        return dal.studentEngFirstName(studentId);
    }

    public String getStudentEngLastName(String studentId)
    {
        return dal.studentEngLastName(studentId);
    }

    public String getStudentAddr(String studentId)
    {
        return dal.studentAddr(studentId);
    }

    public String getStudentCity(String studentId)
    {
        return dal.studentCity(studentId);
    }

    public String getStudentCollageName(String studentId)
    {
        return dal.studentCollageName(studentId);
    }

    public String getStudentCourseCode(String studentId)
    {
        return dal.studentCourseCode(studentId);
    }

    //***************************************************marga********************************
    public string getStudentSr(String studentId)
    {
        return dal.studentSr(studentId);
    }

    public int getStudentFreeTestNum(String studentId, String collage, String course)
    {
        return dal.studentFreeTestNum(studentId, collage, course);
    }

    public string getCollageCodeByCollageName(string collageName)
    {
        return dal.collageCodeByCollageName(collageName);
    }



    //***************************************************marga********************************


    public Boolean setUpdatedStudentPassword(string id, string password)
    {
        Boolean retVal = false;

        retVal = dal.updateStudentPassword(id, password);

        return retVal;
    }

    public Boolean setUpdatedSystemPassword(string userName, string password)
    {
        Boolean retVal = false;

        retVal = dal.updateSystemPassword(userName, password);

        return retVal;
    }


    public LinkedList<String> getTestsByCourseCode(string Id)
    {
        string courseCode = dal.studentCourseCode(Id);

        LinkedList<String> testList = dal.allTestsByCourseCode(courseCode);
        return testList;
    }

    public LinkedList<String> getAvailableDates(string start, string end, string studentId)
    {
        string collageId = dal.studentCollageCode(studentId);
        LinkedList<String> dateList = dal.allAvailableDate(start, end, collageId);
        return dateList;
    }

    public Boolean setNewTestRegInfo(string id, string testCode, string date, string hour, string ss)  /*set new test reg info*/
    {
        return dal.addNewTestRegInfo(id, testCode, date, hour, ss);
    }

    /**************** Calender Page *******************/

    public String getStaffWorkCollageId(string staffUserName)
    {
        return dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));
    }

    public LinkedList<String> getAllConfirmedTests(string date, string staffUserName)
    {
        string staffCollageId = dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));
        LinkedList<String> confirmedTests = dal.allConfirmesTests(date, staffCollageId);
        return confirmedTests;
    }

    public LinkedList<String> getAllConfirmedTestsByStudentId(string id, string date)
    {
        LinkedList<String> confirmedTests = dal.allConfirmesTestsByStudentId(id, date);
        return confirmedTests;
    }

    public Boolean setScheduleByCalender(String date)
    {
        return dal.addSchedule(date);
    }

    public Boolean setScheduleInCollageByCalender(String date, int testCounter, string userName, string str)
    {
        string collageId;
        if (str.Equals("staff"))
            collageId = dal.staffWorkCollageId(dal.staffIdByUserName(userName));
        else
            collageId = dal.studentCollageCode(userName);
        return dal.addScheduleInCollage(date, testCounter, collageId);
    }

    public String getDateExistence(String date)
    {
        return dal.searchScheduleDate(date);
    }

    public String getDateExistence(String date, string userName, string str)
    {
        string collageId;
        if (str.Equals("staff"))
            collageId = dal.staffWorkCollageId(dal.staffIdByUserName(userName));
        else
            collageId = dal.studentCollageCode(userName);
        return dal.searchScheduleInCollageDate(date, collageId);
    }


    public int getNumOfTestsInDay(string date, string UserName, string str)
    {
        string CollageId = "";
        if (str.Equals("staff"))
            CollageId = dal.staffWorkCollageId(dal.staffIdByUserName(UserName));
        else
            CollageId = dal.studentCollageCode(UserName);

        return dal.testsInDay(date, CollageId);
    }


    public String getStudentCollageCode(string studentId)
    {
        return dal.studentCollageCode(studentId);
    }

    /**************** End Calender Page *******************/



    //***************************************************marga********************************
    //system student page details dal for tables

    public LinkedList<String[]> getStudentsTestToApprove(string studentId)
    {
        LinkedList<String[]> approveList = dal.studentsTestToApprove(studentId);
        return approveList;
    }

    public LinkedList<String[]> getStudentsCloseTests(string studentId)
    {
        LinkedList<String[]> closeList = dal.studentsCloseTests(studentId);
        return closeList;
    }

    public LinkedList<String[]> getStudentsPastTests(string studentId)
    {
        LinkedList<String[]> pastList = dal.studentsPastTests(studentId);
        return pastList;
    }

    public Boolean getSecondShotWasUsed(string secShot)
    {
        return dal.secondShotWasUsed(secShot);
    }

    public Boolean setConfirmStudentsTest(string studentId, string testCode, string date, string useFree, string useSec, string staffUserName)
    {
        Boolean retVal = dal.confirmStudentsTest(studentId, testCode, date, useFree, useSec);
        string staffCollageId = dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));
        if (retVal)
            retVal = dal.updateTestCounter(-1, date, staffCollageId);

        return retVal;
    }

    //end of system student page details dal for tables
    //***************************************************marga********************************


    /**ravit changes 2/9 */
    /*ravit 3/9*/
    public Boolean getStudentUseSecoundShot(string id, string date, string testCode)
    {
        Boolean retVal = false;

        retVal = dal.studentUseSecoundShot(id, date, testCode);

        return retVal;
    }

    public Boolean getStudentUseFreeTests(string id, string date, string testCode)
    {
        Boolean retVal = false;

        retVal = dal.studentUseFreeTests(id, date, testCode);

        return retVal;
    }

    public Boolean getStudentPaidForTests(string id, string date, string testCode)
    {
        Boolean retVal = false;

        retVal = dal.studentPaidForTests(id, date, testCode);

        return retVal;
    }
    public string getStudentTestCodeByDateAndId(string id, string date)
    {
        return dal.studentTestCodeByDateAndId(id, date);
    }

    public LinkedList<String[]> getCoursesList()
    {
        return dal.coursesList();
    }



    public Boolean setStudentWaitForApproveTests(string id, string pass, string date)
    {
        Boolean retVal = false;

        retVal = dal.UpdateStudentWaitForApproveTests(id, pass, date);

        return retVal;
    }


    /**************** System Main Page *******************/

    public LinkedList<String[]> getStudentTodayTests(string todayDate, string staffUserName) //show todays tests
    {
        string staffCollageId = dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));
        LinkedList<String[]> infoList = dal.studentTodayTests(todayDate, staffCollageId);
        return infoList;
    }

    public Boolean setStudentPassFailTests(string id, string pass)
    {
        Boolean retVal = false;

        retVal = dal.updateStudentPassFailTests(id, pass);

        return retVal;
    }

    public Boolean setStudentPaidTests(string id)
    {
        Boolean retVal = false;

        retVal = dal.updateStudentPaidTests(id);

        return retVal;
    }




    public LinkedList<String[]> getStudentWaitForConfiremdTests(string staffUserName) // show tests to confirm 
    {
        string staffCollageId = dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));
        LinkedList<String[]> infoList = dal.studentWaitForConfiremdTests(staffCollageId);
        return infoList;
    }

    public LinkedList<String[]> getStudentWaitForCancelTests(string staffUserName) //cancel test
    {
        string staffCollageId = dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));
        LinkedList<String[]> infoList = dal.studentWaitForCancelTests(staffCollageId);
        return infoList;
    }

    public Boolean setUpdateStudentFreeTest(int num, string id, string courseCode)
    {
        Boolean retVal = false;

        retVal = dal.updateStudentFreeTest(num, id, courseCode);

        return retVal;
    }

    public Boolean setStudentWaitForCancelTests(string id, string pass, string date)
    {
        Boolean retVal = false;

        retVal = dal.updateStudentWaitForCancelTests(id, pass, date);

        return retVal;
    }

    /**************** End System Main Page *******************/



    /**************** Student Main Page *******************/
    public Boolean updateNewTestRegInfo(string Id, string thisTestCode, string thisDate, string thisHour, string thisSs, string otherTestCode, string otherDate, string otherHour, string otherSs)  /*update test reg info*/
    {
        return dal.updateTestRegInfo(Id, thisTestCode, thisDate, thisHour, thisSs, otherTestCode, otherDate, otherHour, otherSs);
    }

    public LinkedList<String[]> getApprovedTests(string studentId)
    {
        LinkedList<String[]> infoList = dal.checkTestsApproved(studentId);
        return infoList;
    }

    public LinkedList<String[]> getNotApprovedTests(string studentId)
    {
        LinkedList<String[]> infoList = dal.checkTestsNotApproved(studentId);
        return infoList;
    }

    public Boolean setApproveTestStudentTable(string id, string testCode)
    {
        Boolean retVal = false;
        retVal = dal.updateApproveTestStudentTable(id, testCode);

        return retVal;
    }

    public Boolean setChangeApproveTestStudentTable(string id, string testCode)
    {
        Boolean retVal = false;
        retVal = dal.opesetUpdateApproveTestStudentTable(id, testCode);

        return retVal;
    }

    public Boolean getCancelButtonWaitStatus(string id, string date, string testCode)
    {
        return dal.cancelButtonWaitStatus(id, date, testCode);
    }
    public DateTime getStudaentDateByIdAndTestCode(string studentId, string testCode)
    {
        return dal.studentDateByStudentAndTestCode(studentId, testCode);
    }

    public String getFullStudentTestCode(string testCode)
    {
        return dal.FullStudentTestCode(testCode);
    }

    public String getSecondshot(string testCode, string date, string studentId)
    {
        return dal.secondshotByIdTestCodeDate(testCode, date, studentId);
    }

    public Boolean cancelApproveTestStudentTable(string id, string date, string testCode)
    {
        Boolean retVal = false;
        retVal = dal.updateNotApproveTestStudentTable(id, date, testCode);

        return retVal;
    }

    /**************** End Student Main Page *******************/



    /******************* Course Pages ********************/
    public LinkedList<String[]> getActiveCoursesList(string staffUserName)
    {
        string staffCollageId = dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));

        return dal.activeCoursesList(staffCollageId);
    }

    public LinkedList<String[]> getOldCoursesList(string staffUserName)
    {
        string staffCollageId = dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));

        return dal.oldCoursesList(staffCollageId);
    }

    public Boolean setCreateNewCourse(string code, string name, string sDate, string eDate, string free, string staffUserName)
    {
        Boolean retVal = false;
        string staffCollageId = dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));

        retVal = dal.createNewCourse(code, name, sDate, eDate, free);
        if (retVal)
            retVal = dal.createNewCourseInCollage(code, staffCollageId);

        return retVal;
    }

    public LinkedList<String[]> getstudentThatStudyAtCourseList(string courseCode)
    {
        return dal.studentThatStudyAtCourseList(courseCode);
    }
    public String[] getCourseStartAndEndDates(string courseCode)
    {
        return dal.courseStartAndEndDates(courseCode);
    }


    public Boolean setUpdateCourseInfo(string sDate, string eDate, string code)
    {

        return dal.updateCourseInfo(sDate, eDate, code);
    }

    public LinkedList<String[]> getStudentTestReport(string sDate, string eDate, string staffUserName)
    {
        string staffCollageId = dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));

        return dal.studentTestReport(sDate, eDate, staffCollageId);
    }
    /******************* End Course Pages ********************/

    public Boolean setUpdateTestCounter(int num, string date, string collageId)
    {
        Boolean retVal = false;

        retVal = dal.updateTestCounter(num, date, collageId);

        return retVal;
    }

    /******************************Student History Page *******************************/
    public LinkedList<String[]> getTestsHistoryById(string studentId)
    {
        LinkedList<String[]> history = new LinkedList<string[]>();
        return dal.testHistoryById(studentId);
    }

    /******************************End Student History Page *******************************/


    /******************************Update Students Details *******************************/

    public Boolean setUpdatedStudentDetailes(string id, string hebFirstName, string hebLastName,
               string password, string primePhone, string secPhone, string email, string engFirstName,
               string engLastName, string addr, string city, string collageName, string courseCode, string originalCollage, string originalCourse, int tests)
    {
        Boolean retVal = false;

        //find collageCode
        string collageCode = dal.collageCodeByCollageName(collageName);

        if (!collageCode.Equals(""))
        {
            if (collageCode.Equals("HA"))
            {
                collageCode = "JER";
            }
            else if (collageCode.Equals("EX"))
            {
                if (courseCode.Contains("JER"))
                    collageCode = "JER";
                else
                    collageCode = "TLV";
            }
            //setNewStudent
            retVal = dal.updateStudentDetails(id, hebFirstName, hebLastName, password, primePhone,
                secPhone, email, engFirstName, engLastName, addr, city, collageCode, courseCode,
                originalCollage, originalCourse, tests);
        }

        return retVal;
    }

    public Boolean setUpdatedStudentDetailesBySystem(string id, string hebFirstName, string hebLastName,
              string password, string primePhone, string secPhone, string email, string engFirstName,
              string engLastName, string addr, string city, string collageName, string courseCode, string sr, string originalCollage, string originalCourse, int tests)
    {
        Boolean retVal = false;

        //find collageCode
        string collageCode = dal.collageCodeByCollageName(collageName);

        if (!collageCode.Equals(""))
        {
            if (collageCode.Equals("HA"))
            {
                collageCode = "JER";
            }
            else if (collageCode.Equals("EX"))
            {
                if (courseCode.Contains("JER"))
                    collageCode = "JER";
                else
                    collageCode = "TLV";
            }

            //setNewStudent
            retVal = dal.updateStudentDetailsBySystem(id, hebFirstName, hebLastName, password, primePhone,
                secPhone, email, engFirstName, engLastName, addr, city, collageCode, courseCode, sr,
                originalCollage, originalCourse, tests);
        }

        return retVal;
    }
    public string getCourseById(string studentId)
    {
        return dal.courseById(studentId);
    }

    public int getTestsAmount(string studentId)
    {
        return dal.testAmount(studentId);
    }

    /*ravit 7/9 */
    public LinkedList<String> getAllTests()
    {
        return dal.allTests();
    }

    public Boolean setAddTestToNewCourse(string courseCode, string testCode)
    {
        Boolean retVal = false;

        retVal = dal.addTestToNewCourse(courseCode, testCode);

        return retVal;
    }

    public String[] getSecoundShotAndHourById(string id)
    {
        return dal.secoundShotAndHourById(id);

    }

    /***************************** Tests Page ********************************/
    public Boolean setCreateNewTest(string code, string name)
    {
        Boolean retVal = false;

        retVal = dal.createNewTest(code, name);

        return retVal;
    }

    public LinkedList<String[]> getTestNameAndCodeList()
    {
        return dal.TestNameAndCodeList();
    }

    public Boolean setUpdateTestInfo(string newCode, string newName, string oldCode, string oldName)/*return true if test update*/
    {
        return dal.updateTestInfo(newCode, newName, oldCode, oldName);
    }

    public Boolean setDeleteTests(string Code)/*return true if test deleted*/
    {
        return dal.deleteTests(Code);
    }

    public String getTestNameByCode(string code)
    {
        return dal.testNameByCode(code);
    }

    /***************************** End Tests Page ********************************/


    /*********************************Message Page********************************/
    public LinkedList<String[]> getCurrMessages(string userName, string str)  /*get current messages*/
    {
        string collageId = "";
        if (str.Equals("staff"))
            collageId = dal.staffWorkCollageId(dal.staffIdByUserName(userName));
        else
            collageId = dal.studentCollageCode(userName);
        return dal.currMessages(collageId);
    }

    public Boolean setNewMessage(string date, string message, string staffUserName)  /*add new message*/
    {
        string staffCollageId = dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));
        return dal.addNewMessage(date, message, staffCollageId);
    }

    public Boolean setUpdateMessage(string date, string message, string staffUserName)  /*add new message*/
    {
        string staffCollageId = dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));
        return dal.updateMessageDetails(date, message, staffCollageId);
    }

    public Boolean setDeleteMessage(string date, string message, string staffUserName)  /*add new message*/
    {
        string staffCollageId = dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));
        return dal.deleteMessage(date, message, staffCollageId);
    }

    public LinkedList<String> getMessagesByDateAndCollageId(string staffUserName, string date)
    {
        string staffCollageId = dal.staffWorkCollageId(dal.staffIdByUserName(staffUserName));
        return dal.messagesByDateAndCollageId(staffCollageId, date);
    }
    /*********************************End Message Page********************************/

    /*********************************Forgot User Name Page********************************/
    public String getStaffUserNameById(string id)
    {
        return dal.staffUserNameById(id);
    }

    /*********************************End Forgot User Name  Page********************************/

    /*********************************Forgot Password Name Page********************************/
    public Boolean getConfirmStudentEmailByID(string id, string email)
    {
        Boolean retVal = false;
        string strEmail = dal.confirmStudentEmailByID(id);
        if (strEmail.Equals(email))
            retVal = true;

        return retVal;

    }


    public Boolean getConfirmSystemEmailByUser(string user, string email)
    {
        Boolean retVal = false;
        string strEmail = dal.confirmSystemEmailByUser(user);
        if (strEmail.Equals(email))
            retVal = true;

        return retVal;

    }


    public Boolean getConfirmSystemEmailByID(string id, string email)
    {
        Boolean retVal = false;
        string strEmail = dal.confirmSystemEmailByID(id);
        if (strEmail.Equals(email))
            retVal = true;

        return retVal;

    }

    /*********************************End Forgot Password Name Page********************************/

    /********************************reset admin password****************************************/
    public Boolean setResetAdminPassword(string oldPassword, string password)
    {
        return dal.resetAdminPassword(oldPassword, password);
    }

    /**********************************End reset admin password**********************************/

    public String[] getSystemInfo(string systemId)
    {
        return dal.systemInfo(systemId);

    }


    public string getCollageNameByCollageCode(string collageCode)
    {
        return dal.collageNameByCollageCode(collageCode);

    }
    public Boolean setUpdateStaffInfo(string firstName, string lastName, string userName, string email, string collageCode, string id)
    {
        Boolean retVal1 = dal.updateStaffCollage(collageCode,id);
        Boolean retVal2 = dal.updateStaffInfo(firstName, lastName, userName, email, id);

        return (retVal1 && retVal2);
    }
}


