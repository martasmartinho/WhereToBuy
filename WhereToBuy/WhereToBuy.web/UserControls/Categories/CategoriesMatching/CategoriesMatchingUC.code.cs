using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Categories.CategoriesMatching
{
    public partial class CategoriesMatchingUC
    {
        CoreEngine engine;

        public void UpdateData(string supplierCode, string code, DataState dataState)
        {

            if (ViewState["CategoryMatchingOrderBy"] == null)
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
            ViewState.Add("CategoryMatchingOrderBy", "[Codigo]");
            ViewState.Add("CategoryMatchingOrderByType", "ASC");


            txtExternalCode.MaxLength = CategoryMatchingSpecs.Code_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();


            //// if exist object in session
            //if (Session["SelectedCategoryMatching"] != null)
            //{
            //    SetSelectedMatching((CategoryMatchingUC)Session["SelectedCategoryMatching"]);
            //}


        }


        void SetGridViewEnvironment()
        {
            gvCategoriesMatching.AutoGenerateColumns = false;
            gvCategoriesMatching.ShowHeader = true;
            gvCategoriesMatching.ShowFooter = true;
            gvCategoriesMatching.AllowSorting = true;
            gvCategoriesMatching.AllowPaging = true;
            gvCategoriesMatching.PageSize = 10;
            gvCategoriesMatching.Height = 360;
            gvCategoriesMatching.DataKeyNames = new string[] { "Code", "Supplier" };
            gvCategoriesMatching.SelectedIndex = -1;
            gvCategoriesMatching.AllowPaging = true;
            gvCategoriesMatching.PageIndex = 0;
            gvCategoriesMatching.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            Supplier supplier = null;
            string code;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.CategoryMatching> categoriesMatching;


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
            orderBy = ViewState["CategoryMatchingOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["CategoryMatchingOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                categoriesMatching = engine.CategoriesMatching.Get(supplier, code, dataState, orderBy, 1);
                engine = null;


                // Select selected object
                if (ViewState["SelectedCategoryMatching"] != null)
                {
                    SetSelectedIndex(ref categoriesMatching);
                }


                // show data
                gvCategoriesMatching.DataSource = categoriesMatching;
                gvCategoriesMatching.DataBind();

                // update pager
                if (categoriesMatching.Count > 0)
                {
                    GridViewRow PagerRow = gvCategoriesMatching.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvCategoriesMatching.PageIndex + 1, gvCategoriesMatching.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.CategoryMatching> brandsMatching)
        {
            WhereToBuy.entities.CategoryMatching categoryMatching = (WhereToBuy.entities.CategoryMatching)Session["SelectedCategoryMatching"];

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvCategoriesMatching.PageIndex * gvCategoriesMatching.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvCategoriesMatching.PageIndex != (gvCategoriesMatching.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvCategoriesMatching.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = brandsMatching.Count - 1;
            }

            objectIndex = brandsMatching.IndexOf(categoryMatching);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvCategoriesMatching.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvCategoriesMatching.SelectedIndex = -1;
            }
        }


        void LoadCategoryMatching(Supplier supplier, string codigo)
        {


            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedMatching = engine.CategoriesMatching.Get(supplier, codigo, 1);
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
            gvCategoriesMatching.PageIndex = 0;
            SuppliersSelBox.UpdateData("", true);
            SetSelectedSupplier(null);
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.CategoryMatching rv = new WhereToBuy.entities.CategoryMatching();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvCategoriesMatching.DataSource) as List<WhereToBuy.entities.CategoryMatching>).Count();
        }
    }
}