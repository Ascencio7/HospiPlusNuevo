using HospiPlus.ModeloPaciente;
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

namespace HospiPlus.ServicePaciente
{
    public class DatosPaciente
    {
        public DatosPaciente() { }
        public static List<PacientesModel> MuestraPacientes()
        {
            List<PacientesModel> lstPacientes = new List<PacientesModel>();
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerCnx();
                ConexionDB.AbrirConexion(conexion);

                using (var command = conexion.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "MostrarPacientes";

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PacientesModel modele = new PacientesModel
                            {
                                PacienteID = int.Parse(dr["PacienteID"].ToString()),
                                NombrePaciente = dr["NombrePaciente"].ToString(),
                                ApellidoPaciente = dr["ApellidoPaciente"].ToString(),
                                ApellidoDeCasada = dr["ApellidoDeCasada"].ToString(),
                                FechaNacimientoPaciente = DateTime.Parse(dr["FechaNacimientoPaciente"].ToString()),
                                SexoPaciente = dr["Sexos"].ToString(),
                                EstadoCivilPaciente = dr["EstadoCivil"].ToString(),
                                DUIPaciente = dr["DUIPaciente"].ToString(),
                                TelefonoPaciente = dr["TelefonoPaciente"].ToString(),
                                CorreoPaciente = dr["CorreoPaciente"].ToString(),
                                DepartamentosPaciente = dr["Departamentos"].ToString(),
                                MunicipioPaciente = dr["Municipio"].ToString(),
                                EstadoPaciente = dr["Estados"].ToString()
                            };

                            // Se agrega el modelo a la lista
                            lstPacientes.Add(modele);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al intentar mostrar los registros: " + ex.Message, "Error de Conexion", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lstPacientes;
        } //Fin de Mostrar pacientes



        // Inicio para buscar a los pacientes por numero de DUI
        public static List<PacientesModel> BuscarPacientePorDUI(string dui)
        {
            List<PacientesModel> pacientes = new List<PacientesModel>();
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                using (SqlCommand command = new SqlCommand("BuscarPacientePorDUI", conexion))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DUI", dui);

                    conexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PacientesModel paciente = new PacientesModel
                            {
                                PacienteID = int.Parse(reader["PacienteID"].ToString()),
                                NombrePaciente = reader["NombrePaciente"].ToString(),
                                ApellidoPaciente = reader["ApellidoPaciente"].ToString(),
                                ApellidoDeCasada = reader["ApellidoDeCasada"].ToString(),
                                FechaNacimientoPaciente = DateTime.Parse(reader["FechaNacimientoPaciente"].ToString()),
                                SexoPaciente = reader["Sexos"].ToString(),
                                EstadoCivilPaciente = reader["EstadoCivil"].ToString(),
                                DUIPaciente = reader["DUIPaciente"].ToString(),
                                TelefonoPaciente = reader["TelefonoPaciente"].ToString(),
                                CorreoPaciente = reader["CorreoPaciente"].ToString(),
                                DepartamentosPaciente = reader["Departamentos"].ToString(),
                                MunicipioPaciente = reader["Municipio"].ToString(),
                                EstadoPaciente = reader["Estados"].ToString()
                            };
                            pacientes.Add(paciente);
                        }
                        
                    }
                }
            }
            return pacientes;
        } //Fin del proceso de buscar pacientes por DUI


        //metodo para ingresar o registrar un nuevo paciente
        public static bool GuardarPaciente(PacientesModel paciente)
        {
            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerCnx())
                {
                    using (SqlCommand command = new SqlCommand("GuardarPaciente", conexion))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros para el procedimiento almacenado
                        command.Parameters.AddWithValue("@PacienteID", paciente.PacienteID);
                        command.Parameters.AddWithValue("@NombrePaciente", paciente.NombrePaciente);
                        command.Parameters.AddWithValue("@ApellidoPaciente", paciente.ApellidoPaciente);
                        command.Parameters.AddWithValue("@ApellidoDeCasada", paciente.ApellidoDeCasada ?? string.Empty);
                        command.Parameters.AddWithValue("@FechaNacimientoPaciente", paciente.FechaNacimientoPaciente);
                        command.Parameters.AddWithValue("@Sexos", paciente.SexoPaciente);
                        command.Parameters.AddWithValue("@EstadoCivil", paciente.EstadoCivilPaciente);
                        command.Parameters.AddWithValue("@DUIPaciente", paciente.DUIPaciente);
                        command.Parameters.AddWithValue("@TelefonoPaciente", paciente.TelefonoPaciente);
                        command.Parameters.AddWithValue("@CorreoPaciente", paciente.CorreoPaciente);
                        command.Parameters.AddWithValue("@Departamentos", paciente.DepartamentosPaciente);
                        command.Parameters.AddWithValue("@Municipio", paciente.MunicipioPaciente);
                        command.Parameters.AddWithValue("@Estados", paciente.EstadoPaciente);

                        conexion.Open();
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el paciente: " + ex.Message,
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }


        

    }
}
