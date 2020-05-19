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
    public partial class issue_books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\BUNTY\Documents\database.mdf;Integrated Security=True;Connect Timeout=30");
    
        public issue_books()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int books_qty=0;
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select * from books_info where books_name='"+txt_bookname.Text+"'";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                books_qty = Convert.ToInt32(dr2["available_qty"].ToString());
            }
            if (books_qty > 0)
            {



                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into issue_books values('" + txt_enrollment.Text + "','" + txt_student.Text + "','" + txt_dept.Text + "','" + txt_sem.Text + "','" + txt_contact.Text + "','" + txt_email.Text + "','" + txt_bookname.Text + "','" + dateTimePicker1.Value.ToString() + "','')";
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "update books_info set available_qty=available_qty-1 where books_name='" + txt_bookname.Text + "'";
                cmd1.ExecuteNonQuery();

                MessageBox.Show("Books Issued Successfully");
            }
            else
            {
                MessageBox.Show("books not available");
            }
       }

        private void issue_books_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
            con.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from student_info where student_roll_no='"+txt_enrollment.Text+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());
            if (i == 0)
            {
                MessageBox.Show("Roll number not found");
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txt_student.Text = dr["student_name"].ToString();
                    txt_dept.Text = dr["student_dept"].ToString();
                    txt_sem.Text = dr["student_sem"].ToString();
                    txt_contact.Text = dr["student_contact"].ToString();
                    txt_email.Text = dr["student_email"].ToString();
                }
            }
        }

        private void txt_bookname_KeyUp(object sender, KeyEventArgs e)
        {
            int count = 0;

            if (e.KeyCode != Keys.Enter)
            {

                listBox1.Items.Clear();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where books_name like('%" + txt_bookname.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());
                if (count > 0)
                {
                    listBox1.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        listBox1.Items.Add(dr["books_name"].ToString());
                    }
                }

            }
        }

        private void txt_bookname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;

            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_bookname.Text = listBox1.SelectedItem.ToString();
                listBox1.Visible = false;

            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            txt_bookname.Text = listBox1.SelectedItem.ToString();
            listBox1.Visible = false;

        }

    }
}
