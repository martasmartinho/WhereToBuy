using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Supplements.Supplements
{
    public partial class SupplementsUC
    {
        CoreEngine engine;

        public void UpdateData(string code, DataState dataState)
        {

            if (ViewState["SupplementOrderBy"] == null)
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
            ViewState.Add("SupplementOrderBy", "[Codigo]");
            ViewState.Add("SupplementOrderByType", "ASC");


            txtCode.MaxLength = SupplementSpecs.Code_MaxSize;
            txtDescription.MaxLength = SupplementSpecs.Description_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();



        }


        void SetGridViewEnvironment()
        {
            gvSupplements.AutoGenerateColumns = false;
            gvSupplements.ShowHeader = true;
            gvSupplements.ShowFooter = true;
            gvSupplements.AllowSorting = true;
            gvSupplements.AllowPaging = true;
            gvSupplements.PageSize = 10;
            gvSupplements.Height = 360;
            gvSupplements.DataKeyNames = new string[] { "Code" };
            gvSupplements.SelectedIndex = -1;
            gvSupplements.AllowPaging = true;
            gvSupplements.PageIndex = 0;
            gvSupplements.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            string code;
            string description;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.Supplement> supplements;


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
            orderBy = ViewState["SupplementOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["SupplementOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                supplements = engine.Supplements.Get(code, description, dataState, orderBy);
                engine = null;


                // Select selected object
                if (ViewState["SelectedSupplement"] != null)
                {
                    SetSelectedIndex(ref supplements);
                }


                // show data
                gvSupplements.DataSource = supplements;
                gvSupplements.DataBind();

                // update pager
                if (supplements.Count > 0)
                {
                    GridViewRow PagerRow = gvSupplements.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvSupplements.PageIndex + 1, gvSupplements.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.Supplement> supplements)
        {
            this.selectedSupplement = GetSelectedSupplement();

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvSupplements.PageIndex * gvSupplements.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvSupplements.PageIndex != (gvSupplements.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvSupplements.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = supplements.Count - 1;
            }

            objectIndex = supplements.IndexOf(this.selectedSupplement);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvSupplements.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvSupplements.SelectedIndex = -1;
            }
        }


        void LoadObject(string codigo)
        {


            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedSupplement = engine.Supplements.Get(codigo);
                SetSelectedSupplement(this.selectedSupplement);
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
            gvSupplements.PageIndex = 0;
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.Supplement rv = new WhereToBuy.entities.Supplement();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvSupplements.DataSource) as List<WhereToBuy.entities.Supplement>).Count();
        }
    }
}