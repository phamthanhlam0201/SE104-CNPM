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
    public partial class fThemNCC : Form
    {
        public fThemNCC()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string MaNCC = textBoxMaNCC.Text;
            string TenNCC = textBoxTenNCC.Text;
            string DiaChi = textBoxDiaChi.Text;
            string SDT = textBoxSDT.Text;


            string query = String.Format("insert into NHACUNGCAP values ('{0}', N'{1}', N'{2}', N'{3}')", MaNCC, TenNCC, DiaChi, SDT);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            if (data != -1)
            {
                MessageBox.Show("Đã thêm nhà cung cấp thành công");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ và đúng thông tin");
            }
        }
    }
}
