using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Stocks.Stocks
{
    public partial class StocksUC
    {
        CoreEngine engine;

        public void UpdateData(string code, DataState dataState)
        {

            if (ViewState["StockOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtCode.Text = code.TrimEnd();



            btnActive.Checked = false;
            btnInactive.Checked = false;
            btnAll.Checked = false;

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
                    btnActive.Checked = true;
                    break;
            }

            RefreshGridView();

            UpdatePanel1.Update();


        }


        void SetFormEnvironment()
        {
            ViewState.Add("StockOrderBy", "[Codigo]");
            ViewState.Add("StockOrderByType", "ASC");


            txtCode.MaxLength = StockSpecs.Code_MaxSize;
            txtDescription.MaxLength = StockSpecs.Description_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();



        }


        void SetGridViewEnvironment()
        {
            gvStocks.AutoGenerateColumns = false;
            gvStocks.ShowHeader = true;
            gvStocks.ShowFooter = true;
            gvStocks.AllowSorting = true;
            gvStocks.AllowPaging = true;
            gvStocks.PageSize = 10;
            gvStocks.Height = 360;
            gvStocks.DataKeyNames = new string[] { "Code" };
            gvStocks.SelectedIndex = -1;
            gvStocks.AllowPaging = true;
            gvStocks.PageIndex = 0;
            gvStocks.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            string code;
            string description;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.Stock> stocks;


            // Filter data
            code = txtCode.Text.TrimStart().TrimEnd();
            description = txtDescription.Text.TrimStart().TrimEnd();

            if (btnActive.Checked == true)
            {
                dataState = DataState.Active;
            }
            else if (btnInactive.Checked == true)
            {
                dataState = DataState.Inactive;
            }
            else
            {
                dataState = DataState.All;
            }


            // Orderby instruction
            orderBy = ViewState["StockOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["StockOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                stocks = engine.Stocks.Get(code, description, dataState, orderBy, 1, 1, 1, 1, 1);
                engine = null;


                // Select selected object
                if (ViewState["SelectedStock"] != null)
                {
                    SetSelectedIndex(ref stocks);
                }


                // show data
                gvStocks.DataSource = stocks;
                gvStocks.DataBind();

                // update pager
                if (stocks.Count > 0)
                {
                    GridViewRow PagerRow = gvStocks.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvStocks.PageIndex + 1, gvStocks.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.Stock> stocks)
        {
            this.selectedStock = GetSelectedStock();

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvStocks.PageIndex * gvStocks.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvStocks.PageIndex != (gvStocks.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvStocks.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = stocks.Count - 1;
            }

            objectIndex = stocks.IndexOf(this.selectedStock);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvStocks.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvStocks.SelectedIndex = -1;
            }
        }


        void LoadObject(string codigo)
        {


            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedStock = engine.Stocks.Get(codigo);
                SetSelectedStock(this.selectedStock);
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

            btnActive.Checked = true;
            btnInactive.Checked = false;
            btnAll.Checked = false;

            txtCode.Text = "";
            txtDescription.Text = "";
            gvStocks.PageIndex = 0;
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.Stock rv = new WhereToBuy.entities.Stock();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvStocks.DataSource) as List<WhereToBuy.entities.Stock>).Count();
        }
    }
}