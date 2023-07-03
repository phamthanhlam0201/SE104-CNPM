using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHVBDQ.DAO
{
    class CTPMHDAO
    {
        private static CTPMHDAO instance;

        public static CTPMHDAO Instance
        {
            get { if (instance == null) instance = new CTPMHDAO(); return instance; }
            set => Instance = value;
        }
        private CTPMHDAO() { }

        public void DeleteCTPMH(string SoPhieu)
        {
            string query = String.Format("delete from CTPMH where SoPhieu = '{0}'", SoPhieu);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
        }
        public int DeleteCTPMH_SP_TenSP(string SoPhieu, string TenSP)
        {
            string query = String.Format("delete from CTPMH where SoPhieu = '{0}' and MaSP in (select MaSP from SANPHAM where TenSP = N'{1}')", SoPhieu, TenSP);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            return data;
        }
    }
}
