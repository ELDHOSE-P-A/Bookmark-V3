using System;
using System.Data;
using System.Data.SQLite;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace Bookmark_V3
{
    public partial class Bookmark : Form
    {
        Backend_DB database = new Backend_DB();
        public Bookmark()
        {
            InitializeComponent();
        }
        //---------------------------------BookMark Load Functions Start-----------------------//
        string Branch;       //------Used Later for creating Tables-----------//
        string Batch;        //------Used Later for creating Tables-----------//
        string TableName;    //------Used Later for creating Tables-----------//
        string ExcelSource;
        DateTime exretdate;//?
        byte[] pic;
        string[] array = new string[17];
        String sql = "";
        Boolean flag = true;
        int second = 0;
        public string path;
        int Fine, Return;
        string admin=null;//?
        private void Bookmark_Load(object sender, EventArgs e)
        {
            this.StartPanel.Location = new System.Drawing.Point(1, 1);
            this.StartPanel.Size = new Size(1000, 750);
            this.StartPanel.BringToFront();
            this.StartLabel.Text = "BOOKMARK";
            this.StartPanel.Show();
            LoginTimer.Interval = 200;
            LoginTimer.Start();
            this.LoginTimeLabel.Text = DateTime.Now.ToString("h:mm:ss tt\ndd-MM-yyyy");
            
         //   LoginForm LF = new LoginForm();//?


            //////////SQLiteConnection connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
            //////////SQLiteCommand command;
            //////////connection.Open();     
            //////////sql = "select * from SYSTEM WHERE Id =13;";
            //////////command = new SQLiteCommand(sql, connection);
            //////////SQLiteDataReader reader = command.ExecuteReader();
            //////////while (reader.Read())
            //////////{
            //////////    this.LogUserLabel.Text = reader["Value"].ToString();
            //////////    break;
            //////////}

            if(!database.Get("BOOKMARK_DB.sqlite", "SYSTEM","Id","13", "Value"))
            {
                MessageBox.Show(database.error_message);
            }
            else
            {
                this.LogUserLabel.Text = database.Result;
            }
                     


            Time_Date_Timer.Interval = 1000;
            Time_Date_Timer.Tick += new EventHandler(Time_Date_Timer_Tick);
            Time_Date_Timer.Enabled = true;
            Time_Date_Timer.Start();

            if (!database.Get("BOOKMARK_DB.sqlite", "SYSTEM", "Id", "1", "Value"))
            {
                MessageBox.Show(database.error_message);
            }
            else
            {
                admin = database.Result;
            }

            //////////connection.Close();
            //////////connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
            //////////connection.Open();
            //////////sql = "select * from SYSTEM WHERE Id =1;";
            //////////command = new SQLiteCommand(sql, connection);
            //////////reader = command.ExecuteReader();
            //////////while (reader.Read())
            //////////{
            //////////    admin = reader["Value"].ToString();
            //////////    break;
            //////////}
            //////////reader.Close();
            //////////command.Dispose();
            //////////connection.Close();


            getdetails();//?
        }

        private void LoginTimer_Tick(object sender, EventArgs e)
        {
            second = second + 1;
            if (second >= 5)
            {
                LoginTimer.Stop();
                this.StartPanel.SendToBack();
                this.StartPanel.Hide();
                this.PanelNameLabel.Text = "HOME";
                this.HomePanel.Dock = DockStyle.Fill;
                this.HomeBtn.BackColor = Color.FromArgb(0, 116, 170);
                this.TransBtn.BackColor = Color.FromArgb(0, 175, 240);
                this.BooksBtn.BackColor = Color.FromArgb(0, 175, 240);
                this.BorrowersBtn.BackColor = Color.FromArgb(0, 175, 240);
                this.ToolsBtn.BackColor = Color.FromArgb(0, 175, 240);
                this.SetBtn.BackColor = Color.FromArgb(0, 175, 240);
                this.AboutBtn.BackColor = Color.FromArgb(0, 175, 240);
                this.NotificationsBtn.BackColor = Color.FromArgb(0, 175, 240);
                this.HomeBtn.ForeColor = Color.Black;
                this.TransBtn.ForeColor = Color.White;
                this.BooksBtn.ForeColor = Color.White;
                this.BorrowersBtn.ForeColor = Color.White;
                this.ToolsBtn.ForeColor = Color.White;
                this.SetBtn.ForeColor = Color.White;
                this.AboutBtn.ForeColor = Color.White;
                this.NotificationsBtn.ForeColor = Color.White;
                this.BHomeBtn.BackColor = Color.Transparent;
                this.BTransBtn.BackColor = Color.Transparent;
                this.BBooksBtn.BackColor = Color.Transparent;
                this.BBorrowersBtn.BackColor = Color.Transparent;
                this.TransPanel.Hide();
                this.BooksPanel.Hide();
                this.BorrowersPanel.Hide();
                this.ToolsPanel.Hide();
                this.SetPanel.Hide();
                this.AboutPanel.Hide();
                this.NotificationPanel.Hide();
                this.HomePBox.BackColor = Color.Navy;
                this.TransPBox.BackColor = Color.Transparent;
                this.BooksPBox.BackColor = Color.Transparent;
                this.BorrowersPBox.BackColor = Color.Transparent;
                this.ToolsPBox.BackColor = Color.Transparent;
                this.SetPBox.BackColor = Color.Transparent;
                this.AboutPBox.BackColor = Color.Transparent;
                this.NotificationsPBox.BackColor = Color.Transparent;
                this.HomePanel.Show();
            }
        }

        private void LogoutTimer_Tick(object sender, EventArgs e)
        {
            second = second + 1;
            if (second >= 7)
            {
                LoginTimer.Stop();
                this.Close();
                LoginForm LF = new LoginForm();
                LF.Show();
            }
        }

        private void Time_Date_Timer_Tick(object sender, EventArgs e)
        {
            TimeLabel.Text = DateTime.Now.ToString("h:mm:ss tt");
            DateLabel.Text = DateTime.Now.ToLongDateString();
        }

        private void Sound()
        {
            try
            {
                System.Media.SoundPlayer Player =
                new System.Media.SoundPlayer(){ SoundLocation = "Click.wav" };
                Player.Load();
                Player.Play();
            }
            catch(Exception e)
            {
                MessageBox.Show(""+e);
            }
            
        }
        //-------------------------------Home Panel Functions Start-----------------------//
        private void HomeBtn_Click(object sender, EventArgs e)
        {
            this.Sound();
            this.PanelNameLabel.Text = "HOME";
            this.HomePanel.Dock = DockStyle.Fill;
            this.HomeBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.TransBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BooksBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BorrowersBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ToolsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SetBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.AboutBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.NotificationsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.ForeColor = Color.Black;
            this.TransBtn.ForeColor = Color.White;
            this.BooksBtn.ForeColor = Color.White;
            this.BorrowersBtn.ForeColor = Color.White;
            this.ToolsBtn.ForeColor = Color.White;
            this.SetBtn.ForeColor = Color.White;
            this.AboutBtn.ForeColor = Color.White;
            this.NotificationsBtn.ForeColor = Color.White;
            this.BHomeBtn.BackColor = Color.Transparent;
            this.BTransBtn.BackColor = Color.Transparent;
            this.BBooksBtn.BackColor = Color.Transparent;
            this.BBorrowersBtn.BackColor = Color.Transparent;
            this.TransPanel.Hide();
            this.BooksPanel.Hide();
            this.BorrowersPanel.Hide();
            this.ToolsPanel.Hide();
            this.SetPanel.Hide();
            this.AboutPanel.Hide();
            this.NotificationPanel.Hide();
            this.HomePBox.BackColor = Color.Navy;
            this.TransPBox.BackColor = Color.Transparent;
            this.BooksPBox.BackColor = Color.Transparent;
            this.BorrowersPBox.BackColor = Color.Transparent;
            this.ToolsPBox.BackColor = Color.Transparent;
            this.SetPBox.BackColor = Color.Transparent;
            this.AboutPBox.BackColor = Color.Transparent;
            this.NotificationsPBox.BackColor = Color.Transparent;
            this.HomePanel.Show();
        }

        private void jvitLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            jvitLinkLabel.LinkVisited = true;
            Process.Start("www.jvitedu.in/computer_science_about.php");
        }
        //-------------------------------Home Panel Functions End----------------------//      

        //-------------------------Transaction Panel Functions Start-------------------//
        private void TransBtn_Click(object sender, EventArgs e)
        {
            this.Sound();
            this.PanelNameLabel.Text = "TRANSACTIONS";
            this.TransPanel.Dock = DockStyle.Fill;
            this.TransBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.HomeBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BooksBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BorrowersBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ToolsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SetBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.AboutBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.NotificationsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.ForeColor = Color.White;
            this.TransBtn.ForeColor = Color.Black;
            this.BooksBtn.ForeColor = Color.White;
            this.BorrowersBtn.ForeColor = Color.White;
            this.ToolsBtn.ForeColor = Color.White;
            this.SetBtn.ForeColor = Color.White;
            this.AboutBtn.ForeColor = Color.White;
            this.NotificationsBtn.ForeColor = Color.White;
            this.BHomeBtn.BackColor = Color.Transparent;
            this.BTransBtn.BackColor = Color.Transparent;
            this.BBooksBtn.BackColor = Color.Transparent;
            this.BBorrowersBtn.BackColor = Color.Transparent;
            this.HomePanel.Hide();
            this.BooksPanel.Hide();
            this.BorrowersPanel.Hide();
            this.ToolsPanel.Hide();
            this.SetPanel.Hide();
            this.AboutPanel.Hide();
            this.NotificationPanel.Hide();
            this.RegularBtn_Click(sender, e);
            this.BBSTransPanel.Hide();
            this.HomePBox.BackColor = Color.Transparent;
            this.TransPBox.BackColor = Color.Navy;
            this.BooksPBox.BackColor = Color.Transparent;
            this.BorrowersPBox.BackColor = Color.Transparent;
            this.ToolsPBox.BackColor = Color.Transparent;
            this.SetPBox.BackColor = Color.Transparent;
            this.AboutPBox.BackColor = Color.Transparent;
            this.NotificationsPBox.BackColor = Color.Transparent;
            this.TransPanel.Show();
        }

        private void RegularBtn_Click(object sender, EventArgs e)
        {
            this.RegularBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.BookBankBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.FacultyBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.RegularBtn.ForeColor = Color.Black;
            this.BookBankBtn.ForeColor = Color.White;
            this.FacultyBtn.ForeColor = Color.White;
            this.RegularTransPanel.Dock = DockStyle.Fill;
            this.BBSTransPanel.Hide();
            this.FacultyTransPanel.Hide();
            this.RegularTransPanel.Show();
        }

        private void BookBankBtn_Click(object sender, EventArgs e)
        {
            this.RegularBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BookBankBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.FacultyBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.RegularBtn.ForeColor = Color.White;
            this.BookBankBtn.ForeColor = Color.Black;
            this.FacultyBtn.ForeColor = Color.White;
            this.BBSTransPanel.Dock = DockStyle.Fill;
            this.RegularTransPanel.Hide();
            this.FacultyTransPanel.Hide();
            this.BBSTransPanel.Show();
        }

        private void FacultyBtn_Click(object sender, EventArgs e)
        {
            this.RegularBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BookBankBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.FacultyBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.RegularBtn.ForeColor = Color.White;
            this.BookBankBtn.ForeColor = Color.White;
            this.FacultyBtn.ForeColor = Color.Black;
            this.FacultyTransPanel.Dock = DockStyle.Fill;
            this.RegularTransPanel.Hide();
            this.BBSTransPanel.Hide();
            this.FacultyTransPanel.Show();
        }

        //---------------------Regular Transactions Start-----------------------//
        private void RBookAccnoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RBSearchBtn_Click(sender, e);
            }
        }

        private void RUSNBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RSSearchBtn_Click(sender,e);
            }
        }

        private void RBSearchBtn_Click(object sender, EventArgs e)
        {
            if(RBookAccnoBox.Text.Equals(""))
            {
                MessageBox.Show("Please select a book");
                RegularBookTitleLabel.Text = "";
                RegularBorrowedLabel.Text = "";
            }
            //getting Book title based on book accession number
            if (!database.Get("BOOKMARK_DB.sqlite", "BOOKS", "Accno", RBookAccnoBox.Text, "Title")) MessageBox.Show(database.error_message); else RegularBookTitleLabel.Text = database.Result;
            //getting Book USN(of the student who borrowd it) based on book accession number
            if (!database.Get("BOOKMARK_DB.sqlite", "BOOKS", "Accno", RBookAccnoBox.Text, "USN"))  MessageBox.Show(database.error_message); else RegularBorrowedLabel.Text = database.Result;
            

            ////sql = "SELECT * FROM BOOKS WHERE Accno = '" + RBookAccnoBox.Text + "'";
            ////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
            ////{
            ////    connection.Open();
            ////    using (var command = new SQLiteCommand(sql, connection))
            ////    {
            ////        using (var reader = command.ExecuteReader())
            ////        {
            ////            while (reader.Read())
            ////            {
            ////                RegularBookTitleLabel.Text = reader["Title"].ToString();
            ////                RegularBorrowedLabel.Text = reader["USN"].ToString();
            ////            }
            ////            reader.Close();
            ////        }
            ////        command.Dispose();
            ////    }
            ////    connection.Close();
            ////}// closing using
        }

        private void RSSearchBtn_Click(object sender, EventArgs e)
        {
            if (RUSNBox.Text.Equals(""))
            {
                MessageBox.Show("Please select a student");
                RegularStudentNameLabel.Text = "";
                RegularBook1IdLabel.Text = "";
                RegularBook2IdLabel.Text = "";
            }               
            else
            {
                string temp = RUSNBox.Text.Substring(0, 7);
                string temp1 = RUSNBox.Text;
                TableName = "STUDENTS_" + temp;
                //getting Student name based on usn
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", RUSNBox.Text, "SName")) MessageBox.Show(database.error_message); else this.RegularStudentNameLabel.Text = database.Result;
                //getting Student Book_1 ID based on usn
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", RUSNBox.Text, "B1")) MessageBox.Show(database.error_message); else this.RegularBook1IdLabel.Text = database.Result;
                //getting Student Book_1 ID based on usn
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", RUSNBox.Text, "B2")) MessageBox.Show(database.error_message); else this.RegularBook2IdLabel.Text = database.Result;

                //////sql = "SELECT * FROM " + TableName + "  WHERE USN = '" + RUSNBox.Text + "'";
                //////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                //////{
                //////    connection.Open();
                //////    using (var command = new SQLiteCommand(sql, connection))
                //////    {
                //////        using (var reader = command.ExecuteReader())
                //////        {
                //////            while (reader.Read())
                //////            {
                //////                RegularStudentNameLabel.Text = reader["SName"].ToString();
                //////                RegularBook1IdLabel.Text = reader["B1"].ToString();
                //////                RegularBook2IdLabel.Text = reader["B2"].ToString();
                //////            }
                //////            reader.Close();
                //////        }
                //////        command.Dispose();
                //////    }
                //////    connection.Close();
                //////}// closing using

                if (!database.GetImage("BOOKMARK_DB.sqlite", TableName, "USN", RUSNBox.Text, "Sphoto")) MessageBox.Show("" + database.error_message); else RTSPBox.Image = ByteToImage(database.Picture_Result);

                //////////try//?what does this block of code do
                //////////{
                //////////    string query = "SELECT * FROM " + TableName + " WHERE USN='" + RUSNBox.Text + "';";
                //////////    sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                //////////    SQLiteConnection con = new SQLiteConnection(sql);
                //////////    SQLiteCommand cmd = new SQLiteCommand(query, con);
                //////////    con.Open();
                //////////    IDataReader reader = cmd.ExecuteReader();
                //////////    try
                //////////    {
                //////////        while (reader.Read())
                //////////        {
                //////////            byte[] F = (System.Byte[])reader["Sphoto"];
                //////////            RTSPBox.Image = ByteToImage(F);
                //////////        }
                //////////    }
                //////////    catch
                //////////    {
                //////////        MessageBox.Show("Image Error\n");
                //////////    }
                //////////    reader.Close();
                //////////    cmd.Dispose();
                //////////    con.Close();
                //////////}
                //////////catch
                //////////{
                //////////    MessageBox.Show("Image Error\n");
                //////////}
            }      
        }

        private void IssueBtn_Click(object sender, EventArgs e)
        {
            String usn = "", book = "";
            int tflag = 0;
            flag = false;
            if (RBookAccnoBox.Text.Equals(""))
                MessageBox.Show("Please select a book");
            else if (RUSNBox.Text.Equals(""))
                MessageBox.Show("Please select a student");
            else
            {
                string temp = RUSNBox.Text.Substring(0, 7);
                string temp1 = RUSNBox.Text;
                TableName = "STUDENTS_" + temp;
                //check if book already is issued
                try
                {

                    
                    if (!database.Get("BOOKMARK_DB.sqlite", "BOOKS", "Accno", RBookAccnoBox.Text, "USN")) MessageBox.Show(database.error_message); else usn = database.Result;
                    if (!database.Get("BOOKMARK_DB.sqlite", "BOOKS", "Accno", RBookAccnoBox.Text, "FName")) MessageBox.Show(database.error_message);// the else part as in above statement is not necessary here. the value of database.Result is directly used in the next line below

                    if (usn.Equals("") && database.Result.Equals(""))//NOTE: here the value of database.Result is the value of FName according to the querry that was just previously made.
                        flag = true;

                    //////sql = "SELECT * FROM  WHERE Accno = '" + RBookAccnoBox.Text + "'";
                    //////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    //////{
                    //////    connection.Open();
                    //////    using (var command = new SQLiteCommand(sql, connection))
                    //////    {
                    //////        using (var reader = command.ExecuteReader())
                    //////        {
                    //////            while (reader.Read())
                    //////            {
                    //////                usn = reader["USN"].ToString();
                    //////                if (reader["USN"].ToString().Equals("")&& reader["FName"].ToString().Equals(""))
                    //////                    flag = true;
                    //////            }
                    //////            reader.Close();
                    //////        }
                    //////        command.Dispose();
                    //////    }
                    //////    connection.Close();
                    //////}// closing using
                    if (flag == false)
                    {
                        MessageBox.Show("BOOK IS ALREADY ISSUED.\n PLEASE RETURN IT BEFORE ISSUING TO ANOTHER STUDENT");
                        return;
                    }
                    
                    String temp_b1="some value other than null", temp_b2= "some value other than null";
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", RUSNBox.Text, "B1")) MessageBox.Show(database.error_message); else temp_b1 = database.Result;
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", RUSNBox.Text, "B2")) MessageBox.Show(database.error_message); else temp_b2 = database.Result;

                    if (temp_b1.Equals(""))
                    {
                        flag = true;
                        tflag = 1;
                        book = temp_b1.ToString();

                    }
                    else if (temp_b2.ToString().Equals(""))
                    {
                        flag = true;
                        tflag = 2;
                        book = temp_b2.ToString();

                    }
                    else if (!temp_b1.ToString().Equals(null) && !temp_b2.ToString().Equals(null))
                    {
                        tflag = 3;
                    }


                    ////////sql = "SELECT * FROM "+TableName+" WHERE USN = '" + RUSNBox.Text + "'";
                    ////////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    ////////{
                    ////////    connection.Open();
                    ////////    using (var command = new SQLiteCommand(sql, connection))
                    ////////    {
                    ////////        using (var reader = command.ExecuteReader())
                    ////////        {
                    ////////            while (reader.Read())
                    ////////            {
                    ////////                if (reader["B1"].ToString().Equals(""))
                    ////////                {
                    ////////                    flag = true;
                    ////////                    tflag = 1;
                    ////////                    book = reader["B1"].ToString();
                    ////////                    break;
                    ////////                }
                    ////////                else if (reader["B2"].ToString().Equals(""))
                    ////////                {
                    ////////                    flag = true;
                    ////////                    tflag = 2;
                    ////////                    book = reader["B2"].ToString();
                    ////////                    break;
                    ////////                }
                    ////////                else if (!reader["B1"].ToString().Equals(null)&& !reader["B2"].ToString().Equals(null))
                    ////////                {
                    ////////                    tflag = 3;
                    ////////                    break;
                    ////////                }
                    ////////            }
                    ////////            reader.Close();
                    ////////        }
                    ////////        command.Dispose();
                    ////////    }
                    ////////    connection.Close();
                    ////////}// closing using
                   
                    if (tflag == 3)
                    {
                        MessageBox.Show("STUDENT HAS ALREADY TAKEN 2 BOOKS.\n PLEASE RETURN ATLEAST ONE BOOK BEFORE BORROWING ANOTHER BOOK");
                        return;
                    }




                    if (!database.Update("BOOKMARK_DB.sqlite", "BOOKS", "USN", RUSNBox.Text, "Accno", RBookAccnoBox.Text)) MessageBox.Show(database.error_message);

                    //////sql = "UPDATE BOOKS SET USN = '" + RUSNBox.Text + "' WHERE Accno = '" + RBookAccnoBox.Text + "'";
                    //////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    //////{
                    //////    connection.Open();
                    //////    using (var command = new SQLiteCommand(sql, connection))
                    //////    {
                    //////        command.ExecuteNonQuery();
                    //////        command.Dispose();
                    //////    }
                    //////    connection.Close();
                    //////}

                    if (tflag == 1)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, " B1", RBookAccnoBox.Text, "USN", RUSNBox.Text)) MessageBox.Show(database.error_message);
                    //////sql = "UPDATE "+TableName+ " SET B1 ='" + RBookAccnoBox.Text + "' WHERE USN = '" + RUSNBox.Text + "'";
                    if (tflag == 2)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, " B2", RBookAccnoBox.Text, "USN", RUSNBox.Text)) MessageBox.Show(database.error_message);
                    //////sql = "UPDATE " + TableName + " SET B2 ='" + RBookAccnoBox.Text + "' WHERE USN = '" + RUSNBox.Text + "'";
                    //////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    //////{
                    //////    connection.Open();
                    //////    using (var command = new SQLiteCommand(sql, connection))
                    //////    {
                    //////        command.ExecuteNonQuery();
                    //////        command.Dispose();
                    //////    }
                    //////    connection.Close();
                    //////}
                    MessageBox.Show("BOOK ISSUED SUCESSFULLY");
                    RBSearchBtn_Click(sender, e);//?
                    RSSearchBtn_Click(sender, e);//?

                    if (!database.Insert("TRANSACTIONS",LogUserLabel.Text, RUSNBox.Text, RBookAccnoBox.Text, DateTime.Now.ToShortDateString(), DateTime.Now.AddDays(Return).ToShortDateString(),null,null,null)) MessageBox.Show(database.error_message);
                    ////////string sql1 = "INSERT INTO TRANSACTIONS (Issued_User,USN,Book_Id,Issue_date,Expected_return_date) VALUES ('" + LogUserLabel.Text + "','" + RUSNBox.Text + "','" + RBookAccnoBox.Text + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.AddDays(Return).ToShortDateString() + "');";
                    ////////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    ////////{
                    ////////    connection.Open();
                    ////////    using (var command = new SQLiteCommand(sql1, connection))
                    ////////    {
                    ////////        command.ExecuteNonQuery();
                    ////////        command.Dispose();
                    ////////    }
                    ////////    connection.Close();
                    ////////}    
                }
                catch 
                {
                    MessageBox.Show("Unexpected exception in search student");
                }

            }
        }

        private void ReturnBtn_Click(object sender, EventArgs e)
        {
            String usn = "", book = "";
            int tflag = 0;
            flag = false;
            if (RBookAccnoBox.Text.Equals(""))
                MessageBox.Show("Please select a book");
            else if (RUSNBox.Text.Equals(""))
                MessageBox.Show("Please select a student");
            else
            {
                string temp = RUSNBox.Text.Substring(0, 7);
                TableName = "STUDENTS_" + temp;
                if (RBookAccnoBox.Text.Equals(""))
                    MessageBox.Show("Please select a book");
                else if (RUSNBox.Text.Equals(""))
                    MessageBox.Show("Please select a student");
                else//all data is entered in the respective boxes
                {

                    if (!database.Get("BOOKMARK_DB.sqlite", "BOOKS", "Accno", RBookAccnoBox.Text, "USN")) MessageBox.Show(database.error_message);else usn = database.Result;
                    String tfname="Some Random Value";
                    if (!database.Get("BOOKMARK_DB.sqlite", "BOOKS", "Accno", RBookAccnoBox.Text, "USN")) MessageBox.Show(database.error_message);else tfname = database.Result;
                    if (usn.Equals("") && tfname.ToString().Equals(""))
                        flag = true;//book is not issued to anyone 

                    //////////sql = "SELECT * FROM BOOKS WHERE Accno = '" + RBookAccnoBox.Text + "'";
                    //////////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    //////////{
                    //////////    connection.Open();
                    //////////    using (var command = new SQLiteCommand(sql, connection))
                    //////////    {
                    //////////        using (var reader = command.ExecuteReader())
                    //////////        {
                    //////////            while (reader.Read())
                    //////////            {
                    //////////                usn = reader["USN"].ToString();
                    //////////                if (reader["USN"].ToString().Equals("") && reader["FName"].ToString().Equals(""))
                    //////////                    flag = true;//book is not issued to anyone 
                    //////////            }
                    //////////            reader.Close();
                    //////////        }
                    //////////        command.Dispose();
                    //////////    }
                    //////////    connection.Close();
                    //////////}// closing using
                    if (flag == true)
                    {
                        MessageBox.Show("BOOK IS NOT ISSUED TO ANYONE");
                        return;
                    }
                    String tb1="", tb2="";
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", RUSNBox.Text, "B1")) MessageBox.Show(database.error_message); else tb1 = database.Result;
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", RUSNBox.Text, "B2")) MessageBox.Show(database.error_message); else tb2 = database.Result;
                    if ((tb1.Equals(RBookAccnoBox.Text)))
                    {
                        tflag = 1;
                        book = tb1;
                        
                    }
                    else if ((tb2.Equals(RBookAccnoBox.Text)))
                    {
                        tflag = 2;
                        book = tb2;
                        
                    }
                    if (tb1.Equals("") && (tb2.Equals("")))
                    {
                        tflag = 3;
                        book = tb2;//? may not be required. because here student has not borrowed any book
                        
                    }


                    ////////////sql = "SELECT * FROM " + TableName + " WHERE USN = '" + RUSNBox.Text + "'";
                    ////////////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    ////////////{
                    ////////////    connection.Open();
                    ////////////    using (var command = new SQLiteCommand(sql, connection))
                    ////////////    {
                    ////////////        using (var reader = command.ExecuteReader())
                    ////////////        {
                    ////////////            while (reader.Read())
                    ////////////            {
                    ////////////                if ((reader["B1"].ToString().Equals(RBookAccnoBox.Text)))
                    ////////////                {
                    ////////////                    tflag = 1;
                    ////////////                    book = reader["B1"].ToString();
                    ////////////                    break;
                    ////////////                }
                    ////////////                else if ((reader["B2"].ToString().Equals(RBookAccnoBox.Text)))
                    ////////////                {
                    ////////////                    tflag = 2;
                    ////////////                    book = reader["B2"].ToString();
                    ////////////                    break;
                    ////////////                }
                    ////////////                if (reader["B1"].ToString().Equals("") && (reader["B2"].ToString().Equals("")))
                    ////////////                {
                    ////////////                    tflag = 3;
                    ////////////                    book = reader["B2"].ToString();
                    ////////////                    break;
                    ////////////                }

                    ////////////            }
                    ////////////            reader.Close();
                    ////////////        }
                    ////////////        command.Dispose();
                    ////////////    }
                    ////////////    connection.Close();
                    ////////////}// closing using
                    if (tflag == 3)
                    {
                        MessageBox.Show("STUDENT HAS NOT BORROWED ANY BOOK");
                        return;
                    }
                   if (tflag == 0)
                    {
                        MessageBox.Show(" THIS STUDENT HAS NOT BORROWED THE ENTERED BOOK");
                        return;
                    }
                    //if all is well RETURN the book 

                    if (!database.Update("BOOKMARK_DB.sqlite", "BOOKS", "USN", null, "Accno", RBookAccnoBox.Text)) MessageBox.Show(database.error_message);

                    ////////////sql = "UPDATE BOOKS SET USN = \'\' WHERE Accno = \'" + RBookAccnoBox.Text + "\'";
                    ////////////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    ////////////{
                    ////////////    connection.Open();
                    ////////////    using (var command = new SQLiteCommand(sql, connection))
                    ////////////    {
                    ////////////        command.ExecuteNonQuery();
                    ////////////        command.Dispose();
                    ////////////    }
                    ////////////    connection.Close();
                    ////////////}
                    if (tflag == 1)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "B1", null, "USN", RUSNBox.Text)) MessageBox.Show(database.error_message);
                    //sql = "UPDATE " + TableName + " SET B1 = \'\' WHERE USN = '" + RUSNBox.Text + "';";
                    if (tflag == 2)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "B2", null, "USN", RUSNBox.Text)) MessageBox.Show(database.error_message);
                    ////////////sql = "UPDATE " + TableName + " SET B2 = \'\' WHERE USN = '" + RUSNBox.Text + "';";
                    ////////////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    ////////////{
                    ////////////    connection.Open();
                    ////////////    using (var command = new SQLiteCommand(sql, connection))
                    ////////////    {
                    ////////////        command.ExecuteNonQuery();
                    ////////////        command.Dispose();
                    ////////////    }
                    ////////////    connection.Close();
                    ////////////}
                    MessageBox.Show("BOOK RETURNED SUCESSFULLY");//?
                    RBSearchBtn_Click(sender, e);
                    RSSearchBtn_Click(sender, e);




                    if (!database.Update("BOOKMARK_DB.sqlite", "TRANSACTIONS", "Return_date", DateTime.Now.ToShortDateString(), "Returned_User", LogUserLabel.Text, "USN", RUSNBox.Text, "Book_Id", RBookAccnoBox.Text)) MessageBox.Show(database.error_message);
                    ////////////sql = "UPDATE TRANSACTIONS SET Return_date='" + DateTime.Now.ToShortDateString() + "',Returned_User='" + LogUserLabel.Text + "' WHERE USN='" + RUSNBox.Text + "' AND Book_Id='"+RBookAccnoBox.Text+"';";
                    ////////////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    ////////////{
                    ////////////    connection.Open();
                    ////////////    using (var command = new SQLiteCommand(sql, connection))
                    ////////////    {
                    ////////////        command.ExecuteNonQuery();
                    ////////////        command.Dispose();
                    ////////////    }
                    ////////////    connection.Close();
                    ////////////}                  
                }
            }
        }

        private void RTClearAllBtn_Click(object sender, EventArgs e)
        {
            // this button is to clear all text boxes from the form
            RBookAccnoBox.Text = "";
            RUSNBox.Text = "";
            RegularBookTitleLabel.Text = "";
            RegularBorrowedLabel.Text = "";
            RegularStudentNameLabel.Text = "";
            RegularBook1IdLabel.Text = "";
            RegularBook2IdLabel.Text = "";
            RTSPBox.Image = null;
        }
        //---------------------Regular Transactions End-------------------------//

        //------------------Book-Bank Scheme Transactions Start-----------------//
        private void BBSBookAccnoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BBSBSearchBtn_Click(sender, e);
            }
        }

        private void BBSUSNBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BBSSSearchBtn_Click(sender, e);
            }
        }

        private void BBSBSearchBtn_Click(object sender, EventArgs e)
        {
            if (BBSBookAccnoBox.Text.Equals(""))
            {
                MessageBox.Show("Please select a book");
                RegularBookTitleLabel.Text = "";
                RegularBorrowedLabel.Text = "";
            }

            if (!database.Get("BOOKMARK_DB.sqlite", "BOOKS", "Accno", BBSBookAccnoBox.Text, "Title")) MessageBox.Show(database.error_message); else BBSTitleLabel.Text = database.Result;
            if (!database.Get("BOOKMARK_DB.sqlite", "BOOKS", "Accno", BBSBookAccnoBox.Text, "USN")) MessageBox.Show(database.error_message); else BBSBorrowedByLabel.Text = database.Result;
            ////////sql = "SELECT * FROM BOOKS WHERE Accno = '" + BBSBookAccnoBox.Text + "'";
            ////////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
            ////////{
            ////////    connection.Open();
            ////////    using (var command = new SQLiteCommand(sql, connection))
            ////////    {
            ////////        using (var reader = command.ExecuteReader())
            ////////        {
            ////////            while (reader.Read())
            ////////            {
            ////////                BBSTitleLabel.Text = reader["Title"].ToString();
            ////////                BBSBorrowedByLabel.Text = reader["USN"].ToString();
            ////////            }
            ////////            reader.Close();
            ////////        }
            ////////        command.Dispose();
            ////////    }
            ////////    connection.Close();
            ////////}// closing using
        }

        private void BBSSSearchBtn_Click(object sender, EventArgs e)
        {
            if (BBSUSNBox.Text.Equals(""))
                MessageBox.Show("Please select a student");
            else
            {
                string temp = BBSUSNBox.Text.Substring(0, 7);
                string temp1 = BBSUSNBox.Text;
                TableName = "STUDENTS_" + temp;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB1")) MessageBox.Show(database.error_message); else array[1] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB2")) MessageBox.Show(database.error_message); else array[2] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB3")) MessageBox.Show(database.error_message); else array[3] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB4")) MessageBox.Show(database.error_message); else array[4] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB5")) MessageBox.Show(database.error_message); else array[5] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB6")) MessageBox.Show(database.error_message); else array[6] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB7")) MessageBox.Show(database.error_message); else array[7] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB8")) MessageBox.Show(database.error_message); else array[8] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB9")) MessageBox.Show(database.error_message); else array[9] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB10")) MessageBox.Show(database.error_message); else array[10] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB11")) MessageBox.Show(database.error_message); else array[11] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB12")) MessageBox.Show(database.error_message); else array[12] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB13")) MessageBox.Show(database.error_message); else array[13] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB14")) MessageBox.Show(database.error_message); else array[14] = database.Result;
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "SName")) MessageBox.Show(database.error_message); else BBSStudNameLabel.Text = database.Result;//geting student name from database and seting that name to the label
                ////////////////try
                ////////////////{

                ////////////////        sql = "SELECT * FROM " + TableName + " WHERE USN = '" + BBSUSNBox.Text + "'";
                ////////////////        using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                ////////////////        {
                ////////////////            connection.Open();
                ////////////////            using (var command = new SQLiteCommand(sql, connection))
                ////////////////            {
                ////////////////                using (var reader = command.ExecuteReader())
                ////////////////                {
                ////////////////                    while (reader.Read())
                ////////////////                    {
                ////////////////                        array[1] = reader["BB1"].ToString();
                ////////////////                        array[2] = reader["BB2"].ToString();
                ////////////////                        array[3] = reader["BB3"].ToString();
                ////////////////                        array[4] = reader["BB4"].ToString();
                ////////////////                        array[5] = reader["BB5"].ToString();
                ////////////////                        array[6] = reader["BB6"].ToString();
                ////////////////                        array[7] = reader["BB7"].ToString();
                ////////////////                        array[8] = reader["BB8"].ToString();
                ////////////////                        array[9] = reader["BB9"].ToString();
                ////////////////                        array[10] = reader["BB10"].ToString();
                ////////////////                        array[11] = reader["BB11"].ToString();
                ////////////////                        array[12] = reader["BB12"].ToString();
                ////////////////                        array[13] = reader["BB13"].ToString();
                ////////////////                        array[14] = reader["BB14"].ToString();
                ////////////////                    }
                ////////////////                    reader.Close();
                ////////////////                }
                ////////////////                command.Dispose();
                ////////////////            }
                ////////////////            connection.Close();
                ////////////////        }// closing using
                ////////////////    }
                ////////////////    catch
                ////////////////    {
                ////////////////        MessageBox.Show("Unexpected exception in search student");
                ////////////////    }

                //re-organizing the array to avoid empty spaces in between
                int j = 0, i = 0;
                for (i = 1; i < 14; i++)
                {
                    
                    if (array[i] == "")
                    {
                        for (j = i; j < 14; j++)
                           array[j] = array[j + 1];
                        array[14] = "";
                    }
                }

                // seting respective values of book in the lables
                BBSBookLabel1.Text = array[1];
                BBSBookLabel2.Text = array[2];
                BBSBookLabel3.Text = array[3];
                BBSBookLabel4.Text = array[4];
                BBSBookLabel5.Text = array[5];
                BBSBookLabel6.Text = array[6];
                BBSBookLabel7.Text = array[7];
                BBSBookLabel8.Text = array[8];
                BBSBookLabel9.Text = array[9];
                BBSBookLabel10.Text = array[10];
                BBSBookLabel11.Text = array[11];
                BBSBookLabel12.Text = array[12];
                BBSBookLabel13.Text = array[13];
                BBSBookLabel14.Text = array[14];




                ////////////////////try
                ////////////////////{
                ////////////////////        sql = "SELECT * FROM " + TableName + " WHERE USN = '" + BBSUSNBox.Text + "'";
                ////////////////////        using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                ////////////////////        {
                ////////////////////            connection.Open();
                ////////////////////            using (var command = new SQLiteCommand(sql, connection))
                ////////////////////            {
                ////////////////////                using (var reader = command.ExecuteReader())
                ////////////////////                {
                ////////////////////                    while (reader.Read())
                ////////////////////                    {
                ////////////////////                        BBSStudNameLabel.Text = reader["SName"].ToString();
                ////////////////////                        BBSBookLabel1.Text = array[1];
                ////////////////////                        BBSBookLabel2.Text = array[2];
                ////////////////////                        BBSBookLabel3.Text = array[3];
                ////////////////////                        BBSBookLabel4.Text = array[4];
                ////////////////////                        BBSBookLabel5.Text = array[5];
                ////////////////////                        BBSBookLabel6.Text = array[6];
                ////////////////////                        BBSBookLabel7.Text = array[7];
                ////////////////////                        BBSBookLabel8.Text = array[8];
                ////////////////////                        BBSBookLabel9.Text = array[9];
                ////////////////////                        BBSBookLabel10.Text = array[10];
                ////////////////////                        BBSBookLabel11.Text = array[11];
                ////////////////////                        BBSBookLabel12.Text = array[12];
                ////////////////////                        BBSBookLabel13.Text = array[13];
                ////////////////////                        BBSBookLabel14.Text = array[14];
                ////////////////////                    }
                ////////////////////                    reader.Close();
                ////////////////////                }
                ////////////////////                command.Dispose();
                ////////////////////            }
                ////////////////////            connection.Close();
                ////////////////////        }// closing using
                ////////////////////    }
                ////////////////////    catch
                ////////////////////    {
                ////////////////////        MessageBox.Show("unexpected exception in search student");
                ////////////////////    }


                // geting picture from database as an array and loading it in picture box
                if (!database.GetImage("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "Sphoto")) MessageBox.Show(""+database.error_message); else BBSTSPBox.Image = ByteToImage(database.Picture_Result);
                
                ////////////////////try
                ////////////////////    {
                ////////////////////        string query = "SELECT * FROM " + TableName + " WHERE USN='" + BBSUSNBox.Text + "';";
                ////////////////////        sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                ////////////////////        SQLiteConnection con = new SQLiteConnection(sql);
                ////////////////////        SQLiteCommand cmd = new SQLiteCommand(query, con);
                ////////////////////        con.Open();
                ////////////////////        IDataReader reader = cmd.ExecuteReader();
                ////////////////////        try
                ////////////////////        {
                ////////////////////            while (reader.Read())
                ////////////////////            {
                ////////////////////                byte[] F = (System.Byte[])reader["Sphoto"];
                ////////////////////                BBSTSPBox.Image = ByteToImage(F);
                ////////////////////            }
                ////////////////////            reader.Close();
                ////////////////////        }
                ////////////////////        catch
                ////////////////////        {
                ////////////////////            MessageBox.Show("Image Error\n");
                ////////////////////        }
                ////////////////////        con.Close();
                ////////////////////    }
                ////////////////////    catch
                ////////////////////    {
                ////////////////////        MessageBox.Show("Error\n");
                ////////////////////    }
            }
                
            
        }

        private void IssueBookBtn_Click(object sender, EventArgs e)
        {
            int tflag = 0;
            flag = false;
            if (BBSBookAccnoBox.Text.Equals(""))
                MessageBox.Show("Please select a book");
            else if (BBSUSNBox.Text.Equals(""))
                MessageBox.Show("Please select a student");
            else
            {
                
                    string temp = BBSUSNBox.Text.Substring(0, 7);
                   // string temp1 = BBSUSNBox.Text;
                    TableName = "STUDENTS_" + temp;

                    String usn="", Fname="";
                    if (!database.Get("BOOKMARK_DB.sqlite", "BOOKS", "Accno", BBSBookAccnoBox.Text, "Value")) MessageBox.Show(database.error_message); else usn= database.Result;
                    if (!database.Get("BOOKMARK_DB.sqlite", "BOOKS", "Accno", BBSBookAccnoBox.Text, "Value")) MessageBox.Show(database.error_message); else Fname = database.Result;
                    if (usn.Equals("") && Fname.Equals(""))
                        flag = true;

                    ////////////////sql = "SELECT * FROM BOOKS WHERE Accno = '" + BBSBookAccnoBox.Text + "'";
                    ////////////////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    ////////////////{
                    ////////////////    connection.Open();
                    ////////////////    using (var command = new SQLiteCommand(sql, connection))
                    ////////////////    {
                    ////////////////        using (var reader = command.ExecuteReader())
                    ////////////////        {
                    ////////////////            while (reader.Read())
                    ////////////////            {
                    ////////////////                if (reader["USN"].ToString().Equals("")&& reader["FName"].ToString().Equals(""))
                    ////////////////                    flag = true;
                    ////////////////            }
                    ////////////////            reader.Close();
                    ////////////////        }
                    ////////////////        command.Dispose();
                    ////////////////    }
                    ////////////////    connection.Close();
                    ////////////////}// closing using
                    if (flag == false)
                    {
                        MessageBox.Show("BOOK IS ALREADY ISSUED.\n PLEASE RETURN IT BEFORE ISSUING TO ANOTHER STUDENT");
                        return;
                    }
                    flag = false;
                    // checked if the student has already taken maximum books or is there atleast one book-slot left for the student
                    /*
                     * in the below statements we use a jump label because there is no need to checking if further book_slots are free(null) when we have found one free(null) book slot
                     * this will save time 
                     * 
                     * */
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB1")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 1;
                            goto JumpLabel_1;
                        }
                    }
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB2")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 2;
                            goto JumpLabel_1;
                        }
                    }
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB3")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 3;
                            goto JumpLabel_1;
                        }
                    }
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB4")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 4;
                            goto JumpLabel_1;
                        }
                    }
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB5")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 5;
                            goto JumpLabel_1;
                        }
                    }
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB6")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 6;
                            goto JumpLabel_1;
                        }
                    }
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB7")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 7;
                            goto JumpLabel_1;
                        }
                    }
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB8")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 8;
                            goto JumpLabel_1;
                        }
                    }
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB9")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 9;
                            goto JumpLabel_1;
                        }
                    }
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB10")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 10;
                            goto JumpLabel_1;
                        }
                    }
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB11")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 11;
                            goto JumpLabel_1;
                        }
                    }
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB12")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 12;
                            goto JumpLabel_1;
                        }
                    }
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB13")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 13;
                            goto JumpLabel_1;
                        }
                    }
                    if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB14")) MessageBox.Show(database.error_message);
                    else
                    {
                        if (database.Result.Equals(""))
                        {
                            flag = true;
                            tflag = 14;
                            goto JumpLabel_1;
                        }
                    }






                    //////////////////////sql = "SELECT * FROM "+TableName+" WHERE USN = '" + BBSUSNBox.Text + "'";
                    //////////////////////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    //////////////////////{
                    //////////////////////    connection.Open();
                    //////////////////////    using (var command = new SQLiteCommand(sql, connection))
                    //////////////////////    {
                    //////////////////////        using (var reader = command.ExecuteReader())
                    //////////////////////        {
                    //////////////////////            while (reader.Read())
                    //////////////////////            {
                    //////////////////////                if (reader["BB1"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 1;
                    //////////////////////                    //  book = reader["B1"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }
                    //////////////////////                else if (reader["BB2"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 2;
                    //////////////////////                    // book = reader["B2"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }
                    //////////////////////                else if (reader["BB3"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 3;
                    //////////////////////                    // book = reader["B2"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }
                    //////////////////////                else if (reader["BB4"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 4;
                    //////////////////////                    // book = reader["B2"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }
                    //////////////////////                else if (reader["BB5"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 5;
                    //////////////////////                    // book = reader["B2"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }
                    //////////////////////                else if (reader["BB6"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 6;
                    //////////////////////                    // book = reader["B2"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }
                    //////////////////////                else if (reader["BB7"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 7;
                    //////////////////////                    // book = reader["B2"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }
                    //////////////////////                else if (reader["BB8"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 8;
                    //////////////////////                    // book = reader["B2"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }
                    //////////////////////                else if (reader["BB9"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 9;
                    //////////////////////                    // book = reader["B2"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }
                    //////////////////////                else if (reader["BB10"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 10;
                    //////////////////////                    // book = reader["B2"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }
                    //////////////////////                else if (reader["BB11"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 11;
                    //////////////////////                    // book = reader["B2"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }
                    //////////////////////                else if (reader["BB12"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 12;
                    //////////////////////                    // book = reader["B2"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }
                    //////////////////////                else if (reader["BB13"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 13;
                    //////////////////////                    // book = reader["B2"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }
                    //////////////////////                else if (reader["BB14"].ToString().Equals(""))
                    //////////////////////                {
                    //////////////////////                    flag = true;
                    //////////////////////                    tflag = 14;
                    //////////////////////                    // book = reader["B2"].ToString();
                    //////////////////////                    break;
                    //////////////////////                }

                    //////////////////////            }
                    //////////////////////            reader.Close();
                    //////////////////////        }
                    //////////////////////        command.Dispose();
                    //////////////////////    }
                    //////////////////////    connection.Close();
                    //////////////////////}// closing using

                    JumpLabel_1:
                    if (flag == false)
                    {
                        MessageBox.Show("STUDENT HAS ALREADY TAKEN 14 BOOKS.\n PLEASE RETURN ATLEAST ONE BOOK BEFORE BORROWING ANOTHER BOOK");
                        return;
                    }

                    // All is well. So issue the book 
                    //updating books table
                    if (!database.Update("BOOKMARK_DB.sqlite", "BOOKS", "USN", BBSUSNBox.Text, "Accno", BBSBookAccnoBox.Text)) MessageBox.Show(database.error_message);

                    ////////////////sql = "UPDATE BOOKS SET USN = \'" + BBSUSNBox.Text + "\' WHERE Accno = \'" + BBSBookAccnoBox.Text + "\'";
                    ////////////////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    ////////////////{
                    ////////////////    connection.Open();
                    ////////////////    using (var command = new SQLiteCommand(sql, connection))
                    ////////////////    {
                    ////////////////        command.ExecuteNonQuery();
                    ////////////////        command.Dispose();
                    ////////////////    }
                    ////////////////    connection.Close();
                    ////////////////}

                    //updating student table
                    if (tflag == 1)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                    if (tflag == 2)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB2", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                    if (tflag == 3)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB3", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                    if (tflag == 4)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB4", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                    if (tflag == 5)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB5", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                    if (tflag == 6)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB6", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                    if (tflag == 7)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB7", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                    if (tflag == 8)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB8", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                    if (tflag == 9)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB9", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                    if (tflag == 10)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB10", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                    if (tflag == 11)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB11", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                    if (tflag == 12)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB12", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                    if (tflag == 13)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB13", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                    if (tflag == 14)
                        if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB14", BBSBookAccnoBox.Text, "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);



                    //////////if (tflag == 1)
                    //////////    sql = "UPDATE " + TableName + " SET BB1 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////else if (tflag == 2)
                    //////////    sql = "UPDATE " + TableName + " SET BB2 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////else if (tflag == 3)
                    //////////    sql = "UPDATE " + TableName + " SET BB3 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////else if (tflag == 4)
                    //////////    sql = "UPDATE " + TableName + " SET BB4 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////else if (tflag == 5)
                    //////////    sql = "UPDATE " + TableName + " SET BB5 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////else if (tflag == 6)
                    //////////    sql = "UPDATE " + TableName + " SET BB6 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////else if (tflag == 7)
                    //////////    sql = "UPDATE " + TableName + " SET BB7 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////else if (tflag == 8)
                    //////////    sql = "UPDATE " + TableName + " SET BB8 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////else if (tflag == 9)
                    //////////    sql = "UPDATE " + TableName + " SET BB9 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////else if (tflag == 10)
                    //////////    sql = "UPDATE " + TableName + " SET BB10 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////else if (tflag == 11)
                    //////////    sql = "UPDATE " + TableName + " SET BB11 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////else if (tflag == 12)
                    //////////    sql = "UPDATE " + TableName + " SET BB12 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////else if (tflag == 13)
                    //////////    sql = "UPDATE " + TableName + " SET BB13 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////else if (tflag == 14)
                    //////////    sql = "UPDATE " + TableName + " SET BB14 = \'" + BBSBookAccnoBox.Text + "\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                    //////////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    //////////{
                    //////////    connection.Open();
                    //////////    using (var command = new SQLiteCommand(sql, connection))
                    //////////    {
                    //////////        command.ExecuteNonQuery();
                    //////////        command.Dispose();
                    //////////    }
                    //////////    connection.Close();
                    //////////}


                    MessageBox.Show("BOOK ISSUED SUCESSFULLY");
                    BBSBSearchBtn_Click(sender, e);
                    BBSSSearchBtn_Click(sender, e);
               
            }
        }

        private void ReturnBookBtn_Click(object sender, EventArgs e)
        {

            if (BBSBookAccnoBox.Text.Equals(""))
            {
                MessageBox.Show("Please select a book");
                return;
            }
            else if (BBSUSNBox.Text.Equals(""))
            {
                MessageBox.Show("Please select a student");
                return;
            }
           
                
                
                
                    
                    
                string temp = BBSUSNBox.Text.Substring(0, 7);
                string temp1 = BBSUSNBox.Text;
                TableName = "STUDENTS_" + temp;

                // checking if book is issued to anyone
                if (!database.Get("BOOKMARK_DB.sqlite", "BOOKS", "Accno", BBSBookAccnoBox.Text, "USN")) MessageBox.Show(database.error_message); 

                //////sql = "SELECT * FROM BOOKS WHERE Accno = '" + BBSBookAccnoBox.Text + "'";
                //////        using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                //////        {
                //////            connection.Open();
                //////            using (var command = new SQLiteCommand(sql, connection))
                //////            {
                //////                using (var reader = command.ExecuteReader())
                //////                {
                //////                    while (reader.Read())
                //////                    {
                //////                        if (reader["USN"].ToString().Equals(""))
                //////                            flag = true;
                //////                    }
                //////                    reader.Close();
                //////                }
                //////                command.Dispose();
                //////            }
                //////            connection.Close();
                //////        }// closing using


                if (database.Result.Equals(""))
                {
                    MessageBox.Show("BOOK IS NOT ISSUED TO ANYONE");
                    return;
                }

                // checking if student has  borrowed any book and also check if the book entered is borrowed by the current student
                int tflag = 0;
                int flag2=0;// this will be like a counter. each time we encounter an empty book slot, this flaf2 updated by 1. so at the end if its value is 14, it means all 14 book slots are empty 
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB1")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if(database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 1;
                    }
                }
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB2")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if (database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 2;
                    }
                }
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB3")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if (database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 3;
                    }
                }
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB4")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if (database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 4;
                    }
                }
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB5")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if (database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 5;
                    }
                }
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB6")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if (database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 6;
                    }
                }
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB7")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if (database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 7;
                    }
                }
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB8")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if (database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 8;
                    }
                }
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB9")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if (database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 9;
                    }
                }
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB10")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if (database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 10;
                    }
                }
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB11")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if (database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 11;
                    }
                }
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB12")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if (database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 12;
                    }
                }
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB13")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if (database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 13;
                    }
                }
                if (!database.Get("BOOKMARK_DB.sqlite", TableName, "USN", BBSUSNBox.Text, "BB14")) MessageBox.Show(database.error_message);
                else
                {
                    if (database.Result.Equals(""))
                        flag2++;
                    else if (database.Result.Equals(BBSBookAccnoBox.Text))
                    {
                        flag = true;
                        tflag = 14;
                    }
                }
                if(flag2==14)
                {
                    // this means that student has not selected any book
                    MessageBox.Show("ENTERED STUDENT HAS NOT TAKEN ANY BOOK");
                    return;
                }
                if(tflag==0)
                {
                    MessageBox.Show("THIS STUDENT HAS NOT TAKEN ENTERED BOOK");
                    return;
                }




                ////////sql = "SELECT * FROM " + TableName + " WHERE USN = '" + BBSUSNBox.Text + "'";

                ////////        using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                ////////        {
                ////////            connection.Open();
                ////////            using (var command = new SQLiteCommand(sql, connection))
                ////////            {
                ////////                using (var reader = command.ExecuteReader())
                ////////                {
                ////////                    while (reader.Read())
                ////////                    {
                ////////                        if (reader["BB1"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 1;
                ////////                            break;
                ////////                        }
                ////////                        else if (reader["BB2"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 2;
                ////////                            break;
                ////////                        }
                ////////                        else if (reader["BB3"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 3;
                ////////                            break;
                ////////                        }
                ////////                        else if (reader["BB4"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 4;
                ////////                            break;
                ////////                        }
                ////////                        else if (reader["BB5"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 5;
                ////////                            break;
                ////////                        }
                ////////                        else if (reader["BB6"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 6;
                ////////                            break;
                ////////                        }
                ////////                        else if (reader["BB7"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 7;
                ////////                            break;
                ////////                        }
                ////////                        else if (reader["BB8"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 8;
                ////////                            break;
                ////////                        }
                ////////                        else if (reader["BB9"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 9;
                ////////                            break;
                ////////                        }
                ////////                        else if (reader["BB10"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 10;
                ////////                            break;
                ////////                        }
                ////////                        else if (reader["BB11"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 11;
                ////////                            break;
                ////////                        }
                ////////                        else if (reader["BB12"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 12;
                ////////                            break;
                ////////                        }
                ////////                        else if (reader["BB13"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 13;
                ////////                            break;
                ////////                        }
                ////////                        else if (reader["BB14"].ToString().Equals(BBSBookAccnoBox.Text))
                ////////                        {
                ////////                            flag = true;
                ////////                            tflag = 14;
                ////////                            break;
                ////////                        }
                ////////                        if (reader["BB1"].ToString().Equals("") && (reader["BB2"].ToString().Equals("")) && (reader["BB3"].ToString().Equals("")) && (reader["BB4"].ToString().Equals("")) && (reader["BB5"].ToString().Equals("")) && (reader["BB6"].ToString().Equals("")) && (reader["BB7"].ToString().Equals("")) && (reader["BB8"].ToString().Equals("")) && (reader["BB9"].ToString().Equals("")) && (reader["BB10"].ToString().Equals("")) && (reader["BB11"].ToString().Equals("")) && (reader["BB12"].ToString().Equals("")) && (reader["BB13"].ToString().Equals("")) && (reader["BB14"].ToString().Equals("")))
                ////////                        {
                ////////                            tflag = 15;
                ////////                            break;
                ////////                        }

                ////////                    }
                ////////                    reader.Close();
                ////////                }
                ////////                command.Dispose();
                ////////            }
                ////////            connection.Close();
                ////////        }// closing using
                ////////        if (tflag == 15)
                ////////        {
                ////////            MessageBox.Show("STUDENT HAS NOT BORROWED ANY BOOK");
                ////////            return;
                ////////        }

                ////////        if (tflag == 0)
                ////////        {
                ////////            MessageBox.Show(" THIS STUDENT HAS NOT BORROWED THE ENTERED BOOK");
                ////////            return;
                ////////        }


                //All is well. So RETURN the book 
                //updating books table
                if (!database.Update("BOOKMARK_DB.sqlite", "BOOKS", "USN", "", "Accno", BBSBookAccnoBox.Text)) MessageBox.Show(database.error_message);

                //////sql = "UPDATE BOOKS SET USN = \'\' WHERE Accno = \'" + BBSBookAccnoBox.Text + "\'";
                //////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                //////{
                //////    connection.Open();
                //////    using (var command = new SQLiteCommand(sql, connection))
                //////    {
                //////        command.ExecuteNonQuery();
                //////        command.Dispose();
                //////    }
                //////    connection.Close();
                //////}
                // updating student table
                if (tflag == 1)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                if (tflag == 2)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                if (tflag == 3)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                if (tflag == 4)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                if (tflag == 5)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                if (tflag == 6)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                if (tflag == 7)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                if (tflag == 8)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                if (tflag == 9)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                if (tflag == 10)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                if (tflag == 11)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                if (tflag == 12)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                if (tflag == 13)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);
                if (tflag == 14)
                    if (!database.Update("BOOKMARK_DB.sqlite", TableName, "BB1", "", "USN", BBSUSNBox.Text)) MessageBox.Show(database.error_message);


                        ////////////////if (tflag == 1)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB1 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                        ////////////////else if (tflag == 2)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB2 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                        ////////////////else if (tflag == 3)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB3 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                        ////////////////else if (tflag == 4)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB4 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                        ////////////////else if (tflag == 5)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB5 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                        ////////////////else if (tflag == 6)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB6 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                        ////////////////else if (tflag == 7)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB7 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                        ////////////////else if (tflag == 8)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB8 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                        ////////////////else if (tflag == 9)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB9 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                        ////////////////else if (tflag == 10)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB10 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                        ////////////////else if (tflag == 11)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB11 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                        ////////////////else if (tflag == 12)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB12 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                        ////////////////else if (tflag == 13)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB13 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";
                        ////////////////else if (tflag == 14)
                        ////////////////    sql = "UPDATE " + TableName + " SET BB14 = \'\' WHERE USN = \'" + BBSUSNBox.Text + "\'";

                        ////////////////using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                        ////////////////{
                        ////////////////    connection.Open();
                        ////////////////    using (var command = new SQLiteCommand(sql, connection))
                        ////////////////    {
                        ////////////////        command.ExecuteNonQuery();
                        ////////////////        command.Dispose();
                        ////////////////    }
                        ////////////////    connection.Close();
                        ////////////////}
                        MessageBox.Show("BOOK RETURNED SUCESSFULLY");
                        
                //Book returned successfully, 




                // THE BELOW FUNCTIONALITY IS COMMENTED OUT BECAUSE, ORDERING IS NOT REALLY NECESSARY INSIDE THE DATABASE. WHILE DISPLAYING THE BOOK, ORDERING IS PERFORMED 
                // WITHOUT BOTHERING ABOUT THE ARANGEMENT OF THE THE BOOKS IN THE DATABASE.


                ////////////////////////////////now ordering all books
                //////////////////////////////        try
                //////////////////////////////        {
                //////////////////////////////            sql = "SELECT * FROM " + TableName + " WHERE USN = '" + BBSUSNBox.Text + "'";
                //////////////////////////////            using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                //////////////////////////////            {
                //////////////////////////////                connection.Open();
                //////////////////////////////                using (var command = new SQLiteCommand(sql, connection))
                //////////////////////////////                {
                //////////////////////////////                    using (var reader = command.ExecuteReader())
                //////////////////////////////                    {
                //////////////////////////////                        while (reader.Read())
                //////////////////////////////                        {
                //////////////////////////////                            array[1] = reader["BB1"].ToString();
                //////////////////////////////                            array[2] = reader["BB2"].ToString();
                //////////////////////////////                            array[3] = reader["BB3"].ToString();
                //////////////////////////////                            array[4] = reader["BB4"].ToString();
                //////////////////////////////                            array[5] = reader["BB5"].ToString();
                //////////////////////////////                            array[6] = reader["BB6"].ToString();
                //////////////////////////////                            array[7] = reader["BB7"].ToString();
                //////////////////////////////                            array[8] = reader["BB8"].ToString();
                //////////////////////////////                            array[9] = reader["BB9"].ToString();
                //////////////////////////////                            array[10] = reader["BB10"].ToString();
                //////////////////////////////                            array[11] = reader["BB11"].ToString();
                //////////////////////////////                            array[12] = reader["BB12"].ToString();
                //////////////////////////////                            array[13] = reader["BB13"].ToString();
                //////////////////////////////                            array[14] = reader["BB14"].ToString();
                //////////////////////////////                        }
                //////////////////////////////                        reader.Close();
                //////////////////////////////                    }
                //////////////////////////////                    command.Dispose();
                //////////////////////////////                }
                //////////////////////////////                connection.Close();
                //////////////////////////////            }// closing using
                //////////////////////////////        }
                //////////////////////////////        catch (Exception exp)
                //////////////////////////////        {
                //////////////////////////////            MessageBox.Show("Unexpected exception\n\n" + exp);
                //////////////////////////////        }
                //////////////////////////////        int j = 0, i = 0;
                //////////////////////////////        for (i = 1; i < 14; i++)
                //////////////////////////////        {
                //////////////////////////////            if (array[i] == "")
                //////////////////////////////            {
                //////////////////////////////                for (j = i; j < 14; j++)
                //////////////////////////////                    array[j] = array[j + 1];
                //////////////////////////////                array[14] = "";
                //////////////////////////////            }
                //////////////////////////////        }
                //////////////////////////////        SQLiteConnection con = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
                //////////////////////////////        sql = "UPDATE " + TableName + " SET BB1='" + array[1] + "',BB2='" + array[2] + "',BB3='" + array[3] + "',BB4='" + array[4] + "',BB5='" + array[5] + "',BB6='" + array[6] + "',BB7='" + array[7] + "',BB8='" + array[8] + "',BB9='" + array[9] + "',BB10='" + array[10] + "',BB11='" + array[11] + "',BB12='" + array[12] + "',BB13='" + array[13] + "',BB14='" + array[14] + "'  WHERE USN='" + BBSUSNBox.Text + "';";
                //////////////////////////////        con.Open();
                //////////////////////////////        SQLiteCommand cmd = new SQLiteCommand(sql, con);
                //////////////////////////////        cmd.ExecuteNonQuery();
                //////////////////////////////        con.Close();





                // NOW WE NEED RO REFRENS THE BOOKS LIST IN THE GUI. SO CALL STUDENT SEARCH FINCTION.
                BBSBSearchBtn_Click(sender, e);
                BBSSSearchBtn_Click(sender, e);

            
        }

        private void BBSTClearAllBtn_Click(object sender, EventArgs e)
        {
            BBSBookAccnoBox.Text = "";
            BBSUSNBox.Text = "";
            BBSTitleLabel.Text = "";
            BBSBorrowedByLabel.Text = "";
            BBSStudNameLabel.Text = "";
            BBSBookLabel1.Text = "";
            BBSBookLabel2.Text = "";
            BBSBookLabel3.Text = "";
            BBSBookLabel4.Text = "";
            BBSBookLabel5.Text = "";
            BBSBookLabel6.Text = "";
            BBSBookLabel7.Text = "";
            BBSBookLabel8.Text = "";
            BBSBookLabel9.Text = "";
            BBSBookLabel10.Text = "";
            BBSBookLabel11.Text = "";
            BBSBookLabel12.Text = "";
            BBSBookLabel13.Text = "";
            BBSBookLabel14.Text = "";
            BBSTSPBox.Image = null;
        }
        //------------------Book-Bank Scheme Transactions End-----------------//

        //---------------------Faculty Transactions Start---------------------//
        private void FacultyBookSearchBtn_Click(object sender, EventArgs e)
        {
            if (FacultyBookAccnoBox.Text == "")
            {
                MessageBox.Show("Please enter a book accno");
                FDGV.ClearSelection();//?
                FTPBox.Image = null;
            }
            else
            {
                try
                {
                    string sql;
                    TableName = FacultyNameBox.Text + FTBranchCBox.Text;

                    sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                    SQLiteConnection connection = new SQLiteConnection(sql);
                    SQLiteCommand command;
                    connection.Open();
                    sql = "Select Accno,FName from BOOKS;";
                    command = new SQLiteCommand(sql, connection);
                    SQLiteDataAdapter DA = new SQLiteDataAdapter(command);
                    DataSet DS = new DataSet();
                    try
                    {
                        DA.Fill(DS);
                        System.Data.DataTable DT = DS.Tables[0];
                        this.FacultyBooksDGV.DataSource = DT;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex);
                    }
                    command.Dispose();
                    connection.Close();
                }
                catch
                {
                    MessageBox.Show("Enter proper book");
                }
            }
        }

        private void FacultySearchBtn_Click(object sender, EventArgs e)
        {
            if (FacultyNameBox.Text == "")
            {
                MessageBox.Show("Please enter a faculty name");
                FDGV.ClearSelection();
                FTPBox.Image = null;
            }
            else
            {
                try
                {
                    string sql;
                    TableName = FacultyNameBox.Text + FTBranchCBox.Text;
                    sql = @"Data Source=FBooks_DB.sqlite;Version=3;New=True;Compress=True;";
                    SQLiteConnection connection = new SQLiteConnection(sql);
                    SQLiteCommand command;
                    connection.Open();
                    sql = "Select FBooks from " + TableName + ";";
                    command = new SQLiteCommand(sql, connection);
                    SQLiteDataAdapter DA = new SQLiteDataAdapter(command);
                    DataSet DS = new DataSet();
                    try
                    {
                        DA.Fill(DS);
                        System.Data.DataTable DT = DS.Tables[0];
                        this.FacultyBooksDGV.DataSource = DT;
                    }
                    catch
                    {
                        MessageBox.Show("No such faculty exists");
                    }
                    connection.Close();
                }
                catch
                {
                    MessageBox.Show("Enter proper name");
                }

                try
                {
                    string query = "SELECT * FROM FACULTIES WHERE FName='" + FacultyNameBox.Text + "';";
                    sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                    SQLiteConnection con = new SQLiteConnection(sql);
                    SQLiteCommand cmd = new SQLiteCommand(query, con);
                    con.Open();
                    IDataReader reader = cmd.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            byte[] F = (System.Byte[])reader["Fphoto"];
                            FTPBox.Image = ByteToImage(F);
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Image Error\n" + ex);
                    }
                    con.Close();
                }
                catch 
                {
                    MessageBox.Show("Error\n");
                }
            }
        }

        private void FacultyIssueBtn_Click(object sender, EventArgs e)
        {
            flag = false;
            if (FacultyBookAccnoBox.Text.Equals(""))
                MessageBox.Show("Please select a book");
            else if (FacultyNameBox.Text.Equals("") || FTBranchCBox.Text.Equals(""))
                MessageBox.Show("Please select a faculty and his/her branch");
            else
            {
                TableName = FacultyNameBox.Text + FTBranchCBox.Text;
                try
                {
                    sql = "SELECT * FROM BOOKS WHERE Accno = '" + FacultyBookAccnoBox.Text + "'";
                    using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(sql, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader["FName"].ToString().Equals("")&& reader["USN"].ToString().Equals(""))
                                        flag = true;
                                }
                                reader.Close();
                            }
                            command.Dispose();
                        }
                        connection.Close();
                    }
                    if (flag == false)
                    {
                        MessageBox.Show("BOOK IS ALREADY ISSUED.\n PLEASE RETURN IT BEFORE ISSUING TO ANOTHER FACULTY");
                        return;
                    }
                    flag = false;
                    sql = "UPDATE BOOKS SET FName = \'" + FacultyNameBox.Text + "\' WHERE Accno = \'" + FacultyBookAccnoBox.Text + "\'";
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
                    sql = "INSERT INTO " + TableName + " VALUES ('" + FacultyBookAccnoBox.Text + "');";
                    using (var connection = new SQLiteConnection("Data Source=FBooks_DB.sqlite;Version=3;New=True;Compress=True;"))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(sql, connection))
                        {
                            command.ExecuteNonQuery();
                            command.Dispose();
                        }
                        connection.Close();
                    }
                    MessageBox.Show("BOOK ISSUED SUCESSFULLY");
                }
                catch
                {
                    MessageBox.Show("Error\n");
                }
            }
        }

        private void FacultyReturnBtn_Click(object sender, EventArgs e)
        {
            flag = false;
            int tflag=0;
            if (FacultyBookAccnoBox.Text.Equals(""))
                MessageBox.Show("Please select a book");
            else if (FacultyNameBox.Text.Equals("") || FTBranchCBox.Text.Equals(""))
                MessageBox.Show("Please select a faculty and his/her branch");
            else
            {
                try
                {
                    TableName = FacultyNameBox.Text + FTBranchCBox.Text;
                    sql = "SELECT * FROM BOOKS WHERE Accno = '" + FacultyBookAccnoBox.Text + "'";
                    using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(sql, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader["FName"].ToString().Equals("")&& reader["USN"].ToString().Equals(""))
                                        flag = true;
                                }
                                reader.Close();
                            }
                            command.Dispose();
                        }
                        connection.Close();
                    }
                    if (flag == true)
                    {
                        MessageBox.Show("BOOK IS NOT ISSUED TO ANYONE");
                        return;
                    }
                    sql = "SELECT * FROM " + TableName + ";";
                    using (var connection = new SQLiteConnection("Data Source=FBooks_DB.sqlite;Version=3;New=True;Compress=True;"))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(sql, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader["FBooks"].ToString().Equals(FacultyBookAccnoBox.Text))
                                    {
                                        flag = true;
                                        tflag = 1;
                                        break;
                                    }
                                    else if (reader["FBooks"].ToString().Equals(""))
                                    {
                                        tflag = 2;
                                        break;
                                    }
                                    
                                }
                                reader.Close();
                            }
                            command.Dispose();
                        }
                        connection.Close();
                    }
                    if (tflag == 15)
                    {
                        MessageBox.Show("FACULTY HAS NOT BORROWED ANY BOOK");
                        return;
                    }

                    if (tflag == 0)
                    {
                        MessageBox.Show(" THIS FACULTY HAS NOT BORROWED THE ENTERED BOOK");
                        return;
                    }
                    sql = "UPDATE BOOKS SET FName = \'\' WHERE Accno = \'" + FacultyBookAccnoBox.Text + "\'";
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
                    if (tflag == 1)
                    {
                        sql = "DELETE FROM " + TableName + " WHERE FBooks = \'" + FacultyBookAccnoBox.Text + "\'";
                        using (var connection = new SQLiteConnection("Data Source=FBooks_DB.sqlite;Version=3;New=True;Compress=True;"))
                        {
                            connection.Open();
                            using (var command = new SQLiteCommand(sql, connection))
                            {
                                command.ExecuteNonQuery();
                                command.Dispose();
                            }
                            connection.Close();
                        }                      
                        MessageBox.Show("BOOK RETURNED SUCESSFULLY");
                    }                       
                }
                catch
                {
                    MessageBox.Show("Error\n");
                }
            }
        }

        private void FCLearBtn_Click(object sender, EventArgs e)
        {
            FacultyBookAccnoBox.Text = "";
            FacultyNameBox.Text = "";
            FacultyBooksDGV.DataSource = null;
            FTPBox.Image = null;
        }

        private void FacultyBookAccnoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FacultyBookSearchBtn_Click(sender, e);
            }
        }

        private void FacultyNameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FacultySearchBtn_Click(sender, e);
            }
        }
        //---------------------Faculty Transactions End-----------------------//

        //---------------------------Transaction Panel Functions End---------------------//

        //------------------------------Books Panel Functions Start----------------------//
        private void BooksBtn_Click(object sender, EventArgs e)
        {
            this.Sound();
            this.PanelNameLabel.Text = "BOOKS";
            this.BooksPanel.Dock = DockStyle.Fill;
            this.BooksBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.HomeBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.TransBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BorrowersBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ToolsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SetBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.AboutBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.NotificationsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.ForeColor = Color.White;
            this.TransBtn.ForeColor = Color.White;
            this.BooksBtn.ForeColor = Color.Black;
            this.BorrowersBtn.ForeColor = Color.White;
            this.ToolsBtn.ForeColor = Color.White;
            this.SetBtn.ForeColor = Color.White;
            this.AboutBtn.ForeColor = Color.White;
            this.NotificationsBtn.ForeColor = Color.White;
            this.BHomeBtn.BackColor = Color.Transparent;
            this.BTransBtn.BackColor = Color.Transparent;
            this.BBooksBtn.BackColor = Color.Transparent;
            this.BBorrowersBtn.BackColor = Color.Transparent;
            this.HomePanel.Hide();
            this.TransPanel.Hide();
            this.BorrowersPanel.Hide();
            this.ToolsPanel.Hide();
            this.SetPanel.Hide();
            this.AboutPanel.Hide();
            this.NotificationPanel.Hide();
            this.HomePBox.BackColor = Color.Transparent;
            this.TransPBox.BackColor = Color.Transparent;
            this.BooksPBox.BackColor = Color.Navy;
            this.BorrowersPBox.BackColor = Color.Transparent;
            this.ToolsPBox.BackColor = Color.Transparent;
            this.SetPBox.BackColor = Color.Transparent;
            this.AboutPBox.BackColor = Color.Transparent;
            this.NotificationsPBox.BackColor = Color.Transparent;
            this.BooksPanel.Show();
        }

        //----------------------------Search Book Start-------------------//
        private void BookAccnoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchBtn_Click(sender, e);
            }
        }

        private void ClearAll1Btn_Click(object sender, EventArgs e)
        {
           AccnoBox.ReadOnly = TitleBox.ReadOnly = AuthorBox.ReadOnly =
           PublisherBox.ReadOnly = PriceBox.ReadOnly = SemesterBox.ReadOnly =
           EditionBox.ReadOnly = ClassificationNoBox.ReadOnly = false;
            this.BookAccnoBox.Clear();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (BookAccnoBox.Text == "")
                MessageBox.Show("Please enter a book accno");
            else
            try
            {
                AccnoBox.ReadOnly = TitleBox.ReadOnly = AuthorBox.ReadOnly =
                PublisherBox.ReadOnly = PriceBox.ReadOnly = SemesterBox.ReadOnly =
                EditionBox.ReadOnly = ClassificationNoBox.ReadOnly = true;
                if (BookAccnoBox.Text.Equals(null))//only AccNo
                {
                    MessageBox.Show("Please enter an Accno");
                    return;
                }
                sql = "SELECT * FROM Books WHERE Accno = '" + BookAccnoBox.Text + "';";
                using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AccnoBox.Text = reader["Accno"].ToString();
                                TitleBox.Text = reader["Title"].ToString(); ;
                                AuthorBox.Text = reader["Author"].ToString();
                                PublisherBox.Text = reader["Publisher"].ToString();
                                PriceBox.Text = reader["Price"].ToString();
                                SemesterBox.Text = reader["Semester"].ToString();
                                BranchCBox.Text = reader["Branch"].ToString();
                                EditionBox.Text = reader["Edition"].ToString();
                                ClassificationNoBox.Text = reader["Classification_no"].ToString();
                            }
                            reader.Close();
                        }
                        command.Dispose();
                    }
                    connection.Close();
                }// closing using
            }
            catch
            {
                MessageBox.Show("Unknown exception:\n\n");
            }
        }
        //----------------------------Search Book End----------------------//

        //----------------------------Add Book Start----------------------//
        private void AccnoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.TitleBox.Focus();
            }
        }

        private void TitleBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.AuthorBox.Focus();
            }
        }

        private void AuthorBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.PublisherBox.Focus();
            }
        }

        private void PublisherBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.PriceBox.Focus();
            }
        }

        private void PriceBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SemesterBox.Focus();
            }
        }

        private void SemesterBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.BranchCBox.Focus();
            }
        }

        private void BranchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.EditionBox.Focus();
            }
        }

        private void EditionBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ClassificationNoBox.Focus();
            }
        }

        private void ClassificationNoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.AddBookBtn.Focus();
            }
        }

        private void AddBookBtn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.UpdateBookListBtn.Focus();
            }
        }

        private void ClearAll2Btn_Click(object sender, EventArgs e)
        {
            AccnoBox.ReadOnly = TitleBox.ReadOnly = AuthorBox.ReadOnly =
            PublisherBox.ReadOnly = PriceBox.ReadOnly = SemesterBox.ReadOnly =
            EditionBox.ReadOnly = ClassificationNoBox.ReadOnly = false;
            this.AccnoBox.Clear();
            this.TitleBox.Clear();
            this.AuthorBox.Clear();
            this.PublisherBox.Clear();
            this.PriceBox.Clear();
            this.SemesterBox.Clear();
            this.EditionBox.Clear();
            this.ClassificationNoBox.Clear();
        }

        private void AddBookBtn_Click(object sender, EventArgs e)
        {
            if (AccnoBox.Text == "")
                MessageBox.Show("Please enter a book accno");
            else
            try
            {
                SQLiteConnection connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
                SQLiteCommand command;
                string sql;
                if (AccnoBox.Text == null)
                {
                    MessageBox.Show("Please Enter an ACCNO");                   
                }
                else
                {
                    connection.Open();
                    sql = "INSERT INTO BOOKS (Accno,Title,Author,Publisher,Price,Semester,Branch,Edition,Classification_no) VALUES ('" + AccnoBox.Text + "','" + TitleBox.Text + "','" + AuthorBox.Text + "','" + PublisherBox.Text + "','" + PriceBox.Text + "','" + SemesterBox.Text + "','" + BranchCBox.Text + "','" + EditionBox.Text + "','" + ClassificationNoBox.Text + "');";
                    command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Book Added Successfully");
                }               
            }
            catch
            {             
                MessageBox.Show("Book Already Exists");
            }
        }

        private void UpdateBookListBtn_Click(object sender, EventArgs e)
        {
            if (AccnoBox.Text == "")
                MessageBox.Show("Please enter a book accno");
            else
            try
            {
                SQLiteConnection connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
                SQLiteCommand command;
                string sql;
                if (AccnoBox.Text == null)
                {
                    MessageBox.Show("Please Enter an ACCNO");
                }
                else
                {
                    connection.Open();
                    sql = "UPDATE BOOKS SET Title='" + TitleBox.Text + "',Author='" + AuthorBox.Text + "',Publisher='" + PublisherBox.Text + "',Price='" + PriceBox.Text + "',Semester='" + SemesterBox.Text + "',Branch='" + BranchCBox.Text + "',Edition='" + EditionBox.Text + "',Classification_no='" + ClassificationNoBox.Text + "'  WHERE Accno='" + AccnoBox.Text+"';";
                    command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Book details updated successfully");
                }
            }
            catch
            {
                MessageBox.Show("Failed to update book details");
            }
        }
        //----------------------------Add Book End----------------------//

        //------------------------------Books Panel Functions End------------------------//

        //---------------------------Borrowers Panel Functions Start---------------------//
        private void BorrowersBtn_Click(object sender, EventArgs e)
        {
            this.Sound();
            this.PanelNameLabel.Text = "BORROWERS";
            this.BorrowersPanel.Dock = DockStyle.Fill;
            this.BorrowersBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.HomeBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BooksBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.TransBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ToolsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SetBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.AboutBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.NotificationsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.ForeColor = Color.White;
            this.TransBtn.ForeColor = Color.White;
            this.BooksBtn.ForeColor = Color.White;
            this.BorrowersBtn.ForeColor = Color.Black;
            this.ToolsBtn.ForeColor = Color.White;
            this.SetBtn.ForeColor = Color.White;
            this.AboutBtn.ForeColor = Color.White;
            this.BHomeBtn.BackColor = Color.Transparent;
            this.BTransBtn.BackColor = Color.Transparent;
            this.BBooksBtn.BackColor = Color.Transparent;
            this.BBorrowersBtn.BackColor = Color.Transparent;
            this.HomePanel.Hide();
            this.TransPanel.Hide();
            this.BooksPanel.Hide();
            this.ToolsPanel.Hide();
            this.SetPanel.Hide();
            this.AboutPanel.Hide();
            this.NotificationPanel.Hide();
            this.HomePBox.BackColor = Color.Transparent;
            this.TransPBox.BackColor = Color.Transparent;
            this.BooksPBox.BackColor = Color.Transparent;
            this.BorrowersPBox.BackColor = Color.Navy;
            this.ToolsPBox.BackColor = Color.Transparent;
            this.SetPBox.BackColor = Color.Transparent;
            this.AboutPBox.BackColor = Color.Transparent;
            this.NotificationsPBox.BackColor = Color.Transparent;
            this.SearchStudBtn_Click(sender, e);
            this.AddUSNBox.Text.ToUpper();
            this.SUSNBox.Text.ToUpper();
            this.BorrowersPanel.Show();
        }

        private void SearchStudBtn_Click(object sender, EventArgs e)
        {
            this.SearchStudBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.SearchFacultyBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ARSFBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SearchStudBtn.ForeColor = Color.Black;
            this.SearchFacultyBtn.ForeColor = Color.White;
            this.ARSFBtn.ForeColor = Color.White;
            this.StudentsPanel.Dock = DockStyle.Fill;
            this.FacultyPanel.Hide();
            this.StudentFacultyAddPanel.Hide();
            this.StudentsPanel.Show();
        }

        private void SearchFacultyBtn_Click(object sender, EventArgs e)
        {
            this.SearchStudBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SearchFacultyBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.ARSFBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SearchStudBtn.ForeColor = Color.White;
            this.SearchFacultyBtn.ForeColor = Color.Black;
            this.ARSFBtn.ForeColor = Color.White;
            this.FacultyPanel.Dock = DockStyle.Fill;
            this.StudentsPanel.Hide();
            this.StudentFacultyAddPanel.Hide();
            this.FacultyPanel.Show();
        }

        private void ARSFBtn_Click(object sender, EventArgs e)
        {
            this.SearchStudBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SearchFacultyBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ARSFBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.SearchStudBtn.ForeColor = Color.White;
            this.SearchFacultyBtn.ForeColor = Color.White;
            this.ARSFBtn.ForeColor = Color.Black;
            this.StudentFacultyAddPanel.Dock = DockStyle.Fill;
            this.StudentsPanel.Hide();
            this.FacultyPanel.Hide();
            this.StudentFacultyAddPanel.Show();
        }




        //------------------Students Start----------------//
        //-------------Search Student Start-----------//
        private void Search1Btn_Click(object sender, EventArgs e)
        {
            if (SUSNBox.Text.Equals(""))
            {
                MessageBox.Show("Please select a student");
                SearchStudentNameLabel.Text = "";
                StudentPBox.Image = null;
                BorrowerBookLabel1.Text = "";
                BorrowerBookLabel2.Text = "";
                BorrowerBookLabel3.Text = "";
                BorrowerBookLabel4.Text = "";
                BorrowerBookLabel5.Text = "";
                BorrowerBookLabel6.Text = "";
                BorrowerBookLabel7.Text = "";
                BorrowerBookLabel8.Text = "";
                BorrowerBookLabel9.Text = "";
                BorrowerBookLabel10.Text = "";
                BorrowerBookLabel11.Text = "";
                BorrowerBookLabel12.Text = "";
                BorrowerBookLabel13.Text = "";
                BorrowerBookLabel14.Text = "";
                RBook1Label.Text = "";
                RBook2Label.Text = "";
            }          
            else
            {
                string temp = SUSNBox.Text.Substring(0, 7);
                string temp1 = SUSNBox.Text;
                TableName = "STUDENTS_" + temp;
                try
                {
                    sql = "SELECT * FROM " + TableName + " WHERE USN = '" + BBSUSNBox.Text + "'";
                    using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(sql, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    array[1] = reader["BB1"].ToString();
                                    array[2] = reader["BB2"].ToString();
                                    array[3] = reader["BB3"].ToString();
                                    array[4] = reader["BB4"].ToString();
                                    array[5] = reader["BB5"].ToString();
                                    array[6] = reader["BB6"].ToString();
                                    array[7] = reader["BB7"].ToString();
                                    array[8] = reader["BB8"].ToString();
                                    array[9] = reader["BB9"].ToString();
                                    array[10] = reader["BB10"].ToString();
                                    array[11] = reader["BB11"].ToString();
                                    array[12] = reader["BB12"].ToString();
                                    array[13] = reader["BB13"].ToString();
                                    array[14] = reader["BB14"].ToString();
                                }
                                reader.Close();
                            }
                            command.Dispose();
                        }
                        connection.Close();
                    }// closing using
                }
                catch
                {
                    MessageBox.Show("Unexpected exception in search student");
                }
                int j = 0, i = 0;
                for (i = 1; i < 14; i++)
                {
                    if (array[i] == "")
                    {
                        for (j = i; j < 14; j++)
                            array[j] = array[j + 1];
                        array[14] = "";
                    }
                }
                if (!SUSNBox.Text.Equals(""))
                {
                    try
                    {
                        sql = "SELECT * FROM " + TableName + " WHERE USN = '" + SUSNBox.Text + "'";
                        using (var connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                        {
                            connection.Open();
                            using (var command = new SQLiteCommand(sql, connection))
                            {
                                using (var reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        SearchStudentNameLabel.Text = reader["SName"].ToString();
                                        BorrowerBookLabel1.Text = array[1];
                                        BorrowerBookLabel2.Text = array[2];
                                        BorrowerBookLabel3.Text = array[3];
                                        BorrowerBookLabel4.Text = array[4];
                                        BorrowerBookLabel5.Text = array[5];
                                        BorrowerBookLabel6.Text = array[6];
                                        BorrowerBookLabel7.Text = array[7];
                                        BorrowerBookLabel8.Text = array[8];
                                        BorrowerBookLabel9.Text = array[9];
                                        BorrowerBookLabel10.Text = array[10];
                                        BorrowerBookLabel11.Text = array[11];
                                        BorrowerBookLabel12.Text = array[12];
                                        BorrowerBookLabel13.Text = array[13];
                                        BorrowerBookLabel14.Text = array[14];
                                        RBook1Label.Text = reader["B1"].ToString();
                                        RBook2Label.Text = reader["B2"].ToString();
                                    }
                                    reader.Close();
                                }
                                command.Dispose();
                            }
                            connection.Close();
                        }// closing using
                    }
                    catch
                    {
                        MessageBox.Show("Unexpected exception in search student");
                    }
                    try
                    {
                        string query = "SELECT * FROM " + TableName + " WHERE USN='" + SUSNBox.Text + "';";
                        sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                        SQLiteConnection con = new SQLiteConnection(sql);
                        SQLiteCommand cmd = new SQLiteCommand(query, con);
                        con.Open();
                        IDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                byte[] F = (System.Byte[])reader["Sphoto"];
                                StudentPBox.Image = ByteToImage(F);
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Image Error\n" + ex);
                        }
                        con.Close();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error\n" + exc);
                    }
                }
            }  
        }

        private void SSClearBtn_Click(object sender, EventArgs e)
        {
            SUSNBox.Text = "";
            SearchStudentNameLabel.Text = "";
            StudentPBox.Image = null;
            BorrowerBookLabel1.Text = "";
            BorrowerBookLabel2.Text = "";
            BorrowerBookLabel3.Text = "";
            BorrowerBookLabel4.Text = "";
            BorrowerBookLabel5.Text = "";
            BorrowerBookLabel6.Text = "";
            BorrowerBookLabel7.Text = "";
            BorrowerBookLabel8.Text = "";
            BorrowerBookLabel9.Text = "";
            BorrowerBookLabel10.Text = "";
            BorrowerBookLabel11.Text = "";
            BorrowerBookLabel12.Text = "";
            BorrowerBookLabel13.Text = "";
            BorrowerBookLabel14.Text = "";
            RBook1Label.Text = "";
            RBook2Label.Text = "";
        }
        //--------------Search Student End-----------//
        //--------------Add Student Start------------//
        private void AddStudentBtn_Click(object sender, EventArgs e)
        {
            if (AddUSNBox.Text.Equals(""))
                MessageBox.Show("Please select a student");
            else if(AddStudentPBox.Image==null)
                MessageBox.Show("Please select an image");
            else
            {
                string temp =AddUSNBox.Text.Substring(0, 7);
                TableName = "STUDENTS_" + temp;
                Image photo = new Bitmap(@path);
                pic = ImageToByte(photo, ImageFormat.Jpeg);
                string sql = @"Data Source = BOOKMARK_DB.sqlite; Version = 3;";
                SQLiteConnection con = new SQLiteConnection(sql);
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = String.Format("INSERT INTO " + TableName + " (USN,SName,Sphno,Semester,Sphoto) VALUES (@0,@1,@2,@3,@4);");
                SQLiteParameter param1 = new SQLiteParameter("@0", DbType.String);
                SQLiteParameter param2 = new SQLiteParameter("@1", DbType.String);
                SQLiteParameter param3 = new SQLiteParameter("@2", DbType.String);
                SQLiteParameter param4 = new SQLiteParameter("@3", DbType.String);
                SQLiteParameter param5 = new SQLiteParameter("@4", DbType.Binary);
                param1.Value = AddUSNBox.Text;
                cmd.Parameters.Add(param1);
                param2.Value = AddStudNameBox.Text;
                cmd.Parameters.Add(param2);
                param3.Value = AddStudContactBox.Text;
                cmd.Parameters.Add(param3);
                param4.Value = AddStudSemCBox.Text;
                cmd.Parameters.Add(param4);
                param5.Value = pic;
                cmd.Parameters.Add(param5);
                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Added Successfully");
                }
                catch
                {
                    MessageBox.Show("Student Details Already Exists");
                }
                con.Close();
            }
        }  

        private void StudUpdateBtn_Click(object sender, EventArgs e)
        {
            if (AddUSNBox.Text.Equals(""))
                MessageBox.Show("Please enter a student USN");
            else if (AddStudentPBox.Image == null)
                MessageBox.Show("Please select an image");
            else
                try
                {
                    string temp = AddUSNBox.Text.Substring(0, 7);
                    TableName = "STUDENTS_" + temp;
                    SQLiteConnection connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
                    SQLiteCommand command;
                    string sql;
                    string query = "DELETE  FROM " + TableName + " WHERE USN='"+AddUSNBox.Text+"';";
                    sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                    connection = new SQLiteConnection(sql);
                    command = new SQLiteCommand(query, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    Image photo = new Bitmap(@path);
                    pic = ImageToByte(photo, ImageFormat.Jpeg);
                    sql = @"Data Source = BOOKMARK_DB.sqlite; Version = 3;";
                    SQLiteConnection con = new SQLiteConnection(sql);
                    SQLiteCommand cmd = con.CreateCommand();
                    cmd.CommandText = String.Format("INSERT INTO " + TableName + " (USN,SName,Sphno,Semester,Sphoto) VALUES (@0,@1,@2,@3,@4);");
                    SQLiteParameter param1 = new SQLiteParameter("@0", DbType.String);
                    SQLiteParameter param2 = new SQLiteParameter("@1", DbType.String);
                    SQLiteParameter param3 = new SQLiteParameter("@2", DbType.String);
                    SQLiteParameter param4 = new SQLiteParameter("@3", DbType.String);
                    SQLiteParameter param5 = new SQLiteParameter("@4", DbType.Binary);
                    param1.Value = AddUSNBox.Text;
                    cmd.Parameters.Add(param1);
                    param2.Value = AddStudNameBox.Text;
                    cmd.Parameters.Add(param2);
                    param3.Value = AddStudContactBox.Text;
                    cmd.Parameters.Add(param3);
                    param4.Value = AddStudSemCBox.Text;
                    cmd.Parameters.Add(param4);
                    param5.Value = pic;
                    cmd.Parameters.Add(param5);
                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Student details updated successfully");
                    }
                    catch
                    {
                        MessageBox.Show("Student details already exists");
                    }
                    con.Close(); 
                }
                catch
                {
                    MessageBox.Show("Failed to update student details");
                }  
        }

        public void BrowseImage(object sender, EventArgs e)
        {
            this.OpenFileDialog.Multiselect = false;
            this.OpenFileDialog.Title = "Select a Student's Photo";
            DialogResult DR = this.OpenFileDialog.ShowDialog();
            if (DR == DialogResult.OK)
            {
                path = OpenFileDialog.FileName;
                AddStudentPBox.Image = new Bitmap(@path);
            }
        }

        private void SUSNBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Search1Btn.Focus();
            }
        }

        private void BASClearBtn_Click(object sender, EventArgs e)
        {
            AddStudNameBox.Text = "";
            AddUSNBox.Text = "";
            AddStudSemCBox.Text = "";
            AddStudContactBox.Text = "";
            AddStudentPBox.Image = null;
        }

        
        //---------------Add Student End-------------//
        //------------------Students End------------------//

        //-----------------Faculties Start----------------//
        //-------------Search Faculty Start-----------//
        private void FacultyNameSearchBtn_Click(object sender, EventArgs e)
        {
            if (SearchFacultyNameBox.Text == "")
            {
                MessageBox.Show("Please Enter a Faculty Name");
                FDGV.ClearSelection();
                SearchFacultyPhotoBox.Image = null;
            }
            else
            {
                try
                {
                    string sql;
                    TableName = SearchFacultyNameBox.Text + SearchFacultyBranchCBox.Text;
                    sql = @"Data Source=FBooks_DB.sqlite;Version=3;New=True;Compress=True;";
                    SQLiteConnection connection = new SQLiteConnection(sql);
                    SQLiteCommand command;
                    connection.Open();
                    sql = "Select FBooks from " + TableName + " ;";
                    command = new SQLiteCommand(sql, connection);
                    SQLiteDataAdapter DA = new SQLiteDataAdapter(command);
                    DataSet DS = new DataSet();
                    try
                    {
                        DA.Fill(DS);
                        System.Data.DataTable DT = DS.Tables[0];
                        this.FDGV.DataSource = DT;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex);
                    }
                    connection.Close();
                }
                catch
                {
                    MessageBox.Show("Enter Proper Name");
                }

                try
                {
                    string query = "SELECT * FROM FACULTIES WHERE FName='" + SearchFacultyNameBox.Text + "';";
                    sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                    SQLiteConnection con = new SQLiteConnection(sql);
                    SQLiteCommand cmd = new SQLiteCommand(query, con);
                    con.Open();
                    IDataReader reader = cmd.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            byte[] F = (System.Byte[])reader["Fphoto"];
                            SearchFacultyPhotoBox.Image = ByteToImage(F);
                        }
                        reader.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Image Error\n");
                    }
                    con.Close();
                }
                catch
                {
                    MessageBox.Show("Error\n");
                }
            }          
        }

        private void SearchFacultyNameBox_KeyDown(object sender, KeyEventArgs e)
        {
                if (e.KeyCode == Keys.Enter)
                {
                    this.FacultyNameSearchBtn.Focus();
                }
        }

        private void FacultyNameSearchBtn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SearchFacultyNameBox.Focus();
            }
        }

        private void SFClearBtn_Click(object sender, EventArgs e)
        {
            SearchFacultyNameBox.Text = "";
            FDGV.DataSource = null;
            FDGV.Refresh();
            SearchFacultyPhotoBox.Image = null;
        }

        //-------------Search Faculty End-------------//
        //----------------Add Faculty Start-----------//
        private void AddFacultyBtn_Click(object sender, EventArgs e)
        {
            if(AddFacultyNameBox.Text==""||AddFacBranchCBox.Text=="")
            {
                MessageBox.Show("Please select a faculty and branch");
            }
            else if (AddFacultyPBox.Image == null)
            {
                MessageBox.Show("Please select an image");
            }
            else
            {
                try
                {
                    Image photo = new Bitmap(@path);
                    pic = ImageToByte(photo, ImageFormat.Jpeg);
                    string sql = @"Data Source = BOOKMARK_DB.sqlite; Version = 3;";
                    SQLiteConnection con = new SQLiteConnection(sql);
                    SQLiteCommand cmd = con.CreateCommand();
                    cmd.CommandText = String.Format("INSERT INTO FACULTIES (FName,Fphno,FBranch,Fphoto) VALUES (@0,@1,@2,@3);");
                    SQLiteParameter param1 = new SQLiteParameter("@0", DbType.String);
                    SQLiteParameter param2 = new SQLiteParameter("@1", DbType.String);
                    SQLiteParameter param3 = new SQLiteParameter("@2", DbType.String);
                    SQLiteParameter param4 = new SQLiteParameter("@3", DbType.Binary);
                    param1.Value = AddFacultyNameBox.Text;
                    cmd.Parameters.Add(param1);
                    param2.Value = AddFacultyContactBox.Text;
                    cmd.Parameters.Add(param2);
                    param3.Value = AddFacBranchCBox.Text;
                    cmd.Parameters.Add(param3);
                    param4.Value = pic;
                    cmd.Parameters.Add(param4);
                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("Error creating Faculty books Table");
                    }
                    con.Close();
                    try
                    {
                        TableName = AddFacultyNameBox.Text + AddFacBranchCBox.Text;
                        sql = @"Data Source = FBooks_DB.sqlite; Version = 3;";
                        con = new SQLiteConnection(sql);
                        cmd = con.CreateCommand();
                        cmd.CommandText = String.Format("f TABLE " + TableName + " (FBooks VARCHAR(10) PRIMARY KEY UNIQUE);");
                        con.Open();
                        try
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Faculty Added Successfully");
                        }
                        catch
                        {
                            MessageBox.Show("Faculty already exists\n");
                        }
                        con.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Error creating Faculty books Table");
                    }
                }

                catch
                {
                    MessageBox.Show("Error\n");
                }
            }
        }

        public void BrowsePic(object sender, EventArgs e)
        {
            this.OpenFileDialog.Multiselect = false;
            this.OpenFileDialog.Title = "Select a Faculty's Photo";
            DialogResult DR = this.OpenFileDialog.ShowDialog();
            if (DR == DialogResult.OK)
            {
                path = OpenFileDialog.FileName;
                AddFacultyPBox.Image = new Bitmap(@path);
            }
        }

        private void RemoveFacultyBtn_Click(object sender, EventArgs e)
        {
            if (AddFacultyNameBox.Text == "" || AddFacBranchCBox.Text == "")
            {
                MessageBox.Show("Please select a faculty and branch");
            }
            else
            {
                TableName = AddFacultyNameBox.Text + AddFacBranchCBox.Text;
                sql = @"Data Source=FBooks_DB.sqlite;Version=3;New=True;Compress=True;";
                SQLiteConnection con = new SQLiteConnection(sql);
                SQLiteCommand com;
                con.Open();
                sql = "DROP TABLE " + TableName + " ;";
                com = new SQLiteCommand(sql, con);
                try
                {
                    com.ExecuteNonQuery();
                    MessageBox.Show("Faculty removed");
                }
                catch
                {
                    MessageBox.Show("Error removing faculty details\n");
                }
                con.Close();
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;"))
                {
                    SQLiteCommand command;
                    sql = "DELETE FROM FACULTIES WHERE FName='" + AddFacultyNameBox.Text + "';";
                    command = new SQLiteCommand(sql, conn);
                    conn.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Faculty removed");
                    }
                    catch
                    {
                        MessageBox.Show("Error removing faculty details\n");
                    }
                    conn.Close();
                }
            }           
        }

        private void FacultyUpdateListBtn_Click(object sender, EventArgs e)
        {
            /*here we have 2 steps. firstly delete the faculty and then update the faculty
             * 
             * 
             * 
             * */
            if (AddFacultyNameBox.Text == "" || AddFacBranchCBox.Text == "")
            {
                MessageBox.Show("Please select a faculty and branch");
            }
            else if (AddFacultyPBox.Image == null)
            {
                MessageBox.Show("Please select an image");
            }
            else
            {
                try
                {
                    SQLiteConnection connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
                    SQLiteCommand command;
                    //1.delete faculty
                    string query = "DELETE  FROM FACULTIES WHERE FName='" + AddFacultyNameBox.Text + "';";
                    
                    connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
                    command = new SQLiteCommand(query, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    //2. create faculty
                    connection.Open();
                    Image photo = new Bitmap(@path);
                    pic = ImageToByte(photo, ImageFormat.Jpeg);
                    SQLiteConnection con = new SQLiteConnection("Data Source = BOOKMARK_DB.sqlite; Version = 3;");
                    SQLiteCommand cmd = con.CreateCommand();
                    cmd.CommandText = String.Format("INSERT INTO FACULTIES (FName,Fphno,FBranch,Fphoto) VALUES (@0,@1,@2,@3);");
                    SQLiteParameter param1 = new SQLiteParameter("@0", DbType.String);
                    SQLiteParameter param2 = new SQLiteParameter("@1", DbType.String);
                    SQLiteParameter param3 = new SQLiteParameter("@2", DbType.String);
                    SQLiteParameter param4 = new SQLiteParameter("@3", DbType.Binary);
                    param1.Value = AddFacultyNameBox.Text;
                    cmd.Parameters.Add(param1);
                    param2.Value = AddFacultyContactBox.Text;
                    cmd.Parameters.Add(param2);
                    param3.Value = AddFacBranchCBox.Text;
                    cmd.Parameters.Add(param3);
                    param4.Value = pic;
                    cmd.Parameters.Add(param4);
                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("Failed to update faculty details");
                    }
                    con.Close();
                    MessageBox.Show("Faculty details updated successfully");   
                }
                catch
                {
                    MessageBox.Show("Failed to update faculty details\n");
                }
            }              
        }

        private void BAFClearBtn_Click(object sender, EventArgs e)
        {
            AddFacultyNameBox.Text = "";
            AddFacultyContactBox.Text = "";
            AddFacultyPBox.Image = null;
        }
        //----------------Add Faculty End-------------//
        //-----------------Faculties End------------------//

        //----------------Byte to Image and Image to Byte Functions start-----------//
        public byte[] ImageToByte(Image photo, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                photo.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                return imageBytes;
            }
        }

        public Image ByteToImage(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = new Bitmap(ms);
            return image;
        }
        //----------------Byte to Image and Image to Byte Functions end-------------//

        //------------------Borrowers Panel Functions End-----------------------//

        //---------------------Tools Panel Functions Start---------------------//
        private void ToolsBtn_Click(object sender, EventArgs e)
        {
            this.Sound();
            this.PanelNameLabel.Text = "TOOLS";
            this.ToolsPanel.Dock = DockStyle.Fill;
            this.ToolsBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.HomeBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BooksBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BorrowersBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.TransBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SetBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.AboutBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.NotificationsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.ForeColor = Color.White;
            this.TransBtn.ForeColor = Color.White;
            this.BooksBtn.ForeColor = Color.White;
            this.BorrowersBtn.ForeColor = Color.White;
            this.ToolsBtn.ForeColor = Color.Black;
            this.SetBtn.ForeColor = Color.White;
            this.AboutBtn.ForeColor = Color.White;
            this.NotificationsBtn.ForeColor = Color.White;
            this.BHomeBtn.BackColor = Color.Transparent;
            this.BTransBtn.BackColor = Color.Transparent;
            this.BBooksBtn.BackColor = Color.Transparent;
            this.BBorrowersBtn.BackColor = Color.Transparent;
            this.HomePanel.Hide();
            this.TransPanel.Hide();
            this.BooksPanel.Hide();
            this.BorrowersPanel.Hide();
            this.SetPanel.Hide();
            this.AboutPanel.Hide();
            this.NotificationPanel.Hide();
            this.HomePBox.BackColor = Color.Transparent;
            this.TransPBox.BackColor = Color.Transparent;
            this.BooksPBox.BackColor = Color.Transparent;
            this.BorrowersPBox.BackColor = Color.Transparent;
            this.ToolsPBox.BackColor = Color.Navy;
            this.SetPBox.BackColor = Color.Transparent;
            this.AboutPBox.BackColor = Color.Transparent;
            this.NotificationsPBox.BackColor = Color.Transparent;
            this.CreateTablesBtn_Click(sender, e);
            this.ToolsPanel.Show();
        }

        private void CreateTablesBtn_Click(object sender, EventArgs e)
        {
            this.CreateTablesBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.ImportExportBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.GetDetailsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.CreateTablesBtn.ForeColor = Color.Black;
            this.ImportExportBtn.ForeColor = Color.White;
            this.GetDetailsBtn.ForeColor = Color.White;
            this.CreateTablePanel.Dock = DockStyle.Fill;
            this.ImportExportPanel.Hide();
            this.GetDetailsPdfPanel.Hide();
            this.CreateTablePanel.Show();
        }

        private void ImportExportBtn_Click(object sender, EventArgs e)
        {
            this.CreateTablesBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ImportExportBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.GetDetailsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.CreateTablesBtn.ForeColor = Color.White;
            this.ImportExportBtn.ForeColor = Color.Black;
            this.GetDetailsBtn.ForeColor = Color.White;
            this.ImportExportPanel.Dock = DockStyle.Fill;
            this.CreateTablePanel.Hide();
            this.GetDetailsPdfPanel.Hide();
            this.ImportExportPanel.Show();
        }

        private void GetDetailsBtn_Click(object sender, EventArgs e)
        {
            this.CreateTablesBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ImportExportBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.GetDetailsBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.CreateTablesBtn.ForeColor = Color.White;
            this.ImportExportBtn.ForeColor = Color.White;
            this.GetDetailsBtn.ForeColor = Color.Black;
            this.GetDetailsPdfPanel.Dock = DockStyle.Fill;
            this.CreateTablePanel.Hide();
            this.ImportExportPanel.Hide();
            this.GetStudentsDetailsPanel.Hide();
            this.GetDetailsPdfPanel.Show();
        }

        private void CreateTableBtn_Click(object sender, EventArgs e)
        {
            if (TableTypeCCBox.Text.Equals("") || DeptCCBox.Text.Equals("") || BatchCCBox.Text.Equals(""))
                MessageBox.Show("Please enter table-type, department and batch");
            else
            try
            {
                LoginForm LF = new LoginForm();
                SQLiteConnection connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
                SQLiteCommand command;
                
                if(DeptCCBox.Text == "CIVIL")
                {
                    Branch = "CV";
                    Batch = BatchCCBox.Text.Substring(2, 2);
                }
                else
                {
                    Branch = DeptCCBox.Text.Substring(0, 2);
                    Batch = BatchCCBox.Text.Substring(2, 2); 
                }
                TableName = TableTypeCCBox.Text + "_1JV" + Batch + Branch;
                string sql;
                connection.Open();

                if (TableTypeCCBox.Text == "STUDENTS")
                {
                    //-----------------Creating Students Tables-----------------//
                    sql = "CREATE TABLE " + TableName + " (USN	VARCHAR(10) NOT NULL UNIQUE,SName	VARCHAR(30),Sphno	VARCHAR(12),Semester VARCHAR(1),Sphoto BLOB,B1 VARCHAR(10),B2 VARCHAR(10),BB1 VARCHAR(10),BB2 VARCHAR(10),BB3 VARCHAR(10),BB4 VARCHAR(10),BB5 VARCHAR(10),BB6 VARCHAR(10),BB7 VARCHAR(10),BB8 VARCHAR(10),BB9 VARCHAR(10),BB10 VARCHAR(10),BB11 VARCHAR(10),BB12 VARCHAR(10),BB13 VARCHAR(10),BB14 VARCHAR(10),PRIMARY KEY(USN));";
                    command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();                   
                    //------------------------------------------------------------
                }
                connection.Close();
                MessageBox.Show("Table Created Successfully");
            }
            catch
            {
                MessageBox.Show("Table Already Exists");
            }
            TableTypeCCBox.Text = DeptCCBox.Text = BatchCCBox.Text = "";
        }

        private void ExcelLocBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ExcelLocBox.Text = ofd.FileName;
                ExcelSource = ExcelLocBox.Text;
            }
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            if (ExcelLocBox.Text == "")
                MessageBox.Show("Please select an excel file in order to import data");
            else
            {
                try
                {
                    int count = 0;
                    string con = string.Format("provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", ExcelSource);
                    DataSet data = new DataSet();

                    foreach (var sheetName in GetExcelSheetNames(con))
                    {
                        using (OleDbConnection conn = new OleDbConnection(con))
                        {
                            var dataTable = new System.Data.DataTable();
                            string query = string.Format("SELECT * FROM [{0}]", sheetName);
                            conn.Open();
                            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                            adapter.Fill(dataTable);
                            data.Tables.Add(dataTable);
                        }
                    }
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        count = data.Tables[0].Rows.IndexOf(row);
                    }
                    SQLiteConnection connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
                    SQLiteCommand command;
                    connection.Open();
                    for (int j = 0; j == 0; j++)
                        for (int i = 0;i<=100; i++)
                        {                            
                            sql = "INSERT INTO BOOKS (Accno,Title,Author,Publisher,Price,Semester,Edition) VALUES ('" + data.Tables[j].Rows[i][0].ToString() + "', '" + data.Tables[j].Rows[i][1].ToString() + "', '" + data.Tables[j].Rows[i][2].ToString() + "', '" + data.Tables[j].Rows[i][3].ToString() + "', '" + data.Tables[j].Rows[i][4].ToString() + "', '" + data.Tables[j].Rows[i][5].ToString() + "', '" + data.Tables[j].Rows[i][6].ToString() + "');";
                            command = new SQLiteCommand(sql, connection);
                            command.ExecuteNonQueryAsync();
                            command.Dispose();
                        }
                    connection.Close();
                    MessageBox.Show("Excel data imported successfully");
                }
                catch
                {
                    MessageBox.Show("Error importing book details");
                }
            }
        }
        public static string[] GetExcelSheetNames(string con)
        {
            OleDbConnection conn = null;
            System.Data.DataTable dt = null;
            conn = new OleDbConnection(con);
            conn.Open();
            dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if(dt==null)
            {
                return null;
            }
            String[] excelSheetNames = new String[dt.Rows.Count];
            int i = 0;
            foreach(DataRow row in dt.Rows)
            {
                excelSheetNames[i] = row["TABLE_NAME"].ToString();
                i++;
            }
            return excelSheetNames;
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (TableTypeECbox.Text.Equals("") || TableTypeECbox.Text == "STUDENTS")
            {
                if (TableTypeECbox.Text == "STUDENTS")
                {
                    if (TableTypeECbox.Text.Equals("") || DeptECBox.Text.Equals("") || BatchECBox.Text.Equals(""))
                        MessageBox.Show("Please enter table-type, department and batch");
                    else if (TableTypeECbox.Text == "STUDENTS")
                    {
                        try
                        {
                            if (DeptECBox.Text == "CIVIL")
                            {
                                Branch = "CV";
                                Batch = BatchECBox.Text.Substring(2, 2);
                            }
                            else
                            {
                                Branch = DeptECBox.Text.Substring(0, 2);
                                Batch = BatchECBox.Text.Substring(2, 2);
                            }
                            TableName = TableTypeECbox.Text + "_1JV" + Batch + Branch;
                            SQLiteConnection cnn;
                            string connectionString = null;
                            string sql = null;
                            string data = null;
                            int i = 0;
                            int j = 0;
                            _Application xlApp;
                            _Workbook xlWorkBook;
                            _Worksheet xlWorkSheet;
                            object misValue = System.Reflection.Missing.Value;
                            xlApp = new Microsoft.Office.Interop.Excel.Application();
                            xlWorkBook = xlApp.Workbooks.Add(misValue);
                            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                            connectionString = "Data Source=BOOKMARK_DB.sqlite;Version=3;";
                            cnn = new SQLiteConnection(connectionString);
                            cnn.Open();
                            sql = "SELECT USN,SName as Student_Name,Sphno as Student_Contact,Semester FROM " + TableName + "";
                            SQLiteDataAdapter dscmd = new SQLiteDataAdapter(sql, cnn);
                            DataSet ds = new DataSet();
                            dscmd.Fill(ds);
                            foreach (System.Data.DataTable dt in ds.Tables)
                            {
                                for (int k = 0; k < dt.Columns.Count; k++)
                                {
                                    xlWorkSheet.Cells[1, k + 1] = dt.Columns[k].ColumnName;
                                }
                            }
                            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                            {
                                int s = i + 1;
                                for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                                {
                                    data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                                    xlWorkSheet.Cells[s + 1, j + 1] = data;
                                }
                            }
                            xlWorkBook.SaveAs(TableName, XlFileFormat.xlWorkbookNormal, misValue, XlSaveAsAccessMode.xlShared, misValue);
                            xlWorkBook.Close(true, misValue);
                            xlApp.Quit();
                            releaseObject(xlWorkSheet);
                            releaseObject(xlWorkBook);
                            releaseObject(xlApp);
                            MessageBox.Show("Excel file created successfully\nPlease check the Documents folder for the excel file");
                            cnn.Close();
                        }
                        catch
                        {
                            MessageBox.Show("File already exists\nDelete the present file and try again");
                        }
                    }
                }
            }
            else if (TableTypeECbox.Text == "BOOKS")
            {
                try
                {
                    SQLiteConnection cnn;
                    string connectionString = null;
                    string sql = null;
                    string data = null;
                    int i = 0;
                    int j = 0;
                    _Application xlApp;
                    _Workbook xlWorkBook;
                    _Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;
                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    connectionString = "Data Source=BOOKMARK_DB.sqlite;Version=3;";
                    cnn = new SQLiteConnection(connectionString);
                    cnn.Open();
                    sql = "SELECT Accno,Title,Publisher,Price,Semester,Edition,Branch,Classification_no FROM BOOKS";
                    SQLiteDataAdapter dscmd = new SQLiteDataAdapter(sql, cnn);
                    DataSet ds = new DataSet();
                    dscmd.Fill(ds);
                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            xlWorkSheet.Cells[1, k + 1] = dt.Columns[k].ColumnName;
                        }
                    }
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        int s = i + 1;
                        for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                        {
                            data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                            xlWorkSheet.Cells[s + 1, j + 1] = data;
                        }
                    }
                    xlWorkBook.SaveAs("BOOKS", XlFileFormat.xlWorkbookNormal, misValue, XlSaveAsAccessMode.xlShared, misValue);
                    xlWorkBook.Close(true, misValue);
                    xlApp.Quit();
                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);
                    MessageBox.Show("Excel file created successfully\nPlease check the Documents folder for the excel file");
                    cnn.Close();
                }
                catch
                {
                    MessageBox.Show("File already exists\nDelete the present file and try again");
                }
            }
            else if (TableTypeECbox.Text == "FACULTIES")
            {
                try
                {
                    SQLiteConnection cnn;
                    string connectionString = null;
                    string sql = null;
                    string data = null;
                    int i = 0;
                    int j = 0;
                    _Application xlApp;
                    _Workbook xlWorkBook;
                    _Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;
                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    connectionString = "Data Source=BOOKMARK_DB.sqlite;Version=3;";
                    cnn = new SQLiteConnection(connectionString);
                    cnn.Open();
                    sql = "SELECT FName as Faculty_Name,FBranch as Faculty_Branch,Fphno as Faculty_Phone_no FROM FACULTIES";
                    SQLiteDataAdapter dscmd = new SQLiteDataAdapter(sql, cnn);
                    DataSet ds = new DataSet();
                    dscmd.Fill(ds);
                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            xlWorkSheet.Cells[1, k + 1] = dt.Columns[k].ColumnName;
                        }
                    }
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        int s = i + 1;
                        for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                        {
                            data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                            xlWorkSheet.Cells[s + 1, j + 1] = data;
                        }
                    }
                    xlWorkBook.SaveAs("FACULTIES", XlFileFormat.xlWorkbookNormal, misValue, XlSaveAsAccessMode.xlShared, misValue);
                    xlWorkBook.Close(true, misValue);
                    xlApp.Quit();
                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);
                    MessageBox.Show("Excel file created successfully\nPlease check the Documents folder for the excel file");
                    cnn.Close();
                }
                catch
                {
                    MessageBox.Show("File already exists\nDelete the present file and try again");
                }
            }
            else if (TableTypeECbox.Text == "TRANSACTIONS")
            {
                try
                {
                    SQLiteConnection cnn;
                    string connectionString = null;
                    string sql = null;
                    string data = null;
                    int i = 0;
                    int j = 0;
                    _Application xlApp;
                    _Workbook xlWorkBook;
                    _Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;
                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    connectionString = "Data Source=BOOKMARK_DB.sqlite;Version=3;";
                    cnn = new SQLiteConnection(connectionString);
                    cnn.Open();
                    sql = "SELECT * FROM TRANSACTIONS";
                    SQLiteDataAdapter dscmd = new SQLiteDataAdapter(sql, cnn);
                    DataSet ds = new DataSet();
                    dscmd.Fill(ds);
                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            xlWorkSheet.Cells[1, k + 1] = dt.Columns[k].ColumnName;
                        }
                    }
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        int s = i + 1;
                        for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                        {
                            data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                            xlWorkSheet.Cells[s + 1, j + 1] = data;
                        }
                    }
                    xlWorkBook.SaveAs("TRANSACTIONS", XlFileFormat.xlWorkbookNormal, misValue, XlSaveAsAccessMode.xlShared, misValue);
                    xlWorkBook.Close(true, misValue);
                    xlApp.Quit();
                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);
                    MessageBox.Show("Excel file created successfully\nPlease check the Documents folder for the excel file");
                    cnn.Close();
                }
                catch
                {
                    MessageBox.Show("File already exists\nDelete the present file and try again");
                }
            }
            TableTypeECbox.Text = DeptECBox.Text = BatchECBox.Text = "";
        }

        private void releaseObject(_Application xlApp)
        {
            
        }

        private void releaseObject(_Workbook xlWorkBook)
        {
          
        }

        private void releaseObject(_Worksheet xlWorkSheet)
        {
            
        }

        private void BooksDetailsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                SQLiteConnection connection = new SQLiteConnection(sql);
                SQLiteCommand command;
                connection.Open();
                sql = "Select Accno,Title,Author,Publisher,Price,Semester,Edition,Branch,Classification_no from BOOKS;";
                command = new SQLiteCommand(sql, connection);
                SQLiteDataAdapter DA = new SQLiteDataAdapter(command);
                DataSet DS = new DataSet();
                GetDetailsDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                try
                {
                    DA.Fill(DS);
                    System.Data.DataTable DT = DS.Tables[0];
                    this.GetDetailsDGV.DataSource = DT;
                }
                catch
                {
                    MessageBox.Show("Sorry couldn't fetch books details");
                }
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Sorry couldn't fetch books details");
            }
        }

        private void StudentsDetailsBtn_Click(object sender, EventArgs e)
        {
            this.GetStudentsDetailsPanel.Dock = DockStyle.Fill;
            this.GetStudentsDetailsPanel.Show();
        }

        private void FacultyDetailsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                SQLiteConnection connection = new SQLiteConnection(sql);
                SQLiteCommand command;
                connection.Open();
                sql = "Select FName as Faculty_Name,FBranch as Faculty_Branch,Fphno as Faculty_Phone_no from FACULTIES ;";
                command = new SQLiteCommand(sql, connection);
                SQLiteDataAdapter DA = new SQLiteDataAdapter(command);
                DataSet DS = new DataSet();
                GetDetailsDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                try
                {
                    DA.Fill(DS);
                    System.Data.DataTable DT = DS.Tables[0];
                    this.GetDetailsDGV.DataSource = DT;
                }
                catch 
                {
                    MessageBox.Show("Sorry couldn't fetch faculty details");
                }
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Sorry couldn't fetch faculty details");
            }
        }

        private void TransactionListBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                SQLiteConnection connection = new SQLiteConnection(sql);
                SQLiteCommand command;
                connection.Open();
                sql = "SELECT Username,LoginTime,LogoutTime FROM USERLOG;";
                command = new SQLiteCommand(sql, connection);
                SQLiteDataAdapter DA = new SQLiteDataAdapter(command);
                DataSet DS = new DataSet();
                GetDetailsDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                try
                {
                    DA.Fill(DS);
                    System.Data.DataTable DT = DS.Tables[0];
                    this.GetDetailsDGV.DataSource = DT;
                }
                catch
                { 
                   MessageBox.Show("Sorry couldn't fetch transaction details");
                }
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Sorry couldn't fetch transaction details");
            }
        }

        private void TClearBtn_Click(object sender, EventArgs e)
        {
            this.GetDetailsDGV.DataSource = null;
            this.GetDetailsDGV.Rows.Clear();
        }

        private void GetstudDetailsBtn_Click(object sender, EventArgs e)
        {
            if (StudBranchCBox.Text.Equals("") || StudBatchCBox.Text.Equals(""))
                MessageBox.Show("Please choose branch and batch");
            else
            {
                this.GetStudentsDetailsPanel.Hide();
                if (StudBranchCBox.Text == "CIVIL")
                {
                    Branch = "CV";
                    Batch = StudBatchCBox.Text.Substring(2, 2);
                }
                else
                {
                    Branch = StudBranchCBox.Text.Substring(0, 2);
                    Batch = StudBatchCBox.Text.Substring(2, 2);
                }
                TableName = "STUDENTS_1JV" + Batch + Branch;
                sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                SQLiteConnection connection = new SQLiteConnection(sql);
                SQLiteCommand command;
                connection.Open();
                sql = "SELECT USN,SName as Student_Name,Sphno as Student_Contact,Semester FROM " + TableName + ";";
                command = new SQLiteCommand(sql, connection);
                SQLiteDataAdapter DA = new SQLiteDataAdapter(command);
                DataSet DS = new DataSet();
                GetDetailsDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                try
                {
                    DA.Fill(DS);
                    System.Data.DataTable DT = DS.Tables[0];
                    this.GetDetailsDGV.DataSource = DT;
                    StudBatchCBox.Text = StudBranchCBox.Text = "";
                }
                catch
                {
                    MessageBox.Show("Sorry couldn't fetch student details\n");
                }
                connection.Close();
            }
        }
        //----------------------Tools Panel Functions End------------------------//

        //--------------------Settings Panel Functions Start--------------------//
        private void SetBtn_Click(object sender, EventArgs e)
        {
            this.Sound();
            this.PanelNameLabel.Text = "SETTINGS";
            this.SetPanel.Dock = DockStyle.Fill;
            this.SetBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.HomeBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BooksBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BorrowersBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ToolsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.TransBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.AboutBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.NotificationsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.ForeColor = Color.White;
            this.TransBtn.ForeColor = Color.White;
            this.BooksBtn.ForeColor = Color.White;
            this.BorrowersBtn.ForeColor = Color.White;
            this.ToolsBtn.ForeColor = Color.White;
            this.SetBtn.ForeColor = Color.Black;
            this.AboutBtn.ForeColor = Color.White;
            this.NotificationsBtn.ForeColor = Color.White;
            this.BHomeBtn.BackColor = Color.Transparent;
            this.BTransBtn.BackColor = Color.Transparent;
            this.BBooksBtn.BackColor = Color.Transparent;
            this.BBorrowersBtn.BackColor = Color.Transparent;
            this.HomePanel.Hide();
            this.TransPanel.Hide();
            this.BooksPanel.Hide();
            this.BorrowersPanel.Hide();
            this.ToolsPanel.Hide();
            this.AboutPanel.Hide();
            this.ProfilePanel.Hide();
            this.NotificationPanel.Hide();
            this.HomePBox.BackColor = Color.Transparent;
            this.TransPBox.BackColor = Color.Transparent;
            this.BooksPBox.BackColor = Color.Transparent;
            this.BorrowersPBox.BackColor = Color.Transparent;
            this.ToolsPBox.BackColor = Color.Transparent;
            this.SetPBox.BackColor = Color.Navy;
            this.AboutPBox.BackColor = Color.Transparent;
            this.NotificationsPBox.BackColor = Color.Transparent;
            this.SetPanel.Show();
        }

        private void AdminContinueBtn_Click(object sender, EventArgs e)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
            SQLiteCommand command;
            connection.Open();
            String name = "";
            sql = "select Value from SYSTEM WHERE ID = 1";
            command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                name = reader["Value"].ToString();
            }
            reader.Close();
            command.Dispose();
            if (ProfileAdminPassBox.Text == "")
            {
                MessageBox.Show("Please enter admin password");
                this.ProfileAdminPassBox.Text = "";
            }             
            else if (name == LogUserLabel.Text)
                try
                {
                    String t_adminPwd = "";
                    sql = "select * from SYSTEM WHERE Id = 2";
                    command = new SQLiteCommand(sql, connection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                        t_adminPwd = reader["Value"].ToString();
                    reader.Close();
                    command.Dispose();           
                    if (ProfileAdminPassBox.Text.Equals(t_adminPwd))
                    {
                        this.ProfileAdminPassBox.Text = "";
                        this.ProfilePanel.Dock = DockStyle.Fill;
                        this.AddUserGBox.Enabled = true;
                        this.AddAdminGBox.Enabled = true;
                        this.RemoveUserGBox.Enabled = true;
                        this.ProfilePanel.Show();
                    }
                    else
                    {
                        MessageBox.Show("Please enter correct password");
                        this.ProfileAdminPassBox.Text = "";
                    }                        
                }
                catch 
                {
                    MessageBox.Show("Admin authentication failed");
                    this.ProfileAdminPassBox.Text = "";
                }
            else
            {
                MessageBox.Show("You are not an admin");
                this.ProfileAdminPassBox.Text = "";
            }              
            connection.Close();
        }

        private void UserContinueBtn_Click(object sender, EventArgs e)
        {        
            if(ProfileUserPassBox.Text.Equals(""))
            {
                MessageBox.Show("Please enter user password");
            }
            else
            {
                int counter = 0;
                String userid = LogUserLabel.Text;
                String pwd = ProfileUserPassBox.Text;
                String TPass = "", TUsid = "";
                string sql;
                SQLiteConnection connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
                SQLiteCommand command;
                connection.Open();
                sql = "Select * from System where Id<=12";
                command = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ++counter;
                    if (counter % 2 != 0)
                    {
                        TUsid = reader["Value"].ToString();
                    }
                    else
                    {
                        TPass = reader["Value"].ToString();
                    }
                    if (counter % 2 == 0)
                    {
                        if ((TUsid == userid) && TPass == pwd)
                        {
                            counter = 900;
                            break;
                        }
                        TUsid = "";
                        TPass = "";
                    }
                }
                reader.Close();
                if (counter == 900)
                {
                    this.ProfileUserPassBox.Text = "";
                    this.ProfileUserPassBox.Text = "";
                    this.ProfilePanel.Dock = DockStyle.Fill;
                    this.AddUserGBox.Enabled = false;
                    this.AddAdminGBox.Enabled = false;
                    this.RemoveUserGBox.Enabled = false;
                    this.ProfilePanel.Show();
                }
                else
                {
                    this.ProfileUserPassBox.Text = "";
                    MessageBox.Show("Incorrect password");
                }
                connection.Close();
            }               
        }    

        private void NewPasswordBtn_Click(object sender, EventArgs e)
        {
            if (NewPassBox.Text == ""&&ConfirmPassBox.Text=="")
                MessageBox.Show("Please enter password");
            else if (NewPassBox.Text != ConfirmPassBox.Text)
                MessageBox.Show("Please check the password");
            else
            try
            {
                String sql1, temp = "", pwd = NewPassBox.Text;
                SQLiteConnection connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
                SQLiteCommand command;
                connection.Open();
                String name = "";
                //Obtain currently logged in user name
                sql1 = "select Value from SYSTEM WHERE Id = 13";
                command = new SQLiteCommand(sql1, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    name = reader["Value"].ToString();
                }
                reader.Close();
                command.Dispose();
                sql1 = "select * from SYSTEM order by Id";
                command = new SQLiteCommand(sql1, connection);
                reader = command.ExecuteReader();
                //find in which row the the user is present
                int counter = 0;//temporary loop counter
                while (reader.Read())
                {
                    ++counter;
                    temp = reader["Value"].ToString();
                    if ((counter % 2 != 0) && (counter <= 12))
                    {
                        if (temp.Equals(name))
                        {
                            sql1 = "UPDATE SYSTEM SET Value = '" + pwd + "' where Id = " + (counter + 1).ToString();
                            break;
                        }
                    }
                }//closing while
                reader.Close();
                command.Dispose();
                command = new SQLiteCommand(sql1, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                MessageBox.Show("User password changed");
                    NewPassBox.Text = ConfirmPassBox.Text = "";
            }
            catch
            {
                MessageBox.Show("Failed to change password");
            }
        }

        private void AddUserBtn_Click(object sender, EventArgs e)
        {
            if (NewUserBox.Text == "" || NewUserPassBox.Text == "")
                MessageBox.Show("Please fill complete details");
            else
            try
            {
                String name = NewUserBox.Text;
                String pwd = NewUserPassBox.Text;

                string sql1, sql2 = "", temp = "";
                SQLiteConnection connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
                SQLiteCommand command;
                connection.Open();

                sql1 = "select * from SYSTEM order by Id";
                command = new SQLiteCommand(sql1, connection);
                SQLiteDataReader reader = command.ExecuteReader();

                int counter = 0;//temporary loop counter
                while (reader.Read())
                {

                    ++counter;
                    temp = reader["Value"].ToString();
                    if ((counter % 2 != 0) && (counter <= 12))
                    {
                        if (temp.Equals("NULL"))
                        {
                            sql1 = "UPDATE SYSTEM SET Value = '" + name + "' where Id = " + counter.ToString();
                            sql2 = "UPDATE SYSTEM SET Value = '" + pwd + "' where Id = " + (counter + 1).ToString();
                            counter = 900;
                            break;
                        }
                    }
                }//closing while
                reader.Close();
                command.Dispose();
                if (counter == 900)
                {
                    command = new SQLiteCommand(sql1, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    command = new SQLiteCommand(sql2, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    MessageBox.Show("User-Id and Password added");
                    NewUserBox.Text = NewUserPassBox.Text = "";
                }
                else
                {
                    MessageBox.Show("Maximum number of users is 5.\n\n Already 5 Users have been added.");

                }
                connection.Close();
                connection.Dispose();
                command.Dispose();
            }
            catch
            {
                MessageBox.Show("Failed to add user\n");
            }
        }

        private void AddAdminBtn_Click(object sender, EventArgs e)
        {
            String name = NewAdminNameBox.Text;
            String pwd = NewAdminPassBox.Text;
            string sql;
            SQLiteConnection connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
            SQLiteCommand command;
            connection.Open();
            sql = "UPDATE SYSTEM SET Value = '" + name + "' where Id = 1";
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            sql = "UPDATE SYSTEM SET Value = '" + pwd + "' where Id = 2";
            command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            if (NewAdminNameBox.Text != "" && NewAdminPassBox.Text != "")
            {
                MessageBox.Show("Admin added successfully");
                NewAdminNameBox.Text = NewAdminPassBox.Text = "";
                LogoutBtn_Click(sender, e);
            }
            else if (NewAdminNameBox.Text == "" && NewAdminPassBox.Text == "")
            {
                MessageBox.Show("Please enter the name and password of new admin");
                NewAdminNameBox.Text = NewAdminPassBox.Text = "";
            }
        }

        private void RemoveUserBtn_Click(object sender, EventArgs e)
        {
            if (RemoveUsernameBox.Text == "")
                MessageBox.Show("Please choose an user");
            else
            try
            {
                string name = RemoveUsernameBox.Text;
                string sql1 = "", sql2 = "", temp = "";
                SQLiteConnection connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
                SQLiteCommand command;
                connection.Open();
                int counter = 0;
                SQLiteDataReader reader;
                sql1 = "select Value from SYSTEM order by Id";
                command = new SQLiteCommand(sql1, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                        ++counter;
                        temp = reader["Value"].ToString();
                       if ((counter % 2 != 0) && (counter <= 12))
                        {
                            if (temp.Equals(name))
                            {
                                sql1 = "UPDATE SYSTEM SET Value = 'NULL' where Id = " + (counter).ToString();
                                sql2 = "UPDATE SYSTEM SET Value = 'NULL' where Id = " + (counter + 1).ToString();
                                break;
                            }
                        }
                    }//closing while
                    reader.Close();
                    command.Dispose();
                    if (temp == admin)
                        MessageBox.Show("Cann't remove admin");
                    else
                    {
                        command = new SQLiteCommand(sql1, connection);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        command = new SQLiteCommand(sql2, connection);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        connection.Close();
                        MessageBox.Show("User removed");
                    }                
                    RemoveUsernameBox.Text = "";
                }
            catch
            {
                MessageBox.Show("Failed to remove the user");
            }
        }

        private void FineBtn_Click(object sender, EventArgs e)
        {
            Fine = (int)FineNBox.Value;
            MessageBox.Show("Fine amount per day is set to: " + Fine);
            FineNBox.Value = 0;
            SQLiteConnection connection;
            SQLiteCommand command;
            try
            {
                sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                connection = new SQLiteConnection(sql);
                connection.Open();
                string query = "UPDATE SYSTEM SET VALUE='" + Fine + "' WHERE Id=14;";
                command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Unexpected error\nPlease contact the developer");
            }
        }

        private void ReturnDaysBtn_Click(object sender, EventArgs e)
        {
            Return = (int)ReturnDaysNBox.Value;
            MessageBox.Show("Numbers of days for book return is set to: " + Return);
            ReturnDaysNBox.Value = 0;
            SQLiteConnection connection;
            SQLiteCommand command;
            try
            {
                sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                connection = new SQLiteConnection(sql);
                connection.Open();
                string query = "UPDATE SYSTEM SET VALUE='" + Return + "' WHERE Id=15;";
                command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Unexpected error\nPlease contact the developer");
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.ProfilePanel.Hide();
            this.SetPanel.Show();
        }
        //--------------------Settings Panel Functions End-----------------------//
       
        //------------------------------About Panel Functions Start----------------------//
        private void AboutBtn_Click(object sender, EventArgs e)
        {
            this.Sound();
            this.PanelNameLabel.Text = "ABOUT";
            this.AboutPanel.Dock = DockStyle.Fill;
            this.AboutBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.HomeBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BooksBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BorrowersBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ToolsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SetBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.TransBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.NotificationsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.ForeColor = Color.White;
            this.TransBtn.ForeColor = Color.White;
            this.BooksBtn.ForeColor = Color.White;
            this.BorrowersBtn.ForeColor = Color.White;
            this.ToolsBtn.ForeColor = Color.White;
            this.SetBtn.ForeColor = Color.White;
            this.AboutBtn.ForeColor = Color.Black;
            this.NotificationsBtn.ForeColor = Color.White;
            this.BHomeBtn.BackColor = Color.Transparent;
            this.BTransBtn.BackColor = Color.Transparent;
            this.BBooksBtn.BackColor = Color.Transparent;
            this.BBorrowersBtn.BackColor = Color.Transparent;
            this.HomePanel.Hide();
            this.TransPanel.Hide();
            this.BooksPanel.Hide();
            this.BorrowersPanel.Hide();
            this.ToolsPanel.Hide();
            this.SetPanel.Hide();
            this.NotificationPanel.Hide();
            this.HomePBox.BackColor = Color.Transparent;
            this.TransPBox.BackColor = Color.Transparent;
            this.BooksPBox.BackColor = Color.Transparent;
            this.BorrowersPBox.BackColor = Color.Transparent;
            this.ToolsPBox.BackColor = Color.Transparent;
            this.SetPBox.BackColor = Color.Transparent;
            this.AboutPBox.BackColor = Color.Navy;
            this.NotificationsPBox.BackColor = Color.Transparent;
            this.WeBuiltLabel.Show();
            this.AboutPanel.Show();
        }

        private void BharathPicBox_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, BharathPicBox.Width - 1, BharathPicBox.Height - 1);
            Region rg = new Region(gp);
            BharathPicBox.Region = rg;
        }

        private void EldhosePicBox_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, EldhosePicBox.Width - 1, EldhosePicBox.Height - 1);
            Region rg = new Region(gp);
            EldhosePicBox.Region = rg;
        }

        private void FaizPicBox_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, FaizPicBox.Width - 1, FaizPicBox.Height - 1);
            Region rg = new Region(gp);
            FaizPicBox.Region = rg;
        }
        //------------------------------About Panel Functions End----------------------//

        //-------------------------Notification Panel Functions Start------------------//
        private void NotificationsBtn_Click(object sender, EventArgs e)
        {
            this.Sound();
            this.PanelNameLabel.Text = "NOTIFICATIONS";
            this.AboutPanel.Dock = DockStyle.Fill;
            this.NotificationsBtn.BackColor = Color.FromArgb(0, 116, 170);
            this.HomeBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BooksBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BorrowersBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ToolsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SetBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.TransBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.AboutBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.ForeColor = Color.White;
            this.TransBtn.ForeColor = Color.White;
            this.BooksBtn.ForeColor = Color.White;
            this.BorrowersBtn.ForeColor = Color.White;
            this.ToolsBtn.ForeColor = Color.White;
            this.SetBtn.ForeColor = Color.White;
            this.AboutBtn.ForeColor = Color.White;
            this.NotificationsBtn.ForeColor = Color.Black;
            this.BHomeBtn.BackColor = Color.Transparent;
            this.BTransBtn.BackColor = Color.Transparent;
            this.BBooksBtn.BackColor = Color.Transparent;
            this.BBorrowersBtn.BackColor = Color.Transparent;
            this.HomePanel.Hide();
            this.TransPanel.Hide();
            this.BooksPanel.Hide();
            this.BorrowersPanel.Hide();
            this.ToolsPanel.Hide();
            this.SetPanel.Hide();
            this.AboutPanel.Hide();
            this.HomePBox.BackColor = Color.Transparent;
            this.TransPBox.BackColor = Color.Transparent;
            this.BooksPBox.BackColor = Color.Transparent;
            this.BorrowersPBox.BackColor = Color.Transparent;
            this.ToolsPBox.BackColor = Color.Transparent;
            this.SetPBox.BackColor = Color.Transparent;
            this.AboutPBox.BackColor = Color.Transparent;
            this.NotificationsPBox.BackColor = Color.Navy;
            this.NotificationPanel.Show();
            this.AboutPanel.Dock = DockStyle.Fill;
            try
            {
                SQLiteConnection connection;
                SQLiteCommand command;
                sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                connection = new SQLiteConnection(sql);
                connection.Open();
                sql= "UPDATE TRANSACTIONS SET Fine = '"+DateTime.Now.ToShortDateString()+ "'-Expected_return_date WHERE '" + DateTime.Now.ToShortDateString() + "'>= Expected_return_date AND Return_date IS NULL;";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                sql = @"Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;";
                connection = new SQLiteConnection(sql);
                connection.Open();
                sql = "Select Issued_User,USN,Book_Id,Issue_date,Expected_return_date,Fine from TRANSACTIONS WHERE '" + DateTime.Now.ToShortDateString()+ "'>= Expected_return_date AND Return_date IS NULL;";
                command = new SQLiteCommand(sql, connection);
                SQLiteDataAdapter DA = new SQLiteDataAdapter(command);
                DataSet DS = new DataSet();
                NotificationsDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                try
                {
                    DA.Fill(DS);
                    System.Data.DataTable DT = DS.Tables[0];
                    this.NotificationsDGV.DataSource = DT;
                }
                catch
                {
                    MessageBox.Show("Unexpected error\nPlease contact the developer");
                }
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Unexpected error\nPlease contact the developer");
            }
            this.NotificationPanel.Show();
        }
        //---------------------------Notification Panel Functions End------------------//

        //---------------------------Shortcut Buttons Functions Start------------------//
        private void BHomeBtn_Click(object sender, EventArgs e)
        {
            this.Sound();
            this.PanelNameLabel.Text = "HOME";
            this.HomePanel.Dock = DockStyle.Fill;
            this.BHomeBtn.BackColor = Color.FromArgb(0, 116, 170); ;
            this.BTransBtn.BackColor = Color.Transparent;
            this.BBooksBtn.BackColor = Color.Transparent;
            this.BBorrowersBtn.BackColor = Color.Transparent;
            this.AboutBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BooksBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BorrowersBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ToolsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SetBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.TransBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.NotificationsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.ForeColor = Color.White;
            this.TransBtn.ForeColor = Color.White;
            this.BooksBtn.ForeColor = Color.White;
            this.BorrowersBtn.ForeColor = Color.White;
            this.ToolsBtn.ForeColor = Color.White;
            this.SetBtn.ForeColor = Color.White;
            this.AboutBtn.ForeColor = Color.White;
            this.NotificationsBtn.ForeColor = Color.White;
            this.TransPanel.Hide();
            this.BooksPanel.Hide();
            this.BorrowersPanel.Hide();
            this.ToolsPanel.Hide();
            this.SetPanel.Hide();
            this.AboutPanel.Hide();
            this.NotificationPanel.Hide();
            this.HomePBox.BackColor = Color.Transparent;
            this.TransPBox.BackColor = Color.Transparent;
            this.BooksPBox.BackColor = Color.Transparent;
            this.BorrowersPBox.BackColor = Color.Transparent;
            this.ToolsPBox.BackColor = Color.Transparent;
            this.SetPBox.BackColor = Color.Transparent;
            this.AboutPBox.BackColor = Color.Transparent;
            this.NotificationsPBox.BackColor = Color.Transparent;
            this.HomePanel.Show();
        }

        private void BTransBtn_Click(object sender, EventArgs e)
        {
            this.Sound();
            this.PanelNameLabel.Text = "TRANSACTIONS";
            this.TransPanel.Dock = DockStyle.Fill;
            this.BHomeBtn.BackColor = Color.Transparent;
            this.BTransBtn.BackColor = Color.FromArgb(0, 116, 170); ;
            this.BBooksBtn.BackColor = Color.Transparent;
            this.BBorrowersBtn.BackColor = Color.Transparent;
            this.AboutBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BooksBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BorrowersBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ToolsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SetBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.TransBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.NotificationsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.ForeColor = Color.White;
            this.TransBtn.ForeColor = Color.White;
            this.BooksBtn.ForeColor = Color.White;
            this.BorrowersBtn.ForeColor = Color.White;
            this.ToolsBtn.ForeColor = Color.White;
            this.SetBtn.ForeColor = Color.White;
            this.AboutBtn.ForeColor = Color.White;
            this.NotificationsBtn.ForeColor = Color.White;
            this.HomePanel.Hide();
            this.BooksPanel.Hide();
            this.BorrowersPanel.Hide();
            this.ToolsPanel.Hide();
            this.SetPanel.Hide();
            this.AboutPanel.Hide();
            this.NotificationPanel.Hide();
            this.HomePBox.BackColor = Color.Transparent;
            this.TransPBox.BackColor = Color.Transparent;
            this.BooksPBox.BackColor = Color.Transparent;
            this.BorrowersPBox.BackColor = Color.Transparent;
            this.ToolsPBox.BackColor = Color.Transparent;
            this.SetPBox.BackColor = Color.Transparent;
            this.AboutPBox.BackColor = Color.Transparent;
            this.NotificationsPBox.BackColor = Color.Transparent;
            this.RegularBtn_Click(sender, e);
            this.BBSTransPanel.Hide();
            this.TransPanel.Show();
        }

        private void BBooksBtn_Click(object sender, EventArgs e)
        {
            this.Sound();
            this.PanelNameLabel.Text = "BOOKS";
            this.BooksPanel.Dock = DockStyle.Fill;
            this.BHomeBtn.BackColor = Color.Transparent;
            this.BTransBtn.BackColor = Color.Transparent;
            this.BBooksBtn.BackColor = Color.FromArgb(0, 116, 170); ;
            this.BBorrowersBtn.BackColor = Color.Transparent;
            this.AboutBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BooksBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BorrowersBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ToolsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SetBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.TransBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.NotificationsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.ForeColor = Color.White;
            this.TransBtn.ForeColor = Color.White;
            this.BooksBtn.ForeColor = Color.White;
            this.BorrowersBtn.ForeColor = Color.White;
            this.ToolsBtn.ForeColor = Color.White;
            this.SetBtn.ForeColor = Color.White;
            this.AboutBtn.ForeColor = Color.White;
            this.NotificationsBtn.ForeColor = Color.White;
            this.HomePanel.Hide();
            this.TransPanel.Hide();
            this.BorrowersPanel.Hide();
            this.ToolsPanel.Hide();
            this.SetPanel.Hide();
            this.AboutPanel.Hide();
            this.NotificationPanel.Hide();
            this.HomePBox.BackColor = Color.Transparent;
            this.TransPBox.BackColor = Color.Transparent;
            this.BooksPBox.BackColor = Color.Transparent;
            this.BorrowersPBox.BackColor = Color.Transparent;
            this.ToolsPBox.BackColor = Color.Transparent;
            this.SetPBox.BackColor = Color.Transparent;
            this.AboutPBox.BackColor = Color.Transparent;
            this.NotificationsPBox.BackColor = Color.Transparent;
            this.BooksPanel.Show();
        }

        private void BBorrowersBtn_Click(object sender, EventArgs e)
        {
            this.Sound();
            this.PanelNameLabel.Text = "BORROWERS";
            this.BorrowersPanel.Dock = DockStyle.Fill;
            this.BHomeBtn.BackColor = Color.Transparent;
            this.BTransBtn.BackColor = Color.Transparent;
            this.BBooksBtn.BackColor = Color.Transparent;
            this.BBorrowersBtn.BackColor = Color.FromArgb(0, 116, 170); ;
            this.AboutBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BooksBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.BorrowersBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.ToolsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.SetBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.TransBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.NotificationsBtn.BackColor = Color.FromArgb(0, 175, 240);
            this.HomeBtn.ForeColor = Color.White;
            this.TransBtn.ForeColor = Color.White;
            this.BooksBtn.ForeColor = Color.White;
            this.BorrowersBtn.ForeColor = Color.White;
            this.ToolsBtn.ForeColor = Color.White;
            this.SetBtn.ForeColor = Color.White;
            this.AboutBtn.ForeColor = Color.White;
            this.NotificationsBtn.ForeColor = Color.White;
            this.HomePanel.Hide();
            this.TransPanel.Hide();
            this.BooksPanel.Hide();
            this.ToolsPanel.Hide();
            this.SetPanel.Hide();
            this.AboutPanel.Hide();
            this.NotificationPanel.Hide();
            this.HomePBox.BackColor = Color.Transparent;
            this.TransPBox.BackColor = Color.Transparent;
            this.BooksPBox.BackColor = Color.Transparent;
            this.BorrowersPBox.BackColor = Color.Transparent;
            this.ToolsPBox.BackColor = Color.Transparent;
            this.SetPBox.BackColor = Color.Transparent;
            this.AboutPBox.BackColor = Color.Transparent;
            this.NotificationsPBox.BackColor = Color.Transparent;
            this.SearchStudBtn_Click(sender, e);
            this.BorrowersPanel.Show();
        }
        //---------------------------Shortcut Buttons Functions End--------------------//

        //-----------------------------Minimize  Function Start--------------------------//
        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //-----------------------------Minimize  Function End--------------------------//

        public void getdetails()
        {
            SQLiteConnection connection;
            SQLiteCommand command;
            SQLiteDataReader reader;
            connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
            connection.Open();
            sql = "select * from SYSTEM WHERE Id =14;";
            command = new SQLiteCommand(sql, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Fine = Convert.ToInt32(reader["Value"].ToString());
                break;
            }
            reader.Close();
            connection.Close();
            connection = new SQLiteConnection("Data Source=BOOKMARK_DB.sqlite;Version=3;New=True;Compress=True;");
            connection.Open();
            sql = "select * from SYSTEM WHERE Id =15;";
            command = new SQLiteCommand(sql, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Return = Convert.ToInt32(reader["Value"].ToString());
                break;
            }
            reader.Close();
            connection.Close();
        }

        //-------------------------------Logout  Function Start--------------------------//
        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            this.Sound();
            DialogResult DR = MessageBox.Show("Are you sure to Logout?", "Logout Confirm", MessageBoxButtons.YesNo);
            switch (DR)
            {
                case DialogResult.Yes:
                    {

                        if (!database.Insert("USERLOG", LogUserLabel.Text, LoginTimeLabel.Text, DateTime.Now.ToString("h:mm:ss tt-dd/MM/yyyy")))
                        {
                            MessageBox.Show("" + database.error_message);
                        }
                    }
                    break;
                case DialogResult.No: return;
            }
            this.StartPanel.Location = new System.Drawing.Point(1, 1);
            this.StartPanel.Size = new Size(1000, 750);
            this.StartPanel.BringToFront();
            this.StartLabel.Text = "THANK YOU";
            this.StartPanel.Show();
            LogoutTimer.Interval = 500;
            LogoutTimer.Start();
        }
        //-------------------------------Logout  Function End--------------------------//
    }
}