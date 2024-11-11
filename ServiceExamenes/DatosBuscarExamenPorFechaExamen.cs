using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HospiPlus.ServiceExamenes;
using HospiPlus.ModeloExamen;
using HospiPlus.DataAcces;
using System.Windows;

namespace HospiPlus.ServiceExamenes
{
    public class DatosBuscarExamenPorFechaExamen
    {
        // Método para buscar exámenes por la fecha del examen
        public static List<ExamenesModel> BuscarExamenPorFecha(DateTime fechaExamen)
        {
            List<ExamenesModel> lstExamenPorFecha = new List<ExamenesModel>();
            SqlConnection conexion = null;

            try
            {
                // Obtén la conexión a la base de datos
                conexion = ConexionDB.ObtenerCnx();
                ConexionDB.AbrirConexion(conexion);

                // Configura el comando para el procedimiento almacenado
                using (var command = conexion.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "BuscarExamenPorFecha";

                    // Agrega el parámetro de fecha al comando
                    command.Parameters.Add(new SqlParameter("@FechaExamen", fechaExamen));

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var examen = new ExamenesModel
                            {
                                ID = dr.GetInt32(dr.GetOrdinal("ID")),
                                Pacientes = dr.GetString(dr.GetOrdinal("Pacientes")),
                                FechaConsulta = dr.GetDateTime(dr.GetOrdinal("FechaConsulta")),
                                TipoExamen = dr.GetString(dr.GetOrdinal("TipoExamen")),
                                FechaExamen = dr.GetDateTime(dr.GetOrdinal("FechaExamen")),
                                Resultado = dr.IsDBNull(dr.GetOrdinal("Resultado")) ? null : dr.GetString(dr.GetOrdinal("Resultado")),
                                Observaciones = dr.IsDBNull(dr.GetOrdinal("Observaciones")) ? null : dr.GetString(dr.GetOrdinal("Observaciones"))
                            };
                            lstExamenPorFecha.Add(examen);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar mostrar los registros: " + ex.Message,
                   "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Cierra la conexión si está abierta
                if (conexion != null)
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
            return lstExamenPorFecha;
        }
    }
}
