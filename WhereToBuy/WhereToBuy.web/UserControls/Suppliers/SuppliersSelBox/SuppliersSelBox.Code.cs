using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.Suppliers.SuppliersSelBox
{
    public partial class SuppliersSelBox
    {
        CoreEngine engine;
        bool required;

        public void UpdateData(string code, bool required)
        {
            this.required = required;

            if (ViewState["SupplierOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtSupplier.Text = code.TrimEnd();


            if (code!="")
            {
                RefreshListView();
            }
            
        }

        public void UpdateData(entities.Supplier supplier, bool required)
        {
            this.required = required;

            if (ViewState["SupplierOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtSupplier.Text = supplier.ToString();
        }


        void SetFormEnvironment()
        {
            ViewState.Add("SupplierOrderBy", "[Codigo]");
            ViewState.Add("SupplierOrderByType", "ASC");

           

            txtSupplier.MaxLength = SupplierSpecs.Code_MaxSize;

            if (this.required)
            {
                txtSupplier.Attributes.Add("required", "");
            }
           
            SetListBoxEnvironment();
          
        }

      

        private void SetListBoxEnvironment()
        {
           
            txtSupplier.Text = string.Empty;
            SupplierListView.DataKeyNames = new string[] { "Code", "Name" };
            SupplierListView.Items.Clear();
           
        }


        [System.Web.Services.WebMethod]
        void RefreshListView()
        {

            List<entities.Supplier> suppliers;
            string code = txtSupplier.Text.TrimStart().TrimEnd();
            SupplierListView.Items.Clear();
            SupplierListView.DataBind();

            if (code != "")
            {
                try
                {
                    engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);

                    suppliers = engine.Suppliers.Get(code, true);
                    engine = null;



                    // show data
                    SupplierListView.DataSource = suppliers;
                    SupplierListView.DataBind();
                  
                }
                catch (MyException ex)
                {
                    SupplierSelBoxMessage(this, new SupplierSelBoxEventArgs(null, ex.Message));
                    return;
                    //MessageUC.ShowError("Erro", ex.Message);
                    
                }
                catch (Exception ex)
                {
                    SupplierSelBoxMessage(this, new SupplierSelBoxEventArgs(null, ex.Message));
                    return;
                }
               
            }
            
        }

        entities.Supplier LoadSupplier(string code)
        {
            entities.Supplier supplier;
            supplier = new entities.Supplier();

            try
            {
                
                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                supplier = engine.Suppliers.Get(code);
                engine = null;
            }
            catch (MyException ex)
            {
                SupplierSelBoxMessage(this, new SupplierSelBoxEventArgs(null, ex.Message));
                return supplier;
            }
            catch (Exception ex)
            {
                SupplierSelBoxMessage(this, new SupplierSelBoxEventArgs(null, ex.Message));
                return supplier;
            }
            return supplier;
        }


        void Clear() 
        {
            txtSupplier.Text = "";
            SupplierListView.Items.Clear();
            SupplierListView.DataBind();
        }


    }
}