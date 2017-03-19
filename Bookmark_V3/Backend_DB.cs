using System;
using System.Data;
using System.Data.SQLite;
/*
 * certain formatts of using this class
 * 
 
            if(!database.Get("BOOKMARK_DB.sqlite", "SYSTEM","Id","13", "Value")) MessageBox.Show(database.error_message);
            else this.LogUserLabel.Text = database.Result;
            
            







 * 
 * */
namespace Bookmark_V3
{
    class Backend_DB
    {
        public Backend_DB()
        {
           // String TableName = "", Parameter1 = "", Parameter2 = "", Parameter3 = "", Parameter4 = "", Parameter5 = "", Parameter6 = "", Parameter7 = "", Parameter8 = "", Parameter9 = "", Parameter10 = "", Parameter11 = "", Parameter12 = "", Parameter13 = "", Parameter14 = "", Parameter15 = "", Parameter16 = "", Parameter17 = "", Parameter18 = "", Parameter19 = "", Parameter20 = "";
           // byte[] pic;
        }
        public  String error_message;
        public  String Result;
        public  String sql;
        public  Boolean Initialize()
        {
            try
            {
                SQLiteConnection.CreateFile("BOOKMARK_DB.sqlite");
                SQLiteConnection.CreateFile("FBooks_DB.sqlite");
                SQLiteConnection conn = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;");
                conn.Open();
                SQLiteCommand command;
                string sql;
                //-----------------Create System Table-------------------------------------------------//
                sql = "CREATE TABLE SYSTEM (Id INTEGER NOT NULL,Property VARCHAR(30),Value VARCHAR(20),PRIMARY KEY(Id));";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                //-----------------------Inserting values of system table------------------------------//
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (1, 'Admin Name', 'NULL');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (2, 'Admin Password', 'NULL');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (3, 'User1 Name', 'NULL');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (4, 'User1 Password', 'NULL');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (5, 'User2 Name', 'NULL');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (6, 'User2 Password', 'NULL');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (7, 'User3 Name', 'NULL');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (8, 'User3 Password', 'NULL');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (9, 'User4 Name', 'NULL');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (10, 'User4 Password', 'NULL');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (11, 'User5 Name', 'NULL');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (12, 'User5 Password', 'NULL');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (13, 'Presently Logged in user', 'NULL');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (14, 'Fine per day', '1');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                sql = "INSERT INTO SYSTEM (Id, Property, Value) VALUES (15, 'No of return days', '2');";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                //-----------Create Log table for User-Log-----------------//
                sql = "CREATE TABLE USERLOG (Id INTEGER PRIMARY KEY AUTOINCREMENT,Username VARCHAR(30),LoginTime VARCHAR(30),LogoutTime VARCHAR(30));";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                //-----------Creating Books Tables-------------------------//
                sql = "CREATE TABLE BOOKS (Accno	VARCHAR(10) NOT NULL UNIQUE,Title	VARCHAR(50),Author	VARCHAR(50),Publisher	VARCHAR(30),Price	VARCHAR(10),Semester	VARCHAR(2),Edition	VARCHAR(20),Branch	VARCHAR(5),Classification_no	VARCHAR(10),USN VARCHAR(12),FName VARCHAR(30),PRIMARY KEY(Accno));";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                //-----------Create Faculties table------------------------//
                sql = "CREATE TABLE FACULTIES (FName VARCHAR(30) PRIMARY KEY UNIQUE,FBranch VARCHAR(5),Fphno VARCHAR(12),Fphoto BLOB);";
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                //-----------Create Transactions table---------------------//
                sql = "CREATE TABLE TRANSACTIONS (Issued_User VARCHAR(30),USN VARCHAR(12),Book_Id VARCHAR(10),Issue_date TEXT,Expected_return_date TEXT,Return_date TEXT,Returned_User VARCHAR(30),Fine FLOAT);"; // Lets the SQLiteCommand object know our SQL-Query:
                command = new SQLiteCommand(sql, conn);
                command.ExecuteNonQuery();
                command.Dispose();
                conn.Close();
            }
            catch(Exception e)
            {
                error_message = ""+e;
                return false;
            }
            return true;

        }
        public Boolean Get(String source,String TableName,String ColumName,String Parameter,String SqlColumnName)
        {
            /*
             * THIS FUNCTION IS USED TO RETRIVE DATA FROM THE DATABASE.
             * EACH TIME THIS FUNCTION IS CALLED, ONE "CELL" OF DATA FROM A TABLE IN THE DATABASE CAN BE RETRIVED
             * THE RESULT IS STORED IN THE VARIABLE Result AND IS RETURNED
             * if the operation fails, FALSE is returned the error log is stored in a variable called  error_message
             * if he operation is sucessfull, true is returned and the result can be obtained from the Result variable
             * THE SqlColumnName REFFERS TO THE NAME OF THE COLUMN (AS IN THE REAL TABLE) FROM WHERE THE CELL(DATA) IS RETRIVED
             * */
            sql = "SELECT * FROM "+TableName+" WHERE "+ColumName+" = '" + Parameter + "'";
            try
            {
                using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result = reader[SqlColumnName].ToString();
                            }
                            reader.Close();
                        }
                        command.Dispose();
                    }
                    connection.Close();
                }// closing using
            }
            catch (Exception e)
            {
                error_message = e.ToString();
                return false;
            }
            
            return true;
        }
        public static void Delete()
        {

        }
        /* 
            INSERTING DATA INTO THE TABLE 
                
            * The sql syntax changes based on the table name.
            * To solve this we use function overloading.
            * the Insert() with just one string parameter is the function where the insertion operation happens
            * the other Insert() are used to generate the proper sql syntaxes based on the table into which the value is being inserted, and then this function calls the insert() where insertio happens
            * False is returned when operation failed.In his case the error_message variable can be used to obtain the error message
            * True is returned if the operation is completed sucessfuly
            * In the table list below, only the non auto-incremented variables of he tables are shown.
            * The parameters to the Insert() must be passed strictly according to the order shown below.
            

           TABLE_NAME         |VARIABLES |         VARIABLE LIST
           ------------------------------------------------------------------------------------------------------------------------------
           TABLE SYSTEM       |   3      | Id,Property,Value
           TABLE USERLOG      |   3      | Username,LoginTime,LogoutTime
           TABLE BOOKS        |   11     | Accno,Title,Author,Publisher,Price,Semester,Edition,Branch,Classification_no,USN,FName
           TABLE FACULTIES    |   4      | FName,FBranch,Fphno,Fphoto
           TABLE TRANSACTIONS |   8      | Issued_User,USN,Book_Id,Issue_date,Expected_return_date,Return_date,Returned_User,Fine
           TABLE STUDENT...   |   21     | USN,SName,Sphno,Semester,B1,B2,BB1,BB2,BB3,BB4,BB5,BB6,BB7,BB8,BB9,BB10,BB11,BB12,BB13,BB14,Sphoto
           
        */


        public Boolean Insert(String source,String sql)
        {
            // source is the database source
            try
            {

                using (var connection = new SQLiteConnection("Data Source="+source+";Version=3;New=True;Compress=True;"))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                error_message = e.ToString();
                return false;
            }
            return true;
        }
        public Boolean Insert(String TableName, String Parameter1, String Parameter2, String Parameter3)//userlog
        {
           // sql = "INSERT INTO BOOKS (Accno,Title,Author,Publisher,Price,Semester,Branch,Edition,Classification_no) VALUES ('" + AccnoBox.Text + "','" + TitleBox.Text + "','" + AuthorBox.Text + "','" + PublisherBox.Text + "','" + PriceBox.Text + "','" + SemesterBox.Text + "','" + BranchCBox.Text + "','" + EditionBox.Text + "','" + ClassificationNoBox.Text + "');";

            // To be used for inserting values into system table and userlog table
            if (TableName.Substring(0, 3).Equals("SYS"))//system        :Id,Property,Value
            {
                return Insert("BOOKMARK_DB.sqlite", "INSERT INTO SYSTEM (Id, Property, Value) VALUES (" + Parameter1 + ", '" + Parameter2 + "', '" + Parameter3 + "');");//Id,Property,Value
            }
            else if (TableName.Substring(0, 3).Equals("USE"))//Userlog   :Username,LoginTime,LogoutTime
            {
                 return Insert("BOOKMARK_DB.sqlite", "INSERT INTO USERLOG (Username,LoginTime,LogoutTime) VALUES ('" + Parameter1 + "','" + Parameter2 + "','" + Parameter3 + "');"); //Username,LoginTime,LogoutTime
            }
            else
            {
                error_message = "Table not found";
                return false;
            }
        }
        public Boolean Insert(String TableName, String Parameter1, String Parameter2, String Parameter3, String Parameter4, String Parameter5, String Parameter6, String Parameter7, String Parameter8, String Parameter9, String Parameter10, String Parameter11)//Accno,Title,Author,Publisher,Price,Semester,Edition,Branch,Classification_no,USN,FName
        {
            // To be used for inserting values into Books
            if (TableName != "Books")
            {
                error_message = "Table not found";
                return false;
            }
            return Insert("BOOKMARK_DB.sqlite", "INSERT INTO BOOKS(Accno, Title, Author, Publisher, Price, Semester, Branch, Edition, Classification_no, USN, FName) VALUES('" + Parameter2 + "', '" + Parameter2 + "', '" + Parameter3 + "', '" + Parameter4 + "', '" + Parameter5 + "', '" + Parameter6 + "', '" +Parameter7 + "', '" + Parameter8 + "', '" + Parameter9 + "', '" + Parameter10 + "', '" + Parameter11+ "');");
        }
        public Boolean Insert(String TableName, String Parameter1, String Parameter2, String Parameter3, byte[] pic)//FName,FBranch,Fphno,Fphoto
        {
            // To be used for inserting values into faculties table
            if(TableName!= "FACULTIES")
            {
                error_message = "Table not found";
                return false;
            }
            return Insert("BOOKMARK_DB.sqlite", "INSERT INTO FACULTIES (FName,Fphno,FBranch,Fphoto) VALUES ('" +Parameter1+"','"+Parameter2+"','"+Parameter3+"','"+pic+"');");
        }
        public Boolean Insert(String TableName, String Parameter1, String Parameter2, String Parameter3, String Parameter4, String Parameter5, String Parameter6, String Parameter7, String Parameter8)//Issued_User,USN,Book_Id,Issue_date,Expected_return_date,Return_date,Returned_User,Fine
        {
            // To be used for inserting values into  transactiontable
            if (TableName != "TRANSACTIONS")
            {
                error_message = "Table not found";
                return false;
            }
            return Insert("BOOKMARK_DB.sqlite", "INSERT INTO TRANSACTIONS(Issued_User, USN, Book_Id, Issue_date, Expected_return_date,Return_date,Returned_User,Fine) VALUES('" + Parameter1 + "', '" + Parameter2 + "', '" + Parameter3 + "', '" + Parameter4 + "', '" + Parameter5 + "', '" + Parameter6 + "', '" + Parameter7 + "', '" + Parameter8 + "');");
        }
        public Boolean Insert(String TableName, String Parameter1, String Parameter2, String Parameter3, String Parameter4, String Parameter5, String Parameter6, String Parameter7, String Parameter8, String Parameter9, String Parameter10, String Parameter11, String Parameter12, String Parameter13, String Parameter14, String Parameter15, String Parameter16, String Parameter17, String Parameter18, String Parameter19, String Parameter20, byte[] pic)//USN,SName,Sphno,Semester,B1,B2,BB1,BB2,BB3,BB4,BB5,BB6,BB7,BB8,BB9,BB10,BB11,BB12,BB13,BB14,Sphoto
        {
            // To be used for inserting values into Student table
            if (TableName.Substring(0, 3).Equals("STU"))
            {
                error_message = "Table not found";
                return false;
            }
            return Insert("BOOKMARK_DB.sqlite", "INSERT INTO " + TableName + "(USN,SName,Sphno,Semester,B1,B2,BB1,BB2,BB3,BB4,BB5,BB6,BB7,BB8,BB9,BB10,BB11,BB12,BB13,BB14,Sphoto) VALUES('" + Parameter1 + "','" + Parameter2 + "','" + Parameter3 + "','" + Parameter4 + "','" + Parameter5 + "','" + Parameter6 + "','" + Parameter7 + "','" + Parameter8 + "','" + Parameter9 + "','" + Parameter10 + "','" + Parameter11 + "','" + Parameter12 + "','" + Parameter13 + "','" + Parameter14 + "','" + Parameter15 + "','" + Parameter16 + "','" + Parameter17 + "','" + Parameter18 + "','" + Parameter19 + "','" + Parameter20 + "','" + pic + "');");
        }

       /*
        
        *
        *
        * 
        * 
        * 
        * 
        * 
        * 
        * 
        
             */
        public  Boolean Update(String Table_Name, String UpdateColumn, String parameter, String ConstrainColum, String ConstrainParameter)
        {
            // This function is used to update data in the database
            sql = "UPDATE "+ Table_Name + " SET "+UpdateColumn+" = '" + parameter + "' WHERE "+ConstrainColum+" = '" +ConstrainParameter+ "'";

            try
            {
                using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }
                    connection.Close();
                }

            }
            catch (Exception e)
            {
                error_message = e.ToString();
                return false;
            }
            return true;
        }
        public  Boolean Update(String Table_Name, String UpdateColumn1, String parameter1, String UpdateColumn2, String parameter2, String ConstrainColum1, String ConstrainParameter1, String ConstrainColum2, String ConstrainParameter2)
        {
            // This function is used to update data in the database
            sql = "UPDATE "+Table_Name+" SET "+ UpdateColumn1 + "='" + parameter1 + "',"+ UpdateColumn2 + "='" + parameter2 + "' WHERE "+ ConstrainColum1 + "='" + ConstrainParameter1 + "' AND "+ ConstrainColum2 + "='" + ConstrainParameter2 + "';";

            try
            {
                using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }
                    connection.Close();
                }
                
            }
            catch (Exception e)
            {
                error_message = e.ToString();
                return false;
            }
            return true;
           
        }


    }// closing class
}
