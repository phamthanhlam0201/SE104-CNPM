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
    public partial class fLuuThongTinTaiKhoan : Form
    {
        public fLuuThongTinTaiKhoan()
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
            dateNgaySinh.Text = x.Rows[0][4].ToString();
            string GioiTinh = x.Rows[0][5].ToString();
            if (GioiTinh == "True")
            {
                comBoxGioiTinh.SelectedItem = "Nữ";
            }
            else comBoxGioiTinh.SelectedItem = "Nam";
        }

        private void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            if (textBoxSDT.Text == "" || textBoxSDT.Text == "" || textBoxMatKhau.Text == "" || comBoxGioiTinh.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
            }
            else
            {
                string SDT = textBoxSDT.Text.Trim();
                string tenTK = textBoxTenTK.Text.Trim();
                string ngaySinh = dateNgaySinh.Text;
                int gioiTinh = comBoxGioiTinh.SelectedIndex;
                string matKhau = textBoxMatKhau.Text;
                string query = String.Format("UPDATE NGUOIDUNG SET SDT = '{0}', MatKhau = '{1}', HoTen = N'{2}', NgaySinh = '{3}', GioiTinh = '{4}' WHERE Email = '{5}'", SDT, matKhau, tenTK, ngaySinh, gioiTinh, fLogin.userEmail);
                int data = DataProvider.Instance.ExecuteNonQuery(query);
                if (data != -1)
                {
                    fLogin.userName = tenTK;
                    MessageBox.Show("Thay đổi thông tin thành công");
                }
                else MessageBox.Show("Thay đổi không thành công");
            }
            //this.Close();
        }
    }

}
