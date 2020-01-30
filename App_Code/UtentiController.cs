using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace _20_01_08servizioSoap.App_Code
{
    public class UtentiController
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader dr;
        private UtentiModel _utente;
        private List<UtentiModel> _lstUtenti;
        private String _errore;
        private String _connectionString = System.Configuration.ConfigurationManager.AppSettings["connection"];
        private String _nameDB = System.Configuration.ConfigurationManager.AppSettings["nameDB"];

        public UtentiModel Utente
        {
            get { return _utente; }
            set { _utente=value; }
        }

        public List<UtentiModel> LstUtenti
        {
            get { return _lstUtenti; }
        }

        public String Errore
        {
            get { return _errore; }
        }

        public UtentiController()
        {
            _nameDB = HttpContext.Current.Server.MapPath(_nameDB);
            _connectionString = _connectionString + _nameDB + @";Integrated Security=True";

        }

        public void Create(String user, String password, String name, String surname)
        {
            String PwdSHA256;
            conn = new SqlConnection(@_connectionString);
            cmd = new SqlCommand();

            SHA256 mySHA256 = SHA256.Create();
            //calcolo del codice hash
            byte[] hashvalue = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
            //Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for(int i=0;i<hashvalue.Length;i++)
            {
                builder.Append(hashvalue[i].ToString("x2"));  
                //x2 -> HEX a 2 cifre
            }
            PwdSHA256 = builder.ToString();
            _utente = new UtentiModel();
            _utente.Pwd = PwdSHA256;
            _utente.Utente = user;
            _utente.Nome = name;
            _utente.Cognome = surname;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@user", _utente.Utente);
                cmd.Parameters.AddWithValue("@password", _utente.Pwd);
                cmd.Parameters.AddWithValue("@nome", _utente.Nome);
                cmd.Parameters.AddWithValue("@cognome", _utente.Cognome);
                cmd.CommandText = "insert into utenti (utente, pwd, nome, cognome) values (@user, @password, @nome, @cognome)";
                //cmd.CommandText = "insert into utenti (Id, utente, pwd, nome, cognome) values (@user, @password, @nome, @cognome)";
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                this._errore = ex.Message;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }

        }
        public IEnumerable<UtentiModel> ReadAllUtenti()
        {
            _lstUtenti = new List<UtentiModel>();
            conn = new SqlConnection(@_connectionString);
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from utenti";
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                _utente = new UtentiModel();
                _utente.Utente = dr["utente"].ToString();
                _utente.Pwd = dr["pwd"].ToString();
                _utente.Nome = dr["nome"].ToString();
                _utente.Cognome = dr["cognome"].ToString();
                _lstUtenti.Add(_utente);
            }
            return _lstUtenti;
        }
        public void Update(String user, String password, String name, String surname)
        {
            String PwdSHA256;
            conn = new SqlConnection(@_connectionString);
            cmd = new SqlCommand();

            SHA256 mySHA256 = SHA256.Create();
            //calcolo del codice hash
            byte[] hashvalue = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
            //Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashvalue.Length; i++)
            {
                builder.Append(hashvalue[i].ToString("x2"));
                //x2 -> HEX a 2 cifre
            }
            PwdSHA256 = builder.ToString();
            _utente = new UtentiModel();
            _utente.Pwd = PwdSHA256;
            _utente.Utente = user;
            _utente.Nome = name;
            _utente.Cognome = surname;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@user", _utente.Utente);
                cmd.Parameters.AddWithValue("@password", _utente.Pwd);
                cmd.Parameters.AddWithValue("@nome", _utente.Nome);
                cmd.Parameters.AddWithValue("@cognome", _utente.Cognome);
                cmd.CommandText = "UPDATE utenti SET pwd=@password, nome=@nome, cognome=@cognome WHERE utente=@user;";
                //cmd.CommandText = "insert into utenti (Id, utente, pwd, nome, cognome) values (@user, @password, @nome, @cognome)";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this._errore = ex.Message;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }

        }
    }
}