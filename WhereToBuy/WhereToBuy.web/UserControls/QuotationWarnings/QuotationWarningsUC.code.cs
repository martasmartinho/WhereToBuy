using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.QuotationWarnings
{
    public partial class QuotationWarningsUC
    {
        CoreEngine engine;

        public void UpdateData()
        {

            if (ViewState["QuotationWarningOrderBy"] == null)
            {
                SetFormEnvironment();
            }



            RefreshGridView();

            UpdatePanel1.Update();


        }


        void SetFormEnvironment()
        {
            ViewState.Add("QuotationWarningOrderBy", "[Data]");
            ViewState.Add("QuotationWarningOrderByType", "ASC");

            ClearFilter();

            // Prepare GRIDVIEW
            SetGridViewEnvironment();



        }


        void SetGridViewEnvironment()
        {
            gvQuotationWarnings.AutoGenerateColumns = false;
            gvQuotationWarnings.ShowHeader = true;
            gvQuotationWarnings.ShowFooter = true;
            gvQuotationWarnings.AllowSorting = true;
            gvQuotationWarnings.AllowPaging = true;
            gvQuotationWarnings.PageSize = 10;
            gvQuotationWarnings.Height = 360;
            gvQuotationWarnings.DataKeyNames = new string[] { "Supplier", "WarningType"};
            gvQuotationWarnings.SelectedIndex = -1;
            gvQuotationWarnings.AllowPaging = true;
            gvQuotationWarnings.PageIndex = 0;
            gvQuotationWarnings.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            entities.Supplier supplier;
            entities.WarningType warningType;
            string orderBy;
            List<WhereToBuy.entities.QuotationWarning> quotationWarnings;

            supplier = GetSelectedSupplier();
            warningType = GetSelectedWarningType();


            quotationWarnings = new List<entities.QuotationWarning>();

            // Orderby instruction
            orderBy = ViewState["QuotationWarningOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["QuotationWarningOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                quotationWarnings = engine.QuotationWarnings.Get(supplier, warningType, orderBy, 0);
                engine = null;


                // Select selected object
                if (ViewState["SelectedQuotationWarning"] != null)
                {
                    SetSelectedIndex(ref quotationWarnings);
                }


                // show data
                gvQuotationWarnings.DataSource = quotationWarnings;
                gvQuotationWarnings.DataBind();

                // update pager
                if (quotationWarnings.Count > 0)
                {
                    GridViewRow PagerRow = gvQuotationWarnings.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvQuotationWarnings.PageIndex + 1, gvQuotationWarnings.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.QuotationWarning> quotationWarnings)
        {
            this.selectedQuotationWarning = GetSelectedQuotationWarning();

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvQuotationWarnings.PageIndex * gvQuotationWarnings.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvQuotationWarnings.PageIndex != (gvQuotationWarnings.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvQuotationWarnings.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = quotationWarnings.Count - 1;
            }

            objectIndex = quotationWarnings.IndexOf(this.selectedQuotationWarning);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvQuotationWarnings.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvQuotationWarnings.SelectedIndex = -1;
            }
        }


        //void LoadObject(Supplier supplier, WarningType category, Brand brand, Stock stock)
        //{
        //    try
        //    {
        //        engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
        //        this.selectedQuotationWarning = engine.QuotationWarnings.Get(supplier, brand, category, stock, false, false, 1);
        //        SetSelectedQuotationWarning(this.selectedQuotationWarning);
        //        engine = null;
        //    }
        //    catch (MyException ex)
        //    {
        //        this.MessageUC.ShowError("Erro", ex.Message);
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.MessageUC.ShowError("Erro", ex.Message);
        //        return;
        //    }
        //}




        void ClearFilter()
        {
            //clear selboxes
            SetSelectedSupplier(null);
            var tbx = SuppliersSelBox.FindControl("txtSupplier");
            ((TextBox)tbx).Text = "";
            ((TextBox)tbx).Focus();

            SetSelectedWarningType(null);
            tbx = WarningTypesSelBox.FindControl("txtWarningType");
            ((TextBox)tbx).Text = "";

            gvQuotationWarnings.PageIndex = 0;
            RefreshGridView();
            UpdatePanel1.Update();

        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.QuotationWarning rv = new WhereToBuy.entities.QuotationWarning();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvQuotationWarnings.DataSource) as List<WhereToBuy.entities.QuotationWarning>).Count();
        }
    }
}