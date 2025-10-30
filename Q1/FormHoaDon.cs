using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Q1
{
    public partial class FormHoaDon : Form
    {

        private DataTable dtHoaDon;
        private decimal tongTien = 0;
        private static int SoThuTu = 0;

        public FormHoaDon()
        {
            InitializeComponent();
        }

       rivate void FormHoaDon_Load(object sender, EventArgs e)
        {
            // Khởi tạo bảng dữ liệu chi tiết hóa đơn
            dtHoaDon = new DataTable();
            dtHoaDon.Columns.Add("Tên sản phẩm");
            dtHoaDon.Columns.Add("Số lượng", typeof(int));
            dtHoaDon.Columns.Add("Giá", typeof(decimal));
            dtHoaDon.Columns.Add("Thành tiền", typeof(decimal));

            dgvHoaDon.DataSource = dtHoaDon;

            // Cấu hình bảng
            dgvHoaDon.ReadOnly = true;
            dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoaDon.MultiSelect = false;

            // Tự động cập nhật tổng tiền
            CapNhatTongTien();
            cboSanPham.Items.Clear();

            foreach (var sp in FormSanPham.DanhSachSanPham)
            {
                cboSanPham.Items.Add(sp.Item1); 
            }

            cboSanPham.SelectedIndex = -1;
            txtMaHD.Text = TaoMaHoaDon();
        }
        //Tạo mã hóa đơn tự động
        private string TaoMaHoaDon()
        {
            string prefix = "HD";
            string date = DateTime.Now.ToString("yyyyMMdd");

            // Nếu ngày đã đổi, reset số thứ tự
            if (SoThuTu == 999) SoThuTu = 0;
            SoThuTu++;

            string stt = SoThuTu.ToString("D3"); // Đệm 0 thành 3 chữ số
            return $"{prefix}{date}-{stt}";
        }

        // Thêm sản phẩm vào hóa đơn
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string tenSP = cboSanPham.Text;
                int soLuong = int.Parse(numSL.Text);
                decimal gia = decimal.Parse(txtGia.Text);
                decimal thanhTien = soLuong * gia;

                dtHoaDon.Rows.Add(tenSP, soLuong, gia, thanhTien);
                CapNhatTongTien();
                ClearInput();
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng dữ liệu!", "Lỗi");
            }
        }

        // Xóa dòng sản phẩm
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow != null)
            {
                dgvHoaDon.Rows.RemoveAt(dgvHoaDon.CurrentRow.Index);
                CapNhatTongTien();
            }
        }

        // Sửa sản phẩm trong hóa đơn
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow != null)
            {
                DataGridViewRow row = dgvHoaDon.CurrentRow;
                row.Cells["Tên sản phẩm"].Value = cboSanPham.Text;
                row.Cells["Số lượng"].Value = numSL.Text;
                row.Cells["Giá"].Value = txtGia.Text;

                // Tính lại thành tiền
                decimal sl = decimal.Parse(numSL.Text);
                decimal g = decimal.Parse(txtGia.Text);
                row.Cells["Thành tiền"].Value = sl * g;

                CapNhatTongTien();
                MessageBox.Show("Đã cập nhật sản phẩm trong hóa đơn!");
            }
        }

        // Khi click chọn sản phẩm trong bảng
        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                cboSanPham.Text = dgvHoaDon.Rows[e.RowIndex].Cells["Tên sản phẩm"].Value.ToString();
                numSL.Text = dgvHoaDon.Rows[e.RowIndex].Cells["Số lượng"].Value.ToString();
                txtGia.Text = dgvHoaDon.Rows[e.RowIndex].Cells["Giá"].Value.ToString();
            }
        }
        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sp = FormSanPham.DanhSachSanPham
                       .FirstOrDefault(x => x.Item1 == cboSanPham.Text);

            if (sp != null)
                txtGia.Text = sp.Item2.ToString("N0");
        }
        // Tính tổng tiền hóa đơn
        private void CapNhatTongTien()
        {
            tongTien = 0;
            foreach (DataRow r in dtHoaDon.Rows)
                tongTien += Convert.ToDecimal(r["Thành tiền"]);

            lblTongTien.Text = "Tổng tiền: " + tongTien.ToString("N0") + " VND";
        }

        private void ClearInput()
        {
            cboSanPham.SelectedIndex = -1;
            numSL.Value=0;
            txtGia.Clear();
        }

        // ====== PHẦN IN HÓA ĐƠN ======
        private void btnIn_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font fTitle = new Font("Arial", 16, FontStyle.Bold);
            Font fNormal = new Font("Arial", 12);
            Brush b = Brushes.Black;

            int y = 100;

            // Tiêu đề cửa hàng
            e.Graphics.DrawString("CỬA HÀNG MINI MART", fTitle, Brushes.DarkRed, 200, y);
            y += 40;
            e.Graphics.DrawString("HÓA ĐƠN BÁN HÀNG", fTitle, b, 220, y);
            y += 40;

            e.Graphics.DrawString("Mã hóa đơn: " + txtMaHD.Text, fNormal, b, 50, y);
            y += 25;
            e.Graphics.DrawString("Khách hàng: " + txtTenKH.Text, fNormal, b, 50, y);
            y += 25;
            e.Graphics.DrawString("Ngày lập: " + dtpNgayLap.Value.ToString("dd/MM/yyyy HH:mm"), fNormal, b, 50, y);
            y += 40;

            e.Graphics.DrawString("Tên sản phẩm: ", fNormal, b, 50, y);
            e.Graphics.DrawString("Số lượng: ", fNormal, b, 250, y);
            e.Graphics.DrawString("Giá: ", fNormal, b, 320, y);
            e.Graphics.DrawString("Thành tiền: ", fNormal, b, 420, y);
            y += 25;

            foreach (DataRow r in dtHoaDon.Rows)
            {
                e.Graphics.DrawString(r["Tên sản phẩm"].ToString(), fNormal, b, 50, y);
                e.Graphics.DrawString(r["Số lượng"].ToString(), fNormal, b, 250, y);
                e.Graphics.DrawString(r["Giá"].ToString(), fNormal, b, 320, y);
                e.Graphics.DrawString(r["Thành tiền"].ToString(), fNormal, b, 420, y);
                y += 25;
            }

            y += 30;
            e.Graphics.DrawString(lblTongTien.Text, fTitle, Brushes.Red, 50, y);
            y += 40;
            e.Graphics.DrawString("Ngày in: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), fNormal, b, 50, y);
        }

    }
}

