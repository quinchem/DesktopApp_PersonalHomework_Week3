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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public string TenNguoiDung = "";
        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuDangNhap_Click(object sender, EventArgs e)
        {
            FormDangNhap f = new FormDangNhap();
            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblThoiGian.Text = "Thời gian: "+ DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        }

        private void lblNguoiDung_Click(object sender, EventArgs e)
        {

        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            lblNguoiDung.Text = "Người dùng: Chưa đăng nhập";
            MessageBox.Show("Bạn đã đăng xuất khỏi hệ thống!");
        }



        private void tsDangNhap_Click(object sender, EventArgs e)
        {
            FormDangNhap f = new FormDangNhap();
            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
        }

        private void tsDangXuat_Click(object sender, EventArgs e)
        {
            lblNguoiDung.Text = "Người dùng: Chưa đăng nhập";
            MessageBox.Show("Bạn đã đăng xuất khỏi hệ thống!");
        }

        private void tsNhanVien_Click(object sender, EventArgs e)
        {
            FormNhanVien f = new FormNhanVien();
            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
            lblFormHienTai.Text = "Form hiện tại: Nhân viên";
        }

        private void menuNhanVien_Click(object sender, EventArgs e)
        {
            FormNhanVien f = new FormNhanVien();
            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
            lblFormHienTai.Text = "Form hiện tại: Nhân viên";
        }

        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            FormSanPham f = new FormSanPham();
            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
            lblFormHienTai.Text = "Form hiện tại: Sản phẩm";
        }

        private void tsSanPham_Click(object sender, EventArgs e)
        {
            FormSanPham f = new FormSanPham();
            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
            lblFormHienTai.Text = "Form hiện tại: Sản phẩm";
        }
        private void FormMain_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
                lblFormHienTai.Text = "Form hiện tại: " + this.ActiveMdiChild.Text;
            else
                lblFormHienTai.Text = "Form hiện tại: Chưa mở";
        }

        private void tsThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            FormHoaDon f = new FormHoaDon();
            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
            lblFormHienTai.Text = "Form hiện tại: Hóa đơn";
        }

        private void mnuHoaDon_Click(object sender, EventArgs e)
        {
            FormHoaDon f = new FormHoaDon();
            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
            lblFormHienTai.Text = "Form hiện tại: Hóa đơn";
        }
    }

}
