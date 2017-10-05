using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.Supplements.SuppementsSelBox
{
    public partial class SupplementsSelBox
    {
        CoreEngine engine;
        bool required = false;

        public void UpdateData(string code, bool required)
        {
            this.required = required;

            if (ViewState["SupplementOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtSupplement.Text = code.TrimEnd();


            if (code != "")
            {
                RefreshListView();
            }

        }

        public void UpdateData(WhereToBuy.entities.Supplement supplement, bool required)
        {
            this.required = required;

            if (ViewState["SupplierOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtSupplement.Text = supplement.ToString();

        }


        void SetFormEnvironment()
        {
            ViewState.Add("SupplementOrderBy", "[Codigo]");
            ViewState.Add("SupplementOrderByType", "ASC");

            if (this.required)
            {
                txtSupplement.Attributes.Add("required", "");
            }


            SetListBoxEnvironment();

        }



        private void SetListBoxEnvironment()
        {

            txtSupplement.Text = string.Empty;
            lvSupplements.DataKeyNames = new string[] { "Code", "Description" };
            lvSupplements.Items.Clear();

        }


        void RefreshListView()
        {

            List<WhereToBuy.entities.Supplement> supplements;
            string code = txtSupplement.Text.TrimStart().TrimEnd();

            if (code != "")
            {
                try
                {
                    engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);

                    supplements = engine.Supplements.Get(code, true);
                    engine = null;

                    // show data
                    lvSupplements.DataSource = supplements;
                    lvSupplements.DataBind();
                    UpdatePanel1.Update();

                }
                catch (MyException ex)
                {
                    SupplementsSelBoxMessage(this, new SupplementsSelBoxEventArgs(null, ex.Message));
                    return;


                }
                catch (Exception ex)
                {
                    SupplementsSelBoxMessage(this, new SupplementsSelBoxEventArgs(null, ex.Message));
                    return;
                }

            }
            else
            {
                lvSupplements.Items.Clear();
                lvSupplements.DataBind();
            }

        }

        WhereToBuy.entities.Supplement LoadSupplement(string code)
        {
            WhereToBuy.entities.Supplement supplement;
            supplement = new WhereToBuy.entities.Supplement();

            try
            {

                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                supplement = engine.Supplements.Get(code);
                engine = null;
            }
            catch (MyException ex)
            {
                SupplementsSelBoxMessage(this, new SupplementsSelBoxEventArgs(null, ex.Message));
                return supplement;
            }
            catch (Exception ex)
            {
                SupplementsSelBoxMessage(this, new SupplementsSelBoxEventArgs(null, ex.Message));
                return supplement;
            }
            return supplement;
        }


        void Clear()
        {
            txtSupplement.Text = "";
            lvSupplements.Items.Clear();
            lvSupplements.DataBind();
        }
    }
}