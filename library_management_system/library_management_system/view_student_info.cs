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
using System.IO;

namespace library_management_system
{
    public partial class view_student_info : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\BUNTY\Documents\database.mdf;Integrated Security=True;Connect Timeout=30");
        
        string pwd = Class1.GetRandomPassword(20);
        string wanted_path;
        DialogResult result;
        public view_student_info()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void view_student_info_Load(object sender, EventArgs e)
        {
            
            if(con.State==ConnectionState.Open)
            {
                con.Close();

            }
            con.Open();
            fill_grid();
            

        }

        public void fill_grid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            int i = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from student_info";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            Bitmap img;
            DataGridViewImageColumn imagecol = new DataGridViewImageColumn();

            imagecol.HeaderText = "student image";
            imagecol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imagecol.Width = 100;
            dataGridView1.Columns.Add(imagecol);

            foreach (DataRow dr in dt.Rows)
            {
                img = new Bitmap(@"..\..\" + dr["student_image"].ToString());
                dataGridView1.Rows[i].Cells[8].Value = img;
                dataGridView1.Rows[i].Height = 100;
                i = i + 1;
            }
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Refresh();
                int i = 0;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select *from student_info where student_name like ('%"+textBox1.Text+"%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                Bitmap img;
                DataGridViewImageColumn imagecol = new DataGridViewImageColumn();

                imagecol.HeaderText = "student image";
                imagecol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imagecol.Width = 100;
                dataGridView1.Columns.Add(imagecol);

                foreach (DataRow dr in dt.Rows)
                {
                    img = new Bitmap(@"..\..\" + dr["student_image"].ToString());
                    dataGridView1.Rows[i].Cells[8].Value = img;
                    dataGridView1.Rows[i].Height = 100;
                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from student_info where id="+i+"";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                studentname.Text = dr["student_name"].ToString();
                studentrollno.Text = dr["student_roll_no"].ToString();
                dept.Text = dr["student_dept"].ToString();
                semester.Text = dr["student_sem"].ToString();
                contact.Text = dr["student_contact"].ToString();
                email.Text = dr["student_email"].ToString();

            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            result = openFileDialog1.ShowDialog();
            openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (result == DialogResult.OK)
            {
                int i;
                i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                string img_path;
                File.Copy(openFileDialog1.FileName, wanted_path + "\\student_images\\" + pwd + ".jpg");
                img_path = "student_images\\" + pwd + ".jpg";

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update student_info set student_name='"+ studentname.Text +"',student_image='"+ img_path.ToString() +"',student_roll_no='"+ studentrollno.Text +"',student_dept='"+ dept.Text +"',student_sem='"+ semester.Text +"',student_contact='"+ contact.Text +"',student_email='"+ email.Text +"' where id=" + i + "";
                cmd.ExecuteNonQuery();
                fill_grid();
                MessageBox.Show("Record Updated Successfully");
            }
            else if(result==DialogResult.Cancel)
            {
                int i;
                i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update student_info set student_name='" + studentname.Text + "',student_roll_no='" + studentrollno.Text + "',student_dept='" + dept.Text + "',student_sem='" + semester.Text + "',student_contact='" + contact.Text + "',student_email='" + email.Text + "' where id=" + i + "";
                cmd.ExecuteNonQuery();
                fill_grid();
                MessageBox.Show("Record Updated Successfully");
               
            }
            else
            {
                int i;
                i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update student_info set student_name='" + studentname.Text + "',student_roll_no='" + studentrollno.Text + "',student_dept='" + dept.Text + "',student_sem='" + semester.Text + "',student_contact='" + contact.Text + "',student_email='" + email.Text + "' where id=" + i + "";
                cmd.ExecuteNonQuery();
                fill_grid();
                MessageBox.Show("Record Updated Successfully");
            }
        }
    }
}
