using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DuLieu
    {

        public string connectionString = "Data Source=DESKTOP-FGNUE5N\\SQLEXPRESS;Initial Catalog=QL_NHANSU;Integrated Security=True";

        public DuLieu(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool KiemTraTenDangNhapTonTai(string tenDangNhap)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT COUNT(*) FROM THONGTINTK WHERE TENDANGNHAP = @TENDANGNHAP";
                    cmd.Parameters.AddWithValue("@TENDANGNHAP", tenDangNhap);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public void themNguoiDung(string ho, string ten, string gioitinh, string ngaysinh, string diachi)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO NGUOIDUNG(HO,TEN,GIOITINH,NGAYSINH,DIACHI) VALUES(@HO,@TEN,@GIOITINH,@NGAYSINH,@DIACHI)";
                    cmd.Parameters.AddWithValue("@HO", ho);
                    cmd.Parameters.AddWithValue("@TEN", ten);
                    cmd.Parameters.AddWithValue("@GIOITINH", gioitinh);
                    cmd.Parameters.AddWithValue("@NGAYSINH", ngaysinh);
                    cmd.Parameters.AddWithValue("@DIACHI", diachi);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void ThemThongTinTK(string tenDangNhap, string matKhau, string Email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO THONGTINTK (TENDANGNHAP, MATKHAU,EMAIL) VALUES (@TENDANGNHAP, @MATKHAU,@EMAIL)";
                    cmd.Parameters.AddWithValue("TENDANGNHAP", tenDangNhap);
                    cmd.Parameters.AddWithValue("MATKHAU", matKhau);
                    cmd.Parameters.AddWithValue("EMAIL", Email);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool kiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT COUNT(*) FROM THONGTINTK WHERE TENDANGNHAP = @TENDANGNHAP AND MATKHAU = @MATKHAU";
                    cmd.Parameters.AddWithValue("@TENDANGNHAP", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MATKHAU", matKhau);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public DataTable layDuLieu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM NHANVIEN", conn))
                {
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    return dt;
                }
            }
        }
        public void themNhanVien(int MANV, string tenNV, DateTime SN, string DC, string SDT, string bangCap)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO NHANVIEN (MANV,TenNV, SINHNHAT, DIACHI, SoDT, BANGCAP) VALUES (@MANV, @TenNV, @SINHNHAT, @DIACHI, @SoDT, @BANGCAP) ", conn))
                {
                    cmd.Parameters.AddWithValue("@MANV", MANV);
                    cmd.Parameters.AddWithValue("@TenNV", tenNV);
                    cmd.Parameters.AddWithValue("@SINHNHAT", SN);
                    cmd.Parameters.AddWithValue("@DIACHI", DC);
                    cmd.Parameters.AddWithValue("@SoDT", SDT);
                    cmd.Parameters.AddWithValue("@BANGCAP", bangCap);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void xoaNhanVien(int MANV) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM NHANVIEN WHERE MANV = @MANV", conn))
                {
                    cmd.Parameters.AddWithValue("@MANV", MANV);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool kiemTraMANV(int MANV) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM NHANVIEN WHERE MANV = @MANV", conn))
                {
                    cmd.Connection= conn;
                    cmd.CommandText = "SELECT COUNT(*) FROM NHANVIEN WHERE MANV = @MANV";
                    cmd.Parameters.AddWithValue("@MANV", MANV);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        public DataTable timKiemNhanVien(string searchItem, string searchColum)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $"SELECT * FROM NHANVIEN WHERE {searchColum} = @searchItem";
                    cmd.Parameters.AddWithValue("@searchItem", searchItem);
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) 
                    { 
                        da.Fill(dt); 
                    }
                    return dt;
                }
            }
        }

        public bool checkMaNV(string MANV)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = " SELECT COUNT(*) FROM NHANVIEN WHERE MANV = @MANV";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MANV", MANV);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public void capNhatThongTinNhanVien(string maNhanVien, string tenNV, DateTime ngaySinh, string diaChi, string SoDT, string bangCap)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE NHANVIEN SET TenNV = @TenNV, SINHNHAT = @SINHNHAT, DiaChi = @DiaChi, SoDT = @SoDT, BangCap = @BangCap WHERE MaNV = @MaNV";

                    cmd.Parameters.AddWithValue("@MaNV", maNhanVien);
                    cmd.Parameters.AddWithValue("@TenNV", tenNV);
                    cmd.Parameters.AddWithValue("@SINHNHAT", ngaySinh);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    cmd.Parameters.AddWithValue("@SoDT", SoDT);
                    cmd.Parameters.AddWithValue("@BangCap", bangCap);

                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
