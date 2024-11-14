using HospiPlus.DataAcces;
using HospiPlus.ModeloExamen;
using HospiPlus.ModeloPaciente;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using HospiPlus.SistemaMedico;

namespace HospiPlus.SistemaMedico
{
    /// <summary>
    /// Lógica de interacción para ExamenesMedico.xaml
    /// </summary>
    public partial class ExamenesMedico : Page
    {
        private int examenSeleccionadoId = 0;

        public ExamenesMedico()
        {
            InitializeComponent();
            CargarExamenesMedicos();
            CargarPacientes();
            

        }

        // Método para cargar exámenes médicos desde la base de datos
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
                                    ID = Convert.ToInt32(leertabla["ExamenID"]),
                                    Pacientes = leertabla["NombreCompletoPaciente"].ToString(),
                                    TipoExamen = leertabla["TipoExamen"].ToString(),
                                    FechaExamen = Convert.ToDateTime(leertabla["FechaExamen"]),
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
                MessageBox.Show("Error al cargar los exámenes médicos: " + ex.Message, "HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Método para insertar un nuevo examen médico
        private void InsertarExamenMedico()
        {
            try
            {
                int pacienteID = (int)cmbPExamenMedico.SelectedValue;
                string tipoExamen = txtTExamenMedico.Text;
                DateTime fechaExamen = dtFechaExamMedic.SelectedDate ?? DateTime.Now;
                string resultado = txtRExamMedico.Text;
                string observaciones = txtObservaciones.Text;

                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "InsertarExamenMedico";

                        command.Parameters.Add(new SqlParameter("@PacienteID", pacienteID));
                        command.Parameters.Add(new SqlParameter("@TipoExamen", tipoExamen));
                        command.Parameters.Add(new SqlParameter("@FechaExamen", fechaExamen));
                        command.Parameters.Add(new SqlParameter("@Resultado", resultado));
                        command.Parameters.Add(new SqlParameter("@Observaciones", observaciones));

                        command.ExecuteNonQuery();

                        MessageBox.Show("Examen médico guardado exitosamente.", "HOSPI PLUS | Examen ingresado", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarExamenesMedicos();
                        LimpiarCampos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar el examen médico: " + ex.Message, "HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Método para cargar la lista de pacientes en el ComboBox
        private void CargarPacientes()
        {
            try
            {
                List<KeyValuePair<int, string>> pacientes = new List<KeyValuePair<int, string>>();
                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "SELECT PacienteID, NombrePaciente FROM Pacientes";

                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pacientes.Add(new KeyValuePair<int, string>(Convert.ToInt32(reader["PacienteID"]), reader["NombrePaciente"].ToString()));
                            }
                        }
                    }
                }

                cmbPExamenMedico.ItemsSource = pacientes;
                cmbPExamenMedico.DisplayMemberPath = "Value";
                cmbPExamenMedico.SelectedValuePath = "Key";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los pacientes: " + ex.Message, "HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Evento para manejar la selección de exámenes médicos
        private void gridGestorExamenMedico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridGestorExamenMedico.SelectedItem is ExamenesModel examen)
            {
                examenSeleccionadoId = examen.ID;
                cmbPExamenMedico.Text = examen.Pacientes;
                txtTExamenMedico.Text = examen.TipoExamen;
                dtFechaExamMedic.SelectedDate = examen.FechaExamen;
                txtRExamMedico.Text = examen.Resultado;
                txtObservaciones.Text = examen.Observaciones;

                btnModificarExamMedic.IsEnabled = true;
            }
        }

        // Método para modificar un examen médico
        private void btnModificarExamMedic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbPExamenMedico.SelectedValue == null)
                {
                    MessageBox.Show("Por favor, seleccione un paciente.", "HOSPI PLUS | Selección de paciente", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (!int.TryParse(cmbPExamenMedico.SelectedValue.ToString(), out int pacienteID))
                {
                    MessageBox.Show("El valor seleccionado no es un número válido.", "HOSPI PLUS | Error de selección", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (examenSeleccionadoId == 0)
                {
                    MessageBox.Show("Por favor, seleccione un examen para modificar.", "HOSPI PLUS | Editar Examen", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                DateTime? fechaExamen = dtFechaExamMedic.SelectedDate;
                if (!fechaExamen.HasValue)
                {
                    MessageBox.Show("Por favor, seleccione una fecha para el examen.", "HOSPI PLUS | Selección de fecha", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "EditarExamenMedico";

                        command.Parameters.Add(new SqlParameter("@ExamenID", examenSeleccionadoId));
                        command.Parameters.Add(new SqlParameter("@PacienteID", pacienteID));
                        command.Parameters.Add(new SqlParameter("@TipoExamen", txtTExamenMedico.Text));
                        command.Parameters.Add(new SqlParameter("@FechaExamen", fechaExamen.Value));
                        command.Parameters.Add(new SqlParameter("@Resultado", txtRExamMedico.Text));
                        command.Parameters.Add(new SqlParameter("@Observaciones", txtObservaciones.Text));

                        command.ExecuteNonQuery();

                        MessageBox.Show("Examen médico actualizado exitosamente.", "HOSPI PLUS | Examen actualizado", MessageBoxButton.OK, MessageBoxImage.Information);
                        CargarExamenesMedicos();
                        LimpiarCampos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el examen médico: " + ex.Message + "\n" + ex.StackTrace, "HOSPI PLUS | Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        private void LimpiarCampos()
        {
            examenSeleccionadoId = 0;
            cmbPExamenMedico.Text = "";
            txtTExamenMedico.Text = "";
            dtFechaExamMedic.SelectedDate = DateTime.Now;
            txtRExamMedico.Text = "";
            txtObservaciones.Text = "";
            gridGestorExamenMedico.SelectedItem = null;
            btnModificarExamMedic.IsEnabled = false;
        }


       
        private void btnGuardarExamMedic_Click(object sender, RoutedEventArgs e)
        {
            InsertarExamenMedico();
        }

        private void btnEliminarExamMedic_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelarExamMedic_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la operación?", "HOSPI PLUS | Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LimpiarCampos();
                
            }
        }
    }
}





