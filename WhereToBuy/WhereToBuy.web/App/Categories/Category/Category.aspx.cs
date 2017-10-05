﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;
using WhereToBuy.web.UserControls;

namespace WhereToBuy.web.App.Categories.Category
{
    public partial class Category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MessageUC)(CategoryUC.FindControl("MessageUC"))).SubmitButtonClick += Category_MessageButton;

            // Autentication validation
            if (Session["ActualUser"] == null)
            {
                string returnUrlQueryString;

                returnUrlQueryString = string.Format("returnUrl={0}", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath));
                if (Request.QueryString.Count > 0)
                {
                    returnUrlQueryString += Server.UrlEncode(string.Format("?{0}", Request.QueryString.ToString()));
                }

                ///Response.Redirect(string.Format("{0}?{1}", Application["LoginPage"].ToString().TrimEnd(), returnUrlQueryString), true);
                return;
            }


            // Load page
            if (!Page.IsPostBack)
            {
                string code = string.Empty;
                DataState dataState = DataState.None;

                if (Page.Request.QueryString["Code"] != null)
                {
                    code = Page.Request.QueryString["Code"].ToString().TrimEnd();
                }


                // load data
                CategoryUC.UpdateData(code, dataState);

            }

        }



        private void Category_MessageButton(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ic", "invokeButtonClick();", true);

        }
    }
}