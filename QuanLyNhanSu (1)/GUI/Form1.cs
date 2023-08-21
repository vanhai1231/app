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
    public partial class Form1 : Form
    {
        private string connectionString;
        public bool kt;
        public Form1()
        {
            InitializeComponent();
            connectionString = "Data Source=DESKTOP-FGNUE5N\\SQLEXPRESS;Initial Catalog=QL_NHANSU;Integrated Security=True";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                dangNhap dn = new dangNhap();
                dn.Show();
                this.Hide();

            }
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.ExitThread();

            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtHoten.Enabled = true;
            txtMa.Enabled = true;
            txtDiaChi.Enabled = true;
            txtDienThoai.Enabled = true;
            dtpNgaySinh.Enabled = true;
            cboBangCap.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThem.Enabled = false;
            kt = true;
        }

        private void hienThongTin()
        {
            lsvThongTin.Items.Clear();
            DuLieu dl = new DuLieu(connectionString);
            DataTable dt = dl.layDuLieu();
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row["MANV"].ToString());
                item.SubItems.Add(row["TenNV"].ToString());
                item.SubItems.Add(row["SINHNHAT"].ToString());
                item.SubItems.Add(row["DIACHI"].ToString());
                item.SubItems.Add(row["SoDT"].ToString());
                item.SubItems.Add(row["BANGCAP"].ToString());
                lsvThongTin.Items.Add(item);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hienThongTin();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lsvThongTin.SelectedItems.Count > 0)
            {
                int MANV = int.Parse(lsvThongTin.SelectedItems[0].Text);
                string TENNV = lsvThongTin.SelectedItems[0].SubItems[1].Text;
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên {MANV} {TENNV}", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DuLieu dl = new DuLieu(connectionString);
                    dl.xoaNhanVien(MANV);
                    hienThongTin();
                    MessageBox.Show("Xóa nhân viên thành công");
                } 
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(kt == true)
            {
                try
                {
                    int MANV = int.Parse(txtMa.Text);
                    string tenNV = txtHoten.Text;
                    DateTime SN = dtpNgaySinh.Value;
                    string DC = txtDiaChi.Text;
                    string SDT = txtDienThoai.Text;
                    string bangCap = cboBangCap.Text;
                    DuLieu dl = new DuLieu(connectionString);
                    if (dl.kiemTraMANV(MANV))
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại vui lòng nhập mã khác.");
                        return;
                    }

                    dl.themNhanVien(MANV, tenNV, SN, DC, SDT, bangCap);
                    txtMa.Clear();
                    txtDiaChi.Clear();
                    txtDienThoai.Clear();
                    txtHoten.Clear();
                    cboBangCap.SelectedIndex = -1;
                    dtpNgaySinh.Value = DateTime.Now;
                    txtHoten.Enabled = false;
                    txtMa.Enabled = false;
                    txtDiaChi.Enabled = false;
                    txtDienThoai.Enabled = false;
                    dtpNgaySinh.Enabled = false;
                    cboBangCap.Enabled = false;
                    btnThem.Enabled = true;
                    btnLuu.Enabled = false;
                    btnHuy.Enabled = false;   
                    hienThongTin();
                    MessageBox.Show("Thêm nhân viên thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi trong quá trình thêm nhân viên: " + ex.Message);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMa.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtHoten.Clear();
            cboBangCap.SelectedIndex = -1;
            dtpNgaySinh.Value = DateTime.Now;
            txtHoten.Enabled = false;
            txtMa.Enabled = false;
            txtDiaChi.Enabled = false;
            txtDienThoai.Enabled = false;
            dtpNgaySinh.Enabled = false;
            cboBangCap.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            danhSach ds = new danhSach();
            ds.Show();
            this.Hide();
        }

        private void lsvThongTin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SuaThongTin st = new SuaThongTin();
            st.Show();
            this.Hide();
        }

        public void enableControl(bool enable)
        {
            txtHoten.Enabled = enable;
            txtDiaChi.Enabled = enable;
            txtDienThoai.Enabled = enable;
            dtpNgaySinh.Enabled = enable;
            cboBangCap.Enabled = enable;
            btnLuu.Enabled = enable;
            btnHuy.Enabled = enable;
        }
    }
}
