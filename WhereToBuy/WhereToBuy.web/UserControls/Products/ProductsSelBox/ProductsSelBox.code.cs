using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Products.ProductsSelBox
{
    public partial class ProductsSelBox
    {
        CoreEngine engine;
        bool required = false;

        public void UpdateData(string code, bool required)
        {
            this.required = required;

            if (ViewState["ProductOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtProduct.Text = code.TrimEnd();


            if (code != "")
            {
                RefreshListView();
            }

        }

        public void UpdateData(WhereToBuy.entities.Brand brand, bool required)
        {
            this.required = required;

            if (ViewState["ProductOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtProduct.Text = brand.ToString();

        }


        void SetFormEnvironment()
        {
            ViewState.Add("ProductOrderBy", "[Codigo]");
            ViewState.Add("ProductOrderByType", "ASC");

            //txtProduct.MaxLength = ProductSpecs.Code_MaxSize;

            if (this.required)
            {

                txtProduct.Attributes.Add("required", "");
            }


            SetListBoxEnvironment();

        }


        private void SetListBoxEnvironment()
        {

            txtProduct.Text = string.Empty;
            lvProducts.DataKeyNames = new string[] { "Code", "Description" };
            lvProducts.Items.Clear();

        }


        void RefreshListView()
        {

            List<WhereToBuy.entities.Product> products;
            string code = txtProduct.Text.TrimStart().TrimEnd();

            if (code != "")
            {
                try
                {
                    engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);

                    products = engine.Products.Get(code, true);
                    engine = null;



                    // show data
                    lvProducts.DataSource = products;
                    lvProducts.DataBind();
                    UpdatePanel1.Update();

                }
                catch (MyException ex)
                {
                    ProductSelBoxMessage(this, new ProductSelBoxEventArgs(null, ex.Message));
                    return;


                }
                catch (Exception ex)
                {
                    ProductSelBoxMessage(this, new ProductSelBoxEventArgs(null, ex.Message));
                    return;
                }

            }
            else
            {
                lvProducts.Items.Clear();
                lvProducts.DataBind();
            }

        }

        WhereToBuy.entities.Product LoadProduct(string code)
        {
            WhereToBuy.entities.Product product;
            product = new WhereToBuy.entities.Product();

            try
            {

                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                product = engine.Products.Get(code, 0);
                engine = null;
            }
            catch (MyException ex)
            {
                ProductSelBoxMessage(this, new ProductSelBoxEventArgs(null, ex.Message));
                return product;
            }
            catch (Exception ex)
            {
                ProductSelBoxMessage(this, new ProductSelBoxEventArgs(null, ex.Message));
                return product;
            }
            return product;
        }


        void Clear()
        {
            txtProduct.Text = "";
            lvProducts.Items.Clear();
            lvProducts.DataBind();
        }
    }
}