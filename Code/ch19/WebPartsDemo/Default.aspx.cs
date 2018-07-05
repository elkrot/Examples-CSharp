using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace WebPartsDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (WebPartDisplayMode mode in WebPartManager1.SupportedDisplayModes)
                {
                    DropDownListSupportedModes.Items.Add(mode.Name);
                }
            }
        }

        protected void OnDisplayModeChanged(object sender, EventArgs e)
        {
            WebPartManager1.DisplayMode = WebPartManager1.DisplayModes[DropDownListSupportedModes.SelectedValue];
        }
    }
}
