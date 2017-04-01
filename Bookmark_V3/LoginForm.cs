using System;
using System.IO;
using System.Windows.Forms;

namespace Bookmark_V3
{
    public partial class LoginForm : Form
    {
        Backend_DB database = new Backend_DB();
        public LoginForm()
        {
            InitializeComponent();
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            /* This function is called from the main program.
             * first we check if the database file is present.
             * if the file is not present, it means that this is the first time the app is being opened. 
             * So we create and initialize the database and the open the regestration form
             * if the database is present, we simply continue with the login
             * 
             * */
            string curFile1 = @"BOOKMARK_DB.sqlite",curFile2=@"FBooks_DB.sqlite";
            if (!File.Exists(curFile1)&&!File.Exists(curFile2)) //File not found
            {
                this.RegistrationPanel.Show();
                this.RegistrationPanel.Dock = DockStyle.Fill;
                if(!database.Initialize())
                {
                    MessageBox.Show(database.error_message);
                }
            }
            else
            {
                this.RegistrationPanel.Hide();
            }
            this.ProceedBtn.Enabled = false;
        }
        
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            String userid = UsernameBox.Text;
            String pwd = PasswordBox.Text;
            String TPass = "", TUsid = "";//Temproraty variables to perform login authentication
            int odd_counter = 1, even_counter = 2;
            while(even_counter<=12)
            {
                if (database.Get("BOOKMARK_DB.sqlite", "System", "Id", "" + odd_counter, "Value"))
                    TUsid = database.Result.ToString();
                else
                    MessageBox.Show(""+database.error_message);
                if (database.Get("BOOKMARK_DB.sqlite", "System", "Id", ""+even_counter, "Value"))
                    TPass =database.Result.ToString();
                else
                    MessageBox.Show("" + database.error_message);
                if ((TUsid == userid) && TPass == pwd)
                {
                    //Login success
                    //Log in Successful
                    // Adding details of currently logged in user to the System Table
                    if (!database.Update("BOOKMARK_DB.sqlite", "SYSTEM", "Value", userid, "Id", "13"))
                    {
                        MessageBox.Show("" + database.error_message);
                    }
                    this.PasswordBox.Text = "";
                    Bookmark BM = new Bookmark();
                    this.Hide();
                    BM.Show();
                    return;
                }
                even_counter += 2;
                odd_counter += 2;
            }
            // Login Unsuccessful
            this.PasswordBox.Text = "";
            MessageBox.Show("Log In Failed");
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void AddAdminBtn_Click(object sender, EventArgs e)
        {
            if (AdminNameBox.Text != "" && AdminPasswordBox.Text != "")
            {
                if (!database.Update("BOOKMARK_DB.sqlite", "SYSTEM", "Value", AdminNameBox.Text, "Id", "1"))
                    MessageBox.Show("" + database.error_message); 
                if (!database.Update("BOOKMARK_DB.sqlite", "SYSTEM", "Value", AdminPasswordBox.Text, "Id", "2"))
                    MessageBox.Show("" + database.error_message);

                MessageBox.Show("Admin added successfully");
                ProceedBtn.Enabled = true;
            }
            else
            {
                ProceedBtn.Enabled = false;
                MessageBox.Show("Please enter the complete details");
            }
        }
        
        private void ProceedBtn_Click(object sender, EventArgs e)
        {
            this.RegistrationPanel.Hide();
        }

        private void UsernameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                this.PasswordBox.Focus();
            }
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.LoginBtn_Click(sender,e);
            }
        }
    }
}