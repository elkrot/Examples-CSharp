using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace AuthDemo
{
    public partial class LoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (AuthenticateUser(TextBoxUsername.Text, TextBoxPassword.Text))
            {
                //want to keep user logged in? pass true instead
                FormsAuthentication.RedirectFromLoginPage(TextBoxUsername.Text, false);
            }
            else
            {
                LabelStatus.Text = "Oops";
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            //you could authenticate against a database or a file here
            return username == "user" && password == "pass";
        }
    }
}
