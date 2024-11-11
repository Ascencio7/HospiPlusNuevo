using HospiPlus.DataAcces;
using HospiPlus.ModeloExamen;
using HospiPlus.ModeloPaciente;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospiPlus.SistemaMedico
{
    /// <summary>
    /// Lógica de interacción para ExamenesMedico.xaml
    /// </summary>
    public partial class ExamenesMedico : Page
    {
        public ExamenesMedico()
        {
            InitializeComponent();
            CargarExamenesMedicos();
            CargarPacientes();
        }
        private void CargarExamenesMedicos()
        {
            try
            {
                List<ExamenesModel> examenes = new List<ExamenesModel>();
                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "MostrarExamenesMedicos"; 

                        using (DbDataReader leertabla = command.ExecuteReader())
                        {
                            while (leertabla.Read())
                            {
                                ExamenesModel examen = new ExamenesModel()
                                {
                                    ID = int.Parse(leertabla["ExamenID"].ToString()),  
                                    Pacientes = leertabla["NombreCompletoPaciente"].ToString(),  
                                    FechaConsulta = DateTime.Parse(leertabla["FechaConsulta"].ToString()),  
                                    TipoExamen = leertabla["TipoExamen"].ToString(),  
                                    FechaExamen = DateTime.Parse(leertabla["FechaExamen"].ToString()),  
                                    Resultado = leertabla["Resultado"].ToString(), 
                                    Observaciones = leertabla["Observaciones"].ToString()
                                };

                                examenes.Add(examen);
                            }
                        }
                    }
                }
               
                gridGestorExamenMedico.ItemsSource = examenes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los exámenes médicos: " + ex.Message);
            }
        }

        private void InsertarExamenMedico()
        {
            try
            {
               
                
               
                string tipoExamen = txtTExamenMedico.Text;
                DateTime fechaExamen = dtFechaExamMedic.SelectedDate.Value;
                string resultado = txtRExamMedico.Text;
             

                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);

                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "InsertarExamenMedico";

                        
                        
                        command.Parameters.Add(new SqlParameter("@TipoExamen", tipoExamen));
                        command.Parameters.Add(new SqlParameter("@FechaExamen", fechaExamen));
                        command.Parameters.Add(new SqlParameter("@Resultado", resultado));
                        

                        
                        int examenID = (int)command.ExecuteScalar(); 

                        MessageBox.Show($"Examen médico insertado con éxito. ID: {examenID}");

                        
                        CargarExamenesMedicos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar el examen médico: " + ex.Message);
            }
        }

        private void CargarPacientes()
        {
            try
            {
                List<string> nombresPacientes = new List<string>();
                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "SELECT NombrePaciente FROM Pacientes";

                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nombresPacientes.Add(reader["NombrePaciente"].ToString());
                            }
                        }
                    }
                }

                
                cmbPExamenMedico.ItemsSource = nombresPacientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los pacientes: " + ex.Message);
            }
        }

    }
}
