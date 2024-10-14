using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP7
{
    public partial class MostrarSucursalesSeleccionadas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SucursalesSeleccionadas"] != null)
            {
                GridView1.DataSource = (DataTable)Session["SucursalesSeleccionadas"];
                GridView1.DataBind();
            }
        }
    }
}