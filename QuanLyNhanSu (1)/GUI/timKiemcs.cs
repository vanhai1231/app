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
    public partial class timKiemcs : Form
    {
        private string connectionString;
        private ListView lsvDanhSach;
        public timKiemcs(ListView lsvDanhSach1)
        {
            InitializeComponent();
            connectionString = "Data Source=DESKTOP-FGNUE5N\\SQLEXPRESS;Initial Catalog=QL_NHANSU;Integrated Security=True";
            lsvDanhSach = lsvDanhSach1;
        }

  

        private void button1_Click(object sender, EventArgs e)
        {
            string searchItem = textBox1.Text.Trim();
            string searchColum = null;
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    searchColum = "MANV";
                    break;
                case 1:
                    searchColum = "TenNV";
                    break;
                case 2:
                    searchColum = "SINHNHAT";
                    break;
                case 3: 
                    searchColum = "DIACHI";
                    break;
                case 4:
                    searchColum = "SoDT";
                    break;
                case 5:
                    searchColum = "BANGCAP";
                    break ;
            }
            if (!string.IsNullOrEmpty(searchItem))
            {
                DuLieu dl = new DuLieu(connectionString);
                DataTable dt = dl.timKiemNhanVien(searchItem, searchColum);
                if(dt.Rows.Count > 0)
                {
                    lsvDanhSach.Items.Clear();
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
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin cần tìm kiếm");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
