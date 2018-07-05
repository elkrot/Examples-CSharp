using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UnhandledExceptionWeb
{
    public partial class _Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Click += new EventHandler(Button1_Click);
            //uncomment to handle error at page-level
            //this.Error += new EventHandler(_Default_Error);
        }

        void Button1_Click(object sender, EventArgs e)
        {
            throw new ArgumentException();
        }

        //void _Default_Error(object sender, EventArgs e)
        //{
        //    this.Response.Redirect("ErrorHandlerPage.aspx");
        //}

        
    }
}
