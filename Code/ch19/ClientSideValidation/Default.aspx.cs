using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientSideValidation
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void OnValidatePrime(object source, ServerValidateEventArgs args)
        {
            int val = 0;
            args.IsValid = false;
            if (int.TryParse(args.Value, out val))
            {
                args.IsValid = (val < 1000) && IsPrime(val);
            }
            
        }

        private bool IsPrime(int number)
        {
            //check for evenness
            if (number % 2 == 0)
            {
                if (number == 2)
                    return true;
                return false;
            }
            //don't need to check past the square root
            int max = (int)Math.Sqrt(number);
            for (int i = 3; i <= max; i += 2)
            {
                if ((number % i) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /* Now that data is being submitted, do your server-side validation
                 * here (or at some point before trusting it). */

                /*if (string.IsNullOrEmpty(TextBoxName.Text))
                {
                    Response.Redirect("MyErrorPage.aspx");
                }*/
            }
         }

    }
}
