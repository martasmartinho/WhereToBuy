using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.States.StatesMatching
{
    public partial class StatesMatchingUC
    {
        CoreEngine engine;

        public void UpdateData(string supplierCode, string code, DataState dataState)
        {

            if (ViewState["StateMatchingOrderBy"] == null)
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
            ViewState.Add("StateMatchingOrderBy", "[Codigo]");
            ViewState.Add("StateMatchingOrderByType", "ASC");


            txtExternalCode.MaxLength = StateMatchingSpecs.Code_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();


            //// if exist object in session
            //if (Session["SelectedStateMatching"] != null)
            //{
            //    SetSelectedMatching((StateMatching)Session["SelectedStateMatching"]);
            //}


        }


        void SetGridViewEnvironment()
        {
            gvStatesMatching.AutoGenerateColumns = false;
            gvStatesMatching.ShowHeader = true;
            gvStatesMatching.ShowFooter = true;
            gvStatesMatching.AllowSorting = true;
            gvStatesMatching.AllowPaging = true;
            gvStatesMatching.PageSize = 10;
            gvStatesMatching.Height = 360;
            gvStatesMatching.DataKeyNames = new string[] { "Code", "Supplier" };
            gvStatesMatching.SelectedIndex = -1;
            gvStatesMatching.AllowPaging = true;
            gvStatesMatching.PageIndex = 0;
            gvStatesMatching.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            Supplier supplier = null;
            string code;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.StateMatching> statesMatching;


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
            orderBy = ViewState["StateMatchingOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["StateMatchingOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                statesMatching = engine.StatesMatching.Get(supplier, code, dataState, orderBy, 1);
                engine = null;


                // Select selected object
                if (ViewState["SelectedStateMatching"] != null)
                {
                    SetSelectedIndex(ref statesMatching);
                }


                // show data
                gvStatesMatching.DataSource = statesMatching;
                gvStatesMatching.DataBind();

                // update pager
                if (statesMatching.Count > 0)
                {
                    GridViewRow PagerRow = gvStatesMatching.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvStatesMatching.PageIndex + 1, gvStatesMatching.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.StateMatching> brandsMatching)
        {
            WhereToBuy.entities.StateMatching stateMatching = (WhereToBuy.entities.StateMatching)Session["SelectedStateMatching"];

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvStatesMatching.PageIndex * gvStatesMatching.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvStatesMatching.PageIndex != (gvStatesMatching.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvStatesMatching.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = brandsMatching.Count - 1;
            }

            objectIndex = brandsMatching.IndexOf(stateMatching);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvStatesMatching.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvStatesMatching.SelectedIndex = -1;
            }
        }


        void LoadStateMatching(Supplier supplier, string codigo)
        {
            

            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedMatching = engine.StatesMatching.Get(supplier, codigo, 1);
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
            gvStatesMatching.PageIndex = 0;
            SuppliersSelBox.UpdateData("", true);
            SetSelectedSupplier(null);
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.StateMatching rv = new WhereToBuy.entities.StateMatching();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvStatesMatching.DataSource) as List<WhereToBuy.entities.StateMatching>).Count();
        }
    }
}