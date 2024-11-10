using HospiPlus.DataAcces;
using HospiPlus.ModeloMedico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospiPlus.ServiceMedico
{
    public class DatosCitas
    {
        public DatosCitas() { }

        public static List<CitasModel> MostrarCitas(int? pacienteID, int? medicoID, int? especialidadID, int? estadoCitaID, DateTime? fechaCita)
        {
            List<CitasModel> citas = new List<CitasModel>();

            using (SqlConnection conn = ConexionDB.ObtenerCnx())
            {
                using (SqlCommand cmd = new SqlCommand("MostrarCitas", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Asignar parámetros, no permitiendo nulos
                    cmd.Parameters.AddWithValue("@PacienteID", pacienteID.HasValue ? (object)pacienteID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@MedicoID", medicoID.HasValue ? (object)medicoID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@EspecialidadID", especialidadID.HasValue ? (object)especialidadID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@EstadoCitaID", estadoCitaID.HasValue ? (object)estadoCitaID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaCita", fechaCita.HasValue ? (object)fechaCita.Value : DBNull.Value);

                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CitasModel cita = new CitasModel
                            {
                                CitaID = dr.GetInt32(dr.GetOrdinal("CitaID")),
                                DUIPaciente = dr.GetString(dr.GetOrdinal("DUIPaciente")),
                                Paciente = dr.GetString(dr.GetOrdinal("Paciente")),
                                Medico = dr.GetString(dr.GetOrdinal("Medico")),
                                Especialidad = dr.GetString(dr.GetOrdinal("Especialidad")),
                                FechaCita = dr.GetDateTime(dr.GetOrdinal("FechaCita")),
                                HoraCita = dr.GetTimeSpan(dr.GetOrdinal("HoraCita")),
                                EstadoCita = dr.GetString(dr.GetOrdinal("EstadoCita"))
                            };

                            citas.Add(cita);
                        }
                    }
                }
            }

            return citas;
        }


        // Metodo para buscar la Cita por Numero de DUI
        public static List<CitasModel> BuscarCitasPorDUI(string duiPaciente)
        {
            List<CitasModel> citas = new List<CitasModel>();

            using (SqlConnection conn = ConexionDB.ObtenerCnx())
            {
                using (SqlCommand cmd = new SqlCommand("BuscarCitasPorDUI", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DuiPaciente", duiPaciente);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CitasModel cita = new CitasModel
                            {
                                CitaID = reader.GetInt32(reader.GetOrdinal("CitaID")),
                                Paciente = reader.GetString(reader.GetOrdinal("Paciente")),
                                DUIPaciente = reader.GetString(reader.GetOrdinal("DUIPaciente")),
                                Medico = reader.GetString(reader.GetOrdinal("Medico")),
                                Especialidad = reader.GetString(reader.GetOrdinal("Especialidad")),
                                FechaCita = reader.GetDateTime(reader.GetOrdinal("FechaCita")),
                                HoraCita = reader.GetTimeSpan(reader.GetOrdinal("HoraCita")),
                                EstadoCita = reader.GetString(reader.GetOrdinal("EstadoCita"))
                            };

                            citas.Add(cita);
                        }
                    }
                }
            }

            return citas;
        }



    }
}
