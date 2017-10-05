using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Brands.Brands
{
    public partial class BrandsUC
    {
        CoreEngine engine;


        public void UpdateData(string code,DataState dataState)
        {

            if (ViewState["BrandOrderBy"] == null)
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
            ViewState.Add("BrandOrderBy", "[Codigo]");
            ViewState.Add("BrandOrderByType", "ASC");
            txtCode.MaxLength = BrandSpecs.Code_MaxSize;
            txtDescription.MaxLength = BrandSpecs.Description_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();
        }


        void SetGridViewEnvironment()
        {
            gvBrands.AutoGenerateColumns = false;
            gvBrands.ShowHeader = true;
            gvBrands.ShowFooter = true;
            gvBrands.AllowSorting = true;
            gvBrands.AllowPaging = true;
            gvBrands.PageSize = 10;
            gvBrands.Height = 360;
            gvBrands.DataKeyNames = new string[] { "Code" };
            gvBrands.SelectedIndex = -1;
            gvBrands.AllowPaging = true;
            gvBrands.PageIndex = 0;
            gvBrands.Columns[0].Visible = false;
        }


        void RefreshGridView()
        {
            string code;
            string description;
            DataState dataState;
            string orderBy;
            List<WhereToBuy.entities.Brand> brands;

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
            orderBy = ViewState["BrandOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["BrandOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                brands = engine.Brands.Get(code, description, dataState, orderBy);
                engine = null;

                // Select selected object
                if (ViewState["SelectedBrand"] != null)
                {
                    SetSelectedIndex(ref brands);
                }


                // show data
                gvBrands.DataSource = brands;
                gvBrands.DataBind();

                // update pager
                if (brands.Count > 0)
                {
                    GridViewRow PagerRow = gvBrands.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvBrands.PageIndex + 1, gvBrands.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.Brand> brands)
        {
            WhereToBuy.entities.Brand brand = (WhereToBuy.entities.Brand)Session["SelectedBrand"];

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvBrands.PageIndex * gvBrands.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvBrands.PageIndex != (gvBrands.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvBrands.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = brands.Count - 1;
            }

            objectIndex = brands.IndexOf(brand);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvBrands.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvBrands.SelectedIndex = -1;
            }
        }


        void LoadObject(string codigo)
        {
            WhereToBuy.entities.Brand brand;

            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                brand = engine.Brands.Get(codigo);
                SetSelectedBrand(brand);
                ViewState["SelectedBrand"] = brand;
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
            gvBrands.PageIndex = 0;
            RefreshGridView();
            UpdatePanel1.Update();
        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.Brand rv = new WhereToBuy.entities.Brand();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvBrands.DataSource) as List<WhereToBuy.entities.Brand>).Count();
        }
    }
}