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
    public partial class FormNhanVien : Form
    {
        private DataTable dtNhanVien;

        public FormNhanVien()
        {
            InitializeComponent();
        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            dtNhanVien = new DataTable();
            dtNhanVien.Columns.Add("Mã NV");
            dtNhanVien.Columns.Add("Tên NV");
            dtNhanVien.Columns.Add("Chức vụ");
            dtNhanVien.Columns.Add("Lương");

            dgvNhanVien.DataSource = dtNhanVien;

            dgvNhanVien.ReadOnly = true;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.MultiSelect = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            foreach (DataRow r in dtNhanVien.Rows)
            {
                if (r["Mã NV"].ToString() == txtMaNV.Text)
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại!");
                    return;
                }
            }

            dtNhanVien.Rows.Add(txtMaNV.Text, txtTenNV.Text, txtChucVu.Text, txtLuong.Text);
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtChucVu.Clear();
            txtLuong.Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow != null)
            {
                dgvNhanVien.Rows.RemoveAt(dgvNhanVien.CurrentRow.Index);
                txtMaNV.Clear();
                txtTenNV.Clear();
                txtChucVu.Clear();
                txtLuong.Clear();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow != null)
            {
                DataGridViewRow row = dgvNhanVien.CurrentRow;
                row.Cells["Mã NV"].Value = txtMaNV.Text;
                row.Cells["Tên NV"].Value = txtTenNV.Text;
                row.Cells["Chức vụ"].Value = txtChucVu.Text;
                row.Cells["Lương"].Value = txtLuong.Text;
                MessageBox.Show("Đã cập nhật thông tin!");
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaNV.Text = dgvNhanVien.Rows[e.RowIndex].Cells["Mã NV"].Value.ToString();
                txtTenNV.Text = dgvNhanVien.Rows[e.RowIndex].Cells["Tên NV"].Value.ToString();
                txtChucVu.Text = dgvNhanVien.Rows[e.RowIndex].Cells["Chức vụ"].Value.ToString();
                txtLuong.Text = dgvNhanVien.Rows[e.RowIndex].Cells["Lương"].Value.ToString();
            }
        }
    }
}
