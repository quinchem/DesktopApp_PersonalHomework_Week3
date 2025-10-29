using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Q1
{
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "admin" && txtMatKhau.Text == "123")
            {
                // Lấy form cha MDI
                Form1 fmain = (Form1)this.MdiParent;

                // Lưu tên người dùng vào biến
                fmain.TenNguoiDung = txtTenDangNhap.Text;

                // Cập nhật StatusStrip
                fmain.lblNguoiDung.Text = "Người dùng: " + fmain.TenNguoiDung;

                MessageBox.Show("Đăng nhập thành công!");
                this.Close(); // Đóng form đăng nhập
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
            }
        }
    }
}
