using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Brands.BrandsSelBox
{
    public partial class BrandsSelBox
    {
        CoreEngine engine;
        bool required = false;

        public void UpdateData(string code, bool required)
        {
            this.required = required;

            if (ViewState["BrandOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtBrand.Text = code.TrimEnd();


            if (code != "")
            {
                RefreshListView();
            }

        }

        public void UpdateData(WhereToBuy.entities.Brand brand, bool required)
        {
            this.required = required;

            if (ViewState["BrandOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtBrand.Text = brand.ToString();

        }


        void SetFormEnvironment()
        {
            ViewState.Add("BrandOrderBy", "[Codigo]");
            ViewState.Add("BrandOrderByType", "ASC");

            txtBrand.MaxLength = BrandSpecs.Code_MaxSize;

            if (this.required)
            {
                
               txtBrand.Attributes.Add("required", "");
            }
           

            SetListBoxEnvironment();

        }



        private void SetListBoxEnvironment()
        {

            txtBrand.Text = string.Empty;
            lvBrands.DataKeyNames = new string[] { "Code", "Description" };
            lvBrands.Items.Clear();

        }


        void RefreshListView()
        {

            List<WhereToBuy.entities.Brand> brands;
            string code = txtBrand.Text.TrimStart().TrimEnd();

            if (code != "")
            {
                try
                {
                    engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);

                    brands = engine.Brands.Get(code, true);
                    engine = null;



                    // show data
                    lvBrands.DataSource = brands;
                    lvBrands.DataBind();
                    UpdatePanel1.Update();

                }
                catch (MyException ex)
                {
                    BrandSelBoxMessage(this, new BrandSelBoxEventArgs(null, ex.Message));
                    return;
                    

                }
                catch (Exception ex)
                {
                    BrandSelBoxMessage(this, new BrandSelBoxEventArgs(null, ex.Message));
                    return;
                }

            }
            else
            {
                lvBrands.Items.Clear();
                lvBrands.DataBind();
            }

        }

        WhereToBuy.entities.Brand LoadBrand(string code)
        {
            WhereToBuy.entities.Brand brand;
            brand = new WhereToBuy.entities.Brand();

            try
            {

                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                brand = engine.Brands.Get(code);
                engine = null;
            }
            catch (MyException ex)
            {
                BrandSelBoxMessage(this, new BrandSelBoxEventArgs(null, ex.Message));
                return brand;
            }
            catch (Exception ex)
            {
                BrandSelBoxMessage(this, new BrandSelBoxEventArgs(null, ex.Message));
                return brand;
            }
            return brand;
        }


        void Clear()
        {
            txtBrand.Text = "";
            lvBrands.Items.Clear();
            lvBrands.DataBind();
        }

    }
}