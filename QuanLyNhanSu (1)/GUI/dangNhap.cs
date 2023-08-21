using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class dangNhap : Form
    {
        public string connectionString ;
        public dangNhap()
        {
            InitializeComponent();
            connectionString = "Data Source=DESKTOP-FGNUE5N\\SQLEXPRESS;Initial Catalog=QL_NHANSU;Integrated Security=True";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát chương trinh?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.ExitThread();

            }


        }

        //Dang Nhap
        private void button1_Click(object sender, EventArgs e)
        {
            string tenDangNhap = textBox1.Text;
            string matKhau = textBox2.Text;
            try
            {
                DuLieu dl = new DuLieu(connectionString);
                if(dl.kiemTraDangNhap(tenDangNhap,matKhau))
                {
                    MessageBox.Show("Đăng nhập thành công");
                    Form1 f  = new Form1();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Đăng nhập sai Tài khoản hoặc Mật khẩu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình đăng nhập: " + ex.Message);
            }

        }

        private void dangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát chương trinh?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Application.ExitThread();

            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dangky dk = new Dangky();
            dk.Show ();
            this.Hide();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            QuenMK quenMK = new QuenMK();   
            quenMK.Show ();
            this.Hide ();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //Hàm hiển thị mật khẩu
            textBox2.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter) 
            {
                button1_Click(sender, e);
            }
        }

        private void dangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
