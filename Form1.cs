using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyNhaNghi_KhachSan
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd; 
        SqlDataAdapter adt;
        DataTable dt = new DataTable();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
       
            conn.ConnectionString = "Data Source=DESKTOP-NGB4SEV;Initial Catalog=QuanLyNhanVien;Integrated Security=True;";
                try
            {
                conn.Open();
                cmd = new SqlCommand("select * from NhanVien", conn);
                adt = new SqlDataAdapter(cmd);
                dt.Clear();
                adt.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["MaNhanVien"].Value.ToString();
                textBox2.Text = row.Cells["SoDienThoai"].Value.ToString();
                textBox3.Text = row.Cells["TenNhanVien"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        string macu, tencu, sdtcu;// luu bien tam thoi de quay ve trang thai cu

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string sql = "INSERT INTO NhanVien VALUES (@ma, @ten, @sdt)";
                cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ma", textBox1.Text);
                cmd.Parameters.AddWithValue("@ten", textBox3.Text);
                cmd.Parameters.AddWithValue("@sdt", textBox2.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Thêm thành công");

                conn.Close();

                button1_Click(null, null); // load lại DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            try
            {
                conn.Open();

                string sql = "UPDATE NhanVien SET TenNhanVien=@ten, SoDienThoai=@sdt WHERE MaNhanVien=@ma";
                cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ma", textBox1.Text); // không đổi
                cmd.Parameters.AddWithValue("@ten", textBox3.Text);
                cmd.Parameters.AddWithValue("@sdt", textBox2.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Cập nhật thành công");

                conn.Close();

                button1_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            macu = textBox1.Text;
            tencu = textBox3.Text;
            sdtcu = textBox2.Text;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = macu;
            textBox3.Text = tencu;
            textBox2.Text = sdtcu;
        }
    }
}
