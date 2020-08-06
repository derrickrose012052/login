using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_LogoIn.Models
{
    public class CustomersFactory
    {

        public CCustomers[] getBySql(string sql)
        {
            SqlConnection s = new SqlConnection(@"Data Source=.;Initial Catalog=LogoInCustomers;Integrated Security=true;");
            s.Open();
            SqlCommand cmd = new SqlCommand(sql,s);
            SqlDataReader reader = cmd.ExecuteReader();

            List<CCustomers> list = new List<CCustomers>();
            while(reader.Read())
            {
                list.Add(new CCustomers()
                {
                    id =(int)reader["fid"],
                    Email = reader["femail"].ToString(),
                    Password = reader["fpassword"].ToString(),
                    UserName = reader["fusername"].ToString(),
                    Gender = reader["fgender"].ToString(),
                    Address =reader["faddress"].ToString(),
                    Phone = reader["fphone"].ToString(),
                    Photo = reader["fphoto"].ToString()
                });
            }
            s.Close();
            return list.ToArray();
        }

   
        public CCustomers getEmail(string mail)
        {
            string str ="select * from Customers where femail='"+mail+"'";
            CCustomers[] cust = getBySql(str);
            if (cust.Length > 0)
            {
                return cust[0];
            }
            else
                return null;           
        }

        public CCustomers[] getAll()
        {
            string str = "select * from Customers";
            return getBySql(str);
        }

        public CCustomers[] getByKeyword(string keyword)
        {

            string sql = "SELECT * FROM Customers WHERE fusername LIKE '%" + keyword + "%'";
            sql += "OR fgender LIKE '%" + keyword + "%'";
            sql += "OR faddress LIKE '%" + keyword + "%'";
            return getBySql(sql);
        }
        private static void executeSql(string sql)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=LogoInCustomers;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void inserts(CCustomers p)
        {
            string sql = "insert into Customers (";
            sql += "femail,";
            sql += "fpassword,";
            sql += "fusername,";
            sql += "fgender,";
            sql += "faddress,";
            sql += "fphone,";
            sql += "fphoto";
            sql += ")VALUES(";
            sql += "'" + p.Email + "',";
            sql += "'" + p.Password + "',";
            sql += "'" + p.UserName + "',";
            sql += "'" + p.Gender + "',";
            sql += "'" + p.Address + "',";
            sql += "'" + p.Phone + "',";
            sql += "'" + p.Photo + "')";
            executeSql(sql);
        }
    }
}