﻿using HospiPlus.DataAcces;
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
        private int examenSeleccionadoId = 0;

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
                                    ID = Convert.ToInt32(leertabla["ExamenID"]),
                                    Pacientes = leertabla["NombreCompletoPaciente"].ToString(),
                                    //FechaConsulta = DateTime.Parse(leertabla["FechaConsulta"].ToString()),
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
                MessageBox.Show("Error al cargar los exámenes médicos: " + ex.Message);
            }
        }

        private void InsertarExamenMedico()
        {
            try
            {
                int pacienteID = (int)cmbPExamenMedico.SelectedValue; ;
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

                        MessageBox.Show("Examen médico guardado exitosamente.");
                        CargarExamenesMedicos();
                        LimpiarCampos();
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
        List<KeyValuePair<int, string>> pacientes = new List<KeyValuePair<int, string>>();
        using (var conexion = ConexionDB.ObtenerCnx())
        {
            ConexionDB.AbrirConexion(conexion);
            using (var command = conexion.CreateCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT PacienteID, NombrePaciente FROM Pacientes"; // Asumiendo que PacienteID es la columna del ID del paciente

                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Agregar el ID y nombre al ComboBox como un KeyValuePair
                        pacientes.Add(new KeyValuePair<int, string>(Convert.ToInt32(reader["PacienteID"]), reader["NombrePaciente"].ToString()));
                    }
                }
            }
        }

        // Configurar el ComboBox para mostrar el nombre y usar el ID como valor
        cmbPExamenMedico.ItemsSource = pacientes;
        cmbPExamenMedico.DisplayMemberPath = "Value";  // Mostrar el nombre del paciente
        cmbPExamenMedico.SelectedValuePath = "Key";   // Usar el ID del paciente como valor
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error al cargar los pacientes: " + ex.Message);
    }
}

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
                btnEliminarExamMedic.IsEnabled = true;
            }

        }

        private void btnGuardarExamMedic_Click(object sender, RoutedEventArgs e)
        {
            InsertarExamenMedico();
        }

        private void btnModificarExamMedic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (examenSeleccionadoId == 0)
                {
                    MessageBox.Show("Por favor, seleccione un examen para modificar.");
                    return;
                }

                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "ActualizarExamenMedico";

                        command.Parameters.Add(new SqlParameter("@ExamenID", examenSeleccionadoId));
                        command.Parameters.Add(new SqlParameter("@Paciente", cmbPExamenMedico.Text));
                        command.Parameters.Add(new SqlParameter("@TipoExamen", txtTExamenMedico.Text));
                        command.Parameters.Add(new SqlParameter("@FechaExamen", dtFechaExamMedic.SelectedDate));
                        command.Parameters.Add(new SqlParameter("@Resultado", txtRExamMedico.Text));
                        command.Parameters.Add(new SqlParameter("@Observaciones", txtObservaciones.Text));

                        command.ExecuteNonQuery();

                        MessageBox.Show("Examen médico actualizado exitosamente.");
                        CargarExamenesMedicos();
                        LimpiarCampos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el examen médico: " + ex.Message);
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

            // Deshabilitar botones de modificar y eliminar
            btnModificarExamMedic.IsEnabled = false;
            btnEliminarExamMedic.IsEnabled = false;
        }

        private void btnEliminarExamMedic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (examenSeleccionadoId == 0)
                {
                    MessageBox.Show("Por favor, seleccione un examen para eliminar.");
                    return;
                }

                if (MessageBox.Show("¿Está seguro de que desea eliminar este examen médico?",
                                  "Confirmar eliminación",
                                  MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    using (var conexion = ConexionDB.ObtenerCnx())
                    {
                        ConexionDB.AbrirConexion(conexion);
                        using (var command = conexion.CreateCommand())
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "EliminarExamenMedico";

                            command.Parameters.Add(new SqlParameter("@ExamenID", examenSeleccionadoId));

                            command.ExecuteNonQuery();

                            MessageBox.Show("Examen médico eliminado exitosamente.");
                            CargarExamenesMedicos();
                            LimpiarCampos();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el examen médico: " + ex.Message);
            }
        }

        private void btnCancelarExamMedic_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void btnCancelarExamMedic_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea limpiar la búsqueda?", "Limpiar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Limpia el TextBox donde se ingresa el DUI
                dtFechaExamMedic.Text = ""; // O puedes usar txtBuscarPacientesAdmi.Text = "";

                // Llama al método para mostrar todos los pacientes
                CargarExamenesMedicos();
                return; // Salir del método
            }
        }
    }
}
