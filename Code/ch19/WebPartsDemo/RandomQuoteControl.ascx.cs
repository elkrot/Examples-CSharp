using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPartsDemo
{
    public partial class RandomQuoteControl : System.Web.UI.UserControl
    {
        private static Random rand = new Random();
        private static string[][] quotes = new string[][] {
            new string[]{"There are many tongues to talk, and but few heads to think.","Victor Hugo"},
            new string[]{"To be wicked does not insure prosperity.", "Victor Hugo"},
            new string[]{"Laughter is sunshine; it chases winter from the human face.", "Victor Hugo"},
            new string[]{"Not being heard is no reason for silence.", "Victor Hugo"},
            new string[]{"Not seeing people permits us to imagine in them every perfection.", "Victor Hugo"},
            new string[]{"We should judge a man much more surely from what he dreams than from what he thinks.", "Victor Hugo"},
            new string[]{"If there is anything more poignant than a body agonising for want of bread, it is a soul which is dying of hunger for light.", "Victor Hugo"},
            new string[]{"A compliment is something like a kiss through a veil.", "Victor Hugo"},
            new string[]{"To save yourself by means of that which has ruined you is the masterpiece of great men.", "Victor Hugo"},
            new string[]{"Philosophy is the microscope of thought.", "Victor Hugo"},
            new string[]{"It is nothing to die; it is horrible not to live.", "Victor Hugo"}
            };

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] quote = quotes[rand.Next(0, quotes.Length)];
            QuoteText.Text = quote[0];
            QuoteAuthor.Text = quote[1];
        }
       
    }
}