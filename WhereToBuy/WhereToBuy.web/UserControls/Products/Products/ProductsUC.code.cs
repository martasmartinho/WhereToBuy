using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Products.Products
{
    public partial class ProductsUC
    {
        CoreEngine engine;


        public void UpdateData()
        {

            if (ViewState["ProductOrderBy"] == null)
            {
                SetFormEnvironment();
            }

       
            CurrentRadioButton.Checked = true;
            InactiveRadioButton.Checked = false;
            AllRadioButton.Checked = false;
            IPCRadioButton.Checked = false;
            ManualUpdateRadioButton.Checked = false;

            RefreshGridView();
            UpdatePanel1.Update();
        }


        void SetFormEnvironment()
        {
            ViewState.Add("ProductOrderBy", "[Codigo]");
            ViewState.Add("ProductOrderByType", "ASC");
            PartnumberTxt.MaxLength = ProductSpecs.Partnumber_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();
        }


        void SetGridViewEnvironment()
        {
            ProductGridView.AutoGenerateColumns = false;
            ProductGridView.ShowHeader = true;
            ProductGridView.ShowFooter = true;
            ProductGridView.AllowSorting = true;
            ProductGridView.AllowPaging = true;
            ProductGridView.PageSize = 10;
            ProductGridView.Height = 360;
            ProductGridView.DataKeyNames = new string[] { "Code" };
            ProductGridView.SelectedIndex = -1;
            ProductGridView.AllowPaging = true;
            ProductGridView.PageIndex = 0;
            ProductGridView.Columns[0].Visible = false;
        }


        void RefreshGridView()
        {
            Supplier supplier = null;
            Brand brand = null;
            Category category = null;
            string partnumber;
            ProductFilter productFilter = ProductFilter.All;
            string orderBy;
            List<WhereToBuy.entities.Product> products;

            // Filter data
            partnumber = PartnumberTxt.Text.TrimStart().TrimEnd();

            if (CurrentRadioButton.Checked == true)
            {
                productFilter = ProductFilter.Current;
            }
            else if (ManualUpdateRadioButton.Checked == true)
            {
                productFilter = ProductFilter.ManualUpdate ;
            }
            else if (IPCRadioButton.Checked == true)
            {
                productFilter = ProductFilter.IPCGreaterZero;
            }
             else if (DiscontinuedRadioButton.Checked == true)
            {
                productFilter = ProductFilter.Discontinued;
            }
                 else if (InactiveRadioButton.Checked == true)
            {
                productFilter = ProductFilter.Inactive;
            }
            else
            {
                productFilter = ProductFilter.All;
            }

            if (SelectedSupplierExist)
            {
                supplier = GetSelectedSupplier();
            }

            if (SelectedBrandExist)
            {
                brand = GetSelectedBrand();
            }

            if (SelectedCategoryExist)
            {
                category = GetSelectedCategory();
            }



            // Orderby instruction
            orderBy = ViewState["ProductOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["ProductOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                products = engine.Products.Get(productFilter, brand, category, supplier, partnumber, orderBy);
                engine = null;

                // Select selected object
                if (ViewState["SelectedProduct"] != null)
                {
                    SetSelectedIndex(ref products);
                }

                // show data
                ProductGridView.DataSource = products;
                ProductGridView.DataBind();

                // update pager
                if (products.Count > 0)
                {
                    GridViewRow PagerRow = ProductGridView.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("ActualPageLabel");
                    label.Text = string.Format(" {0} ... {1} ", ProductGridView.PageIndex + 1, ProductGridView.PageCount);
                }
            }
            catch (MyException ex)
            {

                this.MessageUC.ShowError("Erro", ex.Message);
                return;
            }
            catch (Exception ex)
            {
                this.MessageUC.ShowError("Erro", ex.Message);
                return;
            }
        }


        void SetSelectedIndex(ref List<WhereToBuy.entities.Product> products)
        {
            WhereToBuy.entities.Product product = (WhereToBuy.entities.Product)Session["SelectedProduct"];

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
            */
            int firstPageItemIndex = ProductGridView.PageIndex * ProductGridView.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (ProductGridView.PageIndex != (ProductGridView.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + ProductGridView.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = products.Count - 1;
            }

            objectIndex = products.IndexOf(product);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                ProductGridView.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                ProductGridView.SelectedIndex = -1;
            }
        }


        void LoadProduct(string code)
        {
            WhereToBuy.entities.Product product;

            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                product = engine.Products.Get(code, 0);
                SetSelectedProduct(product);
                engine = null;
            }
            catch (MyException ex)
            {
                this.MessageUC.ShowError("Erro", ex.Message);
                return;
            }
            catch (Exception ex)
            {
                this.MessageUC.ShowError("Erro", ex.Message);
                return;
            }
        }




        void ClearFilter()
        {
            CurrentRadioButton.Checked = true;
            InactiveRadioButton.Checked = false;
            AllRadioButton.Checked = false;
            ManualUpdateRadioButton.Checked = true;
            IPCRadioButton.Checked = false;
            DiscontinuedRadioButton.Checked = false;
            PartnumberTxt.Text = "";
            ProductGridView.PageIndex = 0;
            SuppliersSelBox.UpdateData("", true);
            SetSelectedSupplier(null);
            BrandsSelBox.UpdateData("", true);
            SetSelectedBrand(null);
            CategoriesSelBox.UpdateData("", true);
            SetSelectedCategory(null);
            RefreshGridView();
            UpdatePanel1.Update();
        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.Product rv = new WhereToBuy.entities.Product();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((ProductGridView.DataSource) as List<WhereToBuy.entities.Product>).Count();
        }
    }
}