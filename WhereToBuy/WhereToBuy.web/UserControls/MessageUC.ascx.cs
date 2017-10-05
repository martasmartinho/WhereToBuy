using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WhereToBuy.web.UserControls
{
    public partial class MessageUC : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (MessageLabel.Text != string.Empty)
                {
                    //OnSubmitButtonClick();
                }
            }


        }


        public void ShowError(string title, string text)
        {
            SetError(title, text);
        }


        public void ShowError(string title, string text, string returnUrl)
        {
            ViewState["returnUrl"] = returnUrl;
            SetError(title, text);

        }


        void SetError(string title, string text)
        {
            TitleLabel.Text = title.TrimEnd();

            MessageLabel.Text = PepareHtmlText(text);
            OnSubmitButtonClick();
            UpdatePanel1.Update();
            
        }


        public void ShowInformation(string title, string text)
        {
            SetInformation(title, text);
        }


        public void ShowInformation(string title, string text, string returnUrl)
        {
            ViewState["returnUrl"] = returnUrl;
            SetInformation(title, text);
        }


        void SetInformation(string title, string text)
        {
            TitleLabel.Text = title.TrimEnd();

            MessageLabel.Text = PepareHtmlText(text);
            OnSubmitButtonClick();
            UpdatePanel1.Update();
           
        }


        string PepareHtmlText(string text)
        {
            text = text.Replace("[", "<div>").Replace("]", "</div>");
            text = text.Replace("#", "<u>").Replace("$", "</u>:").Replace(".", ";<br>");

            return text.TrimEnd();
        }

        public event EventHandler SubmitButtonClick;

        protected void OnSubmitButtonClick()
        {
            if (SubmitButtonClick != null)
            {
                SubmitButtonClick(this, new EventArgs());
                //UpdatePanel1.Update();
            }
           
        }

    }
}