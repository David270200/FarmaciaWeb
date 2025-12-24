using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BioFarmaWebApi.DAL
{
    public class BaseDeDatos
    {
        private string cadenaConexion;

        public BaseDeDatos()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["BDBiofarma"].ConnectionString;
        }

        private SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }

        public DataTable EjecutarConsulta(string query, params SqlParameter[] parametros)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                if (parametros != null)
                    cmd.Parameters.AddRange(parametros);

                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }
        public void InsertarDetalleVenta(int ventaID, int productoID, int cantidad, decimal precioUnitario)
        {
            string query = @"INSERT INTO DetalleVenta (VentaID, ProductoID, Cantidad, PrecioUnitario)
                     VALUES (@VentaID, @ProductoID, @Cantidad, @PrecioUnitario)";

            SqlParameter[] parametros = new SqlParameter[]
            {
        new SqlParameter("@VentaID", ventaID),
        new SqlParameter("@ProductoID", productoID),
        new SqlParameter("@Cantidad", cantidad),
        new SqlParameter("@PrecioUnitario", precioUnitario)
            };

            EjecutarComando(query, parametros);
        }


        public int EjecutarComando(string query, params SqlParameter[] parametros)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                if (parametros != null)
                    cmd.Parameters.AddRange(parametros);

                conexion.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<DetalleVenta> ObtenerDetallesVenta()
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                con.Open();
                string query = "SELECT DetalleID, VentaID, ProductoID, Cantidad, PrecioUnitario FROM DetalleVenta";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new DetalleVenta
                    {
                        DetalleID = (int)dr["DetalleID"],
                        VentaID = (int)dr["VentaID"],
                        ProductoID = (int)dr["ProductoID"],
                        Cantidad = (int)dr["Cantidad"],
                        PrecioUnitario = (decimal)dr["PrecioUnitario"]
                    });
                }
            }

            return lista;
        }

    }
}