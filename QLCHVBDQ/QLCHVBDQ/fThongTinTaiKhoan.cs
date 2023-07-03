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
    public partial class fThongTinTaiKhoan : Form
    {
        public fThongTinTaiKhoan()
        {
            InitializeComponent();
            LoadTTTK();
        }
        public void LoadTTTK()
        {
            string query = String.Format("select Email, SDT, MatKhau, HoTen, Cast(NgaySinh as varchar(10)), GioiTinh from NGUOIDUNG where Email = '{0}'", fLogin.userEmail);
            DataTable x = DataProvider.Instance.ExecuteQuery(query);
            textBoxEmail.Text = x.Rows[0][0].ToString();
            textBoxSDT.Text = x.Rows[0][1].ToString();
            textBoxMatKhau.Text = x.Rows[0][2].ToString();
            textBoxTenTK.Text = x.Rows[0][3].ToString();
            textBoxNgaySinh.Text = x.Rows[0][4].ToString();
            string GioiTinh = x.Rows[0][5].ToString();
            if(GioiTinh == "True")
            {
                textBoxGioiTinh.Text = "Nữ";
            }
            else textBoxGioiTinh.Text = "Nam";
        }

        private void btnThayDoiThongTin_Click(object sender, EventArgs e)
        {
            fLuuThongTinTaiKhoan f = new fLuuThongTinTaiKhoan();
            this.Hide();
            f.ShowDialog();
            LoadTTTK();
            this.Show();
        }

    }
}
