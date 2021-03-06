﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.WorryingTerms.WorryingTerm
{
    public partial class WorryingTermUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Load page
            if (!Page.IsPostBack)
            {
                string term = string.Empty;
                DataState dataState = DataState.None;

                if (Page.Request.QueryString["Term"] != null)
                {
                    term = Page.Request.QueryString["Term"].ToString().TrimEnd();
                }


                // load data
                UpdateData(term, dataState);

            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        protected void UpdatePanel1_Init(object sender, EventArgs e)
        {

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

            Save();


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        protected void lnkTermSearch_Click(object sender, EventArgs e)
        {
            //string code = txtTerm.Text.TrimStart().TrimEnd();
            //LoadWorryingTerm(code);
        }
    }
}