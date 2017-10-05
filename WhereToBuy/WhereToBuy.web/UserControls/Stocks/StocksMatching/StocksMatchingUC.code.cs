using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Stocks.StocksMatching
{
    public partial class StocksMatchingUC
    {
        CoreEngine engine;

        public void UpdateData(string supplierCode, string code, DataState dataState)
        {

            if (ViewState["StockMatchingOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtExternalCode.Text = code.TrimEnd();



            btnActive.Checked = false;
            btnInactive.Checked = false;
            btnAll.Checked = false;
            btnMatching.Checked = false;

            // Select correct radiobutton
            switch (dataState)
            {
                case DataState.Active:
                    btnActive.Checked = true;
                    break;
                case DataState.Inactive:
                    btnInactive.Checked = true;
                    break;
                case DataState.All:
                    btnAll.Checked = true;
                    break;
                default:
                    btnMatching.Checked = true;
                    break;
            }

            SuppliersSelBox.UpdateData(supplierCode, true);

            RefreshGridView();

            UpdatePanel1.Update();
        }


        void SetFormEnvironment()
        {
            ViewState.Add("StockMatchingOrderBy", "[Codigo]");
            ViewState.Add("StockMatchingOrderByType", "ASC");


            txtExternalCode.MaxLength = StockMatchingSpecs.Code_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();


            //// if exist object in session
            //if (Session["SelectedStockMatching"] != null)
            //{
            //    SetSelectedMatching((StockMatchingUC)Session["SelectedStockMatching"]);
            //}


        }


        void SetGridViewEnvironment()
        {
            gvStocksMatching.AutoGenerateColumns = false;
            gvStocksMatching.ShowHeader = true;
            gvStocksMatching.ShowFooter = true;
            gvStocksMatching.AllowSorting = true;
            gvStocksMatching.AllowPaging = true;
            gvStocksMatching.PageSize = 10;
            gvStocksMatching.Height = 360;
            gvStocksMatching.DataKeyNames = new string[] { "Code", "Supplier" };
            gvStocksMatching.SelectedIndex = -1;
            gvStocksMatching.AllowPaging = true;
            gvStocksMatching.PageIndex = 0;
            gvStocksMatching.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            Supplier supplier = null;
            string code;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.StockMatching> stocksMatching;


            // Filter data
            code = txtExternalCode.Text.TrimStart().TrimEnd();


            if (btnActive.Checked == true)
            {
                dataState = DataState.Active;
            }
            else if (btnInactive.Checked == true)
            {
                dataState = DataState.Inactive;
            }
            else if (btnMatching.Checked == true)
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
            orderBy = ViewState["StockMatchingOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["StockMatchingOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                stocksMatching = engine.StocksMatching.Get(supplier, code, dataState, orderBy, 1);
                engine = null;


                // Select selected object
                if (ViewState["SelectedStockMatching"] != null)
                {
                    SetSelectedIndex(ref stocksMatching);
                }


                // show data
                gvStocksMatching.DataSource = stocksMatching;
                gvStocksMatching.DataBind();

                // update pager
                if (stocksMatching.Count > 0)
                {
                    GridViewRow PagerRow = gvStocksMatching.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvStocksMatching.PageIndex + 1, gvStocksMatching.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.StockMatching> brandsMatching)
        {
            WhereToBuy.entities.StockMatching stockMatching = (WhereToBuy.entities.StockMatching)Session["SelectedStockMatching"];

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvStocksMatching.PageIndex * gvStocksMatching.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvStocksMatching.PageIndex != (gvStocksMatching.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvStocksMatching.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = brandsMatching.Count - 1;
            }

            objectIndex = brandsMatching.IndexOf(stockMatching);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvStocksMatching.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvStocksMatching.SelectedIndex = -1;
            }
        }


        void LoadStockMatching(Supplier supplier, string codigo)
        {


            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedMatching = engine.StocksMatching.Get(supplier, codigo, 1);
                SetSelectedMatching(selectedMatching);
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

            btnActive.Checked = false;
            btnInactive.Checked = false;
            btnAll.Checked = false;
            btnMatching.Checked = true;
            txtExternalCode.Text = "";
            gvStocksMatching.PageIndex = 0;
            SuppliersSelBox.UpdateData("", true);
            SetSelectedSupplier(null);
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.StockMatching rv = new WhereToBuy.entities.StockMatching();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvStocksMatching.DataSource) as List<WhereToBuy.entities.StockMatching>).Count();
        }
    }
}