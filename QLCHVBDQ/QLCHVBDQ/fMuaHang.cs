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
    public partial class fMuaHang : Form
    {
        public fMuaHang()
        {
            InitializeComponent();
            LoadPhieuMH();
        }
        public static string SoPhieu_selected = "Phieu1";
        void LoadPhieuMH()
        {
            textUserName.Text = fLogin.userName;
            dtgvPMH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvPMH.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
            dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
            DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
            dtgvPMH.Columns.Add(IconCol);
            IconCol.Image = Properties.Resources.More_Vertical;
            foreach (DataGridViewColumn item in dtgvPMH.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dtgvPMH.Columns[4].Width = 40;
            dtgvPMH.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void textTimKiem_Enter(object sender, EventArgs e)
        {
            if (textTimKiem.Text == "Tìm kiếm")
            {
                textTimKiem.Text = "";
            }
        }

        private void textTimKiem_Leave(object sender, EventArgs e)
        {
            if (textTimKiem.Text == "")
            {
                textTimKiem.Text = "Tìm kiếm";
            }
        }


        private void dtgvPMH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dtgvPMH.CurrentRow.Selected = true;
                panelMoreOption.Location = dtgvPMH.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                panelMoreOption.Left = panelMoreOption.Left - 50;
                panelMoreOption.Top = Math.Min(panelMoreOption.Top + panelMoreOption.Height + 100, 720);
                panelMoreOption.Visible = true;
                //MessageBox.Show("Bạn đang chọn hàng" + s);
            }
        }

        private void dtgvPBH_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1)
            {
                if (e.ColumnIndex != 0 || e.RowIndex != dtgvPMH.CurrentCell.RowIndex)
                {
                    panelMoreOption.Visible = false;
                }
            }

        }
        private void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvPMH.SelectedRows.Count;
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các phiếu đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvPMH.SelectedRows[i].Index;
                        int colIndex = 1;
                        string SoPhieu = dtgvPMH.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        int temp = PMHDAO.Instance.DeletePMH(SoPhieu);
                        if (temp == 1) data = data + 1;
                    }
                    string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
                    dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} phiếu đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int rowIndex = dtgvPMH.SelectedRows[0].Index;
            string SoPhieu = dtgvPMH.Rows[rowIndex].Cells[1].Value.ToString();
            if (MessageBox.Show("Bạn có thật sự muốn xóa các phiếu đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                panelMoreOption.Visible = false;
                int data = PMHDAO.Instance.DeletePMH(SoPhieu);
                if (data == 1)
                {
                    string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
                    dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
                    dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show("Đã xóa phiếu thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else MessageBox.Show("Xóa phiếu không thành công", "Thông báo", MessageBoxButtons.OK);
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvPMH.SelectedRows[0].Index;
            SoPhieu_selected = dtgvPMH.Rows[rowIndex].Cells[1].Value.ToString();
            fThemCTPMH f = new fThemCTPMH();
            f.ShowDialog();
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
            dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnThemPMH_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            fThemPMH f = new fThemPMH();
            f.ShowDialog();
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
            dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvPMH.SelectedRows[0].Index;
            SoPhieu_selected = dtgvPMH.Rows[rowIndex].Cells[1].Value.ToString();
            fCTPMH f = new fCTPMH();
            f.ShowDialog();
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenNCC as N'Tên nhà cung cấp', TongTien as N'Tổng tiền' from PHIEUMUAHANG, NHACUNGCAP where PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC";
            dtgvPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
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
