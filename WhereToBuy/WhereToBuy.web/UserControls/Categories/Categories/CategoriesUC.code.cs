using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Categories.Categories
{
    public partial class CategoriesUC
    {
        CoreEngine engine;

        public void UpdateData(string code, DataState dataState)
        {

            if (ViewState["CategoryOrderBy"] == null)
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
            ViewState.Add("CategoryOrderBy", "[Codigo]");
            ViewState.Add("CategoryOrderByType", "ASC");


            txtCode.MaxLength = CategorySpecs.Code_MaxSize;
            txtDescription.MaxLength = CategorySpecs.Description_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();



        }


        void SetGridViewEnvironment()
        {
            gvCategories.AutoGenerateColumns = false;
            gvCategories.ShowHeader = true;
            gvCategories.ShowFooter = true;
            gvCategories.AllowSorting = true;
            gvCategories.AllowPaging = true;
            gvCategories.PageSize = 10;
            gvCategories.Height = 360;
            gvCategories.DataKeyNames = new string[] { "Code" };
            gvCategories.SelectedIndex = -1;
            gvCategories.AllowPaging = true;
            gvCategories.PageIndex = 0;
            gvCategories.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            string code;
            string description;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.Category> categories;


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
            orderBy = ViewState["CategoryOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["CategoryOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                categories = engine.Categories.Get(code, description, dataState, orderBy);
                engine = null;


                // Select selected object
                if (ViewState["SelectedCategory"] != null)
                {
                    SetSelectedIndex(ref categories);
                }


                // show data
                gvCategories.DataSource = categories;
                gvCategories.DataBind();

                // update pager
                if (categories.Count > 0)
                {
                    GridViewRow PagerRow = gvCategories.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvCategories.PageIndex + 1, gvCategories.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.Category> categories)
        {
            this.selectedCategory = GetSelectedCategory();

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvCategories.PageIndex * gvCategories.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvCategories.PageIndex != (gvCategories.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvCategories.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = categories.Count - 1;
            }

            objectIndex = categories.IndexOf(this.selectedCategory);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvCategories.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvCategories.SelectedIndex = -1;
            }
        }


        void LoadObject(string codigo)
        {


            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedCategory = engine.Categories.Get(codigo);
                SetSelectedCategory(this.selectedCategory);
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
            gvCategories.PageIndex = 0;
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.Category rv = new WhereToBuy.entities.Category();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvCategories.DataSource) as List<WhereToBuy.entities.Category>).Count();
        }
    }
}