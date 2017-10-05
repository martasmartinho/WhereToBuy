using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.QuotationRules.QuotationRules
{
    public partial class QuotationRulesUC
    {
        CoreEngine engine;

        public void UpdateData()
        {

            if (ViewState["QuotationRuleOrderBy"] == null)
            {
                SetFormEnvironment();
            }


            btnCustomization.Checked = true;
            btnReset.Checked = false;
            btnAll.Checked = false;

            

            RefreshGridView();

            UpdatePanel1.Update();


        }


        void SetFormEnvironment()
        {
            ViewState.Add("QuotationRuleOrderBy", "[FornecedorNome]");
            ViewState.Add("QuotationRuleOrderByType", "ASC");

            ClearFilter();

            // Prepare GRIDVIEW
            SetGridViewEnvironment();



        }


        void SetGridViewEnvironment()
        {
            gvQuotationRules.AutoGenerateColumns = false;
            gvQuotationRules.ShowHeader = true;
            gvQuotationRules.ShowFooter = true;
            gvQuotationRules.AllowSorting = true;
            gvQuotationRules.AllowPaging = true;
            gvQuotationRules.PageSize = 10;
            gvQuotationRules.Height = 360;
            gvQuotationRules.DataKeyNames = new string[] { "Supplier", "Category", "Brand", "Stock" };
            gvQuotationRules.SelectedIndex = -1;
            gvQuotationRules.AllowPaging = true;
            gvQuotationRules.PageIndex = 0;
            gvQuotationRules.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            entities.Supplier supplier;
            entities.Category category;
            entities.Brand brand;
            entities.Stock stock;
            bool withCustomization;
            bool closeReset;
            string orderBy;
            List<WhereToBuy.entities.QuotationRule> quotationRules;

            supplier = GetSelectedSupplier();
            category = GetSelectedCategory();
            brand = GetSelectedBrand();
            stock = GetSelectedStock();


            withCustomization = btnCustomization.Checked;
            closeReset = btnReset.Checked;
            quotationRules = new List<entities.QuotationRule>();

            // Orderby instruction
            orderBy = ViewState["QuotationRuleOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["QuotationRuleOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                quotationRules = engine.QuotationRules.Get(supplier, brand, category, stock, withCustomization, closeReset, 1, orderBy);
                //SetQuotationRules(quotationRules);
                engine = null;


                // Select selected object
                if (ViewState["SelectedQuotationRule"] != null)
                {
                    SetSelectedIndex(ref quotationRules);
                }


                // show data
                gvQuotationRules.DataSource = quotationRules;
                gvQuotationRules.DataBind();

                // update pager
                if (quotationRules.Count > 0)
                {
                    GridViewRow PagerRow = gvQuotationRules.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvQuotationRules.PageIndex + 1, gvQuotationRules.PageCount);
                }


            }
            catch (MyException ex)
            {

                this.MessageUC.ShowError("Erro", ex.Message);

            }
            catch (Exception ex)
            {
                this.MessageUC.ShowError("Erro", ex.Message);

            }
        }

        void RefreshGridPageView()
        {
             
            // update pager
            if (GetQuotationRules().Count > 0)
            {
                GridViewRow PagerRow = gvQuotationRules.BottomPagerRow;
                Label label = (Label)PagerRow.FindControl("lblActualPage");
                label.Text = string.Format(" {0} ... {1} ", gvQuotationRules.PageIndex + 1, gvQuotationRules.PageCount);
            }
        }

        void SetSelectedIndex(ref List<WhereToBuy.entities.QuotationRule> quotationRules)
        {
            this.selectedQuotationRule = GetSelectedQuotationRule();

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvQuotationRules.PageIndex * gvQuotationRules.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvQuotationRules.PageIndex != (gvQuotationRules.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvQuotationRules.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = quotationRules.Count - 1;
            }

            objectIndex = quotationRules.IndexOf(this.selectedQuotationRule);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvQuotationRules.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvQuotationRules.SelectedIndex = -1;
            }
        }


        void LoadObject(Supplier supplier, Category category, Brand brand, Stock stock)
        {
            bool withCustomization;
            bool closeReset;
            
            withCustomization = btnCustomization.Checked;
            closeReset = btnReset.Checked;

            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedQuotationRule = engine.QuotationRules.Get(supplier, brand, category, stock, withCustomization, closeReset, 1);
                SetSelectedQuotationRule(this.selectedQuotationRule);
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
            //clear selboxes
            SetSelectedSupplier(null);
            var tbx = SuppliersSelBox.FindControl("txtSupplier");
            ((TextBox)tbx).Text = "";
            ((TextBox)tbx).Focus();

            SetSelectedCategory(null);
            tbx = CategoriesSelBox.FindControl("txtCategory");
            ((TextBox)tbx).Text = "";

            SetSelectedBrand(null);
            tbx = BrandsSelBox.FindControl("txtBrand");
            ((TextBox)tbx).Text = "";

            SetSelectedStock(null);
            tbx = StocksSelBox.FindControl("txtStock");
            ((TextBox)tbx).Text = "";

            btnCustomization.Checked = true;
            btnReset.Checked = false;
            btnAll.Checked = false;

           
            gvQuotationRules.PageIndex = 0;
            RefreshGridView();
            UpdatePanel1.Update();

        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.QuotationRule rv = new WhereToBuy.entities.QuotationRule();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvQuotationRules.DataSource) as List<WhereToBuy.entities.QuotationRule>).Count();
        }
    }
}