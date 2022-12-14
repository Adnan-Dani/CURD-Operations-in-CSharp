using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            loginBtn.Hide();
        } 
         
        public SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-6D738N5;Initial Catalog=University;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        { }

        private void testCon_Click(object sender, EventArgs e)
        {
            testCon.BackColor = Color.Green;
            testCon.ForeColor = Color.White;
            loginBtn.Show();
            conn.Open();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            String user_email, user_pass;
            user_email = emailInput.Text;
            user_pass = passwordInput.Text;
            
            try
            {
                String query = "SELECT * FROM users WHERE email = '"+user_email+"' " +
                    "AND password = '"+user_pass+"'";


                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Successfully Login");
                    Form2 home = new Form2();
                    this.Hide();
                    home.ShowDialog();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Incoreect email & password");
                }

            }
            catch
            {
                MessageBox.Show("Connect your db");
            } 
        }
    }
}
