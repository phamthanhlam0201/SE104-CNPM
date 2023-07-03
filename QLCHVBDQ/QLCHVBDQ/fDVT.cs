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
    public partial class fDVT : Form
    {
        public fDVT()
        {
            InitializeComponent();
            LoadDVT();
        }
        void LoadDVT()
        {
            textUserName.Text = fLogin.userName;
            dtgvDVT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvDVT.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select MaDVT as N'Mã đơn vị tính', TenDVT as N'Tên đơn vị tính' from DVT";
            dtgvDVT.DataSource = DataProvider.Instance.ExecuteQuery(query);
            if (dtgvDVT.Columns.Count != 3)
            {
                DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
                dtgvDVT.Columns.Add(IconCol);
                IconCol.Image = Properties.Resources.Trash_Full;
                foreach (DataGridViewColumn item in dtgvDVT.Columns)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dtgvDVT.Columns[dtgvDVT.Columns.Count - 1].Width = 40;
                //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void btnThemDVT_Click(object sender, EventArgs e)
        {
            fThemDVT f = new fThemDVT();
            f.ShowDialog();
            string query = "select MaDVT as N'Mã đơn vị tính', TenDVT as N'Tên đơn vị tính' from DVT";
            dtgvDVT.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void btnDeletes_Click(object sender, EventArgs e)
        {
            int count = dtgvDVT.SelectedRows.Count;
            if (count > 0)
            {
                if (MessageBox.Show("Bạn có thật sự muốn xóa các đơn vị tính đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    string query = "";
                    int data = 0;
                    for (int i = 0; i < count; i++)
                    {
                        int rowIndex = dtgvDVT.SelectedRows[i].Index;
                        int colIndex = 1;
                        string MaDVT = dtgvDVT.Rows[rowIndex].Cells[colIndex].Value.ToString();
                        query = String.Format("delete from DVT where MaDVT = '{0}'", MaDVT);
                        int temp = DataProvider.Instance.ExecuteNonQuery(query);
                        if (temp == 1) data = data + 1;
                    }
                    query = "select MaDVT as N'Mã đơn vị tính', TenDVT as N'Tên đơn vị tính' from DVT";
                    dtgvDVT.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    MessageBox.Show(String.Format("{0} đơn vị tính đã được xóa", data), "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void dtgvDVT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int r = e.RowIndex;
            //int c = e.ColumnIndex;
            //MessageBox.Show("Bạn đang chọn hàng" + r + " cột " + c);
            if (e.ColumnIndex == 0)
            {
                //dtgvChiTietPDV.CurrentRow.Selected = true;
                //MessageBox.Show("Bạn đang chọn hàng" + s);
                int rowIndex = e.RowIndex;
                string MaDVT = dtgvDVT.Rows[rowIndex].Cells[1].Value.ToString();
                if (MessageBox.Show("Bạn có thật sự muốn xóa đơn vị tính đã chọn", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    string query = String.Format("delete from DVT where MaDVT = '{0}'", MaDVT);
                    int data = DataProvider.Instance.ExecuteNonQuery(query);
                    if (data != -1)
                    {
                        query = "select MaDVT as N'Mã đơn vị tính', TenDVT as N'Tên đơn vị tính' from DVT";
                        dtgvDVT.DataSource = DataProvider.Instance.ExecuteQuery(query);
                        MessageBox.Show("Đã xóa đơn vị tính thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show("Xóa đơn vị tính không thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnTLTT_Click(object sender, EventArgs e)
        {
            fTLTT f = new fTLTT();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnLoaiNhanVien_Click(object sender, EventArgs e)
        {
            fNhanVien f = new fNhanVien();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            fCaiDat f = new fCaiDat();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }
    }
}
