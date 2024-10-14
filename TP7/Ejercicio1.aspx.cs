using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP7
{
    public partial class Ejercicio1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnSeleccionar_Command(object sender, CommandEventArgs e)
        {
            string NombreSucursal = (string)e.CommandArgument;
            if (e.CommandName == "eventoSeleccionar")
            {
                GestionSucursal gs = new GestionSucursal();
                if (Session["SucursalesSeleccionadas"] == null)
                {
                    Session["SucursalesSeleccionadas"] = gs.crearTabla();

                }
                // Use a parameterized query instead of concatenating the SQL string
                string consulta = "SELECT Id_Sucursal, NombreSucursal, DescripcionSucursal FROM Sucursal WHERE NombreSucursal = @NombreSucursal";
                gs.agregarFila((DataTable)Session["SucursalesSeleccionadas"], consulta, NombreSucursal);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSucursal.Text)) // Si el TextBox no está vacío
            {
                ViewState["FiltroSucursal"] = txtSucursal.Text;  // Guardar el valor del filtro en ViewState
                SqlDataSource1.SelectCommand = "SELECT [NombreSucursal], [URL_Imagen_Sucursal], [DescripcionSucursal] FROM [Sucursal] WHERE [NombreSucursal] LIKE '%' + @NombreSucursal + '%'";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("NombreSucursal", txtSucursal.Text);
            }
            else
            {
                ViewState["FiltroSucursal"] = null;  // Limpiar el ViewState si no hay filtro
                SqlDataSource1.SelectCommand = "SELECT [NombreSucursal], [URL_Imagen_Sucursal], [DescripcionSucursal] FROM [Sucursal]";
                SqlDataSource1.SelectParameters.Clear();
            }
        }

        protected void btnProvincia_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "eventoProvincia")
            {
                SqlDataSource1.SelectCommand = "SELECT [NombreSucursal], [URL_Imagen_Sucursal], [DescripcionSucursal], [Id_Sucursal] FROM [Sucursal] WHERE [Id_ProvinciaSucursal] = @Id_ProvinciaSucursal";
                string Id_Provincia = e.CommandArgument.ToString();
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Id_ProvinciaSucursal", Id_Provincia);
            }
        }

        //protected void DataPager1_PreRender(object sender, EventArgs e)
        //{
        //    if (ViewState["FiltroSucursal"] != null)
        //    {
        //        txtSucursal.Text = ViewState["FiltroSucursal"].ToString();  // Mostrar el filtro en el TextBox (opcional)
        //        SqlDataSource1.SelectCommand = "SELECT [NombreSucursal], [URL_Imagen_Sucursal], [DescripcionSucursal] FROM [Sucursal] WHERE [NombreSucursal] LIKE '%' + @NombreSucursal + '%'";
        //        SqlDataSource1.SelectParameters.Clear();
        //        SqlDataSource1.SelectParameters.Add("@NombreSucursal", txtSucursal.Text);
        //    }
        //    else
        //    {
        //        SqlDataSource1.SelectCommand = "SELECT [NombreSucursal], [URL_Imagen_Sucursal], [DescripcionSucursal] FROM [Sucursal]";
        //        SqlDataSource1.SelectParameters.Clear();
        //    }
        //}
    }
}