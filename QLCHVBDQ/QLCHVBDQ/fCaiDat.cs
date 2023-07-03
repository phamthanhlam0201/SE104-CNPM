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
    public partial class fCaiDat : Form
    {
        public fCaiDat()
        {
            InitializeComponent();
            LoadKH();
        }

        void LoadKH()
        {

            textUserName.Text = fLogin.userName;
            textUserName.Text = fLogin.userName;
            dtgvKH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvKH.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select MaKH as N'Mã khách hàng', TenKH as N'Tên khách hàng', SDT as N'Số điện thoại' from KHACHHANG";
            dtgvKH.DataSource = DataProvider.Instance.ExecuteQuery(query);
            if (dtgvKH.Columns.Count != 4)
            {
                DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
                dtgvKH.Columns.Add(IconCol);
                IconCol.Image = Properties.Resources.Trash_Full;
                foreach (DataGridViewColumn item in dtgvKH.Columns)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dtgvKH.Columns[dtgvKH.Columns.Count - 1].Width = 40;
            }
        }
        private void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvKH.SelectedRows.Count;
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các khách hàng đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvKH.SelectedRows[i].Index;
                        int colIndex = 1;
                        string MaKH = dtgvKH.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        int temp = KhachHangDAO.Instance.Delete_KH(MaKH);
                        if (temp == 1) data = data + 1;
                    }
                    string query = "select MaKH as N'Mã khách hàng', TenKH as N'Tên khách hàng', SDT as N'Số điện thoại' from KHACHHANG";
                    dtgvKH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} khách hàng đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void dtgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            string MLSP = dtgvKH.Rows[rowIndex].Cells[1].Value.ToString();
            if (e.ColumnIndex == 0)
            {
                //dtgvChiTietPDV.CurrentRow.Selected = true;
                //MessageBox.Show("Bạn đang chọn hàng" + s);
                if (MessageBox.Show("Bạn có thật sự muốn xóa khách hàng đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int data = KhachHangDAO.Instance.Delete_KH(MLSP);
                    if (data != -1)
                    {
                        string query = "select MaKH as N'Mã khách hàng', TenKH as N'Tên khách hàng', SDT as N'Số điện thoại' from KHACHHANG";
                        dtgvKH.DataSource = DataProvider.Instance.ExecuteQuery(query);
                        MessageBox.Show(String.Format("Khách hàng đã được xóa"), "Thông báo", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show("Xóa khách hàng không thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnLoaiNhanVien_Click(object sender, EventArgs e)
        {
            fNhanVien f = new fNhanVien();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnDVT_Click(object sender, EventArgs e)
        {
            fDVT f = new fDVT();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnTLTT_Click(object sender, EventArgs e)
        {
            fTLTT f = new fTLTT();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }
    }

    
}
