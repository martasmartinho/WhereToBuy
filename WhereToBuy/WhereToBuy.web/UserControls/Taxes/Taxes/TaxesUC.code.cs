using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Taxes.Taxes
{
    public partial class TaxesUC
    {
        CoreEngine engine;

        public void UpdateData(string code, DataState dataState)
        {

            if (ViewState["TaxOrderBy"] == null)
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
            ViewState.Add("TaxOrderBy", "[Codigo]");
            ViewState.Add("TaxOrderByType", "ASC");


            txtCode.MaxLength = TaxSpecs.Code_MaxSize;
            txtDescription.MaxLength = TaxSpecs.Description_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();



        }


        void SetGridViewEnvironment()
        {
            gvTaxes.AutoGenerateColumns = false;
            gvTaxes.ShowHeader = true;
            gvTaxes.ShowFooter = true;
            gvTaxes.AllowSorting = true;
            gvTaxes.AllowPaging = true;
            gvTaxes.PageSize = 10;
            gvTaxes.Height = 360;
            gvTaxes.DataKeyNames = new string[] { "Code" };
            gvTaxes.SelectedIndex = -1;
            gvTaxes.AllowPaging = true;
            gvTaxes.PageIndex = 0;
            gvTaxes.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            string code;
            string description;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.Tax> taxes;


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
            orderBy = ViewState["TaxOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["TaxOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                taxes = engine.Taxes.Get(code, description, dataState, orderBy);
                engine = null;


                // Select selected object
                if (ViewState["SelectedTax"] != null)
                {
                    SetSelectedIndex(ref taxes);
                }


                // show data
                gvTaxes.DataSource = taxes;
                gvTaxes.DataBind();

                // update pager
                if (taxes.Count > 0)
                {
                    GridViewRow PagerRow = gvTaxes.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvTaxes.PageIndex + 1, gvTaxes.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.Tax> taxes)
        {
            this.selectedTax = GetSelectedTax();

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvTaxes.PageIndex * gvTaxes.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvTaxes.PageIndex != (gvTaxes.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvTaxes.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = taxes.Count - 1;
            }

            objectIndex = taxes.IndexOf(this.selectedTax);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvTaxes.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvTaxes.SelectedIndex = -1;
            }
        }


        void LoadObject(string codigo)
        {
           

            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedTax = engine.Taxes.Get(codigo);
                SetSelectedTax(this.selectedTax);
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
            gvTaxes.PageIndex = 0;
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.Tax rv = new WhereToBuy.entities.Tax();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvTaxes.DataSource) as List<WhereToBuy.entities.Tax>).Count();
        }
    }
}