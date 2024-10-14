using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Reflection;
using TP6.Clases;

namespace TP7
{
    public class GestionSucursal
    {
        private string Ruta = "Data Source=localhost\\sqlexpress;Initial Catalog=BDSucursales;Integrated Security=True";

        public GestionSucursal()
        {
        }
        public DataTable crearTabla()
        {
            DataTable dt = new DataTable();
            DataColumn columna = new DataColumn("Id_Sucursal", System.Type.GetType("System.String"));
            dt.Columns.Add(columna);

            columna = new DataColumn("NombreSucursal", System.Type.GetType("System.String"));
            dt.Columns.Add(columna);

            columna = new DataColumn("DescripcionSucursal", System.Type.GetType("System.String"));
            dt.Columns.Add(columna);

            return dt;
        }

        public void agregarFila(DataTable tabla, string consulta, string nombreSucursal)
        {
            using (SqlConnection conexion = new SqlConnection(Ruta))
            {
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@NombreSucursal", nombreSucursal);  // Parameterized query

                try
                {
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        DataRow dr = tabla.NewRow();
                        dr["Id_Sucursal"] = reader["Id_Sucursal"].ToString();
                        dr["NombreSucursal"] = reader["NombreSucursal"].ToString();
                        dr["DescripcionSucursal"] = reader["DescripcionSucursal"].ToString();
                        tabla.Rows.Add(dr);  // Add row to the DataTable
                    }

                    conexion.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocurrió un error: " + ex.Message);
                }
            }
        }


        //public void Cargar(ListView ListView)
        //{
        //    DataSet ds = new DataSet(); 
        //    using (SqlConnection cn = new SqlConnection(Ruta))
        //    {
        //        cn.Open();
        //        SqlDataAdapter adapter = new SqlDataAdapter("SELECT NombreSucursal,URL_Imagen_Sucursal,DescripcionSucursal FROM Sucursal", cn);
        //        adapter.Fill(ds,"Sucursal");
        //        ListView.DataSource = ds.Tables["Sucursal"];
        //        ListView.DataBind();
        //        cn.Close();
        //    }
        //}

        //public void Filtrar(ListView listView, TextBox txtSucursal)
        //{
        //    DataSet ds = new DataSet();
        //    using (SqlConnection cn = new SqlConnection(Ruta))
        //    {
        //        cn.Open();
        //        // Modificamos la consulta para usar LIKE y los comodines %
        //        SqlDataAdapter adapter = new SqlDataAdapter("SELECT NombreSucursal, URL_Imagen_Sucursal, DescripcionSucursal FROM Sucursal WHERE NombreSucursal LIKE @NombreSucursal;", cn);

        //        // Agregar el comodín % al principio y al final del valor ingresado para permitir coincidencias parciales
        //        adapter.SelectCommand.Parameters.AddWithValue("@NombreSucursal", "%" + txtSucursal.Text + "%");
        //        adapter.Fill(ds, "Sucursal");
               

        //        listView.DataSource = ds.Tables["Sucursal"];
        //        listView.DataBind();

        //        // Limpiar el TextBox
        //        txtSucursal.Text = "";

        //        cn.Close();
        //    }
        //}

        public int Ejecutar(string consulta)
        {
            SqlConnection conexion4 = new SqlConnection(Ruta);
            conexion4.Open();

            SqlCommand comando = new SqlCommand(consulta, conexion4);

            int FilasAfectadas = comando.ExecuteNonQuery();

            return FilasAfectadas;
        }
    }
}