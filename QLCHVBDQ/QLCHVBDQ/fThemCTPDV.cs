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
    public partial class fThemCTPDV : Form
    {
        public fThemCTPDV()
        {
            InitializeComponent();
            LoadPDV();
        }
        public DataTable x;
        public void LoadPDV()
        {
            string query = String.Format("select SoPhieu, CAST(NgayLap AS varchar(10) ), KHACHHANG.TenKH, KHACHHANG.SDT, TongTien, TongTraTruoc, TongConLai, TinhTrang  from PHIEUDICHVU, KHACHHANG where SoPhieu = '{0}' and KHACHHANG.MaKH = PHIEUDICHVU.MaKH", fDichVu.SoPhieu_selected);
            x = DataProvider.Instance.ExecuteQuery(query);
            if (x.Rows.Count >0 )
            {
                textBoxNewSoPhieu.Text = x.Rows[0][0].ToString();
                textBoxNgayLapPhieu.Text = x.Rows[0][1].ToString();
                textBoxNgayLapPhieu.Text = x.Rows[0][1].ToString();
                textBoxKhachHang.Text = x.Rows[0][2].ToString();
                textBoxSDT.Text = x.Rows[0][3].ToString();
                textBoxTongTien.Text = x.Rows[0][4].ToString();
                textBoxTraTruoc.Text = x.Rows[0][5].ToString();
                textBoxConLai.Text = x.Rows[0][6].ToString();
                textBoxConLai.Text = x.Rows[0][6].ToString();
                string tinhTrang = x.Rows[0][7].ToString();
                //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                if (tinhTrang == "True")
                {
                    textBoxTinhTrang.Text = "Đã hoàn thành";
                }
                else textBoxTinhTrang.Text = "Chưa hoàn thành";

            }
            
            DateTime dt;
            DateTime.TryParse(x.Rows[0][1].ToString(), out dt);
            dateNgayGiao.MinDate = dt;

            query = "select TenLDV from LOAIDICHVU";
            DataTable LDV = DataProvider.Instance.ExecuteQuery(query);
            if(comboBoxLDV.Items.Count == 0)
            {
                for (int i = 0; i < LDV.Rows.Count; i++)
                {
                    string s = LDV.Rows[i][0].ToString();
                    comboBoxLDV.Items.Add(s);
                }
            }
                                
        }

        private void btnThem_Click(object sender, EventArgs e)
        {            
            string SoPhieu = x.Rows[0][0].ToString();
            string tenLDV = comboBoxLDV.Text;
            string SoLuong = numUpDowwnSoLuong.Value.ToString();
            string NgayGiao = dateNgayGiao.Text;
            string ChiPhiRieng = numUpDownCPR.Value.ToString();
            string TraTruoc = nudTraTruoc.Value.ToString();
            string TinhTrang = comboBoxTinhTrang.Text;
            if (TinhTrang == "Chưa giao") TinhTrang = "0"; // TinhTrang == 0 là chưa giao
            else TinhTrang = "1"; // tinhtrang = 1 là đã giao
            string query = String.Format("select MaLDV, DonGia from LOAIDICHVU where TenLDV = N'{0}'", tenLDV);
            DataTable Ma = DataProvider.Instance.ExecuteQuery(query);
            string MaLDV = "";
            if (Ma.Rows.Count > 0)
            {
                MaLDV = Ma.Rows[0][0].ToString();
            }            
            string DonGia_DV = Ma.Rows[0][1].ToString();
            string DonGia = (long.Parse(ChiPhiRieng) + long.Parse(DonGia_DV)).ToString();
            long tt = (long.Parse(DonGia) * long.Parse(SoLuong));
            nudTraTruoc.Maximum = tt;
            TraTruoc = (Math.Min(tt, long.Parse(TraTruoc))).ToString();
            string ThanhTien = tt.ToString();
            string ConLai = (long.Parse(ThanhTien) - long.Parse(TraTruoc)).ToString();
            query = String.Format("select GiaTri from THAMSO where TenThamSO = N'Tỉ lệ trả trước'");
            int TLTT = int.Parse(DataProvider.Instance.ExecuteQuery(query).Rows[0][0].ToString());
            
            if (int.Parse(TraTruoc) < int.Parse(ThanhTien)*TLTT/100)
            {
                MessageBox.Show("Số tiền trả trước chưa đủ yêu cầu");
            }
            else
            {
                query = String.Format("insert into CTPDV values('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, {7}, '{8}', {9})", SoPhieu, MaLDV, ChiPhiRieng, SoLuong, DonGia, ThanhTien, TraTruoc, ConLai, NgayGiao, TinhTrang);
                int data = DataProvider.Instance.ExecuteNonQuery(query);
                if (data != -1)
                {
                    LoadPDV();
                    MessageBox.Show("Đã thêm dịch vụ vào phiếu thành công");
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đủ và đúng thông tin");
                }
            }

        }
    }
}
