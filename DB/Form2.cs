using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DB
{
    public partial class Form2 : Form
    {
        public SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-6D738N5;Initial Catalog=University;Integrated Security=True");
        public Form2()
        {
            InitializeComponent();
            conn.Open();
            LoadData();
        }
        public void LoadData()
        {
            try
            { 
                String query = "SELECT * FROM users";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds, "Users");
                dataGridView1.DataSource = ds.Tables["Users"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unhandled Exception occured: " + ex);
            }
        }
         
        private void addBtn_Click(object sender, EventArgs e)
        {
            String user_email, user_pass;
            user_email = emailInput.Text;
            user_pass = passwordInput.Text;
            try { 
            //String query = "INSERT INTO users(email,password) VALUES (" + user_email+","+user_pass+")";
            String query = "INSERT INTO users(email,password) VALUES (@email,@password)";

            SqlCommand cmd = new SqlCommand(query, conn);
       //         cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", user_email);
            cmd.Parameters.AddWithValue("@password", user_pass);

            cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully added new user:  ");
                emailInput.Text = "";
                passwordInput.Text = "";
                LoadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception occured" + ex);
            }

        }
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(deleteInput.Text);
            if (id != 0)
            {

                try {
                    String query = "DELETE FROM users WHERE id = '" + id + "'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Deleted.");
                    deleteInput.Text = "";
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception occured" + ex);
                }
            }
            else
            {
                MessageBox.Show("Please Emter Record ID");
            }
        }

        public void getDataByID(int id)
        {
            try
            {
                String query = "SELECT * FROM users WHERE id = '" + id + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                if(sdr.HasRows){
                    sdr.Read();
                    emailInput.Text = sdr["email"].ToString();
                    passwordInput.Text = sdr["password"].ToString(); ;
                    sdr.Close();
                }
                else
                {
                    MessageBox.Show("User not found.");
                    sdr.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured" + ex);
            }
        }

        // UPDATE RECORD USING ID
        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(deleteInput.Text);
            String user_email, user_pass;
            user_email = emailInput.Text;
            user_pass = passwordInput.Text;
            if (id != null  && emailInput.Text !="" && passwordInput.Text !="")
            { 
 
                try
                {
                    String query = "UPDATE users SET email = '"+user_email+"' , password='"+user_pass+"' WHERE id = '" + id + "' ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated.");
                    emailInput.Text = "";
                    passwordInput.Text = "";
                    deleteInput.Text = "";
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception occured" + ex);
                }
            }
            else
            {

                if (id == null)
                {
                    MessageBox.Show("Please Enter Record ID");

                }
                else if (user_email == "")
                {
                    MessageBox.Show("Please Enter Email");

                }
                else if (user_pass == "")
                {
                    MessageBox.Show("Please Enter Password");

                }
                else
                {
                    MessageBox.Show("All fields are required");
                }
            }
        }

        // SET DATA IN FORM
        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(deleteInput.Text);
            if (id != 0)
            {
                getDataByID(id);
            }
            else
            {
                MessageBox.Show("Please Enter Record ID");
            }
        }
    }
}
