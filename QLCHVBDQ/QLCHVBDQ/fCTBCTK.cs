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
    public partial class fCTBCTK : Form
    {
        public fCTBCTK()
        {
            InitializeComponent();
            LoadCTBCTK();
        }
        public void LoadCTBCTK()
        {
            textThang.Text = fBaoCao.Thang_selected;
            textNam.Text = fBaoCao.Nam_selected;

            dtgvChiTietBCTK.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvChiTietBCTK.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = String.Format("select BCTONKHO.MaSP as N'Mã sản phẩm', SANPHAM.TenSP as N'Tên sản phẩm', TonDau as N'Tồn đầu', SLMua as N'Số lượng mua vào' , SLBan as N'Số lượng bán ra', TonCuoi as N'Tồn cuối', DVT.TenDVT as N'Đơn vị tính' from BCTONKHO, SANPHAM, DVT, LOAISANPHAM where BCTONKHO.Thang = {0} and BCTONKHO.Nam = {1} and SANPHAM.MaSP = BCTONKHO.MaSP and SANPHAM.MaLSP = LOAISANPHAM.MaLSP and LOAISANPHAM.MaDVT = DVT.MaDVT", fBaoCao.Thang_selected, fBaoCao.Nam_selected);
            dtgvChiTietBCTK.DataSource = DataProvider.Instance.ExecuteQuery(query);
            DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
        }
    }
}
