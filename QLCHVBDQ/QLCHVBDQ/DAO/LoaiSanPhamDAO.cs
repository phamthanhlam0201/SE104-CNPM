using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHVBDQ.DAO
{
    class LoaiSanPhamDAO
    {
        private static LoaiSanPhamDAO instance;

        public static LoaiSanPhamDAO Instance
        {
            get { if (instance == null) instance = new LoaiSanPhamDAO(); return instance; }
            set => Instance = value;
        }
        private LoaiSanPhamDAO() { }

        
        public int Delete_LSP(string MaLSP)
        {
            SanPhamDAO.Instance.DeleteSP_MaLSP(MaLSP);
            string query = String.Format("delete from LOAISANPHAM where  MaLSP = '{0}'", MaLSP);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            return data;
        }
    }
}
