using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Suppliers.Suppliers
{
    public partial class SuppliersUC
    {

        CoreEngine engine;

        public void UpdateData(string code, DataState dataState)
        {

            if (ViewState["SupplierOrderBy"] == null)
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
            ViewState.Add("SupplierOrderBy", "[Codigo]");
            ViewState.Add("SupplierOrderByType", "ASC");


            txtCode.MaxLength = SupplierSpecs.Code_MaxSize;
            txtName.MaxLength = SupplierSpecs.Name_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();



        }


        void SetGridViewEnvironment()
        {
            gvSuppliers.AutoGenerateColumns = false;
            gvSuppliers.ShowHeader = true;
            gvSuppliers.ShowFooter = true;
            gvSuppliers.AllowSorting = true;
            gvSuppliers.AllowPaging = true;
            gvSuppliers.PageSize = 10;
            gvSuppliers.Height = 360;
            gvSuppliers.DataKeyNames = new string[] { "Code" };
            gvSuppliers.SelectedIndex = -1;
            gvSuppliers.AllowPaging = true;
            gvSuppliers.PageIndex = 0;
            gvSuppliers.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            string code;
            string name;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.Supplier> suppliers;


            // Filter data
            code = txtCode.Text.TrimStart().TrimEnd();
            name = txtName.Text.TrimStart().TrimEnd();

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
            orderBy = ViewState["SupplierOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["SupplierOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                suppliers = engine.Suppliers.Get(code, name, dataState, orderBy);
                engine = null;


                // Select selected object
                if (ViewState["SelectedSupplier"] != null)
                {
                    SetSelectedIndex(ref suppliers);
                }


                // show data
                gvSuppliers.DataSource = suppliers;
                gvSuppliers.DataBind();

                // update pager
                if (suppliers.Count > 0)
                {
                    GridViewRow PagerRow = gvSuppliers.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvSuppliers.PageIndex + 1, gvSuppliers.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.Supplier> suppliers)
        {
            this.selectedSupplier = GetSelectedSupplier();

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvSuppliers.PageIndex * gvSuppliers.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvSuppliers.PageIndex != (gvSuppliers.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvSuppliers.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = suppliers.Count - 1;
            }

            objectIndex = suppliers.IndexOf(this.selectedSupplier);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvSuppliers.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvSuppliers.SelectedIndex = -1;
            }
        }


        void LoadObject(string codigo)
        {


            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedSupplier = engine.Suppliers.Get(codigo);
                SetSelectedSupplier(this.selectedSupplier);
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
            txtName.Text = "";
            gvSuppliers.PageIndex = 0;
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.Supplier rv = new WhereToBuy.entities.Supplier();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvSuppliers.DataSource) as List<WhereToBuy.entities.Supplier>).Count();
        }
    }
}