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
    public partial class fBanHang : Form
    {
        public fBanHang()
        {
            InitializeComponent();
            LoadPhieuBH();
        }
        public static string SoPhieu_selected = "Phieu1";
        void LoadPhieuBH()
        {
            textUserName.Text = fLogin.userName;
            dtgvPBH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvPBH.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
            dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
            DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
            dtgvPBH.Columns.Add(IconCol);
            IconCol.Image = Properties.Resources.More_Vertical;
            foreach (DataGridViewColumn item in dtgvPBH.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dtgvPBH.Columns[4].Width = 40;
            dtgvPBH.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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


        private void dtgvPBH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string r = e.RowIndex.ToString();
            string c = e.ColumnIndex.ToString();
            //MessageBox.Show("Bạn đang chọn hàng" + r + " cột " + c);
            if (e.ColumnIndex == 0)
            {
                //dtgvPBH.CurrentRow.Selected = true;
                panelMoreOption.Location = dtgvPBH.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
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
                if (e.ColumnIndex != 0 || e.RowIndex != dtgvPBH.CurrentCell.RowIndex)
                {
                    panelMoreOption.Visible = false;
                }
            }

        }
        private void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvPBH.SelectedRows.Count;
            //MessageBox.Show(String.Format("Bạn đang chọn {0} hàng ", count));            //MessageBox.Show("Bạn đang chọn " + total);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các phiếu đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvPBH.SelectedRows[i].Index;
                        int colIndex = 1;
                        string SoPhieu = dtgvPBH.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        int temp = PBHDAO.Instance.DeletePBH(SoPhieu);
                        if (temp == 1) data = data + 1;
                    }
                    string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
                    dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} phiếu đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int rowIndex = dtgvPBH.SelectedRows[0].Index;
            string SoPhieu = dtgvPBH.Rows[rowIndex].Cells[1].Value.ToString();
            if (MessageBox.Show("Bạn có thật sự muốn xóa các phiếu đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                panelMoreOption.Visible = false;
                int data = PBHDAO.Instance.DeletePBH(SoPhieu);
                if (data == 1)
                {
                    string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
                    dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
                    dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show("Đã xóa phiếu thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else MessageBox.Show("Xóa phiếu không thành công", "Thông báo", MessageBoxButtons.OK);
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvPBH.SelectedRows[0].Index;
            SoPhieu_selected = dtgvPBH.Rows[rowIndex].Cells[1].Value.ToString();
            fThemCTPBH f = new fThemCTPBH();
            f.ShowDialog();
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
            dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnThemPBH_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            fThemPBH f = new fThemPBH();
            f.ShowDialog();
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
            dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvPBH.SelectedRows[0].Index;
            SoPhieu_selected = dtgvPBH.Rows[rowIndex].Cells[1].Value.ToString();
            fCTPBH f = new fCTPBH();
            f.ShowDialog();
            string query = "select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', TenKH as N'Tên khách hàng', TongTien as N'Tổng tiền' from PHIEUBANHANG, KHACHHANG where PHIEUBANHANG.MaKH = KHACHHANG.MaKH";
            dtgvPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
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
