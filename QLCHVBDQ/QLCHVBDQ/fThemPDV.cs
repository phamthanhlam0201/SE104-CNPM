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
    public partial class fThemPDV : Form
    {
        public fThemPDV()
        {
            InitializeComponent();
            dateNgayLapPhieu.MaxDate = DateTime.Now;
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            string SoPhieu = textBoxSoPhieu.Text.Trim();
            string NgayLap = dateNgayLapPhieu.Text;
            string TenKH = textBoxTenKH.Text.Trim();
            string SDT = textBoxSDT.Text.Trim();
            string MaKH = "";
            if(SoPhieu == "" || TenKH == "" || SDT == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                if (KhachHangDAO.Instance.checkKhachHang(TenKH, SDT) == 0)
                {
                    int stt = KhachHangDAO.Instance.countkKhachHang()+1;
                    MaKH = "KH" + stt.ToString();
                    KhachHangDAO.Instance.insertkKhachHang(MaKH, TenKH, SDT);
                }
                else
                {
                    DataTable x = KhachHangDAO.Instance.get_MaKH_Ten_SDT(TenKH, SDT);
                    MaKH = x.Rows[0][0].ToString();
                }
                int opt = PDVDAO.Instance.InsertPDV(SoPhieu, NgayLap, MaKH);
                if (opt == 0)
                {
                    MessageBox.Show(String.Format("Số phiếu {0} phiếu đã tồn tại. Vui lòng nhập lại.", SoPhieu));
                }
                else if (opt == 1)
                {
                    MessageBox.Show(String.Format("Khách hàng {0} chưa tồn tại. Vui lòng nhập lại.", TenKH));
                }
                else if (opt == 2)
                {
                    MessageBox.Show(String.Format("Thêm phiếu dịch vụ thành công", TenKH));
                    //fDichVu.LoadPhieuDV();
                }
                else
                {
                    MessageBox.Show(String.Format("Thêm phiếu không thành công", TenKH));
                }
            }

            
        }

    }
}
