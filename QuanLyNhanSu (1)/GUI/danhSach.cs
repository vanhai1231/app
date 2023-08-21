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
    public partial class danhSach : Form
    {
        private string connectionString;
        public danhSach()
        {
            InitializeComponent();
            connectionString = "Data Source=DESKTOP-FGNUE5N\\SQLEXPRESS;Initial Catalog=QL_NHANSU;Integrated Security=True";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timKiemcs timKiemcs = new timKiemcs(lsvDanhSach);
            timKiemcs.ShowDialog();
            
        }

        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void hienThongTin()
        {
            lsvDanhSach.Items.Clear();
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
                lsvDanhSach.Items.Add(item);
            }

        }

        private void danhSach_Load(object sender, EventArgs e)
        {
            hienThongTin(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();    
        }

        private void danhSach_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}
