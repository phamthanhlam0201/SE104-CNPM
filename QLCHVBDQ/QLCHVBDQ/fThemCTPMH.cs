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
    public partial class fThemCTPMH : Form
    {
        public fThemCTPMH()
        {
            InitializeComponent();
            LoadPMH();
        }
        public DataTable x;
        public void LoadPMH()
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
            string query = String.Format("select MaSP, SANPHAM.MaLSP, DonGia from SANPHAM, LOAISANPHAM where TenSP = N'{0}' and SANPHAM.MaLSP = LOAISANPHAM.MaLSP", tenSP);
            DataTable Ma = DataProvider.Instance.ExecuteQuery(query);
            string MaSP = "";
            string MaLSP = "";
            string DonGia_MH = "";
            if (Ma.Rows.Count > 0)
            {
                MaSP = Ma.Rows[0][0].ToString();
                MaLSP = Ma.Rows[0][1].ToString();
                DonGia_MH = Ma.Rows[0][2].ToString();
            }
            string ThanhTien = (int.Parse(DonGia_MH) * int.Parse(SoLuong)).ToString();
            query = String.Format("insert into CTPMH values('{0}', '{1}', {2}, {3}, {4})", SoPhieu, MaSP, SoLuong, DonGia_MH, ThanhTien);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            if (data != -1)
            {
                LoadPMH();
                MessageBox.Show("Đã thêm sản phẩm vào phiếu thành công");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ và đúng thông tin");
            }

        }
    }
}
