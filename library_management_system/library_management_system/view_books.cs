using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace library_management_system
{
    public partial class view_books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\BUNTY\Documents\database.mdf;Integrated Security=True;Connect Timeout=30");
    
        public view_books()
        {
            InitializeComponent();
        }

        private void view_books_Load(object sender, EventArgs e)
        {
            disp_books();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            int i = 0;
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where books_name like('%" + textBox1.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;


                con.Close();
                if (i == 0)
                {
                    MessageBox.Show("No Books Found");
                }
            }catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            int i = 0;
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where books_author_name like('%" + textBox2.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                i=Convert.ToInt32(dt.Rows.Count.ToString());

                dataGridView1.DataSource = dt;


                con.Close();

                if (i == 0)
                {
                    MessageBox.Show("No Books Found");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel3.Visible = true;

            int i;
            i= Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
           
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where id="+i+"";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach(DataRow dr in dt.Rows)
                {
                    booksname.Text = dr["books_name"].ToString();
                    authorname.Text = dr["books_author_name"].ToString();
                    publication.Text = dr["books_publication_name"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dr["books_purchase_date"].ToString());
                    booksprice.Text = dr["books_price"].ToString();
                    booksquantity.Text = dr["books_quantity"].ToString();
                }

                con.Close();

                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
           
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update books_info set books_name='"+ booksname.Text +"',books_author_name='"+ authorname.Text +"',books_publication_name='"+ publication.Text +"',books_purchase_date='"+ dateTimePicker1.Value +"',books_price="+ booksprice.Text +",books_quantity="+ booksquantity.Text +" where id="+i+"";
                cmd.ExecuteNonQuery();
                con.Close();
                disp_books();

                MessageBox.Show("Record updated successfully");
                panel3.Visible = false;
            }catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
        }
        public void disp_books()
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where books_name like('%" + textBox1.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;


                con.Close();
               
                
            }catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }

}
