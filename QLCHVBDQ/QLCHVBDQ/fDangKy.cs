using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCHVBDQ.DAO;
namespace QLCHVBDQ
{
    public partial class fDangKy : Form
    {
        public fDangKy()
        {
            InitializeComponent();
            dateNgaySinh.MaxDate = DateTime.Now;
        }
        private void textTenTK_Enter(object sender, EventArgs e)
        {
            if (textTenTK.Text == "Tên tài khoản")
            {
                textTenTK.Text = "";
                textTenTK.ForeColor = Color.Black;
            }
        }

        private void textTenTK_Leave(object sender, EventArgs e)
        {
            if (textTenTK.Text == "")
            {
                textTenTK.Text = "Tên tài khoản";
                textTenTK.ForeColor = Color.Silver;
            }
        }
        private void Email_Enter(object sender, EventArgs e)
        {
            if (Email.Text == "Email")
            {
                Email.Text = "";
                Email.ForeColor = Color.Black;
            }
        }

        private void Email_Leave(object sender, EventArgs e)
        {
            if (Email.Text == "")
            {
                Email.Text = "Email";
                Email.ForeColor = Color.Silver;
            }
        }

        private void textNhapLaiMK_Enter(object sender, EventArgs e)
        {
            if (textNhapLaiMK.Text == "Nhập lại mật khẩu")
            {
                textNhapLaiMK.Text = "";
                textNhapLaiMK.ForeColor = Color.Black;
                textNhapLaiMK.PasswordChar = '*';
            }
        }

        private void textNhapLaiMK_Leave(object sender, EventArgs e)
        {
            if (textNhapLaiMK.Text == "")
            {
                textNhapLaiMK.PasswordChar = '\0';
                textNhapLaiMK.Text = "Nhập lại mật khẩu";
                textNhapLaiMK.ForeColor = Color.Silver;
            }
        }
        private void password_Enter(object sender, EventArgs e)
        {
            if (password.Text == "Mật khẩu")
            {
                password.Text = "";
                password.ForeColor = Color.Black;
                password.PasswordChar = '*';
            }
        }
        private void password_Leave(object sender, EventArgs e)
        {
            if (password.Text == "")
            {
                password.PasswordChar = '\0';
                password.Text = "Mật khẩu";
                password.ForeColor = Color.Silver;
            }
        }
        private void textSDT_Enter(object sender, EventArgs e)
        {
            if(textSDT.Text == "Số điện thoại")
            {
                textSDT.Text = "";
                textSDT.ForeColor = Color.Black;
            }
        }
        private void textSDT_Leave(object sender, EventArgs e)
        {
            if (textSDT.Text == "")
            {
                textSDT.Text = "Số điện thoại";
                textSDT.ForeColor = Color.Silver;
            }
        }


        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if(Email.ForeColor == Color.Silver || textSDT.ForeColor == Color.Silver || textTenTK.ForeColor == Color.Silver || password.ForeColor == Color.Silver || textNhapLaiMK.ForeColor == Color.Silver || comBoxGioiTinh.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
            }
            else
            {
                string email = Email.Text.Trim();
                string SDT = textSDT.Text.Trim();
                string tenTK = textTenTK.Text.Trim();
                string ngaySinh = dateNgaySinh.Text;
                int gioiTinh = comBoxGioiTinh.SelectedIndex;
                string matKhau = password.Text;
                string nlMatKhau = textNhapLaiMK.Text;
                int id = DangKy(email, SDT, tenTK, ngaySinh, gioiTinh, nlMatKhau, matKhau);
                if ( id  == 1 ) MessageBox.Show("Email này đã được đăng ký");
                else if (id == 2) MessageBox.Show("Mật khẩu nhập lại không chính xác");
                else if (id == 3)
                {
                    if (MessageBox.Show("Tài khoản đã được đăng ký thành công. Quay lại trang đăng nhập.", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else if (id == 4) MessageBox.Show("Đăng ký không thành công");
            }
            //this.Close();
        }
        int DangKy(string email, string SDT, string tenTK, string ngaySinh, int gioiTinh, string nlMatKhau, string matKhau)
        {
            return AccountDAO.Instance.DangKy(email, SDT, tenTK, ngaySinh, gioiTinh, nlMatKhau, matKhau);
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void textNhapLaiMK_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
