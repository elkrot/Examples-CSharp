using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BrowserCapsAndTracing
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Trace.Write("Creating Capabilities Table");
                Table1.Rows.AddRange(
                    new TableRow[] 
                    {
                        CreateCapabilityRow("ActiveX", Request.Browser.ActiveXControls),
                        CreateCapabilityRow("AOL", Request.Browser.AOL),
                        CreateCapabilityRow("Background Sounds", Request.Browser.BackgroundSounds),
                        CreateCapabilityRow("Beta", Request.Browser.Beta),
                        CreateCapabilityRow("Browser",Request.Browser.Browser),
                        CreateCapabilityRow("Can Render After Input or Select Element", Request.Browser.CanRenderAfterInputOrSelectElement),
                        CreateCapabilityRow("Can Send Mail", Request.Browser.CanSendMail),
                        CreateCapabilityRow("CLR Version", Request.Browser.ClrVersion),
                        CreateCapabilityRow("Cookies", Request.Browser.Cookies),
                        CreateCapabilityRow("Crawler", Request.Browser.Crawler),
                        CreateCapabilityRow("Default Submit Button Limit", Request.Browser.DefaultSubmitButtonLimit),
                        CreateCapabilityRow("ECMA Script Version",Request.Browser.EcmaScriptVersion),
                        CreateCapabilityRow("Frames", Request.Browser.Frames),
                        CreateCapabilityRow("Has Back Button", Request.Browser.HasBackButton),
                        CreateCapabilityRow("Input Type", Request.Browser.InputType),
                        CreateCapabilityRow("Is Color", Request.Browser.IsColor),
                        CreateCapabilityRow("Is Mobile Device", Request.Browser.IsMobileDevice),
                        CreateCapabilityRow("Java Applets", Request.Browser.JavaApplets),
                        CreateCapabilityRow("JavaScript Version", Request.Browser.JScriptVersion),
                        CreateCapabilityRow("Maximum HREF Length", Request.Browser.MaximumHrefLength),
                        CreateCapabilityRow("Maximum Rendered Page Size", Request.Browser.MaximumRenderedPageSize),
                        CreateCapabilityRow("Minor Version String", Request.Browser.MinorVersionString),
                        CreateCapabilityRow("Platform", Request.Browser.Platform),
                        CreateCapabilityRow("Preferred Image MIME", Request.Browser.PreferredImageMime),
                        CreateCapabilityRow("Preferred Rendering Type", Request.Browser.PreferredRenderingType),
                        CreateCapabilityRow("Renders Breaks After HTML Lists", Request.Browser.RendersBreaksAfterHtmlLists),
                        CreateCapabilityRow("Requires No Break In Formatting", Request.Browser.RequiresNoBreakInFormatting),
                        CreateCapabilityRow("Screen Bit Depth", Request.Browser.ScreenBitDepth),
                        CreateCapabilityRow("Supports CSS", Request.Browser.SupportsCss),
                        CreateCapabilityRow("Tables", Request.Browser.Tables),
                        CreateCapabilityRow("Type", Request.Browser.Type),
                    });
            }
        }

        private TableRow CreateCapabilityRow(string capability, object value)
        {
            TableRow row = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.Text = capability;
            TableCell cell2 = new TableCell();
            cell2.Text = value.ToString();
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            return row;
        }
    }
}
