using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QLCHVBDQ.DAO;

namespace QLCHVBDQ
{
    public partial class fDichVu : Form
    {
        public fDichVu()
        {
            InitializeComponent();
            LoadPhieuDV();            
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

        public static string SoPhieu_selected = "Phieu1";
        void LoadPhieuDV()
        {
            textUserName.Text = fLogin.userName;
            dtgvPDV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvPDV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU";
            dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
            DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
            dtgvPDV.Columns.Add(IconCol);
            IconCol.Image = Properties.Resources.More_Vertical;
            foreach (DataGridViewColumn item in dtgvPDV.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dtgvPDV.Columns[7].Width = 40;
            dtgvPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        } 



        private void dtgvPDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string r = e.RowIndex.ToString();
            string c = e.ColumnIndex.ToString();
            //MessageBox.Show("Bạn đang chọn hàng" + r + " cột " + c);
            if (e.ColumnIndex == 0)
            {
                //dtgvPDV.CurrentRow.Selected = true;
                panelMoreOption.Location = dtgvPDV.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                panelMoreOption.Left = panelMoreOption.Left - 50;
                panelMoreOption.Top = Math.Min(panelMoreOption.Top + panelMoreOption.Height + 100, 820);
                panelMoreOption.Visible = true;
                //MessageBox.Show("Bạn đang chọn hàng" + s);
            }
        }

        private void dtgvPDV_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.ColumnIndex != -1)
            {
                if (e.ColumnIndex != 0 || e.RowIndex != dtgvPDV.CurrentCell.RowIndex)
                {
                    //dtgvPDV.CurrentRow.Selected = false;
                    panelMoreOption.Visible = false;
                }
            }
            
        }
        private void btnDelete(object sender, EventArgs e)
        {
            int count = dtgvPDV.SelectedRows.Count;
            //MessageBox.Show(String.Format("Bạn đang chọn {0} hàng ", count));            //MessageBox.Show("Bạn đang chọn " + total);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các phiếu đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvPDV.SelectedRows[i].Index;
                        int colIndex = 1;
                        string SoPhieu = dtgvPDV.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        int temp = PDVDAO.Instance.DeletePDV(SoPhieu);
                        if (temp == 1) data = data + 1;
                    }
                    string query = "Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU";
                    dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} phiếu đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int rowIndex = dtgvPDV.SelectedRows[0].Index;
            string SoPhieu = dtgvPDV.Rows[rowIndex].Cells[1].Value.ToString();
            if (MessageBox.Show("Bạn có thật sự muốn xóa các phiếu đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                panelMoreOption.Visible = false;
                int data = PDVDAO.Instance.DeletePDV(SoPhieu);
                if (data == 1)
                {
                    if (btnThoatTraCuu.Visible == true)
                    {
                        string query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU where SoPhieu = '{0}'", textBoxSoPhieu.Text);
                        dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    }
                    else
                    {
                        string query = "Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU";
                        dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    }
                    MessageBox.Show("Đã xóa phiếu thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else MessageBox.Show("Xóa phiếu không thành công", "Thông báo", MessageBoxButtons.OK);
            }
                
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvPDV.SelectedRows[0].Index;
            SoPhieu_selected = dtgvPDV.Rows[rowIndex].Cells[1].Value.ToString();
            fThemCTPDV f = new fThemCTPDV();
            f.ShowDialog();
            if(btnThoatTraCuu.Visible == true)
            {
                string query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU where SoPhieu = '{0}'", textBoxSoPhieu.Text);
                dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
            }
            else
            {
                string query = "Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU";
                dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
            }
            
        }

        private void btnThemPDV_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            fThemPDV f = new fThemPDV();
            f.ShowDialog();
            string query = "Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU";
            dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvPDV.SelectedRows[0].Index;
            SoPhieu_selected = dtgvPDV.Rows[rowIndex].Cells[1].Value.ToString();
            fChiTietPDV f = new fChiTietPDV();
            f.ShowDialog();
            if (btnThoatTraCuu.Visible == true)
            {
                string query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU where SoPhieu = '{0}'", textBoxSoPhieu.Text);
                dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
            }
            else
            {
                string query = "Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU";
                dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
            }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(comboBoxTieuChi.Text == "Tiêu chí")
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm");
            }
            else
            {
                if (textBoxSoPhieu.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập đúng giá trị tìm kiếm");
                }
                else
                {

                    textDsPDV.Text = "Kết quả tra cứu phiếu dịch vụ";
                    pictureBox4.Visible = false;
                    btnThemPDV.Visible = false;
                    string query;

                    if (comboBoxTieuChi.Text == "Số phiếu")
                    {
                        query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU where SoPhieu = '{0}'", textBoxSoPhieu.Text);
                    }
                    else if ((comboBoxTieuChi.Text == "Ngày lập"))
                    {
                        query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU where NgayLap = '{0}'", textBoxSoPhieu.Text);
                    }
                    else
                    {
                        query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU where TinhTrang = '{0}'", textBoxSoPhieu.Text);
                    }

                    dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    btnThoatTraCuu.Visible = true;                       
                }
            }
        }

        private void btnThoatTraCuu_Click(object sender, EventArgs e)
        {
            textDsPDV.Text = "Danh sách phiếu dịch vụ";
            pictureBox4.Visible = true;
            btnThemPDV.Visible = true;
            btnThoatTraCuu.Visible = false;

            string query = String.Format("Select SoPhieu as N'Số phiếu', NgayLap as N'Ngày lập', MaKH as N'Mã khách hàng', TongTien as 'Tổng tiền', TongTraTruoc as N'Tổng trả trước', TongConLai as N'Tổng còn lại', TinhTrang as N'Tình trạng' from PHIEUDICHVU");

            
            dtgvPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
  