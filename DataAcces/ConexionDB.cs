using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HospiPlus.DataAcces
{
    class ConexionDB
    {
        //obtenemos la cadena de cnx desde settings del proyecto
        public static string ObtenerCadena() 
        {
            return Properties.Settings.Default.conexionDB;
        }

        //aca obtenemos la conexion a la BD
        public static SqlConnection ObtenerCnx() 
        {
            SqlConnection connection = new SqlConnection(ObtenerCadena());
            return connection;
        }

        //metodo para abrir la conexion si esta cerrada
        public static void AbrirConexion(SqlConnection connection) 
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Closed) 
            {
                connection.Open();
            }
        }

        public static void CerrarConexion(SqlConnection connection)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

    }
}
