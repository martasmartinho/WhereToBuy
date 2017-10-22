using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;
using WhereToBuy.utils.GlobalVariables;

namespace WhereToBuy.web.UserControls.Products.Product
{
    public partial class ProductUC
    {
        CoreEngine engine;

        public void UpdateData(string code)
        {

            SetFormEnvironment();

            if (code != "")
            {
                LoadProduct(code);

            }

            RefreshGridView();
            UpdatePanel1.Update();

        }


        void SetFormEnvironment()
        {
            var tbx = BrandsSelBox.FindControl("txtBrand");
            ((TextBox)tbx).Attributes.Add("required", "");

            tbx = CategoriesSelBox.FindControl("txtCategory");
            ((TextBox)tbx).Attributes.Add("required", "");

            tbx = TaxesSelBox.FindControl("txtTax");
            ((TextBox)tbx).Attributes.Add("required", "");

            //tbx = SuppliersSelBox.FindControl("txtSupplier");
            //((TextBox)tbx).Attributes.Add("required", "");


            ViewState.Add("DetailsOrderBy", "[IndicePreocupacaoConteudo]");
            ViewState.Add("DetailsOrderByType", "DESC");
            txtCode.MaxLength = ProductSpecs.Code_MaxSize;
            
            SetGridViewEnvironment();
            //New();
        }


        void SetGridViewEnvironment()
        {
            gvDetails.AutoGenerateColumns = false;
            gvDetails.ShowHeader = true;
            gvDetails.ShowFooter = true;
            gvDetails.AllowSorting = true;
            gvDetails.AllowPaging = true;
            gvDetails.PageSize = 3;
            gvDetails.Height = 360;
            gvDetails.DataKeyNames = new string[] { "ProductCode"};
            gvDetails.SelectedIndex = 0;
            gvDetails.AllowPaging = true;
            gvDetails.PageIndex = 0;
            gvDetails.Columns[0].Visible = false;

        }


        void RefreshGridView()
        {

            List<ProductDetail> details = new List<ProductDetail>();
            // show data
            if (SelectedProductExist)
            {
                details = GetSelectedProduct().Details;
                if (details.Count > 0)
                {
                    SetSelectedProductDetail((ProductDetail)details.First());
                }
                
            }

            gvDetails.DataSource = details;
            gvDetails.DataBind();

            // update pager
            if (details.Count > 0)
            {
                GridViewRow PagerRow = gvDetails.BottomPagerRow;
                Label label = (Label)PagerRow.FindControl("lblActualPage");
                label.Text = string.Format(" {0} ... {1} ", gvDetails.PageIndex + 1, gvDetails.PageCount);
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

            int firstPageItemIndex = gvDetails.PageIndex * gvDetails.PageSize;
            int lastPageItemIndex;
            int objectIndex;

            if (gvDetails.PageIndex != (gvDetails.PageCount - 1))
            {
                lastPageItemIndex = (firstPageItemIndex + gvDetails.PageSize) - 1;
            }
            else
            {
                lastPageItemIndex = brands.Count - 1;
            }

            objectIndex = brands.IndexOf(brand);

            if (firstPageItemIndex <= objectIndex && objectIndex <= lastPageItemIndex)
            {
                gvDetails.SelectedIndex = objectIndex - firstPageItemIndex;
            }
            else
            {
                gvDetails.SelectedIndex = -1;
            }
        }


        void LoadDetail(string productCode, string supplier)
        {

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                SetSelectedProductDetail(this.product.Details.Where(i => i.ProductCode == productCode && i.Supplier.Code == supplier).First());

                BindObjectToPage();

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



        void BindObjectToPage()
        {

            // dinamic data
            txtCode.Text = this.product.Code.ToString();
            txtPartnumber.Text = this.product.Partnumber.ToString();
            lblSupplier.Text = this.product.Supplier.ToString();
            lblDescription.Text = this.product.Description;

            #region comercialData
            //stock variation
            lblStockDaysU2.Text = this.product.Stock_U2 != null ? ((DateTime.Now - this.product.Stock_U2Date).Days).ToString() + "d" : "0d";
            lblStockDeltaU2.Text = this.product.Stock_U2 != null ? this.product.Stock_U2.AvailabilityLevel.ToString() : "-";
            lblStockDateU2.Text = this.product.Stock_U2 != null ? this.product.Stock_U2Date.ToString() : "-";
            lblStockDescriptionU2.Text = this.product.Stock_U2 != null ? this.product.Stock_U2.Description.ToString() : "-";

            // static data
            // graphic layout
            if (this.product.Stock_U2 != null && this.product.Stock_U3 != null)
            {
                if (this.product.Stock_U2.AvailabilityLevel > this.product.Stock_U3.AvailabilityLevel)
                {
                    lblStockU2Up.Visible = true;
                    lblStockU2Down.Visible = false;
                }
                else if (this.product.Stock_U2.AvailabilityLevel < this.product.Stock_U3.AvailabilityLevel)
                {
                    lblStockU2Up.Visible = false;
                    lblStockU2Down.Visible = true;
                }
                else
                {
                    lblStockU2Up.Visible = true;
                    lblStockU2Down.Visible = true;
                }
            }

            lblStockDaysU1.Text = this.product.Stock_U1 != null ? ((DateTime.Now - this.product.Stock_U1Date).Days).ToString() + "d" : "0d";
            lblStockDeltaU1.Text = this.product.Stock_U1 != null ? this.product.Stock_U1.AvailabilityLevel.ToString() : "-";
            lblStockDateU1.Text = this.product.Stock_U1 != null ? this.product.Stock_U1Date.ToString() : "-";
            lblStockDescriptionU1.Text = this.product.Stock_U1 != null ? this.product.Stock_U1.Description.ToString() : "-";
           
            //graphic layout
            if (this.product.Stock_U1 != null && this.product.Stock_U2 != null)
            {
                if (this.product.Stock_U1.AvailabilityLevel > this.product.Stock_U2.AvailabilityLevel)
                {
                    lblStockU1Up.Visible = true;
                    lblStockU1Down.Visible = false;
                }
                else if (this.product.Stock_U1.AvailabilityLevel < this.product.Stock_U2.AvailabilityLevel)
                {
                    lblStockU1Up.Visible = false;
                    lblStockU1Down.Visible = true;
                }
                else
                {
                    lblStockU1Up.Visible = true;
                    lblStockU1Down.Visible = true;
                }
            }


            lblStockDays.Text = this.product.Stock != null ? ((DateTime.Now - this.product.Stock_Date).Days).ToString() + "d" : "0d";
            lblStockDelta.Text = this.product.Stock != null ? this.product.Stock.AvailabilityLevel.ToString() : "-";
            lblStockDate.Text = this.product.Stock != null ? this.product.Stock_Date.ToString() : "-";
            lblStockDescription.Text = this.product.Stock != null ? this.product.Stock.Description.ToString() : "-";

            //graphic layout
            if (this.product.Stock != null && this.product.Stock_U1 != null)
            {
                if (this.product.Stock.AvailabilityLevel > this.product.Stock_U1.AvailabilityLevel)
                {
                    lblStockUp.Visible = true;
                    lblStockDown.Visible = false;
                }
                else if (this.product.Stock.AvailabilityLevel < this.product.Stock_U1.AvailabilityLevel)
                {
                    lblStockUp.Visible = false;
                    lblStockDown.Visible = true;
                }
                else
                {
                    lblStockUp.Visible = true;
                    lblStockDown.Visible = true;
                }
            }

            //cost price variation
            lblCostPriceDaysU2.Text = this.product.CostPrice_U2Date.ToString() != DateTime.MinValue.ToString() ? ((DateTime.Now - this.product.CostPrice_U2Date).Days).ToString() + "d" : "-d";
            lblCostPriceDeltaU2.Text = this.product.Supplier.ProductPriceTrust.ToString("#.##");
            lblCostPriceDateU2.Text = this.product.CostPrice_U2Date != DateTime.MinValue ? this.product.CostPrice_U2Date.ToString("dd/MM/yyyy") : "-";
            lblCostPriceU2.Text = this.product.CostPrice_U2.ToString("#.##");

            //graphic layout
             if (this.product.CostPrice_U2 > this.product.CostPrice_U3)
                {
                    lblCostPriceU2Up.Visible = true;
                    lblCostPriceU2Down.Visible = false;
                }
                else if (this.product.CostPrice_U2 < this.product.CostPrice_U3)
                {
                    lblCostPriceU2Up.Visible = false;
                    lblCostPriceU2Down.Visible = true;
                }
                else
                {
                    lblCostPriceU2Up.Visible = true;
                    lblCostPriceU2Down.Visible = true;
                }
            lblCostPriceDaysU1.Text = this.product.CostPrice_U1Date.ToString() != DateTime.MinValue.ToString() ? ((DateTime.Now - this.product.CostPrice_U1Date).Days).ToString() + "d" : "-d";
            lblCostPriceDeltaU1.Text = this.product.Supplier.ProductPriceTrust.ToString("#.##");
            lblCostPriceDateU1.Text = this.product.CostPrice_U1Date != DateTime.MinValue ? this.product.CostPrice_U1Date.ToString("dd/MM/yyyy") : "-";
            lblCostPriceU1.Text = this.product.CostPrice_U1.ToString("#.##");

            //graphic layout
            if (this.product.CostPrice_U1 > this.product.CostPrice_U2)
                {
                    lblCostPriceU1Up.Visible = true;
                    lblCostPriceU1Down.Visible = false;
                }
                else if (this.product.CostPrice_U1 < this.product.CostPrice_U2)
                {
                    lblCostPriceU1Up.Visible = false;
                    lblCostPriceU1Down.Visible = true;
                }
                else
                {
                    lblCostPriceU1Up.Visible = true;
                    lblCostPriceU1Down.Visible = true;
                }

            lblCostPriceDays.Text = this.product.CostPrice_Date.ToString() != DateTime.MinValue.ToString() ? ((DateTime.Now - this.product.CostPrice_Date).Days).ToString() + "d" : "0d";
            lblCostPriceDelta.Text = this.product.Supplier.ProductPriceTrust.ToString("#.##");
            lblCostPriceDate.Text = this.product.CostPrice_Date != DateTime.MinValue ? this.product.CostPrice_Date.ToString("dd/MM/yyyy") : "-";
            lblCostPrice.Text = this.product.CostPrice.ToString("#.##");
            //graphic layout
            if (this.product.CostPrice > this.product.CostPrice_U1)
            {
                lblCostPriceUp.Visible = true;
                lblCostPriceDown.Visible = false;
            }
            else if (this.product.CostPrice < this.product.CostPrice_U1)
            {
                lblCostPriceUp.Visible = false;
                lblCostPriceDown.Visible = true;
            }
            else
            {
                lblCostPriceUp.Visible = true;
                lblCostPriceDown.Visible = true;
            }
         
            cbxDiscontinued.Checked = this.product.Discontinued;
            cbxInactive.Checked = this.product.Inactive;

            #endregion

            //Formulas
            lblEEP.Text = string.Format("{0},    {1}", this.product.EEP.ToString("0.00"), this.product.EEPFormula);
            lblICP.Text = string.Format("{0},    {1}", this.product.ICP.ToString("0.00"), this.product.ICPFormula);
            lblCEPrice.Text = string.Format("{0},    {1}", this.product.ICPCE.ToString("0.00"), this.product.ICPCEFormula);
            lblCTPrice.Text = string.Format("{0},    {1}", this.product.ICPCE.ToString("0.00"), this.product.ICPCEFormula);
            lblCFPrice.Text = string.Format("{0},    {1}", this.product.ICPCF.ToString("0.00"), this.product.ICPCFFormula);
            lblICD.Text = string.Format("{0},    {1}", this.product.ICD.ToString("0.00"), this.product.ICDFormula);
            lblCEStock.Text = string.Format("{0},    {1}", this.product.ICDCE.ToString("0.00"), this.product.ICDCEFormula);
            lblCTStock.Text = string.Format("{0},    {1}", this.product.ICDCE.ToString("0.00"), this.product.ICDCEFormula);
            lblCFStock.Text = string.Format("{0},    {1}", this.product.ICDCF.ToString("0.00"), this.product.ICDCFFormula);
           

            lblMode.Text = (this.product.EditionMode == false) ? GlobalVariables.Resource.GetString("InsertString", GlobalVariables.Culture) : GlobalVariables.Resource.GetString("UpdateString", GlobalVariables.Culture);// traduzir
            lblVersion.Text = (this.product.EditionMode == false) ? GlobalVariables.Resource.GetString("AutomaticString", GlobalVariables.Culture) : this.product.Version.ToString("dddd, dd-MMM-yyyy HH:mm");
            lblCreation.Text = (this.product.EditionMode == false) ? GlobalVariables.Resource.GetString("AutomaticString", GlobalVariables.Culture) : this.product.Creation.ToString("dddd, dd-MMM-yyyy HH:mm");
            ViewState["Version"] = this.product.Version.ToBinary().ToString();
            ViewState["EditionMode"] = (this.product.EditionMode == false) ? "false" : "true";
            RefreshGridView();
            UpdatePanel1.Update();
        }


        void BindPageToObjet()
        {
            this.product = new WhereToBuy.entities.Product();

            this.product.Code = txtCode.Text.TrimEnd().TrimStart();
            this.product.Partnumber = txtPartnumber.Text.TrimEnd().TrimStart();
           

            this.product.Supplier = GetSelectedSupplier();
            this.product.Brand = GetSelectedBrand();
            this.product.Category = GetSelectedCategory();
            this.product.Tax = GetSelectedTax();

            this.product.Discontinued = cbxDiscontinued.Checked;
            this.product.Inactive = cbxInactive.Checked;
            

            this.product.Version = DateTime.FromBinary(long.Parse(ViewState["Version"].ToString()));
            //this.product.EditionMode = (ViewState["EditionMode"].ToString().TrimEnd().ToLower() == "false") ? false : true;

        }



        void LoadProduct(string code)
        {

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.product = this.engine.Products.Get(code, 1, 1, 1, 1, 1, 1);

                // set viewstates
                SetSelectedProduct(this.product);
                SetSupplier(product.Supplier);
                SetBrand(product.Brand);
                SetCategory(product.Category);
                SetTax(product.Tax);
                SetStock(product.Stock);
                SetStock_U1(product.Stock_U1);
                SetStock_U2(product.Stock_U2);
                SetStock_U3(product.Stock_U3);



                var tbx = BrandsSelBox.FindControl("txtBrand");
                ((TextBox)tbx).Text = this.product.Brand.ToString();

                tbx = CategoriesSelBox.FindControl("txtCategory");
                ((TextBox)tbx).Text = this.product.Category.ToString();

                tbx = TaxesSelBox.FindControl("txtTax");
                ((TextBox)tbx).Text = this.product.Tax.ToString();

                //tbx = SuppliersSelBox.FindControl("txtSupplier");
                //((TextBox)tbx).Text = this.product.Supplier.ToString();



                BindObjectToPage();

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


        void Save()
        {
            BindPageToObjet();

            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.engine.Products.Store(this.product);

                engine = null;

                New();
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



        void Delete()
        {
            


            try
            {
                this.engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                this.engine.Products.Delete(GetSelectedProduct());
                New();
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

        void Cancel()
        {
            string returnUrlQueryString;

            returnUrlQueryString = string.Format("returnUrl={0}", Server.UrlEncode(Request.AppRelativeCurrentExecutionFilePath));


            Response.Redirect(string.Format("{0}?{1}", Application["ProductsPage"].ToString().TrimEnd(), returnUrlQueryString), true);
        }



        void New()
        {
            this.product = ProductSpecs.New();
            this.product.Code = string.Empty;
            this.product.Partnumber = string.Empty;
            this.product.Description = string.Empty;
            this.product.Supplier = new Supplier();
            this.product.Supplier.Code = "00000";
            SetSelectedProduct(this.product);
            SetSupplier(this.product.Supplier);
            SetBrand(this.product.Brand);
            SetCategory(this.product.Category);
            SetStock(this.product.Stock);

            //var tbx = SuppliersSelBox.FindControl("txtSupplier");
            //((TextBox)tbx).Text = "";
            
            var tbx = BrandsSelBox.FindControl("txtBrand");
            ((TextBox)tbx).Text = "";

            tbx = CategoriesSelBox.FindControl("txtCategory");
            ((TextBox)tbx).Text = "";

            tbx = TaxesSelBox.FindControl("txtTax");
            ((TextBox)tbx).Text = "";

            txtCode.Focus();
          
            BindObjectToPage();

        }
    }
}