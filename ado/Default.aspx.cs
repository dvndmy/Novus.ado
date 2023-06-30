using System;
using System.Configuration;
using System.Web.UI;

namespace ado
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Hello World");
            Response.Write("<br/>");
            string s = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            Response.Write(s);

        }
    }
}