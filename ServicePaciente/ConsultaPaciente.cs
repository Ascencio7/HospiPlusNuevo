using HospiPlus.DataAcces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HospiPlus.ModeloPaciente;
using HospiPlus.ServicePaciente;

namespace HospiPlus.ServicePaciente
{
    public class ConsultaPaciente
    {
        // Método para ingresar una nueva consulta
        public static bool GuardarConsulta(ModeloConsultaPaciente consulta)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerCnx())
                {
                    using (SqlCommand command = new SqlCommand("IngresarConsulta", conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros al procedimiento almacenado
                        command.Parameters.AddWithValue("@CitaID", consulta.CitaID);
                        command.Parameters.AddWithValue("@PacienteID", consulta.PacienteID);
                        command.Parameters.AddWithValue("@MedicoID", consulta.MedicoID);
                        command.Parameters.AddWithValue("@Altura", consulta.Altura);
                        command.Parameters.AddWithValue("@Peso", consulta.Peso);
                        command.Parameters.AddWithValue("@Alergia", consulta.Alergia);
                        command.Parameters.AddWithValue("@Sintomas", consulta.Sintomas);
                        command.Parameters.AddWithValue("@Diagnostico", consulta.Diagnostico);
                        command.Parameters.AddWithValue("@Observaciones", consulta.Observaciones);
                        command.Parameters.AddWithValue("@FechaConsulta", consulta.FechaConsulta);

                        conexion.Open();
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la consulta: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // Método para obtener todas las consultas
        public static List<ModeloConsultaPaciente> ObtenerConsultas()
        {
            List<ModeloConsultaPaciente> consultas = new List<ModeloConsultaPaciente>();
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerCnx())
                {
                    using (SqlCommand command = new SqlCommand("MostrarConsultas", conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        conexion.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ModeloConsultaPaciente consulta = new ModeloConsultaPaciente
                                {
                                    ConsultaID = int.Parse(reader["ConsultaID"].ToString()),
                                    CitaID = int.Parse(reader["CitaID"].ToString()),
                                    PacienteID = reader["NombreCompletoPaciente"].ToString(),
                                    MedicoID = reader["NombreCompletoMedico"].ToString(),
                                    EspecialidadM = reader["EspecialidadMedico"].ToString(),
                                    Altura = decimal.Parse(reader["Altura"].ToString()),
                                    Peso = decimal.Parse(reader["Peso"].ToString()),
                                    Alergia = reader["Alergia"].ToString(),
                                    Sintomas = reader["Sintomas"].ToString(),
                                    Diagnostico = reader["Diagnostico"].ToString(),
                                    Observaciones = reader["Observaciones"].ToString(),
                                    FechaConsulta = DateTime.Parse(reader["FechaConsulta"].ToString()),
                                    FechaCita = DateTime.Parse(reader["FechaCita"].ToString()),
                                    EstadoCita = reader["EstadoCita"].ToString(),
                                };
                                consultas.Add(consulta);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener las consultas: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return consultas;
        }
    }
}
