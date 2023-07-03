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
    public partial class fCTPBH : Form
    {
        public fCTPBH()
        {
            InitializeComponent();
            LoadCTPBH();
        }
        public DataTable x;
        public void LoadCTPBH()
        {
            string query = String.Format("select SoPhieu, CAST(NgayLap AS varchar(10) ), KHACHHANG.TenKH, KHACHHANG.SDT, TongTien  from PHIEUBANHANG, KHACHHANG where SoPhieu = '{0}' and KHACHHANG.MaKH = PHIEUBANHANG.MaKH", fBanHang.SoPhieu_selected);
            x = DataProvider.Instance.ExecuteQuery(query);
            if (x.Rows.Count > 0)
            {
                textBoxNewSoPhieu.Text = x.Rows[0][0].ToString();
                textBoxNgayLapPhieu.Text = x.Rows[0][1].ToString();
                textBoxNgayLapPhieu.Text = x.Rows[0][1].ToString();
                textBoxKhachHang.Text = x.Rows[0][2].ToString();
                textBoxSDT.Text = x.Rows[0][3].ToString();
                textBoxTongTien.Text = x.Rows[0][4].ToString();
                //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            dtgvChiTietPBH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvChiTietPBH.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            query = String.Format("select SANPHAM.TenSP as N'Tên sản phẩm', LOAISANPHAM.TenLSP as 'Loại sản phẩm', CTPBH.SoLuong as N'Số lượng', DVT.TenDVT as N'Đơn vị tính', CTPBH.DonGiaBH as N'Đơn giá', CTPBH.ThanhTien as N'Thành tiền' from CTPBH, SANPHAM, LOAISANPHAM, DVT where SoPhieu = '{0}' and CTPBH.MaSP = SANPHAM.MaSP and SANPHAM.MaLSP = LOAISANPHAM.MaLSP and LOAISANPHAM.MaDVT = DVT.MaDVT", fBanHang.SoPhieu_selected);
            dtgvChiTietPBH.DataSource = DataProvider.Instance.ExecuteQuery(query);
            DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
            if (dtgvChiTietPBH.Columns.Count != 7)
            {
                dtgvChiTietPBH.Columns.Add(IconCol);
                IconCol.Image = Properties.Resources.Trash_Full;
                foreach (DataGridViewColumn item in dtgvChiTietPBH.Columns)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dtgvChiTietPBH.Columns[dtgvChiTietPBH.Columns.Count - 1].Width = 40;
                //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void dtgvChiTietPBH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            //MessageBox.Show("Bạn đang chọn hàng" + r + " cột " + c);
            if (e.ColumnIndex == 0)
            {
                //dtgvChiTietPDV.CurrentRow.Selected = true;
                //MessageBox.Show("Bạn đang chọn hàng" + s);
                int rowIndex = e.RowIndex;
                string TenSP = dtgvChiTietPBH.Rows[rowIndex].Cells[1].Value.ToString();
                string SoPhieu = fBanHang.SoPhieu_selected;
                if (MessageBox.Show("Bạn có thật sự muốn xóa sản phẩm đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = CTPBHDAO.Instance.DeleteCTPBH_SP_TenSP(SoPhieu, TenSP);
                    if (data != -1)
                    {
                        LoadCTPBH();
                        MessageBox.Show("Đã xóa sản phẩm thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show("Xóa sản phẩm không thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            fThemCTPBH f = new fThemCTPBH();
            f.ShowDialog();
            LoadCTPBH();
        }
    }
}
