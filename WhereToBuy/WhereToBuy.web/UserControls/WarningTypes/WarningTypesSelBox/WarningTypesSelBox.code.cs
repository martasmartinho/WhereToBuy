using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.entities.specs;

namespace WhereToBuy.web.UserControls.WarningTypes.WarningTypesSelBox
{
    public partial class WarningTypesSelBox
    {
        CoreEngine engine;
        bool required = false;

        public void UpdateData(string code, bool required)
        {
            this.required = required;

            if (ViewState["WarningTypeOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtWarningType.Text = code.TrimEnd();


            if (code != "")
            {
                RefreshListView();
            }

        }

        public void UpdateData(WhereToBuy.entities.Brand brand, bool required)
        {
            this.required = required;

            if (ViewState["WarningTypeOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtWarningType.Text = brand.ToString();

        }


        void SetFormEnvironment()
        {
            ViewState.Add("WarningTypeOrderBy", "[Codigo]");
            ViewState.Add("WarningTypeOrderByType", "ASC");

            txtWarningType.MaxLength = WarningTypeSpecs.Code_MaxSize;

            if (this.required)
            {

                txtWarningType.Attributes.Add("required", "");
            }


            SetListBoxEnvironment();

        }


        private void SetListBoxEnvironment()
        {

            txtWarningType.Text = string.Empty;
            lvWarningTypes.DataKeyNames = new string[] { "Code", "Description" };
            lvWarningTypes.Items.Clear();

        }


        void RefreshListView()
        {

            List<WhereToBuy.entities.WarningType> warningTypes;
            string code = txtWarningType.Text.TrimStart().TrimEnd();

            if (code != "")
            {
                try
                {
                    engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);

                    warningTypes = engine.WarningTypes.Get(code, true);
                    engine = null;



                    // show data
                    lvWarningTypes.DataSource = warningTypes;
                    lvWarningTypes.DataBind();
                    UpdatePanel1.Update();

                }
                catch (MyException ex)
                {
                    WarningTypeSelBoxMessage(this, new WarningTypeSelBoxEventArgs(null, ex.Message));
                    return;


                }
                catch (Exception ex)
                {
                    WarningTypeSelBoxMessage(this, new WarningTypeSelBoxEventArgs(null, ex.Message));
                    return;
                }

            }
            else
            {
                lvWarningTypes.Items.Clear();
                lvWarningTypes.DataBind();
            }

        }

        WhereToBuy.entities.WarningType LoadWarningType(string code)
        {
            WhereToBuy.entities.WarningType warningType;
            warningType = new WhereToBuy.entities.WarningType();

            try
            {

                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                warningType = engine.WarningTypes.Get(code);
                engine = null;
            }
            catch (MyException ex)
            {
                WarningTypeSelBoxMessage(this, new WarningTypeSelBoxEventArgs(null, ex.Message));
                return warningType;
            }
            catch (Exception ex)
            {
                WarningTypeSelBoxMessage(this, new WarningTypeSelBoxEventArgs(null, ex.Message));
                return warningType;
            }
            return warningType;
        }


        void Clear()
        {
            txtWarningType.Text = "";
            lvWarningTypes.Items.Clear();
            lvWarningTypes.DataBind();
        }
    }
}