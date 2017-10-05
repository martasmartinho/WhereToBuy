using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.WorryingTerms.WorryingTerms
{
    public partial class WorryingTermsUC
    {
        CoreEngine engine;

        public void UpdateData(string term, DataState dataState)
        {

            if (ViewState["WorryingTermOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtTerm.Text = term.TrimEnd();



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
            ViewState.Add("WorryingTermOrderBy", "[Termo]");
            ViewState.Add("WorryingTermOrderByType", "ASC");


            txtTerm.MaxLength = WorryingTermSpecs.Term_MaxSize;
            // Prepare GRIDVIEW
            SetGridViewEnvironment();



        }


        void SetGridViewEnvironment()
        {
            gvWorryingTerms.AutoGenerateColumns = false;
            gvWorryingTerms.ShowHeader = true;
            gvWorryingTerms.ShowFooter = true;
            gvWorryingTerms.AllowSorting = true;
            gvWorryingTerms.AllowPaging = true;
            gvWorryingTerms.PageSize = 10;
            gvWorryingTerms.Height = 360;
            gvWorryingTerms.DataKeyNames = new string[] { "Term" };
            gvWorryingTerms.SelectedIndex = -1;
            gvWorryingTerms.AllowPaging = true;
            gvWorryingTerms.PageIndex = 0;
            gvWorryingTerms.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            string term;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.WorryingTerm> worryingTerms;


            // Filter data
            term = txtTerm.Text.TrimStart().TrimEnd();

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
            orderBy = ViewState["WorryingTermOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["WorryingTermOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                worryingTerms = engine.WorryingTerms.Get(term, dataState, orderBy);
                engine = null;


                // Select selected object
                if (ViewState["SelectedWorryingTerm"] != null)
                {
                    SetSelectedIndex(ref worryingTerms);
                }


                // show data
                gvWorryingTerms.DataSource = worryingTerms;
                gvWorryingTerms.DataBind();

                // update pager
                if (worryingTerms.Count > 0)
                {
                    GridViewRow PagerRow = gvWorryingTerms.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvWorryingTerms.PageIndex + 1, gvWorryingTerms.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.WorryingTerm> worryingTerms)
        {
            this.selectedWorryingTerm = GetSelectedWorryingTerm();

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvWorryingTerms.PageIndex * gvWorryingTerms.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvWorryingTerms.PageIndex != (gvWorryingTerms.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvWorryingTerms.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = worryingTerms.Count - 1;
            }

            objectIndex = worryingTerms.IndexOf(this.selectedWorryingTerm);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvWorryingTerms.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvWorryingTerms.SelectedIndex = -1;
            }
        }


        void LoadObject(string codigo)
        {


            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedWorryingTerm = engine.WorryingTerms.Get(codigo);
                SetSelectedWorryingTerm(this.selectedWorryingTerm);
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

            txtTerm.Text = "";
            gvWorryingTerms.PageIndex = 0;
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.WorryingTerm rv = new WhereToBuy.entities.WorryingTerm();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvWorryingTerms.DataSource) as List<WhereToBuy.entities.WorryingTerm>).Count();
        }
    }
}