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
    public partial class fThemLDV : Form
    {
        public fThemLDV()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string MaLDV = textBoxMaLDV.Text;
            string TenLDV = textBoxTenLDV.Text;
            string DonGia = numUDDonGia.Value.ToString();


            string query = String.Format("insert into LOAIDICHVU values ('{0}', N'{1}', '{2}')", MaLDV, TenLDV, DonGia);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            //query = String.Format("insert into THAMSO values ('{0}', 0)", MaLDV);
            //int data1 = DataProvider.Instance.ExecuteNonQuery(query);
            if (data != -1)
            {
                MessageBox.Show("Đã thêm loại dịch vụ thành công");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ và đúng thông tin");
            }
        }

        private void fThemLDV_Load(object sender, EventArgs e)
        {

        }
    }
}
