using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.WarningTypes.WarningTypes
{
    public partial class WarningTypesUC
    {
        CoreEngine engine;

        public void UpdateData(string code, DataState dataState)
        {

            if (ViewState["WarningTypeOrderBy"] == null)
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
            ViewState.Add("WarningTypeOrderBy", "[Codigo]");
            ViewState.Add("WarningTypeOrderByType", "ASC");


            txtCode.MaxLength = WarningTypeSpecs.Code_MaxSize;
            txtDescription.MaxLength = WarningTypeSpecs.Description_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();



        }


        void SetGridViewEnvironment()
        {
            gvWarningTypes.AutoGenerateColumns = false;
            gvWarningTypes.ShowHeader = true;
            gvWarningTypes.ShowFooter = true;
            gvWarningTypes.AllowSorting = true;
            gvWarningTypes.AllowPaging = true;
            gvWarningTypes.PageSize = 10;
            gvWarningTypes.Height = 360;
            gvWarningTypes.DataKeyNames = new string[] { "Code" };
            gvWarningTypes.SelectedIndex = -1;
            gvWarningTypes.AllowPaging = true;
            gvWarningTypes.PageIndex = 0;
            gvWarningTypes.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            string code;
            string description;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.WarningType> warningTypes;


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
            orderBy = ViewState["WarningTypeOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["WarningTypeOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                warningTypes = engine.WarningTypes.Get(code, description, dataState, orderBy);
                engine = null;


                // Select selected object
                if (ViewState["SelectedWarningType"] != null)
                {
                    SetSelectedIndex(ref warningTypes);
                }


                // show data
                gvWarningTypes.DataSource = warningTypes;
                gvWarningTypes.DataBind();

                // update pager
                if (warningTypes.Count > 0)
                {
                    GridViewRow PagerRow = gvWarningTypes.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvWarningTypes.PageIndex + 1, gvWarningTypes.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.WarningType> warningTypes)
        {
            this.selectedWarningType = GetSelectedWarningType();

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvWarningTypes.PageIndex * gvWarningTypes.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvWarningTypes.PageIndex != (gvWarningTypes.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvWarningTypes.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = warningTypes.Count - 1;
            }

            objectIndex = warningTypes.IndexOf(this.selectedWarningType);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvWarningTypes.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvWarningTypes.SelectedIndex = -1;
            }
        }


        void LoadObject(string codigo)
        {


            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.selectedWarningType = engine.WarningTypes.Get(codigo);
                SetSelectedWarningType(this.selectedWarningType);
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
            gvWarningTypes.PageIndex = 0;
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.WarningType rv = new WhereToBuy.entities.WarningType();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvWarningTypes.DataSource) as List<WhereToBuy.entities.WarningType>).Count();
        }
    }
}