using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHVBDQ.DAO
{
    class SanPhamDAO
    {
        private static SanPhamDAO instance;

        public static SanPhamDAO Instance
        {
            get { if (instance == null) instance = new SanPhamDAO(); return instance; }
            set => Instance = value;
        }
        private SanPhamDAO() { }

        public int DeleteSP_MaSanPham(string MaSanPham)
        {
            string query = String.Format("delete from SANPHAM where MaSP = '{0}'", MaSanPham);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            return data;
        }
        public void DeleteSP_MaLSP(string MaLSP)
        {
            string query = String.Format("delete from SANPHAM where MaLSP = '{0}'", MaLSP);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
        }

    }
}
