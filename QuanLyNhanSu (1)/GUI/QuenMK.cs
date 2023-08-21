using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Net.Mime.MediaTypeNames;

namespace GUI
{
    public partial class QuenMK : Form
    {
        private string connectionString;
        public QuenMK()
        {
            InitializeComponent();
            connectionString = "Data Source=DESKTOP-FGNUE5N\\SQLEXPRESS;Initial Catalog=QL_NHANSU;Integrated Security=True";
        }

        private void QuenMK_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                dangNhap dn = new dangNhap();
                dn.Show();
                this.Hide();
            }
            else
            {
                e.Cancel = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                dangNhap dn = new dangNhap();
                dn.Show();
                this.Hide();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string Email = txt_Email.Text;
            if (string.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Vui lòng điền Email.");
                return;
            }
            // Thực hiện lưu thông tin vào cơ sở dữ liệu
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Kiểm tra xem Email có tồn tại không
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT COUNT(*) FROM THONGTINTK WHERE Email = @Email";
                        cmd.Parameters.AddWithValue("@Email", Email);
                        int EmailCount = Convert.ToInt32(cmd.ExecuteScalar());
                        if (EmailCount == 0)
                        {
                            MessageBox.Show("Email này không tồn tại. Vui lòng nhập lại.");
                            return;
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT tk.TENDANGNHAP, tk.MATKHAU, nd.ho, nd.ten " +
                                          "FROM THONGTINTK tk " +
                                          "INNER JOIN NGUOIDUNG nd ON tk.id = nd.id " +
                                          "WHERE tk.Email = @Email";
                        cmd.Parameters.AddWithValue("@Email", Email);

                        using (SqlDataReader data = cmd.ExecuteReader())
                        {
                            if (data.Read())
                            {
                                string tentk = data["TENDANGNHAP"].ToString();
                                string matkhau = data["MATKHAU"].ToString();
                                string ho = data["HO"].ToString();
                                string ten = data["TEN"].ToString();
                                string EmailNhap = txt_Email.Text;
                                MailMessage message = new MailMessage();
                                SmtpClient smtp = new SmtpClient();

                                // Cập nhật với thông tin tài khoản Gmail mới
                                message.From = new MailAddress("thongbaoquanlynhansu@gmail.com");
                                smtp.Credentials = new NetworkCredential("thongbaoquanlynhansu@gmail.com", "afgliezlwxnlfaer");
                                message.To.Add(new MailAddress(EmailNhap));
                                message.Subject = "Xác nhận thông tin tài khoản";
                                message.Body = $"Xin chào {ho} {ten},\nTên tài khoản của bạn là: {tentk}\nMật khẩu của bạn là: {matkhau}";
                                smtp.Port = 587;
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                smtp.UseDefaultCredentials = false;
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                try
                                {
                                    smtp.Send(message);
                                    MessageBox.Show("Email gửi thành công, hãy kiểm tra Email để lấy thông tin tài khoản!.");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Có lỗi trong quá trình gửi Email: " + ex.Message);
                                }
                            }
                        }
                    }
                    dangNhap f = new dangNhap();
                    f.Show();
                    this.Hide();
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình xác nhận Email: " + ex.Message);
            }

        }
    }
}

