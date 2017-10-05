using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Catalogs.Catalogs
{
    public partial class CatalogsUC
    {
        CoreEngine engine;

        public void UpdateData(string code, DataState dataState)
        {

            if (ViewState["CatalogOrderBy"] == null)
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
            ViewState.Add("CatalogOrderBy", "[Codigo]");
            ViewState.Add("CatalogOrderByType", "ASC");


            txtCode.MaxLength = CatalogSpecs.Code_MaxSize;
            txtDescription.MaxLength = CatalogSpecs.Description_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();



        }


        void SetGridViewEnvironment()
        {
            gvCatalogs.AutoGenerateColumns = false;
            gvCatalogs.ShowHeader = true;
            gvCatalogs.ShowFooter = true;
            gvCatalogs.AllowSorting = true;
            gvCatalogs.AllowPaging = true;
            gvCatalogs.PageSize = 10;
            gvCatalogs.Height = 360;
            gvCatalogs.DataKeyNames = new string[] { "Code" };
            gvCatalogs.SelectedIndex = -1;
            gvCatalogs.AllowPaging = true;
            gvCatalogs.PageIndex = 0;
            gvCatalogs.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            string code;
            string description;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.Catalog> catalogs;


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
            orderBy = ViewState["CatalogOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["CatalogOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                catalogs = engine.Catalogs.Get(code, description, dataState, orderBy);
                engine = null;


                // Select selected object
                if (ViewState["SelectedCatalog"] != null)
                {
                    SetSelectedIndex(ref catalogs);
                }


                // show data
                gvCatalogs.DataSource = catalogs;
                gvCatalogs.DataBind();

                // update pager
                if (catalogs.Count > 0)
                {
                    GridViewRow PagerRow = gvCatalogs.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvCatalogs.PageIndex + 1, gvCatalogs.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.Catalog> catalogs)
        {
            this.selectedCatalog = GetSelectedCatalog();

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvCatalogs.PageIndex * gvCatalogs.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvCatalogs.PageIndex != (gvCatalogs.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvCatalogs.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = catalogs.Count - 1;
            }

            objectIndex = catalogs.IndexOf(this.selectedCatalog);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvCatalogs.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvCatalogs.SelectedIndex = -1;
            }
        }


        void LoadObject(string codigo)
        {


            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedCatalog = engine.Catalogs.Get(codigo);
                SetSelectedCatalog(this.selectedCatalog);
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
            gvCatalogs.PageIndex = 0;
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.Catalog rv = new WhereToBuy.entities.Catalog();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvCatalogs.DataSource) as List<WhereToBuy.entities.Catalog>).Count();
        }
    }
}