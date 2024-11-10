using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows;
using HospiPlus.ModeloMedico;
using HospiPlus.DataAcces;

namespace HospiPlus.ServiceMedico
{
    public class DatosRecetas
    {
        public DatosRecetas() { }

        // Metodo para Mostrar las Recetas
        public static List<RecetasModel> MostrarRecetas()
        {
            List<RecetasModel> lstRecetas = new List<RecetasModel>();
            SqlConnection conexion = null;

            try
            {

                conexion = ConexionDB.ObtenerCnx();
                ConexionDB.AbrirConexion(conexion);

                using (var command = conexion.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "MostrarRecetas";

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            RecetasModel receta = new RecetasModel
                            {
                                RecetaID = Convert.ToInt32(dr["RecetaID"]),
                                NombreCompletoPaciente = dr["NombreCompletoPaciente"].ToString(),
                                DuiPaciente = dr["DUIPaciente"].ToString(),
                                NombreCompletoMedico = dr["NombreCompletoMedico"].ToString(),
                                FechaEmision = Convert.ToDateTime(dr["FechaEmision"]),
                                ConsultaID = Convert.ToInt32(dr["ConsultaID"]),
                                Medicamento = dr["Medicamento"].ToString(),
                                Dosis = dr["Dosis"].ToString(),
                                Frecuencia = dr["Frecuencia"].ToString(),
                                Duracion = dr["Duracion"].ToString(),
                                Instrucciones = dr["Instrucciones"].ToString()
                            };
                            lstRecetas.Add(receta);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar mostrar las recetas: " + ex.Message, "Validación",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ConexionDB.CerrarConexion(conexion);
            }
            return lstRecetas;
        }


        public static List<RecetasModel> BuscarRecetasPorDUI(string duiPaciente)
        {
            List<RecetasModel> recetas = new List<RecetasModel>();

            using (SqlConnection conn = ConexionDB.ObtenerCnx())
            {
                using (SqlCommand cmd = new SqlCommand("BuscarRecetasPorDUI", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DuiPaciente", duiPaciente);

                    try
                    {
                        conn.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                RecetasModel receta = new RecetasModel
                                {
                                    RecetaID = (int)dr["RecetaID"],
                                    NombreCompletoPaciente = dr["NombreCompletoPaciente"].ToString(),
                                    DuiPaciente = dr["DuiPaciente"].ToString(),
                                    NombreCompletoMedico = dr["NombreCompletoMedico"].ToString(),
                                    FechaEmision = (DateTime)dr["FechaEmision"],
                                    ConsultaID = (int)dr["ConsultaID"],
                                    Medicamento = dr["Medicamento"].ToString(),
                                    Dosis = dr["Dosis"].ToString(),
                                    Frecuencia = dr["Frecuencia"].ToString(),
                                    Duracion = dr["Duracion"].ToString(),
                                    Instrucciones = dr["Instrucciones"].ToString()
                                };

                                recetas.Add(receta);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Ocurrió un error al buscar las recetas: " + ex.Message, "Error",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error: " + ex.Message, "Error",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            return recetas;
        }


    }
}
