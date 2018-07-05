using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Redirect
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                this.Label1.Text = "Choice: " + ProcessingChoice.SelectedValue;
            }
        }

        protected void buttonRedirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("TargetPage.aspx");
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (ProcessingChoice.SelectedValue == "Transfer")
            {
                Server.Transfer("TargetPage.aspx");
            }
        }
    }
}
