using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Globalization;

public class JBTestDAL
{
    static readonly string connectionStr = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\USERS\USER\DOCUMENTS\JBTESTDB.MDF;Integrated Security=True;Connect Timeout=30";
       
        public LinkedList<String> allCollagesNames()  /*return all collages name*/
        {
            LinkedList<String> collageNameList = null;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Name from collage";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                collageNameList = new LinkedList<String>();

                while (rdr.Read())
                {
                    collageNameList.AddLast((string)rdr["Name"]);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return collageNameList;
        }


        public String collageCodeByCollageName(string collageName)  /*return collage code*/
        {
            String collageCode ="";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Id from collage where name = N'"+collageName+"'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    collageCode=(String)rdr["Id"];
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return collageCode;
        }


        public String collageNameByCollageCode(string collageCode)  /*return collage name*/
        {
            String collageName = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Name from collage where Id = '" + collageCode + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    collageName = (String)rdr["Name"];
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return collageName;
        }




        public LinkedList<String> allCoursesNamesInCollage(string collageName)  /*return all courses name in collage*/
        {
            LinkedList<String> courseNameList = null;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select cic.Course_Code from course_in_collage cic,collage c where cic.Collage_Id=c.Id AND c.Name=N'" + collageName + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                courseNameList = new LinkedList<String>();

                while (rdr.Read())
                {
                    courseNameList.AddLast((string)rdr["Course_Code"]);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return courseNameList;
        }



        public Boolean addNewStudentDetails(string id, string hebFirstName , string hebLastName ,
            string password , string primePhone ,string secPhone , string email , string engFirstName ,
            string engLastName, string addr , string city, string collageId, string courseCode)  /*add new student from student registration*/
        {
            Boolean retVal = false; //return val if adding the student was successful
            string sqlFreeTest;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //add new student
                string sqlStringStudent = "INSERT INTO students VALUES ( '"+ id +"','',N'"+ hebFirstName
                    + "',N'" + hebLastName + "','" + engFirstName + "','" + engLastName + "','" + password + "','"
                    + addr + "',N'" + city + "','" + email + "','" + primePhone + "','" + secPhone + "')";
    
                //add student course and collage
                string sqlStringStudyAt = "INSERT INTO study_at VALUES ( '"+ id +"','"+ collageId +"','"+ courseCode +"',0)";
                
                //update student free test numbers
                if (courseCode.Contains("Extern") || courseCode.Contains("Haredit"))
                {
                  sqlFreeTest = "UPDATE study_at SET Free_Test_Num=0 WHERE Student_Id ='"+ id +"'";
                }
                else
                {
                    int temp = freeTestNumByCourseCode(courseCode);
                    sqlFreeTest = "UPDATE study_at SET Free_Test_Num=" + temp + " WHERE Student_Id='" + id + "'";
                }

                //execute querys
                SqlCommand command1 = new SqlCommand(sqlStringStudent, connection);
                SqlCommand command2 = new SqlCommand(sqlStringStudyAt, connection);
                SqlCommand command3 = new SqlCommand(sqlFreeTest, connection);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public Boolean addNewSystemDetails(string id, string firstName, string lastName,
              string password, string username,string collageId, string email) /*add new system from admin registration*/
        {
            Boolean retVal = false; //return val if adding the system was successful
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //add new system
                string sqlStringSystem = "INSERT INTO staff VALUES ( '" + id + "',N'" + firstName
                    + "',N'" + lastName + "','" + username + "','"+ password + "','" + email +"')";

                //add system collage
                string sqlStringWorkAt = "INSERT INTO work_at VALUES ( '" + id + "','" + collageId + "')";

                //execute querys
                SqlCommand command1 = new SqlCommand(sqlStringSystem, connection);
                SqlCommand command2 = new SqlCommand(sqlStringWorkAt, connection);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }



        public Boolean studentExist(string studentId)  /*return true if student already exist*/
        {
            string strId = "";
            Boolean retVal = true;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Id from students where Id='"+studentId+"'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();


                while (rdr.Read())
                {
                    strId = ((string)rdr["Id"]);
                }
                if (strId.Equals(""))
                    retVal = false;

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }



        public Boolean systemExist(string sysUserName)  /*return true if system already exist*/
        {
            string strUsername = "";
            Boolean retVal = true;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select User_Name from staff where User_Name='" + sysUserName + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();


                while (rdr.Read())
                {
                    strUsername = ((string)rdr["User_Name"]);
                }
                if (strUsername.Equals(""))
                    retVal = false;

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public Boolean systemExistById(string sysId)  /*return true if system already exist*/
        {
            string strId = "";
            Boolean retVal = true;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Id from staff where Id='" + sysId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();


                while (rdr.Read())
                {
                    strId = ((string)rdr["Id"]);
                }
                if (strId.Equals(""))
                    retVal = false;

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }


        public String checkStudentLogin(string userNameStudent)  /*return student password*/
        {
            string password = "";
            
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Password from students where Id='" + userNameStudent + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();


                while (rdr.Read())
                {
                    password = ((string)rdr["Password"]);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return password;
        }

        public String checkSystemLogin(string userNameSystem)  /*return system password*/
        {
            string password = "";

            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Password from staff where User_Name='" + userNameSystem + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();


                while (rdr.Read())
                {
                    password = ((string)rdr["Password"]);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return password;
        }


        public String studentFullNameById(string studentId)  /*return student full name*/
        {
            string strFullName = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select First_Name_Heb , Last_Name_Heb from students where Id='" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strFullName = ((string)rdr["First_Name_Heb"]) + " " + ((string)rdr["Last_Name_Heb"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strFullName;
        }


        public String systemFullNameById(string username)  /*return system full name*/
        {
            string strFullName = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select First_Name , Last_Name from staff where User_Name='" + username + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strFullName = ((string)rdr["First_Name"]) + " " + ((string)rdr["Last_Name"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strFullName;
        }



       
       
        /*student detail functions*/
        public String studentEmail(string studentId)  /*return the student email by id*/
        {
            string strEmail = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Email from students where Id='" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strEmail = ((string)rdr["Email"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strEmail;
        }



        public String studentPrimePhone(string studentId)  /*return the student prime phone by id*/
        {
            string strPrimePhone = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Prime_Phone from students where Id='" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strPrimePhone = ((string)rdr["Prime_Phone"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strPrimePhone;
        }


        public String studentSecPhone(string studentId)  /*return the student Second phone by id*/
        {
            string strSecPhone = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Sec_Phone from students where Id='" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strSecPhone = ((string)rdr["Sec_Phone"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strSecPhone;
        }


        public String studentEngFirstName(string studentId)  /*return the student english first name by id*/
        {
            string strFirstName = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select First_Name_Eng from students where Id='" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strFirstName = ((string)rdr["First_Name_Eng"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strFirstName;
        }


        public String studentEngLastName(string studentId)  /*return the student english last name by id*/
        {
            string strLastName = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Last_Name_Eng from students where Id='" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strLastName = ((string)rdr["Last_Name_Eng"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strLastName;
        }

        public String studentAddr(string studentId)  /*return the student address by id*/
        {
            string strAddr = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Address from students where Id='" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strAddr = ((string)rdr["Address"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strAddr;
        }

        public String studentCity(string studentId)  /*return the student city by id*/
        {
            string strCity = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select City from students where Id='" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strCity = ((string)rdr["City"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strCity;
        }


        public String studentCollageName(string studentId)  /*return the student collage name by id*/
        {
            string strCollageName = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Collage_Id from study_at where Student_Id='" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strCollageName = ((string)rdr["Collage_Id"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return collageNameByCollageCode(strCollageName);
        }

        public String studentCollageCode(string studentId)  /*return the student collage name by id*/
        {
            string strCollageCode = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Collage_Id from study_at where Student_Id='" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strCollageCode = ((string)rdr["Collage_Id"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strCollageCode;
        }

        public String studentCourseCode(string studentId)  /*return the student course code by id*/
        {
            string strCourseCode = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Course_Code from study_at where Student_Id='" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strCourseCode = ((string)rdr["Course_Code"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strCourseCode;
        }

    //***************************************************marga********************************
        public String studentSr(string studentId)  /*return the student address by id*/
        {
            string strSr = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Sr from students where Id='" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strSr = ((string)rdr["Sr"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strSr;
        }
  

        public int studentFreeTestNum(string studentId, string collageId, string courseCode)  /*return the student free test num*/
        {
            int testNum = -1;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Free_Test_Num from study_at where Student_Id='" + studentId + "' AND Collage_Id='" + collageId + "' AND Course_Code='" + courseCode + "'"  ;
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    testNum = Convert.ToInt32(rdr["Free_Test_Num"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return testNum;
        }


       
      
    public Boolean updateStudentPassword(string id, string password)  /*update password*/
        {

            Boolean retVal = false; //return val if updating the student was successful
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //update student

                string sqlStringStudent = "UPDATE students SET Password='" + password + "' WHERE Id= '" + id + "'";


                //execute querys
                SqlCommand command = new SqlCommand(sqlStringStudent, connection);
                command.ExecuteNonQuery();
                
                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }


    public Boolean updateSystemPassword(string user, string password)  /*update password*/
    {

        Boolean retVal = false; //return val if updating the student was successful
        string connectionString = connectionStr;
        SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            connection.Open();

            //update student

            string sqlStringStudent = "UPDATE staff SET Password='" + password + "' WHERE User_Name= '" + user + "'";


            //execute querys
            SqlCommand command = new SqlCommand(sqlStringStudent, connection);
            command.ExecuteNonQuery();

            retVal = true;
        }
        catch (SqlException)
        {
            Console.WriteLine("Error connecting to sql server");
        }

        connection.Close();
        return retVal;
    }





        /*test list by course code*/
        public LinkedList<String> allTestsByCourseCode(string courseCode)  
        {
            LinkedList<String> testList = null;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select tc.Test_Code, t.Name from test_in_course tc , tests t where tc.Course_Code='" + courseCode + "'AND t.Code=tc.Test_Code";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                testList = new LinkedList<String>();

                while (rdr.Read())
                {
                    testList.AddLast((string)rdr["Test_Code"] + " - " + (string)rdr["Name"]);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return testList;
        }

        /*available date list*/
        public LinkedList<String> allAvailableDate(string startDate,string endDate,string collageId)
        {
            LinkedList<String> dateList = null;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select s.Date from schedule s, schedule_in_collage sc  where s.Date = sc.Date AND s.Date <='"+endDate +"'AND s.Date >'"+startDate+"'AND sc.Test_Counter > 0 AND sc.Collage_Id='" + collageId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                dateList = new LinkedList<String>();

                while (rdr.Read())
                {
                    DateTime date = (DateTime)rdr["Date"];
                    dateList.AddLast(date.ToString("dd-MM-yyyy",CultureInfo.InvariantCulture));
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return dateList;
        }

 
    /*insert new test reg info*/
        public Boolean addNewTestRegInfo(string id, string testCode, string date, string hour, string ss)  /*add new test reg info*/
        {
            Boolean retVal = false; //return val if adding the info was successful

            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //add new test
                string sqlStringManage = "INSERT INTO manager VALUES ( '" + id + "','" + testCode + "','" + date + "','" + hour + "','" + ss + "','Nyet','Nyet','Nyet','Nyet','Nyet','Nyet')";

                //execute querys
                SqlCommand command = new SqlCommand(sqlStringManage, connection);

                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public Boolean updateStudentWaitForCancelTests(string studentId, string cancel, string date)  /*student confirmed test*/
        {

            Boolean retVal = false; //return val if updating the student was successful
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //update student

                string sqlStringStudent = "UPDATE manager SET Cancelled ='" + cancel + "' WHERE Student_Id= '" + studentId + "' AND Date= '" + date + "'";


                //execute querys
                SqlCommand command = new SqlCommand(sqlStringStudent, connection);
                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }


    /**************** System Main Page *******************/

        public LinkedList<String[]> studentTodayTests(string todayDate, string staffCollageId)  /*student today's tests info*/
        {
            
            LinkedList<String[]> infoList = new LinkedList<String[]>();
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select m.Student_Id,m.Hour from manager m, study_at s where m.Date='" + todayDate + "' AND m.Confirmed = 'yes' AND m.Pass = 'Nyet' AND m.Student_Id = s.Student_Id AND s.Collage_Id='" + staffCollageId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    String[] arr = new String[4];
                    TimeSpan temp = ((TimeSpan)rdr["Hour"]);

                    arr[0] = ((string)rdr["Student_Id"]);
                    arr[1] = studentFullNameById(arr[0]).Substring(0,studentFullNameById(arr[0]).IndexOf(" "));
                    arr[2] = studentFullNameById(arr[0]).Substring(studentFullNameById(arr[0]).IndexOf(" ")+1);
                    arr[3] = string.Format("{0:hh\\:mm}", temp);
                    infoList.AddLast(arr);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return infoList;
        }

        public Boolean updateStudentPassFailTests(string studentId, string pass)  /*student pass/fail test*/
        {

            Boolean retVal = false; //return val if updating the student was successful
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //update student

                string sqlStringStudent = "UPDATE manager SET Pass='" + pass + "' WHERE Student_Id= '" + studentId + "'";


                //execute querys
                SqlCommand command = new SqlCommand(sqlStringStudent, connection);
                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public Boolean updateStudentPaidTests(string studentId)  /*student pass/fail test*/
        {

            Boolean retVal = false; //return val if updating the student was successful
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //update student

                string sqlStringStudent = "UPDATE manager SET Paid='yes' WHERE Student_Id= '" + studentId + "'";


                //execute querys
                SqlCommand command = new SqlCommand(sqlStringStudent, connection);
                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }



        public LinkedList<String[]> studentWaitForConfiremdTests(string staffCollageId)  /*student wait for approve tests info*/
        {
            LinkedList<String[]> infoList = new LinkedList<String[]>();
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select m.Student_Id,m.Date,m.Hour from manager m, study_at s where m.Confirmed = 'Nyet' AND m.Cancelled != 'yes' AND m.Student_Id = s.Student_Id AND s.Collage_Id='" + staffCollageId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    String[] arr = new String[5];

                    DateTime res = (DateTime)rdr["Date"];
                    TimeSpan temp = ((TimeSpan)rdr["Hour"]);

                    arr[0] = ((string)rdr["Student_Id"]);
                    arr[1] = studentFullNameById(arr[0]).Substring(0, studentFullNameById(arr[0]).IndexOf(" "));
                    arr[2] = studentFullNameById(arr[0]).Substring(studentFullNameById(arr[0]).IndexOf(" ") + 1);
                    arr[3] = res.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    arr[4] = string.Format("{0:hh\\:mm}", temp);
                    infoList.AddLast(arr);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return infoList;
        }

        public LinkedList<String[]> studentWaitForCancelTests(string staffCollageId)  /*show students wait for cancel tests info*/
        {
            LinkedList<String[]> infoList = new LinkedList<String[]>();
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select m.Student_Id, m.Date, m.Hour from manager m, study_at s where m.Cancelled = 'wait' AND m.Pass ='Nyet' AND m.Confirmed = 'yes' AND m.Student_Id = s.Student_Id AND s.Collage_Id='" + staffCollageId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    String[] arr = new String[5];

                    DateTime res = (DateTime)rdr["Date"];
                    TimeSpan temp = ((TimeSpan)rdr["Hour"]);

                    arr[0] = ((string)rdr["Student_Id"]);
                    arr[1] = studentFullNameById(arr[0]).Substring(0, studentFullNameById(arr[0]).IndexOf(" "));
                    arr[2] = studentFullNameById(arr[0]).Substring(studentFullNameById(arr[0]).IndexOf(" ") + 1);
                    arr[3] = res.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    arr[4] = string.Format("{0:hh\\:mm}", temp);
                    infoList.AddLast(arr);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return infoList;
        }

        public Boolean updateStudentFreeTest(int num, string id, string coursrCode)  /*student test code by id and date*/
        {
            Boolean retVal = false;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                int freeTest = StudentFreeTest(num, id, coursrCode);

                string sqlStringUpdate = "UPDATE study_at SET Free_Test_Num=" + freeTest + " WHERE Student_Id= '" + id + "' AND Course_Code = '" + coursrCode + "'";

                //execute querys
                SqlCommand commandUpdate = new SqlCommand(sqlStringUpdate, connection);
                commandUpdate.ExecuteNonQuery();
                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }
    


        /**************** End System Main Page *******************/




        /**************** Calender Page *******************/


        public LinkedList<String> allConfirmesTests(string date, string staffCollageId)  /*return student with confirmes tests*/
        {
            LinkedList<String> confirmTestList = null;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select s.First_Name_Heb, s.Last_Name_Heb, m.Hour from students s, manager m, study_at sa where m.Student_Id=s.Id AND m.Confirmed='yes' AND m.Date='" + date + "' AND m.Student_Id = sa.Student_Id AND sa.Collage_Id='" + staffCollageId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                confirmTestList = new LinkedList<String>();

                while (rdr.Read())
                {
                    TimeSpan time = (TimeSpan)(rdr["Hour"]);
                    string strTime = string.Format("{0:hh\\:mm}", time);
                    confirmTestList.AddLast(strTime + " " + (string)rdr["First_Name_Heb"] + " " + (string)rdr["Last_Name_Heb"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return confirmTestList;
        }

        public LinkedList<String> allConfirmesTestsByStudentId(string id, string date)  /*return student with confirmes tests*/
        {
            LinkedList<String> confirmTestList = null;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select s.First_Name_Heb, s.Last_Name_Heb, m.Hour from students s, manager m where m.Student_Id=s.Id AND m.Student_Id = '" + id + "' AND m.Confirmed='yes' AND m.Date='" + date + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                confirmTestList = new LinkedList<String>();

                while (rdr.Read())
                {
                    TimeSpan time = (TimeSpan)(rdr["Hour"]);
                    string strTime = string.Format("{0:hh\\:mm}", time);
                    confirmTestList.AddLast(strTime + " " + (string)rdr["First_Name_Heb"] + " " + (string)rdr["Last_Name_Heb"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return confirmTestList;
        }


        public Boolean addSchedule(string date)  /*add new schedule from calender*/
        {
            Boolean retVal = false; //return val if adding the student was successful

            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //add new student
                string sqlStringSchedule = "INSERT INTO schedule VALUES ( '" + date + "')";

                //execute querys
                SqlCommand command = new SqlCommand(sqlStringSchedule, connection);
               
                command.ExecuteNonQuery();
              
                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public Boolean addScheduleInCollage(string date, int testCounter, string collageId)  /*add new schedule from calender*/
        {
            Boolean retVal = false; //return val if adding the student was successful

            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //add new student
                string sqlStringScheduleInCollage = "INSERT INTO schedule_in_collage VALUES ( '" + date + "','" + collageId + "'," + testCounter + ")";

                //execute querys
                SqlCommand command = new SqlCommand(sqlStringScheduleInCollage, connection);

                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }



        public String searchScheduleDate(string date)  /*return if recived date exists*/
        {
            string strDate = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Date from schedule where Date='" + date + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    DateTime res = (DateTime)rdr["Date"];
                    strDate = res.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strDate;
        }


        public String searchScheduleInCollageDate(string date, string collageId)  /*return if recived date exists*/
        {
            string strDate = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Date from schedule_in_collage where Date='" + date + "' AND Collage_Id='" + collageId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    DateTime res = (DateTime)rdr["Date"];
                    strDate = res.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strDate;
        }
    

        /**************** End Calender Page *******************/

        public int testsInDay(string date, string collageId)  /*test num in date*/
        {
            int numOftests = -1;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Test_Counter from schedule_in_collage where Date='" + date + "' AND Collage_Id='" + collageId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();


                while (rdr.Read())
                {
                    numOftests = (int)rdr["Test_counter"];
                }



            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return numOftests;
        }


    /*********************System Student Page Details*****************************/

        public LinkedList<String[]> studentsTestToApprove(string id)  /*student test requests*/
        {

            LinkedList<String[]> approveList = new LinkedList<String[]>();
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Test_Code, Date, Hour, Secound_Shot from manager where Student_Id='" + id + "' AND Confirmed = 'Nyet'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    String[] arr = new String[4];

                    DateTime date = ((DateTime)rdr["Date"]);
                    TimeSpan time = ((TimeSpan)rdr["Hour"]);

                    arr[0] = ((string)rdr["Test_Code"]);
                    arr[1] = date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    arr[2] = string.Format("{0:hh\\:mm}", time);
                    arr[3] = ((string)rdr["Secound_Shot"]);
                    approveList.AddLast(arr);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return approveList;
        }

        public LinkedList<String[]> studentsCloseTests(string id)  /*student test requests*/
        {

            LinkedList<String[]> closeList = new LinkedList<String[]>();
            CalenderClass cal = new CalenderClass();
            string todayDate = cal.getTodayFullDate().ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Test_Code, Date, Hour, Secound_Shot from manager where Student_Id='" + id + "' AND Confirmed = 'yes' AND pass='Nyet' AND Date>='" + todayDate + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    String[] arr = new String[4];

                    DateTime date = ((DateTime)rdr["Date"]);
                    TimeSpan time = ((TimeSpan)rdr["Hour"]);

                    arr[0] = ((string)rdr["Test_Code"]);
                    arr[1] = date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    arr[2] = string.Format("{0:hh\\:mm}", time);
                    arr[3] = ((string)rdr["Secound_Shot"]);
                    closeList.AddLast(arr);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return closeList;
        }


        public LinkedList<String[]> studentsPastTests(string id)  /*student test requests*/
        {

            LinkedList<String[]> pastList = new LinkedList<String[]>();
            CalenderClass cal = new CalenderClass();
            string todayDate = cal.getTodayFullDate().ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Test_Code, Date, Hour, Pass, Secound_Shot from manager where Student_Id='" + id + "' AND Confirmed = 'yes' AND pass!='Nyet' AND Date<='" + todayDate + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    String[] arr = new String[5];

                    DateTime date = ((DateTime)rdr["Date"]);
                    TimeSpan time = ((TimeSpan)rdr["Hour"]);

                    arr[0] = ((string)rdr["Test_Code"]);
                    arr[1] = date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    arr[2] = string.Format("{0:hh\\:mm}", time);
                    arr[3] = ((string)rdr["Pass"]);
                    if (arr[3].Equals("yes"))
                        arr[3] = "עבר";
                    else
                        arr[3] = "נכשל";
                    arr[4] = ((string)rdr["Secound_Shot"]);
                    pastList.AddLast(arr);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return pastList;
        }

        public Boolean secondShotWasUsed(string secShot)  /*if recived second shot was ever used*/
        {
            Boolean retVal = false; //was not used
            string scStr = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Secound_Shot from manager where Secound_Shot='" + secShot + "' AND Use_Sec_Shot = 'yes' AND Confirmed='yes'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();


                while (rdr.Read())
                {
                    scStr = (string)rdr["Secound_Shot"];
                }

                if(!scStr.Equals(""))
                {
                    retVal = true; //was used
                }


            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

 


        public Boolean confirmStudentsTest(string id, string testCode, string date, string free, string sec)  /*confirm students test*/
        {

            Boolean retVal = false; //return val if updating the student was successful
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "UPDATE manager SET Confirmed='yes', Use_Free='" + free +
                    "', Use_Sec_Shot='" + sec + "'WHERE Student_Id= '" + id + "' AND Test_Code='" 
                    + testCode + "' AND Date='" + date + "'";

                //execute querys
                SqlCommand command = new SqlCommand(sqlString, connection);
                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

    /*********************System Student Page Details*****************************/

    
        public Boolean UpdateStudentWaitForApproveTests(string studentId, string confirmed, string date)  /*student confirmed test*/
        {

            Boolean retVal = false; //return val if updating the student was successful
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //update student

                string sqlStringStudent = "UPDATE manager SET Confirmed='" + confirmed + "' WHERE Student_Id= '" + studentId + "' AND Date= '" + date + "'";


                //execute querys
                SqlCommand command = new SqlCommand(sqlStringStudent, connection);
                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }
        

        public Boolean studentUseSecoundShot(string id, string date, string testCode)  /*student secound shot*/
        {
            Boolean retVal = false;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Use_Shot_Shot from manager where Use_Free != 'yes'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    if (((string)rdr["Use_Sec_Shot"]).Equals("yes"))
                        retVal = true;

                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

       public Boolean studentUseFreeTests(string id, string date, string testCode)  /*student free tests*/
        {
            Boolean retVal = false;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Use_Free from manager where Use_Sec_Shot != 'yes'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    if (((string)rdr["Use_Free"]).Equals("yes"))
                        retVal = true;

                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }


       public Boolean studentPaidForTests(string id, string date, string testCode)  /*student paid for tests*/
       {
           Boolean retVal = false;
           string connectionString = connectionStr;
           SqlConnection connection = new SqlConnection(connectionString);
           try
           {
               connection.Open();

               string sqlString = "select Paid from manager where Paid = 'yes' AND Student_Id='"+id+"' AND Date='" + date +"' AND Test_Code='" + testCode +"'";
               SqlCommand command = new SqlCommand(sqlString, connection);
               SqlDataReader rdr = command.ExecuteReader();
               while (rdr.Read())
               {
                   if (!((string)rdr["Paid"]).Equals(""))
                       retVal = true;

               }

           }
           catch (SqlException)
           {
               Console.WriteLine("Error connecting to sql server");
           }

           connection.Close();
           return retVal;
       }




        public string studentTestCodeByDateAndId(string id, string date)  /*student test code by id and date*/
        {
            string testCode = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Test_Code from manager where Student_Id ='" + id + "' AND Date='" + date + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    testCode = ((string)rdr["Test_Code"]);

                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return testCode;
        }

        public int StudentFreeTest(int num, string id, string coursrCode)  /*student test code by id and date*/
        {
            int freeTest = -1;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Free_Test_Num from study_at where Student_Id ='" + id + "' AND Course_Code='" + coursrCode + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    freeTest = ((int)rdr["Free_Test_Num"]);
                }

                freeTest += num;
                if (freeTest < 0 || freeTest > 4)
                    freeTest = -1;
             }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return freeTest;
        }

       
        public LinkedList<String[]> coursesList()  /*return student with confirmes tests*/
        {
            LinkedList<String[]> courseCode = null;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Code , Name , Start_Date , End_Date from courses where Code != 'network'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                courseCode = new LinkedList<String[]>();

                while (rdr.Read())
                {
                    String[] arr = new String[4];
                    DateTime start = (DateTime)rdr["Start_Date"];
                    DateTime end = (DateTime)rdr["End_Date"];

                    arr[0] = (string)(rdr["Code"]);
                    arr[1] = (string)(rdr["Name"]);
                    arr[2] = start.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    arr[3] = end.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                    courseCode.AddLast(arr);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return courseCode;
        }

        public DateTime studentDateByStudentAndTestCode(string studentId, string testCode)  /*return the student date by id and test Code*/
        {
            DateTime date = new DateTime();
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string sqlString = "select Date from manager where Student_Id='" + studentId + "' AND Test_Code = '" + testCode + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    date = (DateTime)rdr["Date"];
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return date;
        }

        public String FullStudentTestCode(string testCode)  /*return the full student test code*/
        {
            string testName = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Name from tests where Code='" + testCode + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    testName = ((string)rdr["Name"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return testCode + " - " + testName;
        }

        public String secondshotByIdTestCodeDate(string testCode, string date, string studentId)  /*return the secondshot by test code, date and id*/
        {
            string secondshot = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Secound_Shot from manager where Test_Code='" + testCode + "' AND Date = '" + date + "' AND Student_Id = '" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    secondshot = ((string)rdr["Secound_Shot"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return secondshot;
        }

        public Boolean updateTestRegInfo(string Id, string thisTestCode, string thisDate, string thisHour, string thisSs, string otherTestCode, string otherDate, string otherHour, string otherSs)  /*update test reg info*/
        {
            Boolean retVal = false; //return val if adding the info was successful

            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                //add new test
                string sqlStringManage = "UPDATE manager SET Test_Code='" + thisTestCode + "' , Date = '" + thisDate + "' , Hour ='" + thisHour + "' , Secound_Shot ='" + thisSs + "' WHERE Test_Code ='" + otherTestCode + "' AND Date = '" + otherDate + "' AND Hour = '" + otherHour + "' AND Secound_Shot = '" + otherSs + "'";
                //execute querys
                SqlCommand command = new SqlCommand(sqlStringManage, connection);

                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }


        public LinkedList<String[]> checkTestsApproved(string studentId)
        {
            LinkedList<String[]> infoList = new LinkedList<string[]>();
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select sa.Course_Code, m.Test_Code, t.Name, m.Date, m.Hour from manager m, tests t, study_at sa where sa.Student_Id = '" + studentId + "' AND m.Student_Id='" + studentId + "' AND m.Test_Code = t.Code AND m.Confirmed = 'yes' AND m.Pass = 'Nyet'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    String[] arr = new String[5];
                    TimeSpan tmp = ((TimeSpan)rdr["Hour"]);
                    DateTime date = (DateTime)rdr["Date"];

                    arr[0] = ((string)rdr["Course_Code"]);
                    arr[1] = ((string)rdr["Test_Code"]);
                    arr[2] = ((string)rdr["Name"]);
                    arr[3] = date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                    arr[4] = string.Format("{0:hh\\:mm}", tmp);
                    infoList.AddLast(arr);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return infoList;
        }

        public LinkedList<String[]> checkTestsNotApproved(string studentId)
        {
            LinkedList<String[]> infoList = new LinkedList<string[]>();
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select sa.Course_Code, m.Test_Code, t.Name, m.Date, m.Hour from manager m, tests t, study_at sa where sa.Student_Id = '" + studentId + "' AND m.Student_Id='" + studentId + "' AND m.Test_Code = t.Code AND m.Confirmed = 'Nyet'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    String[] arr = new String[5];
                    TimeSpan tmp = ((TimeSpan)rdr["Hour"]);
                    DateTime date = (DateTime)rdr["Date"];

                    arr[0] = ((string)rdr["Course_Code"]);
                    arr[1] = ((string)rdr["Test_Code"]);
                    arr[2] = ((string)rdr["Name"]);
                    arr[3] = date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                    arr[4] = string.Format("{0:hh\\:mm}", tmp);
                    infoList.AddLast(arr);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return infoList;
        }

        public Boolean updateApproveTestStudentTable(string studentId, string testCode)  //update if student want to cancel a test
        {

            Boolean retVal = false;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlStringStudent = "UPDATE manager SET Cancelled = 'wait' WHERE Student_Id ='" + studentId + "' AND Test_Code = '" + testCode + "'";

                //execute querys
                SqlCommand command = new SqlCommand(sqlStringStudent, connection);
                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public Boolean opesetUpdateApproveTestStudentTable(string studentId, string testCode)  //update if student want to change his cancel 
        {

            Boolean retVal = false;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlStringStudent = "UPDATE manager SET Cancelled = 'Nyet' WHERE Student_Id ='" + studentId + "' AND Test_Code = '" + testCode + "'";

                //execute querys
                SqlCommand command = new SqlCommand(sqlStringStudent, connection);
                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }
    /*end dadi*/


        public String staffWorkCollageId(string staffId)  /*return the staff work place by id*/
        {
            string strCollage = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Collage_Id from work_at where Staff_Id='" + staffId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strCollage = ((string)rdr["Collage_Id"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strCollage;
        }

        public String staffIdByUserName(string staffUserName)  /*return the staff id num by userName*/
        {
            string strStaffId = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Id from staff where User_Name='" + staffUserName + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strStaffId = ((string)rdr["Id"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strStaffId;
        }

        public String staffUserNameById(string id)  /*return the staff id num by userName*/
        {
            string strStaffUserName = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select User_Name from staff where Id='" + id + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strStaffUserName = ((string)rdr["User_Name"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strStaffUserName;
        }



    /******************* Course Pages ************************* */
     
        public LinkedList<String[]> activeCoursesList(string collageId)  /*return all active courses list*/
        {
            CalenderClass cal = new CalenderClass();
            string todayDate = cal.getTodayFullDate().ToString("MM/dd/yyyy",CultureInfo.InvariantCulture);
            LinkedList<String[]> courseCode = null;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select c.Code , c.Name , c.Start_Date , c.End_Date from courses c, course_in_collage cc where c.Code != 'network' AND c.End_Date >'"+ todayDate+"' AND cc.Course_Code = c.Code AND cc.Collage_Id = '"+collageId+"'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                courseCode = new LinkedList<String[]>();

                while (rdr.Read())
                {
                    String[] arr = new String[4];
                    DateTime start = (DateTime)rdr["Start_Date"];
                    DateTime end = (DateTime)rdr["End_Date"];

                    arr[0] = (string)(rdr["Code"]);
                    arr[1] = (string)(rdr["Name"]);
                    arr[2] = start.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    arr[3] = end.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                    courseCode.AddLast(arr);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return courseCode;
        }

        public LinkedList<String[]> oldCoursesList(string collageId)  /*return all active courses list*/
        {
            CalenderClass cal = new CalenderClass();
            string todayDate = cal.getTodayFullDate().ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            LinkedList<String[]> courseCode = null;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select c.Code , c.Name , c.Start_Date , c.End_Date from courses c,course_in_collage cc where c.Name != N'אקסטרני' AND c.Name != N'המכללה החרדית' AND c.End_Date <'" + todayDate + "' AND cc.Course_Code = c.Code AND cc.Collage_Id = '"+collageId+"'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                courseCode = new LinkedList<String[]>();

                while (rdr.Read())
                {
                    String[] arr = new String[4];
                    DateTime start = (DateTime)rdr["Start_Date"];
                    DateTime end = (DateTime)rdr["End_Date"];

                    arr[0] = (string)(rdr["Code"]);
                    arr[1] = (string)(rdr["Name"]);
                    arr[2] = start.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    arr[3] = end.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                    courseCode.AddLast(arr);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return courseCode;
        }


        public Boolean createNewCourse(string code, string name, string sDate, string eDate, string free)  /*create new course*/
        {
            Boolean retVal = false; //return val if adding the info was successful

            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //add new test
                string sqlStringManage = "INSERT INTO courses VALUES ( '" + code + "',N'" + name + "','" + sDate + "','" + eDate + "'," + free + ")";

                //execute querys
                SqlCommand command = new SqlCommand(sqlStringManage, connection);

                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public int freeTestNumByCourseCode(string code)  /*return coure code */
        {
            int freeNum = -1;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Free_Test_Num from courses where Code = '" + code + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    freeNum = (int)rdr["Free_Test_Num"];
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return freeNum;
        }


        //**********changed
        public Boolean createNewCourseInCollage(string code, string collageId)  /*create new course*/
        {
            Boolean retVal = false; //return val if adding the info was successful
            string collage = "";
            if (code.Contains("Extern"))
                collage = "EX";            
            else if (code.Contains("Haredit"))
                collage = "HA";
            else
                collage = collageId;

            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();


                //add new course in collage
                string sqlStringManage = "INSERT INTO course_in_collage VALUES ( '" + collage + "','" + code +  "')";

                //execute querys
                SqlCommand command = new SqlCommand(sqlStringManage, connection);

                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

    


        public LinkedList<String[]> studentThatStudyAtCourseList(string cousreCode)  /*return all active courses list*/
        {
            CalenderClass cal = new CalenderClass();
           // string todayDate = cal.getTodayFullDate().ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            LinkedList<String[]> studentList = null;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select s.Id, s.First_Name_Heb,s.Last_Name_Heb from students s, study_at sa where s.Id = sa.Student_Id AND sa.Course_Code='"+cousreCode+"'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                studentList = new LinkedList<String[]>();

                while (rdr.Read())
                {
                    String[] arr = new String[2];
                
                    arr[0] = (string)(rdr["Id"]);
                    arr[1] = (string)(rdr["First_Name_Heb"]) + " " + (string)(rdr["Last_Name_Heb"]);
                    

                    studentList.AddLast(arr);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return studentList;
        }

        public String[] courseStartAndEndDates(string cousreCode)  /*return start and end date of a course*/
        {
            String[] arr = new String[2];

            CalenderClass cal = new CalenderClass();
            // string todayDate = cal.getTodayFullDate().ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Start_Date,End_Date from courses where Code='" + cousreCode + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {

                    DateTime res1 = (DateTime)rdr["Start_Date"];
                    DateTime res2 = (DateTime)rdr["End_Date"];

                    arr[0] = res1.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    arr[1] = res2.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); 

                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return arr;
        }




        public Boolean updateCourseInfo(string sDate,string eDate,string code)  /*update course info-start date and end date*/
        {

            Boolean retVal = false; //return val if updateing course was successful
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //update course

                string sqlStringStudent = "UPDATE courses SET Start_Date='" + sDate + "',End_Date='" + eDate +"' WHERE Code= '" + code + "'";



                //execute querys
                SqlCommand command = new SqlCommand(sqlStringStudent, connection);
               
                command.ExecuteNonQuery();
                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }



        public LinkedList<String[]> studentTestReport(string sDate,string eDate,string collageid)  /*return info of all student that had test in this dates*/
        {
            LinkedList<String[]> infoList = new LinkedList<String[]>();
            CalenderClass cal = new CalenderClass();
            // string todayDate = cal.getTodayFullDate().ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select s.Id, s.First_Name_Heb, s.Last_Name_Heb, m.Test_Code, m.Date, m.Pass, st.Course_Code from students s, manager m, study_at st where s.Id =m.Student_Id AND s.Id = st.Student_Id AND m.Date < '" + eDate + "' AND m.Date > '" + sDate + "' AND st.Collage_Id = '" + collageid + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    String[] arr = new String[6];

                    DateTime res = (DateTime)rdr["Date"];
                    arr[0] = (string)rdr["Id"];
                    arr[1] = (string)rdr["First_Name_Heb"] + " " + (string)rdr["Last_Name_Heb"];
                    arr[2] = (string)rdr["Course_Code"];
                    arr[3] = (string)rdr["Test_Code"];
                    arr[4] = res.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (((string)rdr["Pass"]).Equals("yes"))
                        arr[5] = "כן";
                    else if (((string)rdr["Pass"]).Equals("no"))
                        arr[5]="לא";
                    else 
                        arr[5] = "טרם";
                   
                    infoList.AddLast(arr);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return infoList;
        }


    /******************* End Course Pages ********************/


   /******************* System Student Details Page ********************/

        public int ScheduleTestCounter(int num, string date, string CollageId)  /*schedule counter by id*/
        {
            int testCounter = -1;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Test_Counter from schedule_in_collage where Date='" + date + "' AND Collage_Id='" + CollageId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    testCounter = ((int)rdr["Test_Counter"]);
                }

                testCounter += num;
                if (testCounter < 0 )
                    testCounter = -1;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return testCounter;
        }

        public Boolean updateTestCounter(int num, string date, string collageId)  /*student test code by id and date*/
        {
            Boolean retVal = false;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                int testCounter = ScheduleTestCounter(num, date, collageId);

                string sqlStringUpdate = "UPDATE schedule_in_collage SET Test_Counter=" + testCounter + " WHERE Date= '" + date + "' AND Collage_Id = '" + collageId + "'";

                //execute querys
                SqlCommand commandUpdate = new SqlCommand(sqlStringUpdate, connection);
                commandUpdate.ExecuteNonQuery();
                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }


    /******************* End System Student Details Page ********************/
   
    /**************** Student Main Page *******************/

        public Boolean updateNotApproveTestStudentTable(string studentId, string date, string testCode)  //update if student want to cancel a test
        {

            Boolean retVal = false;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlDeleteStudent = "DELETE FROM manager WHERE Student_Id ='" + studentId + "' AND Test_Code = '" + testCode + "' AND Date='" + date + "'";

                //execute querys
                SqlCommand command = new SqlCommand(sqlDeleteStudent, connection);
                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }
        /**************** Student Main Page *******************/


        /******************************Student History Page *******************************/

        public LinkedList<String[]> testHistoryById(string studentId)
        {
            CalenderClass cal = new CalenderClass();
            LinkedList<String[]> historyList = new LinkedList<string[]>();
            string todayDate = cal.getTodayFullDate().ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);


            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select sa.Course_Code, m.Test_Code, t.Name, m.Date, m.Pass from manager m, tests t, study_at sa where sa.Student_Id = '" + studentId + "' AND m.Student_Id='" + studentId + "' AND m.Test_Code = t.Code AND m.Pass != 'Nyet' AND m.Date <= '" + todayDate + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    String[] arr = new String[5];
                    DateTime date = (DateTime)rdr["Date"];
                    string str = ((string)rdr["Pass"]);


                    arr[0] = ((string)rdr["Course_Code"]);
                    arr[1] = ((string)rdr["Test_Code"]);
                    arr[2] = ((string)rdr["Name"]);
                    arr[3] = date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                    if (str.Equals("yes"))
                    {
                        arr[4] = "עבר";
                    }
                    else if (str.Equals("no"))
                    {
                        arr[4] = "לא עבר";
                    }
                    historyList.AddLast(arr);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return historyList;
        }
    /******************************End Student History Page *******************************/


        public Boolean updateStudentDetailsBySystem(string id, string hebFirstName, string hebLastName,
               string password, string primePhone, string secPhone, string email, string engFirstName,
               string engLastName, string addr, string city, string collageId, string courseCode, string sr, string originalCollage, string originalCourse, int tests)  /*add new student from student registration*/
        {

            Boolean retVal = false; //return val if adding the student was successful
            string sqlFreeTest;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //update student

                string sqlStringStudent = "UPDATE students SET Id='" + id + "', Sr='" + sr + "',First_Name_Heb= N'" + hebFirstName
                    + "',Last_Name_Heb=N'" + hebLastName + "',First_Name_Eng='" + engFirstName + "',Last_Name_Eng='" + engLastName +
                    "',Address='" + addr + "',City=N'" + city + "',Email='" + email + "',Prime_Phone='" + primePhone + "',Sec_Phone='" + secPhone + "' WHERE Id= '" + id + "'";

                //update student course and collage
                string sqlStringStudyAt = "UPDATE study_at SET Student_Id= '" + id + "',Collage_Id='" + collageId + "',Course_Code='" + courseCode + "'WHERE Student_Id='" + id + "'";

                //update student free test numbers
                if (collageNameByCollageCode(collageId).Equals(originalCollage) && courseCode.Equals(originalCourse))
                {
                    sqlFreeTest = "UPDATE study_at SET Free_Test_Num=" + tests + " WHERE Student_Id ='" + id + "' AND Course_Code = '" + originalCourse + "'";
                }
                else if (courseCode.Contains("Extern") || courseCode.Contains("Haredit"))
                {
                    sqlFreeTest = "UPDATE study_at SET Free_Test_Num=0 WHERE Student_Id ='" + id + "'";
                }
                else
                {
                    int temp = freeTestNumByCourseCode(courseCode);
                    sqlFreeTest = "UPDATE study_at SET Free_Test_Num=" + temp + " WHERE Student_Id='" + id + "'";
                }

                //execute querys
                SqlCommand command1 = new SqlCommand(sqlStringStudent, connection);
                SqlCommand command2 = new SqlCommand(sqlStringStudyAt, connection);
                SqlCommand command3 = new SqlCommand(sqlFreeTest, connection);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public Boolean updateStudentDetails(string id, string hebFirstName, string hebLastName,
               string password, string primePhone, string secPhone, string email, string engFirstName,
               string engLastName, string addr, string city, string collageId, string courseCode, string originalCollage, string originalCourse, int tests)  /*add new student from student registration*/
        {

            Boolean retVal = false; //return val if adding the student was successful
            string sqlFreeTest;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //update student

                string sqlStringStudent = "UPDATE students SET Id='" + id + "',First_Name_Heb= N'" + hebFirstName
                    + "',Last_Name_Heb=N'" + hebLastName + "',First_Name_Eng='" + engFirstName + "',Last_Name_Eng='" + engLastName +
                    "',Address='" + addr + "',City=N'" + city + "',Email='" + email + "',Prime_Phone='" + primePhone + "',Sec_Phone='" + secPhone + "' WHERE Id= '" + id + "'";

                //update student course and collage
                string sqlStringStudyAt = "UPDATE study_at SET Student_Id= '" + id + "',Collage_Id='" + collageId + "',Course_Code='" + courseCode + "'WHERE Student_Id='" + id + "'";

                //update student free test numbers
                if (collageNameByCollageCode(collageId).Equals(originalCollage) && courseCode.Equals(originalCourse))
                {
                    sqlFreeTest = "UPDATE study_at SET Free_Test_Num=" + tests + " WHERE Student_Id ='" + id + "' AND Course_Code = '" + originalCourse + "'";
                }
                else if (courseCode.Contains("Extern") || courseCode.Contains("Haredit"))
                {
                    sqlFreeTest = "UPDATE study_at SET Free_Test_Num=0 WHERE Student_Id ='" + id + "'";
                }
                else
                {
                    int temp = freeTestNumByCourseCode(courseCode);
                    sqlFreeTest = "UPDATE study_at SET Free_Test_Num=" + temp + " WHERE Student_Id='" + id + "'";
                }


                //execute querys
                SqlCommand command1 = new SqlCommand(sqlStringStudent, connection);
                SqlCommand command2 = new SqlCommand(sqlStringStudyAt, connection);
                SqlCommand command3 = new SqlCommand(sqlFreeTest, connection);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }


        public String courseById(string studentId)  /*return coure code */
        {
            String courseCode = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Course_Code from study_at where Student_Id = '" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    courseCode = (String)rdr["Course_Code"];
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return courseCode;
        }

        public int testAmount(string studentId)  /*return how many tests left in student's tests credit*/
        {
            int tmp = 0;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Free_Test_Num from study_at where Student_Id='" + studentId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    tmp = ((int)rdr["Free_Test_Num"]);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return tmp;
        }



        /*ravit 7/9*/
        public LinkedList<String> allTests()
        {
            LinkedList<String> testList = null;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Code from tests";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                testList = new LinkedList<String>();

                while (rdr.Read())
                {
                    if ((string)rdr["Code"] != null)
                        testList.AddLast((string)rdr["Code"]);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return testList;
        }


        public Boolean addTestToNewCourse(string courseCode, string testCode)  /*create new course*/
        {
            Boolean retVal = false; //return val if adding the info was successful

            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //add new test
                string sqlStringManage = "INSERT INTO test_in_course VALUES ( '" + testCode + "','" + courseCode + "')";
                //execute querys
                SqlCommand command = new SqlCommand(sqlStringManage, connection);

                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }


        public String[] secoundShotAndHourById(string id)
        {
            String[] arr = null;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Secound_Shot,Hour from manager where Student_Id='" + id + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                arr = new String[2];

                while (rdr.Read())
                {


                    TimeSpan time = (TimeSpan)rdr["Hour"];
                    arr[0] = (string)rdr["Secound_Shot"];
                    arr[1] = string.Format("{0:hh\\:mm}", time);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return arr;
        }

        public LinkedList<String[]> TestNameAndCodeList()
        {
            LinkedList<String[]> testList = new LinkedList<String[]>();
            
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Code, Name from tests";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();


                while (rdr.Read())
                {
                    String[] arr = new String[2];
                    arr[0] = (string)rdr["Code"];
                    arr[1] = (string)rdr["Name"];
                    testList.AddLast(arr);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return testList;
        }

        public Boolean createNewTest(string code, string name)  /*create new course*/
        {
            Boolean retVal = false; //return val if adding the info was successful

            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //add new test
                string sqlStringManage = "INSERT INTO tests VALUES ( '" + code + "',N'" + name + "' )";

                //execute querys
                SqlCommand command = new SqlCommand(sqlStringManage, connection);

                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public Boolean cancelButtonWaitStatus(string studentId, string date, string testCode)  /*return true if wait */
        {
            Boolean retVal = false;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Cancelled from manager where Student_Id = '" + studentId + "' AND Date='" + date + "' AND Test_Code='" + testCode + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    if (((String)rdr["Cancelled"]).Equals("wait"))
                        retVal = true;
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

    /**********************************Test Page**********************************/

        public Boolean updateTestInfo(string newCode, string newName, string oldCode, string oldName)  /*update test code and name*/
        {
            Boolean retVal = false;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlStringUpdateTests = "UPDATE tests SET Name='" + newName + "' WHERE Code='" + oldCode + "' AND Name= '" + oldName + "'";
                
                //execute querys

                SqlCommand commandUpdate = new SqlCommand(sqlStringUpdateTests, connection);
                commandUpdate.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public Boolean deleteTests(string Code)  //delete test
        {

            Boolean retVal = false;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlDeleteTest = "DELETE FROM tests WHERE Code ='" + Code + "'";

                //execute querys
                SqlCommand command = new SqlCommand(sqlDeleteTest, connection);
                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public String testNameByCode(string code)  /*return the student email by id*/
        {
            string testName = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Name from tests where Code='" + code + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    testName = ((string)rdr["Name"]);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return testName;
        }



    /**********************************End Test Page**********************************

        /*********************************Message Page********************************/
        public LinkedList<String[]> currMessages(string staffCollageId)  /*get current messages*/
        {

            LinkedList<String[]> infoList = new LinkedList<String[]>();
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Date, Message from message where Collage_Id='" + staffCollageId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    String[] arr = new String[2];

                    DateTime date = (DateTime)rdr["Date"];

                    arr[0] = date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                    arr[1] = ((string)rdr["Message"]);
                    infoList.AddLast(arr);
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return infoList;
        }

        public Boolean addNewMessage(string date, string message, string collageId)  /*add new message*/
        {
            Boolean retVal = false; //return val if adding the info was successful

            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //add new test
                string sqlStringMessage = "INSERT INTO message (Collage_Id , Date, Message) VALUES ('" + collageId + "','" + date + "',N'" + message + "')";

                //execute querys
                SqlCommand command = new SqlCommand(sqlStringMessage, connection);

                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public Boolean updateMessageDetails(string date, string message, string collageId)  /*update message*/
        {

            Boolean retVal = false; //return val if updating the student was successful
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                //update student

                string sqlStringStudent = "UPDATE message SET Message=N'" + message + "' WHERE Collage_Id='" + collageId + "' AND Date='" + date + "'";


                //execute querys
                SqlCommand command = new SqlCommand(sqlStringStudent, connection);
                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public Boolean deleteMessage(string date, string message, string collageId)  //update if student want to cancel a test
        {

            Boolean retVal = false;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlDeleteMessage = "DELETE FROM message WHERE Collage_Id ='" + collageId + "' AND Date = '" + date + "' AND Message=N'" + message + "'";

                //execute querys
                SqlCommand command = new SqlCommand(sqlDeleteMessage, connection);
                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }

        public LinkedList<String> messagesByDateAndCollageId(string staffCollageId, string date)  /*get messages by date and collage id*/
        {

            LinkedList<String> infoList = new LinkedList<String>();
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Message from message where Collage_Id='" + staffCollageId + "' AND Date='" + date + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    infoList.AddLast(((string)rdr["Message"]));
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return infoList;
        }


    /*********************************End Message Page********************************/

    /*********************************Forgot Password Page********************************/
        public String confirmStudentEmailByID(string id)  /*confirm student email*/
        {
            string strEmail = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Email from students where Id='" + id + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strEmail = (String)rdr["Email"];
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strEmail;
        }


        public String confirmSystemEmailByUser(string user)  /*confirm system email*/
        {
            string strEmail = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Email from staff where User_Name='" + user + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strEmail = (String)rdr["Email"];
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strEmail;
        }


        public String confirmSystemEmailByID(string id)  /*confirm system email*/
        {
            string strEmail = "";
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select Email from staff where Id='" + id + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strEmail = (String)rdr["Email"];
                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strEmail;
        }

    /*********************************End Forgot Password Page********************************/

    /*********************************reset Admin Password***********************************/
        public Boolean resetAdminPassword(string oldPassword, string password)  /*update password*/
        {

            Boolean retVal = false; //return val if reset password was successful
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();


                string sqlStringStudent = "UPDATE staff SET Password='" + password + "' WHERE Password= '" + oldPassword + "' AND Id='000000000'";


                //execute querys
                SqlCommand command = new SqlCommand(sqlStringStudent, connection);
                command.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }
    /**************************************End reset admin pasword***************************************/

    /**************************************get system info*********************************************/

        public String[] systemInfo(string systemId)  /*return the student address by id*/
        {
            String[] strInfo = new String[4];
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlString = "select First_Name,Last_Name,User_Name,Email from staff where Id='" + systemId + "'";
                SqlCommand command = new SqlCommand(sqlString, connection);
                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    strInfo[0] = ((String)rdr["First_Name"]);
                    strInfo[1] = ((String)rdr["Last_Name"]);
                    strInfo[2] = ((String)rdr["User_Name"]);
                    strInfo[3] = ((String)rdr["Email"]);

                }

            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return strInfo;
        }

    /**************************************upadte system info from admin***********************/


        public Boolean updateStaffInfo(string firstName, string lastName, string userName, string email,string id)  /*update staff info  */
        {
            Boolean retVal = false;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlStringUpdateStaff = "UPDATE staff SET First_Name=N'" + firstName + "',Last_Name=N'"+lastName+"',User_Name='"+userName+"',Email='"+email+"'WHERE Id='" + id + "'";
                //execute querys

                SqlCommand commandUpdateStaff = new SqlCommand(sqlStringUpdateStaff, connection);
                commandUpdateStaff.ExecuteNonQuery();
                

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }
        public Boolean updateStaffCollage(string collageCode, string id)  /*update staff  work_at  */
        {
            Boolean retVal = false;
            string connectionString = connectionStr;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                string sqlStringWorkAt = "UPDATE work_at SET Collage_Id =N'" + collageCode + "' WHERE Staff_Id='" + id + "'";
                //execute querys

              
                SqlCommand commandUpdateWorkAt = new SqlCommand(sqlStringWorkAt, connection);
                commandUpdateWorkAt.ExecuteNonQuery();

                retVal = true;
            }
            catch (SqlException)
            {
                Console.WriteLine("Error connecting to sql server");
            }

            connection.Close();
            return retVal;
        }
    


}
