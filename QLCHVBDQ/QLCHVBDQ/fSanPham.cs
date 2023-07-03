using QLCHVBDQ.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHVBDQ
{
    public partial class fSanPham : Form
    {
        public fSanPham()
        {
            InitializeComponent();
            LoadSP();
        }
        void LoadSP()
        {

            textUserName.Text = fLogin.userName;
            dtgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvSanPham.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select MaSP as N'Mã sảm phẩm', TenSP as N'Tên sản phẩm', MaLSP as N'Mã loại sản phẩm', DonGia as N'Đơn giá', TonKho as N'Tồn kho' from SANPHAM";
            dtgvSanPham.DataSource = DataProvider.Instance.ExecuteQuery(query);
            if (dtgvSanPham.Columns.Count != 6)
            {
                DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
                dtgvSanPham.Columns.Add(IconCol);
                IconCol.Image = Properties.Resources.Trash_Full;
                foreach (DataGridViewColumn item in dtgvSanPham.Columns)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dtgvSanPham.Columns[dtgvSanPham.Columns.Count - 1].Width = 40;
                //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            fThemSP f = new fThemSP();
            f.ShowDialog();
            string query = "select MaSP as N'Mã sảm phẩm', TenSP as N'Tên sản phẩm', MaLSP as N'Mã loại sản phẩm', DonGia as N'Đơn giá', TonKho as N'Tồn kho' from SANPHAM";
            dtgvSanPham.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }


        private void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvSanPham.SelectedRows.Count;
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các sản phẩm đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvSanPham.SelectedRows[i].Index;
                        int colIndex = 1;
                        string MaSP = dtgvSanPham.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        int temp = SanPhamDAO.Instance.DeleteSP_MaSanPham(MaSP);
                        if (temp == 1) data = data + 1;
                    }
                    string query = "select MaSP as N'Mã sảm phẩm', TenSP as N'Tên sản phẩm', MaLSP as N'Mã loại sản phẩm', DonGia as N'Đơn giá', TonKho as N'Tồn kho' from SANPHAM";
                    dtgvSanPham.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} sản phẩm đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void dtgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int rowIndex = e.RowIndex;
                string MaSP = dtgvSanPham.Rows[rowIndex].Cells[1].Value.ToString();
                if (MessageBox.Show("Bạn có thật sự muốn xóa sản phẩm đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = SanPhamDAO.Instance.DeleteSP_MaSanPham(MaSP);
                    if (data != -1)
                    {
                        string query = "select MaSP as N'Mã sảm phẩm', TenSP as N'Tên sản phẩm', MaLSP as N'Mã loại sản phẩm', DonGia as N'Đơn giá', TonKho as N'Tồn kho' from SANPHAM";
                        dtgvSanPham.DataSource = DataProvider.Instance.ExecuteQuery(query);
                        MessageBox.Show("Xóa sản phẩm thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show("Xóa sản phẩm không thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        public static implicit operator fSanPham(fDichVu v)
        {
            throw new NotImplementedException();
        }
        private void btnDichVu_Click(object sender, EventArgs e)
        {
            fDichVu f = new fDichVu();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnLoaiSanPham_Click(object sender, EventArgs e)
        {
            fLoaiSanPham f = new fLoaiSanPham();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnLoaiDichVu_Click(object sender, EventArgs e)
        {
            fLoaiDichVu f = new fLoaiDichVu();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnMuaHang_Click(object sender, EventArgs e)
        {
            fMuaHang f = new fMuaHang();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            fBanHang f = new fBanHang();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            fNhaCungCap f = new fNhaCungCap();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            fBaoCao f = new fBaoCao();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            fTrangChu f = new fTrangChu();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnThongTinTaiKhoan_Click(object sender, EventArgs e)
        {
            panelTTTK.Visible = false;
            fThongTinTaiKhoan f = new fThongTinTaiKhoan();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void buttonDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn đăng xuất?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
            }

        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (panelTTTK.Visible == true) panelTTTK.Visible = false;
            else panelTTTK.Visible = true;
        }

        private void dtgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
