using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinSpec.UserControls
{
    public partial class AlertBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }
        public string AlertMessage
        {
            get
            {
                return lblMessageBox.InnerHtml;
            }
            set
            {
                lblMessageBox.InnerHtml = value;
            }
        }
    }
}