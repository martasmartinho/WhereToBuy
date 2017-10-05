using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Catalogs.CatalogsSelBox
{
    public partial class CatalogsSelBox
    {
        CoreEngine engine;
        bool required = false;

        public void UpdateData(string code, bool required)
        {
            this.required = required;

            if (ViewState["CatalogOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtCatalog.Text = code.TrimEnd();


            if (code != "")
            {
                RefreshListView();
            }

        }

        public void UpdateData(WhereToBuy.entities.Catalog catalog, bool required)
        {
            this.required = required;

            if (ViewState["SupplierOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtCatalog.Text = catalog.ToString();

        }


        void SetFormEnvironment()
        {
            ViewState.Add("CatalogOrderBy", "[Codigo]");
            ViewState.Add("CatalogOrderByType", "ASC");

            if (this.required)
            {
                txtCatalog.Attributes.Add("required", "");
            }


            SetListBoxEnvironment();

        }



        private void SetListBoxEnvironment()
        {

            txtCatalog.Text = string.Empty;
            lvCatalogs.DataKeyNames = new string[] { "Code", "Description" };
            lvCatalogs.Items.Clear();

        }


        void RefreshListView()
        {

            List<WhereToBuy.entities.Catalog> catalogs;
            string code = txtCatalog.Text.TrimStart().TrimEnd();

            if (code != "")
            {
                try
                {
                    engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);

                    catalogs = engine.Catalogs.Get(code, true);
                    engine = null;



                    // show data
                    lvCatalogs.DataSource = catalogs;
                    lvCatalogs.DataBind();
                    UpdatePanel1.Update();

                }
                catch (MyException ex)
                {
                    CatalogsSelBoxMessage(this, new CatalogsSelBoxEventArgs(null, ex.Message));
                    return;


                }
                catch (Exception ex)
                {
                    CatalogsSelBoxMessage(this, new CatalogsSelBoxEventArgs(null, ex.Message));
                    return;
                }

            }
            else
            {
                lvCatalogs.Items.Clear();
                lvCatalogs.DataBind();
            }

        }

        WhereToBuy.entities.Catalog LoadCatalog(string code)
        {
            WhereToBuy.entities.Catalog catalog;
            catalog = new WhereToBuy.entities.Catalog();

            try
            {

                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                catalog = engine.Catalogs.Get(code);
                engine = null;
            }
            catch (MyException ex)
            {
                CatalogsSelBoxMessage(this, new CatalogsSelBoxEventArgs(null, ex.Message));
                return catalog;
            }
            catch (Exception ex)
            {
                CatalogsSelBoxMessage(this, new CatalogsSelBoxEventArgs(null, ex.Message));
                return catalog;
            }
            return catalog;
        }


        void Clear()
        {
            txtCatalog.Text = "";
            lvCatalogs.Items.Clear();
            lvCatalogs.DataBind();
        }
    }
}