using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Categories.CategoriesSelBox
{
    public partial class CategoriesSelBox
    {
        CoreEngine engine;
        bool required = false;

        public void UpdateData(string code, bool required)
        {
            this.required = required;

            if (ViewState["CategoryOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtCategory.Text = code.TrimEnd();


            if (code != "")
            {
                RefreshListView();
            }

        }

        public void UpdateData(WhereToBuy.entities.Category category, bool required)
        {
            this.required = required;

            if (ViewState["SupplierOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtCategory.Text = category.ToString();

        }


        void SetFormEnvironment()
        {
            ViewState.Add("CategoryOrderBy", "[Codigo]");
            ViewState.Add("CategoryOrderByType", "ASC");

            if (this.required)
            {
                txtCategory.Attributes.Add("required", "");
            }


            SetListBoxEnvironment();

        }



        private void SetListBoxEnvironment()
        {

            txtCategory.Text = string.Empty;
            lvCategories.DataKeyNames = new string[] { "Code", "Description" };
            lvCategories.Items.Clear();

        }


        void RefreshListView()
        {

            List<WhereToBuy.entities.Category> categories;
            string code = txtCategory.Text.TrimStart().TrimEnd();

            if (code != "")
            {
                try
                {
                    engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);

                    categories = engine.Categories.Get(code, true);
                    engine = null;

                    // show data
                    lvCategories.DataSource = categories;
                    lvCategories.DataBind();
                    UpdatePanel1.Update();

                }
                catch (MyException ex)
                {
                    CategoriesSelBoxMessage(this, new CategoriesSelBoxEventArgs(null, ex.Message));
                    return;


                }
                catch (Exception ex)
                {
                    CategoriesSelBoxMessage(this, new CategoriesSelBoxEventArgs(null, ex.Message));
                    return;
                }

            }
            else
            {
                lvCategories.Items.Clear();
                lvCategories.DataBind();
            }

        }

        WhereToBuy.entities.Category LoadCategory(string code)
        {
            WhereToBuy.entities.Category category;
            category = new WhereToBuy.entities.Category();

            try
            {

                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                category = engine.Categories.Get(code);
                engine = null;
            }
            catch (MyException ex)
            {
                CategoriesSelBoxMessage(this, new CategoriesSelBoxEventArgs(null, ex.Message));
                return category;
            }
            catch (Exception ex)
            {
                CategoriesSelBoxMessage(this, new CategoriesSelBoxEventArgs(null, ex.Message));
                return category;
            }
            return category;
        }


        void Clear()
        {
            txtCategory.Text = "";
            lvCategories.Items.Clear();
            lvCategories.DataBind();
        }
    }
}