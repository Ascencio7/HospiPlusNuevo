using HospiPlus.ModeloExamen;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HospiPlus.DataAcces;
using System.Windows.Controls;


namespace HospiPlus.ServiceExamenes
{
    public class DatosExamenes
    {
        public DatosExamenes() { }

        // Método para mostrar los Examenes
        public static List<ExamenesModel> MostrarExamenes()
        {
            List<ExamenesModel> lsExamenes = new List<ExamenesModel>();
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionDB.ObtenerCnx();
                ConexionDB.AbrirConexion(conexion);

                using (var command = conexion.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "MostrarExamenesMedicos";

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ExamenesModel model = new ExamenesModel
                            {
                                ID = dr.GetInt32(dr.GetOrdinal("ExamenID")),
                                Pacientes = dr.IsDBNull(dr.GetOrdinal("NombreCompletoPaciente")) ? string.Empty : dr.GetString(dr.GetOrdinal("NombreCompletoPaciente")),
                                FechaConsulta = dr.IsDBNull(dr.GetOrdinal("FechaConsulta")) ? DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("FechaConsulta")),
                                TipoExamen = dr.IsDBNull(dr.GetOrdinal("TipoExamen")) ? string.Empty : dr.GetString(dr.GetOrdinal("TipoExamen")),
                                FechaExamen = dr.IsDBNull(dr.GetOrdinal("FechaExamen")) ? DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("FechaExamen")),
                                Resultado = dr.IsDBNull(dr.GetOrdinal("Resultado")) ? string.Empty : dr.GetString(dr.GetOrdinal("Resultado")),
                                Observaciones = dr.IsDBNull(dr.GetOrdinal("Observaciones")) ? string.Empty : dr.GetString(dr.GetOrdinal("Observaciones"))
                            };

                            lsExamenes.Add(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar mostrar los registros: " + ex.Message,
                "Validación",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
            finally
            {
                ConexionDB.CerrarConexion(conexion);
            }

            return lsExamenes;
        }

    }
}