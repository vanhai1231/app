using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;


namespace GUI
{
    public partial class Dangky : Form
    {
        private string connectionString;
        private string tenDangNhap;
        private string matKhau;
        private string ho;
        private string ten;
        private string gioitinh;
        private string ngaysinh;
        private string diachi;
        private string Email;
        public Dangky()
        {
            InitializeComponent();
            connectionString = "Data Source=DESKTOP-FGNUE5N\\SQLEXPRESS;Initial Catalog=QL_NHANSU;Integrated Security=True";
        }


        private void Dangky_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn hủy đăng ký?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                dangNhap dn  = new dangNhap();                  
                dn.Show();
                this.Hide();

            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dangNhap dn = new dangNhap();
            dn.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn hủy đăng ký?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                dangNhap dn = new dangNhap();
                dn.Show();
                this.Hide();

            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ho = textBox1.Text;
                ten = textBox2.Text;
                diachi = textBox3.Text; 
                matKhau = textBox4.Text;
                tenDangNhap = textBox5.Text;
                Email = textBox7.Text;
                gioitinh = null;
                if (radioButton1.Checked)
                {
                    gioitinh = "Nam";
                }
                else if (radioButton2.Checked)
                {
                    gioitinh = "Nữ";
                }
                ngaysinh = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                DuLieu dl = new DuLieu(connectionString);
                if (dl.KiemTraTenDangNhapTonTai(tenDangNhap))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên đăng nhập khác.");
                    return;
                }
                dl.themNguoiDung(ho, ten, gioitinh, ngaysinh, diachi);
                dl.ThemThongTinTK(tenDangNhap, matKhau,Email);
                MessageBox.Show("Đăng ký tài khoản thành công, đăng nhập ngay!");
                dangNhap f = new dangNhap();
                f.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình đăng ký: " + ex.Message);
            }
        }

        private void Dangky_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //Hàm hiển thị mật khẩu
            textBox4.UseSystemPasswordChar = !checkBox1.Checked;
            //Hàm hiển thị mật khẩu
            textBox6.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}
