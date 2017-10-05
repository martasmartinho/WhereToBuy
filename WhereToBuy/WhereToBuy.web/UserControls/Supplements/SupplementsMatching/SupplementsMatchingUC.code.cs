using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Supplements.SupplementsMatching
{
    public partial class SupplementsMatchingUC
    {
        CoreEngine engine;

        public void UpdateData(string supplierCode, string code, DataState dataState)
        {

            if (ViewState["SupplementMatchingOrderBy"] == null)
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
            ViewState.Add("SupplementMatchingOrderBy", "[Codigo]");
            ViewState.Add("SupplementMatchingOrderByType", "ASC");


            txtExternalCode.MaxLength = SupplementMatchingSpecs.Code_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();


            //// if exist object in session
            //if (Session["SelectedSupplementMatching"] != null)
            //{
            //    SetSelectedMatching((SupplementMatchingUC)Session["SelectedSupplementMatching"]);
            //}


        }


        void SetGridViewEnvironment()
        {
            gvSupplementsMatching.AutoGenerateColumns = false;
            gvSupplementsMatching.ShowHeader = true;
            gvSupplementsMatching.ShowFooter = true;
            gvSupplementsMatching.AllowSorting = true;
            gvSupplementsMatching.AllowPaging = true;
            gvSupplementsMatching.PageSize = 10;
            gvSupplementsMatching.Height = 360;
            gvSupplementsMatching.DataKeyNames = new string[] { "Code", "Supplier" };
            gvSupplementsMatching.SelectedIndex = -1;
            gvSupplementsMatching.AllowPaging = true;
            gvSupplementsMatching.PageIndex = 0;
            gvSupplementsMatching.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            Supplier supplier = null;
            string code;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.SupplementMatching> supplementsMatching;


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
            orderBy = ViewState["SupplementMatchingOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["SupplementMatchingOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                supplementsMatching = engine.SupplementsMatching.Get(supplier, code, dataState, orderBy, 1);
                engine = null;


                // Select selected object
                if (ViewState["SelectedSupplementMatching"] != null)
                {
                    SetSelectedIndex(ref supplementsMatching);
                }


                // show data
                gvSupplementsMatching.DataSource = supplementsMatching;
                gvSupplementsMatching.DataBind();

                // update pager
                if (supplementsMatching.Count > 0)
                {
                    GridViewRow PagerRow = gvSupplementsMatching.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvSupplementsMatching.PageIndex + 1, gvSupplementsMatching.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.SupplementMatching> brandsMatching)
        {
            WhereToBuy.entities.SupplementMatching supplementMatching = (WhereToBuy.entities.SupplementMatching)Session["SelectedSupplementMatching"];

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvSupplementsMatching.PageIndex * gvSupplementsMatching.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvSupplementsMatching.PageIndex != (gvSupplementsMatching.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvSupplementsMatching.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = brandsMatching.Count - 1;
            }

            objectIndex = brandsMatching.IndexOf(supplementMatching);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvSupplementsMatching.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvSupplementsMatching.SelectedIndex = -1;
            }
        }


        void LoadSupplementMatching(Supplier supplier, string codigo)
        {


            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedMatching = engine.SupplementsMatching.Get(supplier, codigo, 1);
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
            gvSupplementsMatching.PageIndex = 0;
            SuppliersSelBox.UpdateData("", true);
            SetSelectedSupplier(null);
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.SupplementMatching rv = new WhereToBuy.entities.SupplementMatching();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvSupplementsMatching.DataSource) as List<WhereToBuy.entities.SupplementMatching>).Count();
        }
    }
}