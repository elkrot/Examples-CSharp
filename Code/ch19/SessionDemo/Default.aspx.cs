using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SessionDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        private const bool useCookie = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession && useCookie)
            {
                ReadCookie();
            }
            SetVisibility();
        }

        private void SetVisibility()
        {
            bool hasUser = !string.IsNullOrEmpty(Session["UserName"] as string);
            LabelHello.Visible = LabelName.Visible = hasUser;
            LabelEnterYourName.Visible = TextBoxName.Visible = ButtonSubmit.Visible = !hasUser;
            if (hasUser)
            {
                LabelName.Text = Session["UserName"] as string;
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            Session["UserName"] = TextBoxName.Text;
            
            SetVisibility();

            if (useCookie)
            {
                WriteCookie();
            }
        }

        private void ReadCookie()
        {
            foreach (string cookie in Request.Cookies)
            {
                if (cookie == "username")
                {
                    Session["UserName"] = Request.Cookies[cookie].Value;
                    return;
                }
            }
        }

        private void WriteCookie()
        {
            HttpCookie cookie = new HttpCookie("username", Session["UserName"] as string);
            //comment out if you want a session-cookie 
            cookie.Expires = DateTime.Now.AddSeconds(30);
            Response.Cookies.Add(cookie);
        }
    }
}
