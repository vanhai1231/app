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
    public partial class SuaThongTin : Form
    {
        private string connectionString;
        private int maNhanVien;
        public SuaThongTin()
        {
            InitializeComponent();
            connectionString = "Data Source=DESKTOP-FGNUE5N\\SQLEXPRESS;Initial Catalog=QL_NHANSU;Integrated Security=True";
        }

        //xác nhận
        private void button1_Click(object sender, EventArgs e)
        {
            string MANV = textBox1.Text;
            DuLieu dl = new DuLieu(connectionString);
            if (dl.checkMaNV(MANV))
            {
                Sua s = new Sua(MANV);
                s.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Mã nhân viên không tồn tại");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void SuaThongTin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}
