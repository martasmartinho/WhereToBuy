using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Taxes.TaxSelBox
{
    public partial class TaxSelBox
    {
        CoreEngine engine;
        bool required = false;

        public void UpdateData(string code, bool required)
        {
            this.required = required;

            if (ViewState["TaxOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtTax.Text = code.TrimEnd();


            if (code != "")
            {
                RefreshListView();
            }

        }

        public void UpdateData(WhereToBuy.entities.Tax tax, bool required)
        {
            this.required = required;

            if (ViewState["SupplierOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtTax.Text = tax.ToString();

        }


        void SetFormEnvironment()
        {
            ViewState.Add("TaxOrderBy", "[Codigo]");
            ViewState.Add("TaxOrderByType", "ASC");

            if (this.required)
            {
                txtTax.Attributes.Add("required", "");
            }


            SetListBoxEnvironment();

        }



        private void SetListBoxEnvironment()
        {

            txtTax.Text = string.Empty;
            lvTaxes.DataKeyNames = new string[] { "Code", "Description" };
            lvTaxes.Items.Clear();

        }


        void RefreshListView()
        {

            List<WhereToBuy.entities.Tax> taxes;
            string code = txtTax.Text.TrimStart().TrimEnd();

            if (code != "")
            {
                try
                {
                    engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);

                    taxes = engine.Taxes.Get(code, true);
                    engine = null;

                    // show data
                    lvTaxes.DataSource = taxes;
                    lvTaxes.DataBind();
                    UpdatePanel1.Update();

                }
                catch (MyException ex)
                {
                    TaxesSelBoxMessage(this, new TaxesSelBoxEventArgs(null, ex.Message));
                    return;


                }
                catch (Exception ex)
                {
                    TaxesSelBoxMessage(this, new TaxesSelBoxEventArgs(null, ex.Message));
                    return;
                }

            }
            else
            {
                lvTaxes.Items.Clear();
                lvTaxes.DataBind();
            }

        }

        WhereToBuy.entities.Tax LoadTax(string code)
        {
            WhereToBuy.entities.Tax tax;
            tax = new WhereToBuy.entities.Tax();

            try
            {

                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                tax = engine.Taxes.Get(code);
                engine = null;
            }
            catch (MyException ex)
            {
                TaxesSelBoxMessage(this, new TaxesSelBoxEventArgs(null, ex.Message));
                return tax;
            }
            catch (Exception ex)
            {
                TaxesSelBoxMessage(this, new TaxesSelBoxEventArgs(null, ex.Message));
                return tax;
            }
            return tax;
        }


        void Clear()
        {
            txtTax.Text = "";
            lvTaxes.Items.Clear();
            lvTaxes.DataBind();
        }
    }
}