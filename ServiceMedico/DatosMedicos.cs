using HospiPlus.ModeloMedico;
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

namespace HospiPlus.ServiceMedico
{
    public class DatosMedicos
    {
        public DatosMedicos() { }

        #region Mostrar Medicos
        // Método para mostrar los Médicos
        public static List<MedicosModel> MuestraMedico()
        {
            List<MedicosModel> lstMedicos = new List<MedicosModel>();
            SqlConnection conexion = null;
            try
            {
                conexion = ConexionDB.ObtenerCnx();
                ConexionDB.AbrirConexion(conexion);

                using (var command = conexion.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "MostrarMedicos";

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MedicosModel model = new MedicosModel
                            {
                                MedicoID = dr["MedicoID"] != DBNull.Value ? int.Parse(dr["MedicoID"].ToString()) : 0,
                                NombreMedico = dr["NombreMedico"].ToString(),
                                ApellidoMedico = dr["ApellidoMedico"].ToString(),
                                ApellidoCasada = dr["ApellidoMedicoCasada"].ToString(),
                                FechaNacimientoMedico = dr["FechaNacimientoMedico"] != DBNull.Value ? DateTime.Parse(dr["FechaNacimientoMedico"].ToString()) : DateTime.MinValue,
                                TelefonoMedico = dr["TelefonoMedico"].ToString(),
                                DepartamentosMedicoID = dr["Departamento"].ToString(),
                                MunicipioMedico = dr["Municipio"].ToString(),
                                CorreoMedico = dr["CorreoMedico"].ToString(),
                                DUIMedico = dr["DUIMedico"].ToString(),
                                SexoMedico = dr["Sexo"].ToString(),
                                EstadoCivilMedico = dr["EstadoCivil"].ToString(),
                                EspecialidadID = dr["Especialidad"].ToString(),
                                EstadoMedico = dr["Estado"].ToString(),
                                HoraInicio = dr["HoraInicio"] != DBNull.Value ? TimeSpan.Parse(dr["HoraInicio"].ToString()) : TimeSpan.Zero,
                                HoraFin = dr["HoraFin"] != DBNull.Value ? TimeSpan.Parse(dr["HoraFin"].ToString()) : TimeSpan.Zero,
                                Dias = dr["Dias"].ToString()
                            };
                            lstMedicos.Add(model);
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
            return lstMedicos;
        }
        #endregion


    }
}