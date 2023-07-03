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
    public partial class fNhaCungCap : Form
    {
        public fNhaCungCap()
        {
            InitializeComponent();
            LoadNCC();
        }
        void LoadNCC()
        {

            textUserName.Text = fLogin.userName;
            dtgvNCC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvNCC.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select MaNCC as N'Mã nhà cung cấp', TenNCC as N'Tên nhà cung cấp', DiaChi as N'Địa chỉ', SDT as N'SDT' from NHACUNGCAP";
            dtgvNCC.DataSource = DataProvider.Instance.ExecuteQuery(query);
            if (dtgvNCC.Columns.Count != 5)
            {
                DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
                dtgvNCC.Columns.Add(IconCol);
                IconCol.Image = Properties.Resources.Trash_Full;
                foreach (DataGridViewColumn item in dtgvNCC.Columns)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dtgvNCC.Columns[dtgvNCC.Columns.Count - 1].Width = 40;
                //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            fThemNCC f = new fThemNCC();
            f.ShowDialog();
            string query = "select MaNCC as N'Mã nhà cung cấp', TenNCC as N'Tên nhà cung cấp', DiaChi as N'Địa chỉ', SDT as N'SDT' from NHACUNGCAP";
            dtgvNCC.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvNCC.SelectedRows.Count;
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các nhà cung cấp đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvNCC.SelectedRows[i].Index;
                        int colIndex = 1;
                        string MaNCC = dtgvNCC.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        int temp = NhaCungCapDAO.Instance.Delete_NCC(MaNCC);
                        if (temp == 1) data = data + 1;
                    }
                    string query = "select MaNCC as N'Mã nhà cung cấp', TenNCC as N'Tên nhà cung cấp', DiaChi as N'Địa chỉ', SDT as N'SDT' from NHACUNGCAP";
                    dtgvNCC.DataSource = DataProvider.Instance.ExecuteQuery(query);
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
                string MaNCC = dtgvNCC.Rows[rowIndex].Cells[1].Value.ToString();
                if (MessageBox.Show("Bạn có thật sự muốn xóa nhà cung cấp đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = NhaCungCapDAO.Instance.Delete_NCC(MaNCC);
                    if (data != -1)
                    {
                        string query = "select MaNCC as N'Mã nhà cung cấp', TenNCC as N'Tên nhà cung cấp', DiaChi as N'Địa chỉ', SDT as N'SDT' from NHACUNGCAP";
                        dtgvNCC.DataSource = DataProvider.Instance.ExecuteQuery(query);
                        MessageBox.Show("Đã xóa nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show("Xóa nhà cung cấp không thành công", "Thông báo", MessageBoxButtons.OK);
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
        private void btnLoaiSanPham_Click(object sender, EventArgs e)
        {
            fLoaiSanPham f = new fLoaiSanPham();
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
