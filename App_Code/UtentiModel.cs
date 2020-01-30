using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20_01_08servizioSoap.App_Code
{
    public class UtentiModel
    {
        private int _id;
        private String _utente;
        private String _pwd;
        private String _nome;
        private String _cognome;

        public int Id { get => _id; set => _id = value; }
        public string Utente { get => _utente; set => _utente = value; }
        public string Pwd { get => _pwd; set => _pwd = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Cognome { get => _cognome; set => _cognome = value; }
    }
}