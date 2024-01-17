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
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuillosaFinalSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        //server
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=register;Integrated Security=True;Pooling=False");


        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE dbSystem SET Name=@Name, Age=@Age, Gender=@Gender WHERE ID=@ID ", con);
            cmd.Parameters.AddWithValue("@Id", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@Name", textBox2.Text);
            cmd.Parameters.AddWithValue("@Age", textBox3.Text);
            cmd.Parameters.AddWithValue("@Gender", textBox4.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            MessageBox.Show("Succesfully Updated");
            loadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO dbSystem(Id,Name,Age,Gender) VALUES (@Id,@Name,@Age,@Gender)", con);
                cmd.Parameters.AddWithValue("@Id", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                cmd.Parameters.AddWithValue("@Age", textBox3.Text);
                cmd.Parameters.AddWithValue("@Gender", textBox4.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                MessageBox.Show("Succesfully Inserted");
                loadData();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Unique constraint violation error number
                {
                    MessageBox.Show("Error: This ID already exists in the database. Please choose a different ID.");
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            
       }

        public void Form1_Load(object sender, EventArgs e)
        {

            loadData();

            if (textBox5.Text.Equals(""))
            {
                loadData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                if (int.TryParse(textBox1.Text, out int id))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM dbSystem WHERE ID=@ID", con);
                    cmd.Parameters.AddWithValue("@Id", int.Parse(textBox1.Text));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Successfully deleted");
                    }
                    else
                    {
                        MessageBox.Show("No records found.");
                    }
                    
                }
                else
                {
                    throw new FormatException("Invalid ID format. Please enter a valid integer.");
                }


            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }

            catch (SqlException ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
            loadData();

    }

        private void timer1_Tick(object sender, EventArgs e)
        {
            


        }

        public void loadData()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM dbSystem", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {



        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (textBox5.Text.Equals(""))
                {
                    loadData();
                }
                else
                {

                    if (int.TryParse(textBox5.Text, out int id))
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("SELECT * FROM dbSystem Where ID=@ID", con);
                        cmd.Parameters.AddWithValue("@Id", int.Parse(textBox5.Text));
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;

                        con.Close();
                    }
                    else
                    {
                        throw new FormatException("Invalid ID format. Please enter a valid integer.");
                    }

                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }


    }
}

