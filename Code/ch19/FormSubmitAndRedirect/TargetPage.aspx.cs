﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Redirect
{
    public partial class TargetPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["ProcessingChoice"] != null)
            {
                LabelOutput.Text = "Choice: " + Request.Params["ProcessingChoice"];
            }
        }
    }
}
