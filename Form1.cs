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


namespace login
{
    public partial class EmployeesMangementSystem : Form
    {
       static string sql = "Data Source = DESKTOP-NO54C7F ; Initial Catalog =logindb ;  Integrated Security=True ; User ID = '' ; password = '' ; ";
        //Data Source=DESKTOP-NO54C7F;Initial Catalog=logindb;Integrated Security=True
        SqlConnection con = new SqlConnection(sql);
        private object txtpassword;

        public EmployeesMangementSystem()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        //insert_Btn
        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "INSERT INTO userdb (EmployeeName , phone, Email , Address) VALUES(@EmployeeName ,@phone ,@Email , @Address)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@EmployeeName", txtusername.Text);
            cmd.Parameters.AddWithValue("@phone", txtpass.Text);
            cmd.Parameters.AddWithValue("@Email", txtemail.Text);
            cmd.Parameters.AddWithValue("@Address", txtadd.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("User Saved");
            dataGridView1.DataSource = LoadUserTable();

        }

        //Update Data
        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "UPDATE userdb SET EmployeeName=@EmployeeName ,Phone=@Phone ,Email=@Email , Address=@Address  WHERE ID=" + dataGridView1.CurrentRow.Cells[0].Value.ToString()+"";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@EmployeeName", txtusername.Text);
            cmd.Parameters.AddWithValue("@phone", txtpass.Text);
            cmd.Parameters.AddWithValue("@Email", txtemail.Text);
            cmd.Parameters.AddWithValue("@Address", txtadd.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Updated Successfulley");
            dataGridView1.DataSource = LoadUserTable();//reload.
        }

        //Delete_Btn
        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            String query = "DELETE FROM userdb WHERE ID ="+dataGridView1.CurrentRow.Cells[0].Value.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Deleteted Successfulley");
            dataGridView1.DataSource = LoadUserTable();//reload

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = LoadUserTable(); 

        }

        public DataTable LoadUserTable()
        {
            DataTable dt = new DataTable();
            String query = "SELECT * FROM userdb";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;

        }

        //select current row and show it 
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtusername.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtpass.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtemail.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtadd.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }


        //Search_data
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            String query = "SELECT * FROM userdb WHERE EmployeeName LIKE '%" + txtsearch.Text+"%'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            dataGridView1.DataSource = dt;



        }

        //minimize_Btn
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Exit_Btn
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //phone label
        private void txtpass_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtpass.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtpass.Text = txtpass.Text.Remove(txtpass.Text.Length - 1);
            }
        }
    }
}
