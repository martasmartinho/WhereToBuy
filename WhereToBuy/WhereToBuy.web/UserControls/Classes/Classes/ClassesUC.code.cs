using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Classes.Classes
{
    public partial class ClassesUC
    {
        CoreEngine engine;

        public void UpdateData(string code, DataState dataState)
        {

            if (ViewState["ClasseOrderBy"] == null)
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
            ViewState.Add("ClasseOrderBy", "[Codigo]");
            ViewState.Add("ClasseOrderByType", "ASC");


            txtCode.MaxLength = ClasseSpecs.Code_MaxSize;
            txtDescription.MaxLength = ClasseSpecs.Description_MaxSize;

            // Prepare GRIDVIEW
            SetGridViewEnvironment();



        }


        void SetGridViewEnvironment()
        {
            gvClasses.AutoGenerateColumns = false;
            gvClasses.ShowHeader = true;
            gvClasses.ShowFooter = true;
            gvClasses.AllowSorting = true;
            gvClasses.AllowPaging = true;
            gvClasses.PageSize = 10;
            gvClasses.Height = 360;
            gvClasses.DataKeyNames = new string[] { "Code" };
            gvClasses.SelectedIndex = -1;
            gvClasses.AllowPaging = true;
            gvClasses.PageIndex = 0;
            gvClasses.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {
            string code;
            string description;
            DataState dataState;
            string orderBy;

            List<WhereToBuy.entities.Classe> classes;


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
            orderBy = ViewState["ClasseOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["ClasseOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                classes = engine.Classes.Get(dataState, 1);
                engine = null;


                // Select selected object
                if (ViewState["SelectedClasse"] != null)
                {
                    SetSelectedIndex(ref classes);
                }


                // show data
                gvClasses.DataSource = classes;
                gvClasses.DataBind();

                // update pager
                if (classes.Count > 0)
                {
                    GridViewRow PagerRow = gvClasses.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("lblActualPage");
                    label.Text = string.Format(" {0} ... {1} ", gvClasses.PageIndex + 1, gvClasses.PageCount);
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


        void SetSelectedIndex(ref List<WhereToBuy.entities.Classe> classes)
        {
            this.selectedClasse = GetSelectedClasse();

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
             */

            int firstPageItemIndex = gvClasses.PageIndex * gvClasses.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvClasses.PageIndex != (gvClasses.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvClasses.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = classes.Count - 1;
            }

            objectIndex = classes.IndexOf(this.selectedClasse);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvClasses.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvClasses.SelectedIndex = -1;
            }
        }


        void LoadObject(string codigo)
        {


            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                //this.selectedClasse = engine.Classes.Get(codigo,);
                SetSelectedClasse(this.selectedClasse);
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
            gvClasses.PageIndex = 0;
            RefreshGridView();
            UpdatePanel1.Update();



        }


        public int GetTotalPageCount()
        {
            int count = 0;
            WhereToBuy.entities.Classe rv = new WhereToBuy.entities.Classe();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((gvClasses.DataSource) as List<WhereToBuy.entities.Classe>).Count();
        }
    }
}