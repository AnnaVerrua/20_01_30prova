using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using _20_01_08servizioSoap.App_Code;


namespace _20_01_08servizioSoap
{
    /// <summary>
    /// Descrizione di riepilogo per WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public List<String> LeggiUtenti()
        {
            List<String> nomi = new List<string>();
            UtentiController u = new UtentiController();
            u.ReadAllUtenti();
            for (int i = 0; i < u.LstUtenti.Count(); i++)
                nomi.Add(u.LstUtenti[i].Nome);
            return nomi;
        }
    }
}
