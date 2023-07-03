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
    public partial class fThemLoaiSP : Form
    {
        public fThemLoaiSP()
        {
            InitializeComponent();
            LoadThemLSP();
        }
        public void LoadThemLSP()
        {
            string query = "select MaDVT from DVT";
            DataTable MaDVT = DataProvider.Instance.ExecuteQuery(query);
            if (comboBoxMaDVT.Items.Count == 0)
            {
                for (int i = 0; i < MaDVT.Rows.Count; i++)
                {
                    string s = MaDVT.Rows[i][0].ToString();
                    comboBoxMaDVT.Items.Add(s);
                }
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string MaLoaiSP = textBoxMaLSP.Text;
            string MaDVT = comboBoxMaDVT.Text;
            string TenLSP = textBoxTenLSP.Text;
            string PTLN = numUDPTLN.Value.ToString();
            
            
            string query = String.Format("insert into LOAISANPHAM values ('{0}', N'{1}', '{2}', '{3}')", MaLoaiSP, TenLSP, MaDVT, PTLN);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            if (data != -1)
            {            
                MessageBox.Show("Đã thêm loại sản phẩm thành công");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ và đúng thông tin");
            }
        }

        private void fThemLoaiSP_Load(object sender, EventArgs e)
        {

        }
    }
}
