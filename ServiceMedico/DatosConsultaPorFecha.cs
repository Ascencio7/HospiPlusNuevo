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


using HospiPlus.ModeloMedico;


namespace HospiPlus.ServiceMedico
{
    public class DatosConsultaPorFecha
    {
        public DatosConsultaPorFecha() { }

        // Método para Mostrar las Consultas
        public static List<ConsultaPorFechaModel> MuestraConsulta()
        {
            List<ConsultaPorFechaModel> lstConsultas = new List<ConsultaPorFechaModel>();
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionDB.ObtenerCnx();
                ConexionDB.AbrirConexion(conexion);

                using (var command = conexion.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "MostrarConsultas";

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var model = new ConsultaPorFechaModel
                            {
                                ConsultaID = Convert.ToInt32(dr["ConsultaID"]),
                                CitaID = Convert.ToInt32(dr["CitaID"]),
                                PacienteID = Convert.ToInt32(dr["PacienteID"]), 
                                NombreCompletoPaciente = dr["NombreCompletoPaciente"].ToString(),
                                NombreCompletoMedico = dr["NombreCompletoMedico"].ToString(),
                                EspecialidadMedico = dr["EspecialidadMedico"].ToString(),
                                Altura = Convert.ToDecimal(dr["Altura"]),
                                Peso = Convert.ToDecimal(dr["Peso"]),
                                Alergia = dr["Alergia"].ToString(),
                                Sintomas = dr["Sintomas"].ToString(),
                                Diagnostico = dr["Diagnostico"].ToString(),
                                Observaciones = dr["Observaciones"].ToString(),
                                FechaConsulta = Convert.ToDateTime(dr["FechaConsulta"]),
                                FechaCita = Convert.ToDateTime(dr["FechaCita"]),
                                EstadoCita = dr["EstadoCita"].ToString()
                            };
                            lstConsultas.Add(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar mostrar los registros: " + ex.Message,
                    "Validación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (conexion != null)
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
            return lstConsultas;
        } // Fin del Método de Mostrar las Consultas



        // Metodo para buscar la consulta por Fecha de Consulta
        public static List<ConsultaPorFechaModel> BuscarConsultasPorFecha(DateTime fechaConsulta)
        {
            List<ConsultaPorFechaModel> lstConsultas = new List<ConsultaPorFechaModel>();
            SqlConnection conexion = null;

            try
            {

                conexion = ConexionDB.ObtenerCnx();
                ConexionDB.AbrirConexion(conexion);

                using (var command = conexion.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "BuscarConsultasPorFecha";

                    // Anadir el parametro
                    var paramFecha = new SqlParameter("@FechaConsulta", SqlDbType.Date);
                    paramFecha.Value = fechaConsulta;
                    command.Parameters.Add(paramFecha);

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ConsultaPorFechaModel model = new ConsultaPorFechaModel
                            {
                                ConsultaID = Convert.ToInt32(dr["ConsultaID"]),
                                CitaID = Convert.ToInt32(dr["CitaID"]),
                                NombreCompletoPaciente = dr["NombreCompletoPaciente"].ToString(),
                                NombreCompletoMedico = dr["NombreCompletoMedico"].ToString(),
                                EspecialidadMedico = dr["EspecialidadMedico"].ToString(),
                                Altura = Convert.ToDecimal(dr["Altura"]),
                                Peso = Convert.ToDecimal(dr["Peso"]),
                                Alergia = dr["Alergia"].ToString(),
                                Sintomas = dr["Sintomas"].ToString(),
                                Diagnostico = dr["Diagnostico"].ToString(),
                                Observaciones = dr["Observaciones"].ToString(),
                                FechaConsulta = Convert.ToDateTime(dr["FechaConsulta"]),
                                FechaCita = Convert.ToDateTime(dr["FechaCita"]),
                                EstadoCita = dr["EstadoCita"].ToString()
                            };
                            lstConsultas.Add(model);
                        }
                    }
                }

            }catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar buscar las consultas: " + ex.Message, "Validación",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ConexionDB.CerrarConexion(conexion);
            }
            return lstConsultas;
        }// Fin del Método del Buscar Consultas por Fecha de Consulta
    }
}
