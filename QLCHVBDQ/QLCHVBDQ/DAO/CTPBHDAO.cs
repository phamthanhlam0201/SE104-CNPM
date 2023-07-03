using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHVBDQ.DAO
{
    class CTPBHDAO
    {
        private static CTPBHDAO instance;

        public static CTPBHDAO Instance
        {
            get { if (instance == null) instance = new CTPBHDAO(); return instance; }
            set => Instance = value;
        }
        private CTPBHDAO() { }

        public void DeleteCTPBH(string SoPhieu)
        {
            string query = String.Format("delete from CTPBH where SoPhieu = '{0}'", SoPhieu);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
        }
        public int DeleteCTPBH_SP_TenSP(string SoPhieu, string TenSP)
        {
            string query = String.Format("delete from CTPBH where SoPhieu = '{0}' and MaSP in (select MaSP from SANPHAM where TenSP = N'{1}')", SoPhieu, TenSP);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            return data;
        }
    }
}
