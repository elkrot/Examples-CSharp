using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPartsDemo
{
    public partial class TimeDisplayControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TimeLabel.Text = DateTime.Now.ToString();
        }
    }
}