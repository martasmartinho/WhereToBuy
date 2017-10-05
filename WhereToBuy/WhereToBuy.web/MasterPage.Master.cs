using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhereToBuy.core;
using WhereToBuy.entities;
using WhereToBuy.utils.GlobalVariables;
using WhereToBuy.web.Helpers;


namespace WhereToBuy.web
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {

            // RESOLUÇÃO PARA NAO HAVER CACHE
            /*
                NOTA: O seguinte bloco de código, impede os browser de mantrem cache das paginas desta aplicação.
                      No entanto, quando se faz o "backpage" para uma url com argumentos de entrada, o browser
                      processa a pagina como nova navegação, e só expira se o resultado desse processamento for 
                      diferente da pagina previamente carregada em cache (memória).
             */
            Response.ClearHeaders();
            Response.AppendHeader("Cache-Control", "no-cache");
            Response.AppendHeader("Cache-Control", "private");
            Response.AppendHeader("Cache-Control", "no-store");
            Response.AppendHeader("Cache-Control", "must-revalidate");
            Response.AppendHeader("Cache-Control", "max-stale=0");
            Response.AppendHeader("Cache-Control", "post-check=0");
            Response.AppendHeader("Cache-Control", "pre-check=0");
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Keep-Alive", "timeout=3, max=993");
            Response.AppendHeader("Expires", "Sat, 01 Jan 2000 00:00:00 GMT");
            Response.Expires = -1;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-1));
            Response.Cache.SetNoStore();


            ///* SISTEMA DE SEGURANÇA ---> IMPLEMENTAR*/
            ////bypass
            //Session["utilizadorAutenticado"] = "CustomerSpaceUser";

            if (Session["ActualUser"] == null)
            {
                UserLabel.Text = "Sign up";
                SignUpLink.Visible = true;
                LoginLink.Visible = true;
                LogoutLink.Visible = false;
            }
            else
            {
                UserLabel.Text = Session["ActualUser"].ToString();
                SignUpLink.Visible = true;
                LoginLink.Visible = false;
                LogoutLink.Visible = true;
            }


            if (!Page.IsPostBack)
            {
                // definir caminhos para paginas
                //linkHome.NavigateUrl = Application["homePage"].ToString().TrimEnd();
                //linkPainelEstado.NavigateUrl = Application["painelEstadoPage"].ToString().TrimEnd();
                //linkProdutosHabituais.NavigateUrl = Application["produtosHabituaisPage"].ToString().TrimEnd();
                //linkCheckout.NavigateUrl = Application["checkoutPage"].ToString().TrimEnd();
                if (Session["ActualUser"] == null)
                {
                    try
                    {
                        User user = new User { Username = "martasmartinho", Name = "Marta Martinho", Language = new Language() { Code = "pt" } };
                        string connectionstring = Application["ConnectionString"].ToString().Trim();
                        Session["ActualUser"] = user;
                        CoreEngine engine = new CoreEngine(connectionstring, user);
                        GlobalVariables.Language = engine.Languages.Get(user.Language.Code);
                        GlobalVariables.Culture = new CultureInfo("pt");
                        engine = null;
                        UserLabel.Text = ((User)Session["ActualUser"]).Name;
                        SignUpLink.Visible = true;
                        LoginLink.Visible = false;
                        LogoutLink.Visible = true;
                    }
                    catch (Exception)
                    {
                        return;

                    }
                }
               
            }


        }

    
       


    }
}