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
    public partial class fThemCTPBH : Form
    {
        public fThemCTPBH()
        {
            InitializeComponent();
            LoadPBH();
        }
        public DataTable x;
        public void LoadPBH()
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

            query = "select TenSP from SANPHAM";
            DataTable TenSP = DataProvider.Instance.ExecuteQuery(query);
            if (comboBoxTenSP.Items.Count == 0)
            {
                for (int i = 0; i < TenSP.Rows.Count; i++)
                {
                    string s = TenSP.Rows[i][0].ToString();
                    comboBoxTenSP.Items.Add(s);
                }
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string SoPhieu = x.Rows[0][0].ToString();
            string tenSP = comboBoxTenSP.Text;
            string SoLuong = numUpDowwnSoLuong.Value.ToString();
            string query = String.Format("select MaSP, SANPHAM.MaLSP, DonGia, PhanTramLoiNhuan, TonKho from SANPHAM, LOAISANPHAM where TenSP = N'{0}' and SANPHAM.MaLSP = LOAISANPHAM.MaLSP", tenSP);
            DataTable Ma = DataProvider.Instance.ExecuteQuery(query);
            string MaSP = "";
            string MaLSP = "";
            string DonGia_SP = "";
            string PTLN_LSP = "";
            string TonKho = "0";
            if (Ma.Rows.Count > 0)
            {
                MaSP = Ma.Rows[0][0].ToString();
                MaLSP = Ma.Rows[0][1].ToString();
                DonGia_SP = Ma.Rows[0][2].ToString();
                PTLN_LSP = Ma.Rows[0][3].ToString();
                TonKho = Ma.Rows[0][4].ToString();
            }
            if (int.Parse(TonKho) < int.Parse(SoLuong))
            {
                MessageBox.Show("Sản phẩm còn trong kho không đủ");
            }
            else
            {
                string DonGia_BH = (int.Parse(DonGia_SP) + (int.Parse(DonGia_SP) * (int.Parse(PTLN_LSP))) / 100).ToString();
                string ThanhTien = (int.Parse(DonGia_BH) * int.Parse(SoLuong)).ToString();
                query = String.Format("insert into CTPBH values('{0}', '{1}', {2}, {3}, {4})", SoPhieu, MaSP, SoLuong, DonGia_BH, ThanhTien);
                int data = DataProvider.Instance.ExecuteNonQuery(query);
                if (data != -1)
                {
                    LoadPBH();
                    MessageBox.Show("Đã thêm sản phẩm vào phiếu thành công");
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đủ và đúng thông tin");
                }
            }
           

        }
    }
}
