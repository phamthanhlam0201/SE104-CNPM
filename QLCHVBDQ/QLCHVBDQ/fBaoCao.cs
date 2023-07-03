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
    public partial class fBaoCao : Form
    {
        public fBaoCao()
        {
            InitializeComponent();
            LoadBaoCao();
        }
        public static string Thang_selected = "1";
        public static string Nam_selected = "2023";

        public void LoadBaoCao()
        {
            textUserName.Text = fLogin.userName;
            nUDThang.Text = DateTime.Now.Month.ToString();
            nUDNam.Text = DateTime.Now.Year.ToString();
            dtgvBCTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvBCTonKho.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select Thang as N'Tháng', Nam as N'Năm' from BCTONKHO group by Thang, Nam";
            dtgvBCTonKho.DataSource = DataProvider.Instance.ExecuteQuery(query);
            DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
            dtgvBCTonKho.Columns.Add(IconCol);
            IconCol.Image = Properties.Resources.More_Vertical;
            foreach (DataGridViewColumn item in dtgvBCTonKho.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dtgvBCTonKho.Columns[2].Width = 40;
            dtgvBCTonKho.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        private void btnThemBCTK_Click(object sender, EventArgs e)
        {
            string Thang = nUDThang.Text;
            string Nam = nUDNam.Text;
            string query = String.Format("select * from BAOCAO where Thang = {0} and Nam = {1}", Thang, Nam);
            DataTable check = DataProvider.Instance.ExecuteQuery(query);
            if (check.Rows.Count > 0)
            {
                MessageBox.Show("Báo cáo này đã tồn tại");
            }
            else
            {
                query = String.Format("insert into BAOCAO values ({0}, {1})", Thang, Nam);
                int data = DataProvider.Instance.ExecuteNonQuery(query);
                query = "select MaSP, TenSP, TonKho from SANPHAM";
                DataTable Ma_Ten_TonKho_SP = DataProvider.Instance.ExecuteQuery(query);
                int countSanPham = Ma_Ten_TonKho_SP.Rows.Count;
                for(int i = 0; i < countSanPham; i++)
                {
                    string MaSP = Ma_Ten_TonKho_SP.Rows[i][0].ToString();
                    string TenSP = Ma_Ten_TonKho_SP.Rows[i][1].ToString();
                    string TonCuoi = Ma_Ten_TonKho_SP.Rows[i][2].ToString();
                    // Số lượng mua vào
                    query = String.Format("select sum(CTPMH.SoLuong) from PHIEUMUAHANG, CTPMH where PHIEUMUAHANG.SoPhieu = CTPMH.SoPhieu and MONTH(PHIEUMUAHANG.NgayLap) = {0} and Year(PHIEUMUAHANG.NgayLap) = {1} and MaSP = '{2}'", Thang, Nam, MaSP);
                    string MuaVao = DataProvider.Instance.ExecuteScalar(query).ToString();
                    if (MuaVao == "") MuaVao = "0";

                    query = String.Format("select sum(CTPBH.SoLuong) from PHIEUBANHANG, CTPBH where PHIEUBANHANG.SoPhieu = CTPBH.SoPhieu and MONTH(PHIEUBANHANG.NgayLap) = {0} and Year(PHIEUBANHANG.NgayLap) = {1} and MaSP =  '{2}'", Thang, Nam, MaSP);
                    string BanRa = DataProvider.Instance.ExecuteScalar(query).ToString();
                    if (BanRa == "") BanRa = "0";
                    string TonDau = (int.Parse(TonCuoi) + int.Parse(BanRa) - int.Parse(MuaVao)).ToString();
                    query = String.Format("insert into BCTONKHO values ({0}, {1}, '{2}', {3}, {4}, {5}, {6})", Thang, Nam, MaSP, TonDau, MuaVao, BanRa, TonCuoi);
                    data = DataProvider.Instance.ExecuteNonQuery(query);
                }          
                MessageBox.Show("Đã tạo báo cáo thành công");
                query = "select Thang as N'Tháng', Nam as N'Năm' from BCTONKHO group by Thang, Nam";
                dtgvBCTonKho.DataSource = DataProvider.Instance.ExecuteQuery(query);
            }
        }
        private void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvBCTonKho.SelectedRows.Count;
            if (count > 0)
            {
                string query = "";
                if (MessageBox.Show("Bạn có thật sự muốn xóa các báo cáo đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvBCTonKho.SelectedRows[i].Index;
                        string Thang = dtgvBCTonKho.Rows[rowIndex].Cells[1].Value.ToString();
                        string Nam = dtgvBCTonKho.Rows[rowIndex].Cells[2].Value.ToString();

                        query = String.Format("delete from BAOCAO where Thang = {0} and Nam = {1}", Thang, Nam);
                        int temp1 = DataProvider.Instance.ExecuteNonQuery(query);

                        query = String.Format("delete from BCTONKHO where Thang = {0} and Nam = {1}", Thang, Nam);
                        int temp = DataProvider.Instance.ExecuteNonQuery(query);
                        if (temp != -1) data = data + 1;
                    }
                    query = "select Thang as N'Tháng', Nam as N'Năm' from BCTONKHO group by Thang, Nam";
                    dtgvBCTonKho.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} báo cáo đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }
        private void dtgvBCTonKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //string r = e.RowIndex.ToString();
            //string c = e.ColumnIndex.ToString();
            ////MessageBox.Show("Bạn đang chọn hàng" + r + " cột " + c);
            if (e.ColumnIndex == 0)
            {
                int rowIndex = dtgvBCTonKho.SelectedRows[0].Index;
                string Thang = dtgvBCTonKho.Rows[rowIndex].Cells[1].Value.ToString();
                string Nam = dtgvBCTonKho.Rows[rowIndex].Cells[2].Value.ToString();
                string curMonth = DateTime.Now.Month.ToString();
                string curYear = DateTime.Now.Year.ToString();
                if (curMonth == Thang && curYear == Nam)
                {
                    //dtgvPBH.CurrentRow.Selected = true;
                    panelMoreOption.Location = dtgvBCTonKho.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                    panelMoreOption.Left = panelMoreOption.Left - 50;
                    panelMoreOption.Top = Math.Min(panelMoreOption.Top + panelMoreOption.Height + 120, 720);
                    panelMoreOption.Visible = true;
                    //MessageBox.Show("Bạn đang chọn hàng" + s);
                }
                else
                {
                    btnCTLess.Location = dtgvBCTonKho.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                    btnCTLess.Left = btnCTLess.Left - 50;
                    btnCTLess.Top = Math.Min(btnCTLess.Top + btnCTLess.Height + 157, 720);
                    btnCTLess.Visible = true;
                    //MessageBox.Show("Bạn đang chọn hàng" + s);
                }
                //dtgvPBH.CurrentRow.Selected = true;

            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvBCTonKho.SelectedRows[0].Index;
            string Thang = dtgvBCTonKho.Rows[rowIndex].Cells[1].Value.ToString();
            string Nam = dtgvBCTonKho.Rows[rowIndex].Cells[2].Value.ToString();
            if (MessageBox.Show("Bạn có thật sự muốn xóa báo cáo đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                string query = String.Format("delete from BAOCAO where Thang = {0} and Nam = {1}", Thang, Nam);
                int data = DataProvider.Instance.ExecuteNonQuery(query);
                panelMoreOption.Visible = false;
                btnCTLess.Visible = false;
                query = String.Format("delete from BAOCAO where Thang = {0} and Nam = {1}", Thang, Nam);
                int temp1 = DataProvider.Instance.ExecuteNonQuery(query);
                query = String.Format("delete from BCTONKHO where Thang = {0} and Nam = {1}", Thang, Nam);
                data = DataProvider.Instance.ExecuteNonQuery(query);
                if (data != -1)
                {
                    query = "select Thang as N'Tháng', Nam as N'Năm' from BCTONKHO group by Thang, Nam";
                    dtgvBCTonKho.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show("Đã xóa báo cáo thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else MessageBox.Show("Xóa báo cáo không thành công", "Thông báo", MessageBoxButtons.OK);
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            int rowIndex = dtgvBCTonKho.SelectedRows[0].Index;
            string Thang = dtgvBCTonKho.Rows[rowIndex].Cells[1].Value.ToString();
            string Nam = dtgvBCTonKho.Rows[rowIndex].Cells[2].Value.ToString();
            if (MessageBox.Show("Bạn có thật sự muốn làm mới báo cáo đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {

                panelMoreOption.Visible = false;
                btnCTLess.Visible = false;

                string query = String.Format("delete from BCTONKHO where Thang = {0} and Nam = {1}", Thang, Nam);
                int data = DataProvider.Instance.ExecuteNonQuery(query);


                query = "select MaSP, TenSP, TonKho from SANPHAM";
                DataTable Ma_Ten_TonKho_SP = DataProvider.Instance.ExecuteQuery(query);
                int countSanPham = Ma_Ten_TonKho_SP.Rows.Count;
                for (int i = 0; i < countSanPham; i++)
                {
                    string MaSP = Ma_Ten_TonKho_SP.Rows[i][0].ToString();
                    string TenSP = Ma_Ten_TonKho_SP.Rows[i][1].ToString();
                    string TonCuoi = Ma_Ten_TonKho_SP.Rows[i][2].ToString();
                    // Số lượng mua vào
                    query = String.Format("select sum(CTPMH.SoLuong) from PHIEUMUAHANG, CTPMH where PHIEUMUAHANG.SoPhieu = CTPMH.SoPhieu and MONTH(PHIEUMUAHANG.NgayLap) = {0} and Year(PHIEUMUAHANG.NgayLap) = {1} and MaSP = '{2}'", Thang, Nam, MaSP);
                    string MuaVao = DataProvider.Instance.ExecuteScalar(query).ToString();
                    if (MuaVao == "") MuaVao = "0";

                    query = String.Format("select sum(CTPBH.SoLuong) from PHIEUBANHANG, CTPBH where PHIEUBANHANG.SoPhieu = CTPBH.SoPhieu and MONTH(PHIEUBANHANG.NgayLap) = {0} and Year(PHIEUBANHANG.NgayLap) = {1} and MaSP =  '{2}'", Thang, Nam, MaSP);
                    string BanRa = DataProvider.Instance.ExecuteScalar(query).ToString();
                    if (BanRa == "") BanRa = "0";
                    string TonDau = (int.Parse(TonCuoi) + int.Parse(BanRa) - int.Parse(MuaVao)).ToString();
                    query = String.Format("insert into BCTONKHO values ({0}, {1}, '{2}', {3}, {4}, {5}, {6})", Thang, Nam, MaSP, TonDau, MuaVao, BanRa, TonCuoi);
                    data = DataProvider.Instance.ExecuteNonQuery(query);
                }
                                
                if (data != -1)
                {
                    query = "select Thang as N'Tháng', Nam as N'Năm' from BCTONKHO group by Thang, Nam";
                    dtgvBCTonKho.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show("Đã làm mới báo cáo thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else MessageBox.Show("Làm mới báo cáo không thành công", "Thông báo", MessageBoxButtons.OK);
            }
        }
        private void dtgvBCTonKho_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != -1)
            {
                if (e.ColumnIndex != 0 || e.RowIndex != dtgvBCTonKho.CurrentCell.RowIndex)
                {
                    panelMoreOption.Visible = false;
                    btnCTLess.Visible = false;
                }
            }
        }
        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            panelMoreOption.Visible = false;
            btnCTLess.Visible = false;
            int rowIndex = dtgvBCTonKho.SelectedRows[0].Index;
            Thang_selected = dtgvBCTonKho.Rows[rowIndex].Cells[1].Value.ToString();
            Nam_selected = dtgvBCTonKho.Rows[rowIndex].Cells[2].Value.ToString();
            fCTBCTK f = new fCTBCTK();
            f.ShowDialog();
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
