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
    public partial class fLoaiSanPham : Form
    {
        public fLoaiSanPham()
        {
            InitializeComponent();
            LoadLoaiSP();
        }

        void LoadLoaiSP()
        {
            
            textUserName.Text = fLogin.userName;
            textUserName.Text = fLogin.userName;
            dtgvLoaiSP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvLoaiSP.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select MaLSP as N'Mã loại sản phẩm', TenLSP as N'Tên loại sản phẩm', MaDVT as N'Mã đơn vị tính', PhanTramLoiNhuan as N'Phần trăm lợi nhuận' from LOAISANPHAM";
            dtgvLoaiSP.DataSource = DataProvider.Instance.ExecuteQuery(query);
            if (dtgvLoaiSP.Columns.Count != 5)
            {
                DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
                dtgvLoaiSP.Columns.Add(IconCol);
                IconCol.Image = Properties.Resources.Trash_Full;
                foreach (DataGridViewColumn item in dtgvLoaiSP.Columns)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dtgvLoaiSP.Columns[dtgvLoaiSP.Columns.Count - 1].Width = 40;
                //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
        private void btnThemLSP_Click(object sender, EventArgs e)
        {
            fThemLoaiSP f = new fThemLoaiSP();
            f.ShowDialog();
            string query = "select MaLSP as N'Mã loại sản phẩm', TenLSP as N'Tên loại sản phẩm', MaDVT as N'Mã đơn vị tính', PhanTramLoiNhuan as N'Phần trăm lợi nhuận' from LOAISANPHAM";
            dtgvLoaiSP.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvLoaiSP.SelectedRows.Count;
            //MessageBox.Show(String.Format("Bạn đang chọn {0} hàng ", count));            //MessageBox.Show("Bạn đang chọn " + total);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các loại sản phẩm đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvLoaiSP.SelectedRows[i].Index;
                        int colIndex = 1;
                        string MaLSP = dtgvLoaiSP.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        int temp = LoaiSanPhamDAO.Instance.Delete_LSP(MaLSP);
                        if (temp == 1) data = data + 1;
                    }
                    string query = "select MaLSP as N'Mã loại sản phẩm', TenLSP as N'Tên loại sản phẩm', MaDVT as N'Mã đơn vị tính', PhanTramLoiNhuan as N'Phần trăm lợi nhuận' from LOAISANPHAM";
                    dtgvLoaiSP.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} loại sản phẩm đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void dtgvLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int r = e.RowIndex;
            //int c = e.ColumnIndex;
            //MessageBox.Show("Bạn đang chọn hàng" + r + " cột " + c);
            if (e.ColumnIndex == 0)
            {
                //dtgvChiTietPDV.CurrentRow.Selected = true;
                //MessageBox.Show("Bạn đang chọn hàng" + s);
                int rowIndex = e.RowIndex;
                string MLSP = dtgvLoaiSP.Rows[rowIndex].Cells[1].Value.ToString();
                if (MessageBox.Show("Bạn có thật sự muốn xóa loại sản phẩm đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = LoaiSanPhamDAO.Instance.Delete_LSP(MLSP);
                    if (data != -1)
                    {
                        string query = "select MaLSP as N'Mã loại sản phẩm', TenLSP as N'Tên loại sản phẩm', MaDVT as N'Mã đơn vị tính', PhanTramLoiNhuan as N'Phần trăm lợi nhuận' from LOAISANPHAM";
                        dtgvLoaiSP.DataSource = DataProvider.Instance.ExecuteQuery(query);
                        MessageBox.Show("Đã xóa loại sản phẩm thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show("Xóa loại sản phẩm không thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            fTrangChu f = new fTrangChu();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            fSanPham f = new fSanPham();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }
        private void btnDichVu_Click(object sender, EventArgs e)
        {
            fDichVu f = new fDichVu();
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
    }
}
