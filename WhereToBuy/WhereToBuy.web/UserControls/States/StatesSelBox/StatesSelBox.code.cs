using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhereToBuy.core;
using WhereToBuy.entities;

namespace WhereToBuy.web.UserControls.States.StatesSelBox
{
    public partial class StatesSelBox
    {
        CoreEngine engine;
        bool required = false;

        public void UpdateData(string code, bool required)
        {
            this.required = required;

            if (ViewState["StateOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtState.Text = code.TrimEnd();


            if (code != "")
            {
                RefreshListView();
            }

        }

        public void UpdateData(WhereToBuy.entities.State state, bool required)
        {
            this.required = required;

            if (ViewState["StateOrderBy"] == null)
            {
                SetFormEnvironment();
            }

            txtState.Text = state.ToString();

        }


        void SetFormEnvironment()
        {
            ViewState.Add("StateOrderBy", "[Codigo]");
            ViewState.Add("StateOrderByType", "ASC");

            if (this.required)
            {
                txtState.Attributes.Add("required", "");
            }


            SetListBoxEnvironment();

        }



        private void SetListBoxEnvironment()
        {

            txtState.Text = string.Empty;
            lvStates.DataKeyNames = new string[] { "Code", "Description" };
            lvStates.Items.Clear();

        }


        void RefreshListView()
        {

            List<WhereToBuy.entities.State> states;
            string code = txtState.Text.TrimStart().TrimEnd();
            
            if (code != "")
            {
                try
                {
                    engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);

                    states = engine.States.Get(code, true);
                    engine = null;



                    // show data
                    lvStates.DataSource = states;
                    lvStates.DataBind();
                    UpdatePanel1.Update();

                }
                catch (MyException ex)
                {
                    StateSelBoxMessage(this, new StateSelBoxEventArgs(null, ex.Message));
                    return;


                }
                catch (Exception ex)
                {
                    StateSelBoxMessage(this, new StateSelBoxEventArgs(null, ex.Message));
                    return;
                }

            }
            else
            {
                lvStates.Items.Clear();
                lvStates.DataBind();
            }

        }

        WhereToBuy.entities.State LoadState(string code)
        {
            WhereToBuy.entities.State state;
            state = new WhereToBuy.entities.State();

            try
            {

                engine = new CoreEngine(Application["ConnectionString"].ToString().TrimEnd(), (User)Session["ActualUser"]);
                state = engine.States.Get(code);
                engine = null;
            }
            catch (MyException ex)
            {
                StateSelBoxMessage(this, new StateSelBoxEventArgs(null, ex.Message));
                return state;
            }
            catch (Exception ex)
            {
                StateSelBoxMessage(this, new StateSelBoxEventArgs(null, ex.Message));
                return state;
            }
            return state;
        }


        void Clear()
        {
            txtState.Text = string.Empty;
            lvStates.Items.Clear();
            lvStates.DataBind();
        }
    }
}