using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereToBuy.web.UserControls.Supplements.SuppementsSelBox
{
    public class SupplementsSelBoxEventArgs : EventArgs
    {

        WhereToBuy.entities.Supplement supplement;
        string message = string.Empty;



        public SupplementsSelBoxEventArgs(WhereToBuy.entities.Supplement supplement, string message)
        {
            this.supplement = supplement;
            this.message = message;
        }


        public WhereToBuy.entities.Supplement Supplement
        {
            get { return supplement; }
        }

        public string Message
        {
            get { return message; }
        }

    }


    public delegate void SupplementsSelBoxHandler(object sender, SupplementsSelBoxEventArgs e);


    public partial class SupplementsSelBox
    {
        public event SupplementsSelBoxHandler SubmitButtonClick;

        protected void OnSubmitButtonClick(SupplementsSelBoxEventArgs e)
        {
            if (SubmitButtonClick != null)
            {
                SubmitButtonClick(this, e);
            }
        }

        public event SupplementsSelBoxHandler SelectedSupplementUpdate;

        protected void OnSelectedSupplementUpdate(SupplementsSelBoxEventArgs e)
        {

            if (SelectedSupplementUpdate != null)
            {
                SelectedSupplementUpdate(this, e);
            }
        }


        public event SupplementsSelBoxHandler SupplementsSelBoxMessage;

        protected virtual void OnSupplementsSelBoxMessageHandlerMessage(SupplementsSelBoxEventArgs e)
        {
            if (SupplementsSelBoxMessage != null)  // Isto é nulo se nenhum codigo está à escuta deste envento
            {
                SupplementsSelBoxMessage(this, e);
            }
        }
    }
}