using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Q1
{
    public partial class FormSanPham : Form
    {
        public FormSanPham()
        {
            InitializeComponent();
        }

        private void FormSanPham_Load(object sender, EventArgs e)
        {
            TreeNode thucpham = new TreeNode("Thực phẩm");
            TreeNode douong = new TreeNode("Đồ uống");
            TreeNode giavi = new TreeNode("Gia vị");
            TreeNode dogiadung = new TreeNode("Đồ gia dụng");

            tvDanhMuc.Nodes.Add(thucpham);
            tvDanhMuc.Nodes.Add(douong);
            tvDanhMuc.Nodes.Add(giavi);
            tvDanhMuc.Nodes.Add(dogiadung);

            lvSanPham.Columns.Add("Mã", 80);
            lvSanPham.Columns.Add("Tên sản phẩm", 200);
            lvSanPham.Columns.Add("Giá", 100);
            lvSanPham.Columns.Add("Tồn kho", 80);
        }

        private void tvDanhMuc_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Thực phẩm")
            {
                lvSanPham.Items.Add(new ListViewItem(new string[] { "TP01", "Gạo", "18000", "120" }));
                lvSanPham.Items.Add(new ListViewItem(new string[] { "TP02", "Mì gói", "4000", "250" }));
            }
            else if (e.Node.Text == "Đồ uống")
            {
                lvSanPham.Items.Add(new ListViewItem(new string[] { "DU01", "Coca-Cola", "10000", "60" }));
                lvSanPham.Items.Add(new ListViewItem(new string[] { "DU02", "Trà xanh 0 độ", "8000", "80" }));
            }
            else if (e.Node.Text == "Gia vị")
            {
                lvSanPham.Items.Add(new ListViewItem(new string[] { "GV01", "Muối", "5000", "100" }));
                lvSanPham.Items.Add(new ListViewItem(new string[] { "GV02", "Nước mắm", "25000", "40" }));
            }
            else if (e.Node.Text == "Đồ gia dụng")
            {
                lvSanPham.Items.Add(new ListViewItem(new string[] { "GD01", "Chảo chống dính", "150000", "20" }));
                lvSanPham.Items.Add(new ListViewItem(new string[] { "GD02", "Bình nước", "80000", "50" }));
            }
        }

        private void tsXemCT_Click(object sender, EventArgs e)
        {
            if (lvSanPham.SelectedItems.Count > 0)
            {
                var item = lvSanPham.SelectedItems[0];
                MessageBox.Show($"Mã: {item.SubItems[0].Text}\nTên: {item.SubItems[1].Text}\nGiá: {item.SubItems[2].Text}\nTồn: {item.SubItems[3].Text}",
                                "Chi tiết sản phẩm");
            }
        }

        private void tsXoaSP_Click(object sender, EventArgs e)
        {
            if (lvSanPham.SelectedItems.Count > 0)
            {
                var item = lvSanPham.SelectedItems[0];
                if (MessageBox.Show($"Xóa sản phẩm {item.SubItems[1].Text}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lvSanPham.Items.Remove(item);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            sfd.FileName = "baocao.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    // Ghi dòng tiêu đề
                    sw.WriteLine("Ma,Ten,Gia,Ton");

                    // Ghi từng dòng dữ liệu
                    foreach (ListViewItem item in lvSanPham.Items)
                    {
                        sw.WriteLine(string.Join(",", new string[]
                        {
                    item.SubItems[0].Text,
                    item.SubItems[1].Text,
                    item.SubItems[2].Text,
                    item.SubItems[3].Text
                        }));
                    }
                }

                MessageBox.Show("Đã export thành công!");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lvSanPham.Items.Clear(); // Xóa dữ liệu cũ

                var lines = File.ReadAllLines(ofd.FileName);

                // Bỏ qua dòng tiêu đề (nếu có)
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');
                    if (parts.Length >= 4)
                        lvSanPham.Items.Add(new ListViewItem(parts));
                }

                MessageBox.Show("Đã import thành công " + (lines.Length - 1) + " sản phẩm!");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (lvSanPham.Items.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm để mô phỏng!");
                return;
            }

            progressBar1.Minimum = 0;
            progressBar1.Maximum = lvSanPham.Items.Count;
            progressBar1.Value = 0;

            for (int i = 0; i < lvSanPham.Items.Count; i++)
            {
                // Mô phỏng xử lý 1 sản phẩm
                await Task.Delay(1000); // Dừng 1 giây
                progressBar1.Value = i + 1;
                lblPhanTram.Text = ((i + 1) * 100 / lvSanPham.Items.Count).ToString() + " %";
                Application.DoEvents();
            }

            MessageBox.Show("Mô phỏng Export hoàn tất!");
        }
    }

}
