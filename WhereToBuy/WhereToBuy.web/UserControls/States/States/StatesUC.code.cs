using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.States.States
{
    public partial class StatesUC
    {
        CoreEngine engine;

        public void UpdateData(string code, DataState dataState)
        {

            if (ViewState["StateOrderBy"] == null)
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
            ViewState.Add("StateOrderBy", "[Codigo]");
            ViewState.Add("StateOrderByType", "ASC");


            txtCode.MaxLength = StateSpecs.Code_MaxSize;
            txtDescription.MaxLength = StateSpecs.Description_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();



        }


        void SetGridViewEnvironment()
        {
            gvStates.AutoGenerateColumns = false;
            gvStates.ShowHeader = true;
            gvStates.ShowFooter = true;
            gvStates.AllowSorting = true;
            gvStates.AllowPaging = true;
            gvStates.PageSize = 10;
            gvStates.Height = 360;
            gvStates.DataKeyNames = new string[] { "Code" };
            gvStates.SelectedIndex = -1;
            gvStates.AllowPaging = true;
            gvStates.PageIndex = 0;
            gvStates.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            string code;
            string description;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.State> states;


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
            orderBy = ViewState["StateOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["StateOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                states = engine.States.Get(code, description, dataState, orderBy);
                engine = null;


                // Select selected object
                if (ViewState["SelectedState"] != null)
                {
                    SetSelectedIndex(ref states);
                }


                // show data
                gvStates.DataSource = states;
                gvStates.DataBind();

                // update pager
                if (states.Count > 0)
                {
                    GridViewRow PagerRow = gvStates.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvStates.PageIndex + 1, gvStates.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.State> states)
        {
            WhereToBuy.entities.State state = (WhereToBuy.entities.State)Session["SelectedState"];

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvStates.PageIndex * gvStates.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvStates.PageIndex != (gvStates.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvStates.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = states.Count - 1;
            }

            objectIndex = states.IndexOf(state);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvStates.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvStates.SelectedIndex = -1;
            }
        }


        void LoadObject(string codigo)
        {
            WhereToBuy.entities.State state;

            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                state = engine.States.Get(codigo);
                SetSelectedState(state);
                ViewState["SelectedState"] = state;
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
            gvStates.PageIndex = 0;
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.State rv = new WhereToBuy.entities.State();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvStates.DataSource) as List<WhereToBuy.entities.State>).Count();
        }
    }
}