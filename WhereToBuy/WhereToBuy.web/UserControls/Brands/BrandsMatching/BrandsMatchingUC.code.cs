using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.BrandsMatching
{
    public partial class BrandsMatchingUC
    {
        CoreEngine engine;


        public void UpdateData(string supplierCode, string code, DataState dataState)
        {

            if (ViewState["BrandMatchingOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            ExternalCodeTextBox.Text = code.TrimEnd();
            ActiveRadioButton.Checked = false;
            InactiveRadioButton.Checked = false;
            AllRadioButton.Checked = false;
            MatchingRadioButton.Checked = false;

            // Select correct radiobutton
            switch (dataState)
            {
                case DataState.Active:
                    ActiveRadioButton.Checked = true;
                    break;
                case DataState.Inactive:
                    InactiveRadioButton.Checked = true;
                    break;
                case DataState.All:
                    AllRadioButton.Checked = true;
                    break;
                default:
                    MatchingRadioButton.Checked = true;
                    break;
            }

            SuppliersSelBox.UpdateData(supplierCode, true);
            RefreshGridView();
            UpdatePanel1.Update();
        }


        void SetFormEnvironment()
        {
            ViewState.Add("BrandMatchingOrderBy", "[Codigo]");
            ViewState.Add("BrandMatchingOrderByType", "ASC");
            ExternalCodeTextBox.MaxLength = BrandMatchingSpecs.Code_MaxSize;
            
            // Prepare GRIDVIEW
            SetGridViewEnvironment();
        }

      
        void SetGridViewEnvironment()
        {
            BrandMatchingGridView.AutoGenerateColumns = false;
            BrandMatchingGridView.ShowHeader = true;
            BrandMatchingGridView.ShowFooter = true;     
            BrandMatchingGridView.AllowSorting = true;
            BrandMatchingGridView.AllowPaging = true;
            BrandMatchingGridView.PageSize = 10;
            BrandMatchingGridView.Height = 360;           
            BrandMatchingGridView.DataKeyNames = new string[] { "Code" , "Supplier" };
            BrandMatchingGridView.SelectedIndex = -1;
            BrandMatchingGridView.AllowPaging = true;
            BrandMatchingGridView.PageIndex = 0;
            BrandMatchingGridView.Columns[0].Visible = false;
            BrandMatchingGridView.DataSource = new List<BrandMatching>();
            BrandMatchingGridView.DataBind();
        }


        void RefreshGridView()
        {
            Supplier supplier = null;
            string code;
            DataState dataState;
            string orderBy;          
            List<BrandMatching> brandsMatching;

            // Filter data
            code = ExternalCodeTextBox.Text.TrimStart().TrimEnd();

            if (ActiveRadioButton.Checked == true)
            {
                dataState = DataState.Active;
            }
            else if (InactiveRadioButton.Checked == true)
            {
                dataState = DataState.Inactive;
            }
            else if (MatchingRadioButton.Checked == true)
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
            orderBy = ViewState["BrandMatchingOrderBy"].ToString().TrimEnd();
            orderBy += " ";
            orderBy += ViewState["BrandMatchingOrderByType"].ToString().TrimEnd();

            try
            {
                // load data
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                brandsMatching = engine.BrandsMatching.Get(supplier, code, dataState, orderBy, 1);
                engine = null;

                // Select selected object
                if (ViewState["SelectedBrandMatching"] != null)
                {
                    SetSelectedIndex(ref brandsMatching);
                }

                // show data
                BrandMatchingGridView.DataSource = brandsMatching;
                BrandMatchingGridView.DataBind();

                // update pager
                if (brandsMatching.Count > 0)
                {
                    GridViewRow PagerRow = BrandMatchingGridView.BottomPagerRow;
                    Label label = (Label)PagerRow.FindControl("ActualPageLabel");
                    label.Text = string.Format(" {0} ... {1} ", BrandMatchingGridView.PageIndex + 1, BrandMatchingGridView.PageCount);
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


        void SetSelectedIndex(ref List<BrandMatching> brandsMatching)
        {
            BrandMatching brandMatching = (BrandMatching)Session["SelectedBrandMatching"];

            /*
                EXPLICAÇÃO:
                Este metodo calcula o indice real do primeiro e ultimo registo mostrado na pagina atual.
                Se o indice do objeto selecionado estiver dentro desse intervalo então seleciona a linha 
                correspondente ao objeto. Caso contrário não seleciona linha nenhuma.
            */
            int firstPageItemIndex = BrandMatchingGridView.PageIndex * BrandMatchingGridView.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (BrandMatchingGridView.PageIndex != (BrandMatchingGridView.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + BrandMatchingGridView.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = brandsMatching.Count - 1;
            }

            objectIndex = brandsMatching.IndexOf(brandMatching);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                BrandMatchingGridView.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                BrandMatchingGridView.SelectedIndex = -1;
            }
        }


        void LoadBrandMatching(Supplier supplier, string codigo)
        {
            BrandMatching brandMatching;

            try
            {
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                brandMatching = engine.BrandsMatching.Get(supplier, codigo, 1);
                SetSelectedMatching(brandMatching);
                ViewState["SelectedBrandMatching"] = brandMatching;
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
            ActiveRadioButton.Checked = false;
            InactiveRadioButton.Checked = false;
            AllRadioButton.Checked = false;
            MatchingRadioButton.Checked = true;
            ExternalCodeTextBox.Text = "";
            BrandMatchingGridView.PageIndex = 0;
            SuppliersSelBox.UpdateData("", true);
            SetSelectedSupplier(null);
            RefreshGridView();
            UpdatePanel1.Update();
        }


        public int GetTotalPageCount()
        {
            int count = 0;
            BrandMatching rv = new BrandMatching();
            count = GetTotalRecords();
            count = count / 10;
            return count;
        }


        int GetTotalRecords()
        {
            return ((BrandMatchingGridView.DataSource) as List<BrandMatching>).Count();
        }
    }
}