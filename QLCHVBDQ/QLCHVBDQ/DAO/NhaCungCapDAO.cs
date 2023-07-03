using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHVBDQ.DAO
{
    class NhaCungCapDAO
    {
        private static NhaCungCapDAO instance;

        public static NhaCungCapDAO Instance
        {
            get { if (instance == null) instance = new NhaCungCapDAO(); return instance; }
            set => Instance = value;
        }
        private NhaCungCapDAO() { }


        public int Delete_NCC(string MaNCC)
        {
            SanPhamDAO.Instance.DeleteSP_MaLSP(MaNCC);
            string query = String.Format("delete from NHACUNGCAP where  MaNCC = '{0}'", MaNCC);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            return data;
        }
    }
}
