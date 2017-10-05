using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Products.ProductsMatching
{
    public partial class ProductsMatchingUC
    {
        CoreEngine engine;


        public void UpdateData(string supplierCode, string code, DataState dataState)
        {

            if (ViewState["ProductMatchingOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            ExternalCodeTextBox.Text = code.TrimEnd();
            ActiveRadioButton.Checked = false;
            InactiveRadioButton.Checked = false;
            AllRadioButton.Checked = false;
            MatchingRadioButton.Checked = false;

            // Select correct radiobutton
            switch (dataState)
            {
                case DataState.Active:
                    ActiveRadioButton.Checked = true;
                    break;
                case DataState.Inactive:
                    InactiveRadioButton.Checked = true;
                    break;
                case DataState.All:
                    AllRadioButton.Checked = true;
                    break;
                default:
                    MatchingRadioButton.Checked = true;
                    break;
            }

            SuppliersSelBox.UpdateData(supplierCode, true);
            RefreshGridView();
            UpdatePanel1.Update();
        }


        void SetFormEnvironment()
        {
            ViewState.Add("ProductMatchingOrderBy", "[FornecedorCodigo]");
            ViewState.Add("ProductMatchingOrderByType", "ASC");
            ExternalCodeTextBox.MaxLength = ProductMatchingSpecs.Code_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();
        }


        void SetGridViewEnvironment()
        {
            ProductMatchingGridView.AutoGenerateColumns = false;
            ProductMatchingGridView.ShowHeader = true;
            ProductMatchingGridView.ShowFooter = true;
            ProductMatchingGridView.AllowSorting = true;
            ProductMatchingGridView.AllowPaging = true;
            ProductMatchingGridView.PageSize = 10;
            ProductMatchingGridView.Height = 360;
            ProductMatchingGridView.DataKeyNames = new string[] { "Code", "Supplier", "Supplement" };
            ProductMatchingGridView.SelectedIndex = -1;
            ProductMatchingGridView.AllowPaging = true;
            ProductMatchingGridView.PageIndex = 0;
            ProductMatchingGridView.Columns[0].Visible = false;
        }


        void RefreshGridView()
        {
            Supplier supplier = null;
            string code;
            string supplement = string.Empty;
            DataState dataState;
            bool withCustomization;
            bool closeReset;
            string orderBy;
            List<WhereToBuy.entities.ProductMatching> productsMatching;

            withCustomization = ResetRadioButton.Checked;
            closeReset = CustomRadioButton.Checked;

            // Filter data
            code = ExternalCodeTextBox.Text.TrimStart().TrimEnd();

            if (ActiveRadioButton.Checked == true)
            {
                dataState = DataState.Active;
            }
            else if (InactiveRadioButton.Checked == true)
            {
                dataState = DataState.Inactive;
            }
            else if (MatchingRadioButton.Checked == true)
            {
                dataState = DataState.None;
            }
            else
            {
                dataState = DataState.All;
            }

            if (SelectedSupplierExist)
            {
                supplier = GetSelectedSupplier();
            }

            // Orderby instruction
            orderBy = ViewState["ProductMatchingOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["ProductMatchingOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                productsMatching = engine.ProductsMatching.Get(supplier, code, supplement, dataState, withCustomization, closeReset, orderBy, 0, 0);
                engine = null;

                // Select selected object
                if (ViewState["SelectedProductMatching"] != null)
                {
                    SetSelectedIndex(ref productsMatching);
                }

                // show data
                ProductMatchingGridView.DataSource = productsMatching;
                ProductMatchingGridView.DataBind();

                // update pager
                if (productsMatching.Count > 0)
                {
                    GridViewRow PagerRow = ProductMatchingGridView.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("ActualPageLabel");
                    label.Text = string.Format(" {0} ... {1} ", ProductMatchingGridView.PageIndex + 1, ProductMatchingGridView.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.ProductMatching> productsMatching)
        {
            WhereToBuy.entities.ProductMatching productMatching = (WhereToBuy.entities.ProductMatching)Session["SelectedProductMatching"];

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
            */
            int firstPageItemIndex = ProductMatchingGridView.PageIndex * ProductMatchingGridView.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (ProductMatchingGridView.PageIndex != (ProductMatchingGridView.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + ProductMatchingGridView.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = productsMatching.Count - 1;
            }

            objectIndex = productsMatching.IndexOf(productMatching);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                ProductMatchingGridView.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                ProductMatchingGridView.SelectedIndex = -1;
            }
        }


        void LoadProductMatching(Supplier supplier, string codigo, string supplement)
        {
            WhereToBuy.entities.ProductMatching productMatching;

            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                productMatching = engine.ProductsMatching.Get(supplier, codigo, supplement, 1, 1);
                SetSelectedMatching(productMatching);
                ViewState["SelectedProductMatching"] = productMatching;
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
            ActiveRadioButton.Checked = false;
            InactiveRadioButton.Checked = false;
            AllRadioButton.Checked = false;
            MatchingRadioButton.Checked = true;
            CustomRadioButton.Checked = false;
            ResetRadioButton.Checked = false;
            ExternalCodeTextBox.Text = "";
            ProductMatchingGridView.PageIndex = 0;
            SuppliersSelBox.UpdateData("", true);
            SetSelectedSupplier(null);
            RefreshGridView();
            UpdatePanel1.Update();
        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.ProductMatching rv = new WhereToBuy.entities.ProductMatching();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((ProductMatchingGridView.DataSource) as List<WhereToBuy.entities.ProductMatching>).Count();
        }
    }
}