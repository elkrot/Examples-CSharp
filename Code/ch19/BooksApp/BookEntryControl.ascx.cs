using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BooksApp
{
    public partial class BookEntryControl : System.Web.UI.UserControl
    {
        public int BookID
        {
            get { return int.Parse(TextBoxID.Text); }
            set { TextBoxID.Text = value.ToString(); }
        }

        public string Title         
        {
            get { return TextBoxTitle.Text; }
            set { TextBoxTitle.Text = value; }
        }

        public string Author
        {
            get { return TextBoxAuthor.Text; }
            set { TextBoxAuthor.Text = value; }
        }

        public int PublishYear
        {
            get { return int.Parse(TextBoxPublishYear.Text); }
            set { TextBoxPublishYear.Text = value.ToString(); }
        }

        public bool IsEditable
        {
            get { return TextBoxID.ReadOnly; }
            set { TextBoxID.ReadOnly = TextBoxTitle.ReadOnly =
                  TextBoxAuthor.ReadOnly = TextBoxPublishYear.ReadOnly = !value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}