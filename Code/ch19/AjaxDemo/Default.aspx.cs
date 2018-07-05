using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ButtonIncrement_Click(object sender, EventArgs e)
        {
            int val = Int32.Parse(TextBoxValue.Text);
            ++val;
            TextBoxValue.Text = val.ToString();
        }

        protected void OnTick(object sender, EventArgs e)
        {
            TextBoxTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
