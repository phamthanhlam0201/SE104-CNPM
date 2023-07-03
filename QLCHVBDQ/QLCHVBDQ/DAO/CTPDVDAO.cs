using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHVBDQ.DAO
{
    class CTPDVDAO
    {
        private static CTPDVDAO instance;

        public static CTPDVDAO Instance
        {
            get { if (instance == null) instance = new CTPDVDAO(); return instance; }
            set => Instance = value;
        }
        private CTPDVDAO() { }

        public void DeleteCTPDV(string SoPhieu)
        {
            string query = String.Format("delete from CTPDV where SoPhieu = '{0}'", SoPhieu);
             int data = DataProvider.Instance.ExecuteNonQuery(query);
        }
        public int DeleteCTPDV_SP_MaLDV(string SoPhieu, string TenLDV)
        {
            string query = String.Format("delete from CTPDV where SoPhieu = '{0}' and MaLDV in (select MaLDV from LOAIDICHVU where TenLDV = N'{1}')", SoPhieu, TenLDV);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            return data;
        }
    }
}
