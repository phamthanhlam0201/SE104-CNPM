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
    public partial class fTLTT : Form
    {
        public fTLTT()
        {
            InitializeComponent();
            LoadTLTT();
        }
        void LoadTLTT()
        {
            textUserName.Text = fLogin.userName;
            dtgvTLTT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvTLTT.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            string query = "select TenThamSo as N'Tên tham số', GiaTri as N'Giá trị' from THAMSO";
            dtgvTLTT.DataSource = DataProvider.Instance.ExecuteQuery(query);
            dtgvTLTT.Columns[0].ReadOnly = true;
            //if (dtgvTLTT.Columns.Count != 3)
            //{
            //    DataGridViewImageColumn IconCol = new DataGridViewImageColumn();
            //    dtgvTLTT.Columns.Add(IconCol);
            //    IconCol.Image = Properties.Resources.Trash_Full;
            //    foreach (DataGridViewColumn item in dtgvTLTT.Columns)
            //    {
            //        item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    }
            //    dtgvTLTT.Columns[dtgvTLTT.Columns.Count - 1].Width = 40;
            //    //dtgvChiTietPDV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //}
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

        private void btnDVT_Click(object sender, EventArgs e)
        {
            fDVT f = new fDVT();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void dtgvTLTT_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            string MaLDV = dtgvTLTT.Rows[rowIndex].Cells[0].Value.ToString();
            string GiaTri = dtgvTLTT.Rows[rowIndex].Cells[1].Value.ToString();
            string query = String.Format("update THAMSO set GiaTri = {0} where TenThamSo = N'{1}'", GiaTri, MaLDV);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            if (data != -1)
            {
                MessageBox.Show("Thay đổi giá trị thành công");
                query = "select TenThamSo as N'Tên tham số', GiaTri as N'Tỉ lệ trả trước' from THAMSO";
                dtgvTLTT.DataSource = DataProvider.Instance.ExecuteQuery(query);
            }
            else
            {
                MessageBox.Show("Thay đổi giá trị không thành công");
                query = "select TenThamSo as N'Tên tham số', GiaTri as N'Tỉ lệ trả trước' from THAMSO";
                dtgvTLTT.DataSource = DataProvider.Instance.ExecuteQuery(query);
            }
        }
    }
}
