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
    public partial class fCTPMH : Form
    {
        public fCTPMH()
        {
            InitializeComponent();
            LoadCTPMH();
        }
        public DataTable x;
        public void LoadCTPMH()
        {
            string query = String.Format("select SoPhieu, CAST(NgayLap AS varchar(10) ), TenNCC, NHACUNGCAP.SDT, NHACUNGCAP.DiaChi, TongTien  from PHIEUMUAHANG, NHACUNGCAP where SoPhieu = '{0}' and PHIEUMUAHANG.MaNCC = NHACUNGCAP.MaNCC", fMuaHang.SoPhieu_selected);
            x = DataProvider.Instance.ExecuteQuery(query);
            if (x.Rows.Count > 0)
            {
                textBoxNewSoPhieu.Text = x.Rows[0][0].ToString();
                textBoxNgayLapPhieu.Text = x.Rows[0][1].ToString();
                textBoxNCC.Text = x.Rows[0][2].ToString();
                textBoxSDT.Text = x.Rows[0][3].ToString();
                textBoxDiaChi.Text = x.Rows[0][4].ToString();
                textBoxTongTien.Text = x.Rows[0][5].ToString();
                //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            dtgvChiTietPMH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvChiTietPMH.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            query = String.Format("select SANPHAM.TenSP as N'Tên sản phẩm', LOAISANPHAM.TenLSP as 'Loại sản phẩm', CTPMH.SoLuong as N'Số lượng', DVT.TenDVT as N'Đơn vị tính', CTPMH.DonGiaMH as N'Đơn giá', CTPMH.ThanhTien as N'Thành tiền' from CTPMH, SANPHAM, LOAISANPHAM, DVT where SoPhieu = '{0}' and CTPMH.MaSP = SANPHAM.MaSP and SANPHAM.MaLSP = LOAISANPHAM.MaLSP and LOAISANPHAM.MaDVT = DVT.MaDVT", fMuaHang.SoPhieu_selected);
            dtgvChiTietPMH.DataSource = DataProvider.Instance.ExecuteQuery(query);
            DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
            if (dtgvChiTietPMH.Columns.Count != 7)
            {
                dtgvChiTietPMH.Columns.Add(IconCol);
                IconCol.Image = Properties.Resources.Trash_Full;
                foreach (DataGridViewColumn item in dtgvChiTietPMH.Columns)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dtgvChiTietPMH.Columns[dtgvChiTietPMH.Columns.Count - 1].Width = 40;
                //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void dtgvChiTietPMH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //dtgvChiTietPDV.CurrentRow.Selected = true;
                //MessageBox.Show("Bạn đang chọn hàng" + s);
                int rowIndex = e.RowIndex;
                string TenSP = dtgvChiTietPMH.Rows[rowIndex].Cells[1].Value.ToString();
                string SoPhieu = fMuaHang.SoPhieu_selected;
                if (MessageBox.Show("Bạn có thật sự muốn xóa sản phẩm đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = CTPMHDAO.Instance.DeleteCTPMH_SP_TenSP(SoPhieu, TenSP);
                    if (data != -1)
                    {
                        LoadCTPMH();
                        MessageBox.Show("Đã xóa sản phẩm thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show("Xóa sản phẩm không thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            fThemCTPMH f = new fThemCTPMH();
            f.ShowDialog();
            LoadCTPMH();
        }
    }
}
