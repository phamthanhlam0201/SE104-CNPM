using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHVBDQ.DAO
{
    class PMHDAO
    {
        private static PMHDAO instance;

        public static PMHDAO Instance
        {
            get { if (instance == null) instance = new PMHDAO(); return instance; }
            set => Instance = value;
        }
        private PMHDAO() { }

        public int DeletePMH(string sp)
        {
            CTPDVDAO.Instance.DeleteCTPDV(sp);
            string query = String.Format("delete from PHIEUMUAHANG where SoPhieu = '{0}'", sp);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            return data;
        }

        public int InsertPMH(string SoPhieu, string NgayLapPhieu, string MaNCC)
        {
            string query = String.Format("select count(SoPhieu) from PHIEUMUAHANG where SoPhieu = '{0}'", SoPhieu);
            string data_SoPhieu = DataProvider.Instance.ExecuteScalar(query).ToString();
            int count_SoPhieu = int.Parse(data_SoPhieu);
            if (count_SoPhieu > 0)
            {
                return 0;
            }
            else
            {
                query = String.Format("INSERT INTO PHIEUMUAHANG VALUES('{0}', '{1}', '{2}', 0)", SoPhieu, NgayLapPhieu, MaNCC);
                if (DataProvider.Instance.ExecuteNonQuery(query) == 1) return 1;
                else return 2;
            }
        }
    }
}
