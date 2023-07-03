using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHVBDQ.DAO
{
    class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            set => Instance = value;
        }
        private AccountDAO() { }
        public int DangKy(string email, string SDT, string tenTK, string ngaySinh, int gioiTinh, string nlMatKhau, string matKhau)
        {
            string query = "EXEC dbo.GetAccountByEmail @email = '" + email + "'" ;
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            if (result.Rows.Count > 0) return 1;
            if (matKhau != nlMatKhau) return 2;
            query = String.Format("EXEC dbo.InsertAccount '{0}', '{1}', N'{2}', '{3}', {4}, '{5}'", email, SDT, tenTK, ngaySinh, gioiTinh, matKhau);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            if (data == 1) return 3;
            return 4;
        }
        public bool Login(string email, string password)
        {
            string query = "EXEC dbo.GetAccountByEmailPassword @email = '" + email + "' , @password = '" + password + "'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result.Rows.Count > 0;
        }
        public string getUserName(string email, string password)
        {
            string query = String.Format("select HoTen from NGUOIDUNG where Email = '{0}' and MatKhau = '{1}'", email, password);
            object data = DataProvider.Instance.ExecuteScalar(query);
            return data.ToString();
        }
        public int Delete_NV(string Email)
        {
            string query = String.Format("delete from NGUOIDUNG where Email = '{0}'", Email);
            int data = DataProvider.Instance.ExecuteNonQuery(query);
            return data;
        }
        
    }
}

