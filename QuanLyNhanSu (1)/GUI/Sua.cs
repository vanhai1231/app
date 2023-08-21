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
    public partial class Sua : Form
    {
        private string MANV;
        private string connectionString;
        public Sua(string mANV)
        {
            InitializeComponent();
            MANV = mANV;
            connectionString = "Data Source=DESKTOP-FGNUE5N\\SQLEXPRESS;Initial Catalog=QL_NHANSU;Integrated Security=True";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin mới từ các điều khiển
                string tenNV = txtTenNV.Text;
                DateTime SN = dtpNgaySinh.Value;
                string DC = txtDiaChi.Text;
                string SDT = txtDienThoai.Text;
                string bangCap = cboBangCap.Text;

                DuLieu dl = new DuLieu(connectionString);
                dl.capNhatThongTinNhanVien(MANV, tenNV, SN, DC, SDT, bangCap); // Cập nhật thông tin nhân viên

                MessageBox.Show("Cập nhật thông tin nhân viên thành công!");
                Form1 form  = new Form1();
                form.Show();
                this.Hide();     
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình cập nhật: " + ex.Message);
            }
        }
    }
}
