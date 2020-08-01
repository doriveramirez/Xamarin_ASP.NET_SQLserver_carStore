using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Text;

namespace APPValper.Services
{
    class LoginActiveDirectory
    {
        public static string Name, NTusername, version;
        public string message;
        public void Login(string Username, string Password)
        {
            if (AuthUser(Username, Password))
            {
                Console.WriteLine("Has accedido");
                //se accede
            }
        }
        string co = string.Empty;
        private bool AuthUser(string Username, string Password)
        {
            bool ret = false;
            try
            {
                DirectoryEntry de = new DirectoryEntry(GetCurrentDomainPath(), Username, Password);
                DirectorySearcher dsearch = new DirectorySearcher(de);
                dsearch.Filter = "sAMAccountName=" + Username + "";
                SearchResult results = null;
                results = dsearch.FindOne();

                Name = results.GetDirectoryEntry().Properties["DisplayName"].Value.ToString();
                NTusername = results.GetDirectoryEntry().Properties["sAMAccountName"].Value.ToString();
                co = results.GetDirectoryEntry().Properties["deparment"].Value.ToString();
                if (GetNTuser(NTusername)){
                    ret = true;
                } else
                {
                    message = "La información proporcionada es correcta,\n pero su usuario no tiene permiso para acceder.";
                }
            } catch (Exception ex)
            {
                ret = false;
                message = "Error: " + ex.Message;
            }
            return ret;
        }
        private string GetCurrentDomainPath()
        {
            DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");
            //LDAP://na.miempresa.com
            return "LDAP://" + de.Properties["defaultNamingContext"][0].ToString();
        }
        bool status = false;
        public bool GetNTuser(string NTuserAD)
        {
            SqlConnection conn = new SqlConnection("conexion");
            SqlCommand queryNT = new SqlCommand("SELECT * FROM Usuarios WHERE NTUser ='" + NTuserAD + "' and Permisos = " + 1 + "", conn);
            SqlDataReader ReaderNt;
            conn.Open();
            ReaderNt = queryNT.ExecuteReader();
            if (ReaderNt.Read())
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }
    }
}
