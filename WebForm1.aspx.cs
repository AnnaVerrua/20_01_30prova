using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _20_01_08servizioSoap
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            String[] n = new String[10];
            localhost.WebService1 servizioSoap = new localhost.WebService1();
            servizioSoap.LeggiUtenti();
        }
    }
}