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
    public partial class fChiTietPDV : Form
    {
        public fChiTietPDV()
        {
            InitializeComponent();
            LoadCTPDV();
        }
        public void LoadCTPDV()
        {
          
            string query = String.Format("select SoPhieu, CAST(NgayLap AS varchar(10) ), KHACHHANG.TenKH, KHACHHANG.SDT, TongTien, TongTraTruoc, TongConLai, TinhTrang  from PHIEUDICHVU, KHACHHANG where SoPhieu = '{0}' and KHACHHANG.MaKH = PHIEUDICHVU.MaKH", fDichVu.SoPhieu_selected);
            DataTable x = DataProvider.Instance.ExecuteQuery(query);
            textBoxSoPhieu.Text = x.Rows[0][0].ToString();
            textBoxNgayLapPhieu.Text = x.Rows[0][1].ToString();
            textBoxKhachHang.Text = x.Rows[0][2].ToString();
            textBoxSDT.Text = x.Rows[0][3].ToString();
            textBoxTongTien.Text = x.Rows[0][4].ToString();
            textBoxTraTruoc.Text= x.Rows[0][5].ToString();
            textBoxConLai.Text = x.Rows[0][6].ToString();
            string tinhTrang = x.Rows[0][7].ToString();
            //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            if (tinhTrang == "True")
            {
                textBoxTinhTrang.Text = "Đã hoàn thành";
            }
            else textBoxTinhTrang.Text = "Chưa hoàn thành";


            dtgvChiTietPDV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvChiTietPDV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            query = String.Format("select LOAIDICHVU.TenLDV as N'Loại dịch vụ', LOAIDICHVU.DonGia as N'ĐG dịch vụ', CTPDV.DonGia as N'ĐG được tính', SoLuong as N'Số lượng', ThanhTien as N'Thành tiền', TraTruoc as N'Trả trước', ConLai as N'Còn lại', NgayGiao as N'Ngày giao', TinhTrang as N'Tình trạng' from CTPDV, LOAIDICHVU where SoPhieu = '{0}' and LOAIDICHVU.MaLDV = CTPDV.MaLDV", fDichVu.SoPhieu_selected);
            dtgvChiTietPDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
            DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
            if(dtgvChiTietPDV.Columns.Count != 10)
            {
                dtgvChiTietPDV.Columns.Add(IconCol);
                IconCol.Image = Properties.Resources.Trash_Full;
                foreach (DataGridViewColumn item in dtgvChiTietPDV.Columns)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dtgvChiTietPDV.Columns[dtgvChiTietPDV.Columns.Count - 1].Width = 40;
                //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

        }


        private void dtgvChiTietPDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            //MessageBox.Show("Bạn đang chọn hàng" + r + " cột " + c);
            if (e.ColumnIndex == 0)
            {
                //dtgvChiTietPDV.CurrentRow.Selected = true;
                //MessageBox.Show("Bạn đang chọn hàng" + s);
                int rowIndex = e.RowIndex;
                string TenLDV = dtgvChiTietPDV.Rows[rowIndex].Cells[1].Value.ToString();
                string SoPhieu = fDichVu.SoPhieu_selected;
                if (MessageBox.Show("Bạn có thật sự muốn xóa dịch vụ đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = CTPDVDAO.Instance.DeleteCTPDV_SP_MaLDV(SoPhieu, TenLDV);
                    if (data != - 1)
                    {
                        LoadCTPDV();
                        MessageBox.Show("Đã xóa dịch vụ thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show("Xóa dịch vụ không thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            fThemCTPDV f = new fThemCTPDV();
            f.ShowDialog();
            LoadCTPDV();
        }

    }
}
