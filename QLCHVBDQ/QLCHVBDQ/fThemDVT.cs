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
    public partial class fThemDVT : Form
    {
        public fThemDVT()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string MaDVT = textBoxMaDVT.Text;
            string TenDVT = textBoxTenDVT.Text;

            string query = String.Format("insert into DVT values ('{0}', N'{1}')", MaDVT, TenDVT);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            if (data != -1)
            {
                MessageBox.Show("Đã thêm đơn vị tính thành công");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ và đúng thông tin");
            }
        }
    }
}
